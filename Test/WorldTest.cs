using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoL.Test
{
    [TestClass]
    public class WorldTest
    {
        private List<Position> _testMap;
        private CellWorld _world;

        [TestInitialize]
        public void Prepare()
        {
            _testMap = new List<Position>
            {

            };

            for (var i = 0; i < 100; i++)
            {
                for (var j = 0; j < 100; j++)
                {
                    _testMap.Add(new Position { X = i, Y = j });
                }
            }

            var r = new Random();
            for (var i = 0; i < 10000; i++)
            {
                var x = r.Next(1000);
                var y = r.Next(1000);
                var n = new Position { X = 100 + x, Y = 100 + y };
                if (_testMap.Contains(n)) continue;
                _testMap.Add(n);
            }

            _world = new CellWorld();

            foreach (var position in _testMap)
            {
                _world[position.X, position.Y] = true;
            }
        }

        [TestMethod]
        public void Run()
        {
            foreach (var position in _testMap)
            {
                Assert.IsTrue(_world[position.X, position.Y]);
            }
            //set it again
            foreach (var position in _testMap)
            {
                _world[position.X, position.Y] = true;
                Assert.IsTrue(_world[position.X, position.Y]);
            }
        }

        [TestMethod]
        public void Enumerate()
        {
            var count = 0;
            foreach (var position in _world)
            {
                var ex = _testMap.FirstOrDefault(p => p.X == position.X && p.Y == position.Y);
                Assert.AreEqual(position, ex);
                Assert.IsTrue(_testMap.Contains(position));
                count += 1;
            }
            Assert.AreEqual(count, _testMap.Count);


            var sw = new Stopwatch();
            sw.Start();
            count = _world.Count();
            sw.Stop();
            Assert.AreEqual(count, _testMap.Count);

            Debug.WriteLine($"\n\n\n//ulong enumerate: count:{count}, ticks: {sw.ElapsedTicks }, ms:{sw.ElapsedMilliseconds}\n\n\n");
            //uint enumerate: count:19959, ticks: 7375, ms:3
            //uint enumerate: count:105049, ticks: 37074, ms:19

            //ulong enumerate: count:9940, ticks: 8296, ms:4
            //ulong enumerate: count:19950, ticks: 8976, ms:4
            //ulong enumerate: count:109938, ticks: 71533, ms:36

        }

        [TestMethod]
        public void Clear()
        {
            var random = new Random();
            while (_testMap.Count > 0)
            {
                var removeIndex = random.Next(_testMap.Count);
                var remove = _testMap[removeIndex];
                _testMap.RemoveAt(removeIndex);

                Assert.IsTrue(_world[remove.X, remove.Y]);
                _world[remove.X, remove.Y] = false;
                Assert.IsFalse(_world[remove.X, remove.Y]);

                foreach (var position in _testMap)
                {
                    Assert.IsTrue(_world[position.X, position.Y]);
                }
            }
        }

        [TestMethod]
        public void Clone()
        {

            var sw = new Stopwatch();
            sw.Start();
            var newWorld = (CellWorld)_world.Clone();
            sw.Stop();
            Debug.WriteLine($"\n\n\n//ulong clone: count:{_world._version}, ticks: {sw.ElapsedTicks }, ms:{sw.ElapsedMilliseconds}\n\n\n");
            //ulong clone: count:20000, ticks: 10234, ms:5
            //ulong clone: count:109949, ticks: 35756, ms:18


            foreach (var position in _world)
            {
                Assert.IsTrue(newWorld[position.X, position.Y]);
            }
            foreach (var position in newWorld)
            {
                Assert.IsTrue(_world[position.X, position.Y]);
            }

            var random = new Random();
            while (_testMap.Count > 0)
            {
                var removeIndex = random.Next(_testMap.Count);
                var remove = _testMap[removeIndex];
                _testMap.RemoveAt(removeIndex);

                Assert.IsTrue(newWorld[remove.X, remove.Y]);
                newWorld[remove.X, remove.Y] = false;
                Assert.IsFalse(newWorld[remove.X, remove.Y]);
                Assert.IsTrue(_world[remove.X, remove.Y]);
            }
        }

        [TestMethod]
        public void Add()
        {
            var addMap = new List<Position>();
            var addWorld = new CellWorld();

            var r = new Random();
            for (var i = 0; i < 1000; i++)
            {
                var x = r.Next(1000);
                var y = r.Next(1000);
                var n = new Position { X = x, Y = y };
                if (addMap.Contains(n) || _world[x, y]) continue;
                addMap.Add(n);
                addWorld[x, y] = true;
            }

            _world = _world.Add(addWorld);

            foreach (var position in _testMap)
            {
                Assert.IsTrue(_world[position.X, position.Y]);
            }
            foreach (var position in addMap)
            {
                Assert.IsTrue(_world[position.X, position.Y]);
            }
        }


        [TestMethod]
        public void Remove()
        {
            var removeMap = new List<Position>();
            var removeWorld = new CellWorld();

            var r = new Random();
            for (var i = 0; i < 1000; i++)
            {
                var index = r.Next(_testMap.Count);
                removeWorld[_testMap[index].X, _testMap[index].Y] = true;
                removeMap.Add(_testMap[index]);
                _testMap.RemoveAt(index);
            }

            _world = _world.Remove(removeWorld);

            foreach (var position in _testMap)
            {
                Assert.IsTrue(_world[position.X, position.Y]);
            }
            foreach (var position in removeMap)
            {
                Assert.IsFalse(_world[position.X, position.Y]);
            }
        }
    }
}
