using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GoL
{
    public struct Cell
    {
        public int X;
        public int Y;
    }

    /// <summary>
    /// A sparse array of booleans (stored as bits)
    /// </summary>
    public class CellWorld : IWorld
    {
        internal int _version;
        private readonly SortedList<int, SortedList<int, ulong>> _rows;

        public CellWorld()
        {
            MinX = MinY = int.MaxValue;
            MaxX = MaxY = int.MinValue;
            _rows = new SortedList<int, SortedList<int, ulong>>();
        }

        public CellWorld(CellWorld existing)
        {
            _rows = new SortedList<int, SortedList<int, ulong>>(existing._rows.Count);
            foreach (var row in existing._rows)
            {
                var existingRow = row.Value;
                var newRow = new SortedList<int, ulong>(existingRow.Count);
                _rows[row.Key] = newRow;
                foreach (var key in existingRow.Keys)
                {
                    newRow[key] = existingRow[key];
                }
            }
        }

        public bool this[int x, int y]
        {
            get
            {
                var rowIndex = _rows.IndexOfKey(y);
                if (rowIndex < 0) return false;
                var row = _rows.Values[rowIndex];

                var colIndex = row.IndexOfKey(x >> 6);
                if (colIndex < 0) return false;
                var memCell = row.Values[colIndex];

                return ((memCell >> (x & 63)) & 1) == 1;
            }
            set
            {
                SetInterval(x, y);
                var rowIndex = _rows.IndexOfKey(y);
                SortedList<int, ulong> row;
                if (rowIndex < 0)
                {
                    row = new SortedList<int, ulong>();
                    _rows.Add(y, row);
                }
                else row = _rows.Values[rowIndex];

                var memIndex = x >> 6;
                var colIndex = row.IndexOfKey(memIndex);
                ulong memCell = 0;
                if (colIndex < 0)
                {
                    if (!value) return; // row is empty, setting to false is redundant, not changing version
                    row.Add(memIndex, memCell);
                }
                else
                {
                    memCell = row.Values[colIndex];
                }

                if (value)
                {
                    memCell |= (ulong)1 << (x & 0b111111);
                }
                else
                {
                    memCell &= ~((ulong)1 << (x & 0b111111));
                }
                row[memIndex] = memCell;
                _version += 1;

                if (memCell != 0) return;
                row.RemoveAt(colIndex);
                if (row.Count == 0)
                {
                    _rows.RemoveAt(rowIndex);
                }
            }
        }

        private void SetInterval(int x, int y)
        {
            if (x - 1 < MinX) MinX = x - 1;
            if (x + 1 > MaxX) MaxX = x + 1;
            if (y - 1 < MinY) MinY = y - 1;
            if (y + 1 > MaxY) MaxY = y + 1;
        }

        public int MinX { get; private set; }
        public int MinY { get; private set; }
        public int MaxX { get; private set; }
        public int MaxY { get; private set; }

        //public int Rows => _rows.Keys[_rows.Count];
        //public int Columns => _rows.Values.Max(list => list.Keys[list.Count] + 63);

        public IWorld Add(IWorld toAdd)
        {
            return Add((CellWorld)toAdd);
        }
        public CellWorld Add(CellWorld toAdd)
        {
            if (toAdd == null) return this;
            foreach (var rowToAdd in toAdd._rows)
            {
                if (_rows.ContainsKey(rowToAdd.Key))
                {
                    var mergeRow = _rows[rowToAdd.Key];
                    foreach (var colToAdd in rowToAdd.Value)
                    {
                        if (mergeRow.ContainsKey(colToAdd.Key))
                        {
                            mergeRow[colToAdd.Key] = mergeRow[colToAdd.Key] | colToAdd.Value;
                        }
                        else
                        {
                            mergeRow.Add(colToAdd.Key, colToAdd.Value);
                        }
                    }
                }
                else
                {
                    _rows.Add(rowToAdd.Key, rowToAdd.Value);
                }
            }
            return this;
        }

        public static CellWorld operator +(CellWorld a, CellWorld b)
        {
            return a.Add(b);
        }

        public IWorld Remove(IWorld toRemove)
        {
            return Remove((CellWorld)toRemove);
        }

        public CellWorld Remove(CellWorld toRemove)
        {
            if (toRemove == null) return this;
            foreach (var rowToDel in toRemove._rows)
            {
                if (!_rows.ContainsKey(rowToDel.Key)) continue;

                var mergeRow = _rows[rowToDel.Key];
                foreach (var colToDel in rowToDel.Value)
                {
                    if (mergeRow.ContainsKey(colToDel.Key))
                    {
                        mergeRow[colToDel.Key] = mergeRow[colToDel.Key] & ~colToDel.Value;
                    }
                }
            }
            return this;
        }

        public static CellWorld operator -(CellWorld a, CellWorld b)
        {
            return a.Remove(b);
        }



        public IEnumerator<Cell> GetEnumerator()
        {
            return new SparseArrayEnumerator(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public object Clone()
        {
            return new CellWorld(this);
        }

        public class SparseArrayEnumerator : IEnumerator<Cell>
        {
            private readonly CellWorld _world;
            private readonly SortedList<int, SortedList<int, ulong>> _rows;
            private SortedList<int, ulong> row = new SortedList<int, ulong>();

            private Cell _current;
            private readonly int _version;

            private int rowIndex = -1, colIndex = -1, memIndex = -1;
            private ulong memCell;

            internal SparseArrayEnumerator(CellWorld world)
            {
                _world = world;
                _rows = world._rows;
                if (_rows.Count > 0)
                {
                    row = _rows.Values[0];
                    rowIndex = 0;
                }
                _version = world._version;
            }

            public bool MoveNext()
            {
                if (_version != _world._version)
                    throw new InvalidOperationException("Collection was modified");
                if (!FindNext()) return false;
                _current.X = (row.Keys[colIndex] << 6) + memIndex;
                _current.Y = _rows.Keys[rowIndex];
                return true;
            }

            private bool FindNext()
            {
                if (memCell != 0 && FindNextBit())
                {
                    return true;
                }

                //i'm done with the current memCell, get next memCell
                if (FindNextMemCell())
                    return true;

                // i'm done with the last memCell of the row, get new row
                do
                {
                    if (rowIndex + 1 >= _rows.Count)
                    {
                        row = new SortedList<int, ulong>(); //leave an empty row in place
                        return false; //i'm done with the last row
                    }
                    rowIndex += 1;
                } while (rowIndex < _rows.Count && _rows.Values[rowIndex].Count == 0);
                colIndex = -1;
                row = _rows.Values[rowIndex];
                return FindNextMemCell();

                bool FindNextBit()
                {
                    do
                    {
                        var firstBitIsClear = (memCell & 1) == 0;
                        memIndex += 1;
                        memCell >>= 1;
                        if (firstBitIsClear) continue;
                        return true;
                    } while (memIndex < 63);
                    return false;
                }

                bool FindNextMemCell()
                {
                    if (colIndex + 1 >= row.Count) return false;
                    colIndex += 1;
                    memCell = row.Values[colIndex]; //it should not be 0
                    memIndex = -1;
                    return FindNextBit();
                }
            }

            public void Reset()
            {
                rowIndex = -1; colIndex = -1; memIndex = -1; memCell = 0;
            }

            public Cell Current => _current;
            object IEnumerator.Current => _current;

            public void Dispose()
            {
                row = null;
            }

        }
    }
}