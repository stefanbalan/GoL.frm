namespace GoL
{
    public class GameOfLifeFinal : GameOfLifeBase<CellWorld>
    {
        public override bool IsAlive(int cellPositionX, int cellPositionY)
        {
            // currentGeneration
            if (currentGeneration[cellPositionX, cellPositionY] == Alive)
                return true;
            else
                return false;
        }


        public override int GetNumberOfNeighbours(int x, int y)
        {
            //  1,1     1,2     1,3
            //  2,1     2,2     2,3
            //  3,1     3,2     3,3

            var neighbours = 0;

            //  1,1     1,2     1,3
            if (IsAlive(x - 1, y - 1)) neighbours = neighbours + 1;
            if (IsAlive(x, y - 1)) neighbours = neighbours + 1;
            if (IsAlive(x + 1, y - 1)) neighbours = neighbours + 1;
            //  2,1     2,2     2,3
            if (IsAlive(x - 1, y)) neighbours = neighbours + 1;
            if (IsAlive(x + 1, y)) neighbours = neighbours + 1;
            //  3,1     3,2     3,3
            if (IsAlive(x - 1, y + 1)) neighbours = neighbours + 1;
            if (IsAlive(x, y + 1)) neighbours = neighbours + 1;
            if (IsAlive(x + 1, y + 1)) neighbours = neighbours + 1;
            return neighbours;
        }

        public override void ComputeNextGeneration()
        {
            for (var x = currentGeneration.MinX; x <= currentGeneration.MaxX; x++)
            {
                for (var y = currentGeneration.MinY; y <= currentGeneration.MaxY; y++)
                {
                    var numberOfNeighbours = GetNumberOfNeighbours(x, y);

                    if (!IsAlive(x, y) &&
                    numberOfNeighbours == 3)
                        nextGeneration[x, y] = true;

                    if (IsAlive(x, y) &&
                    (numberOfNeighbours == 2 || numberOfNeighbours == 3))
                        nextGeneration[x, y] = true;

                    if (IsAlive(x, y) &&
                        numberOfNeighbours < 2)
                        nextGeneration[x, y] = false;

                    if (IsAlive(x, y) &&
                        numberOfNeighbours > 3)
                        nextGeneration[x, y] = false;
                }
            }
        }
    }
}
