

namespace GoL
{
    public class GameOfLife : GameOfLifeBase<CellWorld>
    {
        public override bool IsAlive(IWorld world, int cellPositionX, int cellPositionY)
        {
            if (world[cellPositionX, cellPositionY] == Alive)
                return true;
            else
                return false;
        }

        public override int GetNumberOfNeighbours(IWorld world, int cellPositionX, int cellPositionY)
        {
            var neighbours = 0;
            if (IsAlive(world, cellPositionX - 1, cellPositionY - 1)) neighbours = neighbours + 1;
            if (IsAlive(world, cellPositionX, cellPositionY - 1)) neighbours = neighbours + 1;
            if (IsAlive(world, cellPositionX + 1, cellPositionY - 1)) neighbours = neighbours + 1;
            if (IsAlive(world, cellPositionX - 1, cellPositionY)) neighbours = neighbours + 1;
            if (IsAlive(world, cellPositionX + 1, cellPositionY)) neighbours = neighbours + 1;
            if (IsAlive(world, cellPositionX - 1, cellPositionY + 1)) neighbours = neighbours + 1;
            if (IsAlive(world, cellPositionX, cellPositionY + 1)) neighbours = neighbours + 1;
            if (IsAlive(world, cellPositionX + 1, cellPositionY + 1)) neighbours = neighbours + 1;
            return neighbours;
        }

        public override bool IsItLonely(int numberOfNeighbours)
        {
            return numberOfNeighbours < 2;
        }

        public override bool IsItJustRight(int numberOfNeighbours)
        {
            return numberOfNeighbours == 2;
        }

        public override bool IsItPerfect(int numberOfNeighbours)
        {
            return numberOfNeighbours == 3;
        }

        public override bool IsItOvercrowded(int numberOfNeighbours)
        {
            return numberOfNeighbours > 3;
        }


        public override bool WillItBeBorn(int numberOfNeighbours)
        {
            return IsItPerfect(numberOfNeighbours);
        }

        public override bool WillItLive(int numberOfNeighbours)
        {
            return IsItJustRight(numberOfNeighbours) || IsItPerfect(numberOfNeighbours);
        }

        public override bool WillItDie(int numberOfNeighbours)
        {
            return IsItLonely(numberOfNeighbours) || IsItOvercrowded(numberOfNeighbours);
        }

        public override Generation<CellWorld> ComputeNextGeneration(Generation<CellWorld> previousGeneration)
        {
            var newGeneration = new Generation<CellWorld>
            {
                Previous = previousGeneration.Live + previousGeneration.Born - previousGeneration.Dead
            };

            foreach (var cell in newGeneration.Previous)
            {
                for (var i = cell.X - 1; i <= cell.X + 1; i++)
                {
                    for (var j = cell.Y - 1; j <= cell.Y + 1; j++)
                    {
                        var numberOfNeighbours = GetNumberOfNeighbours(newGeneration.Previous, i, j);
                        if (!IsAlive(newGeneration.Previous, i, j) && WillItBeBorn(numberOfNeighbours))
                            newGeneration.Born[i, j] = true;

                        if (IsAlive(newGeneration.Previous, i, j) && WillItLive(numberOfNeighbours))
                            newGeneration.Live[i, j] = true;

                        if (IsAlive(newGeneration.Previous, i, j) && WillItDie(numberOfNeighbours))
                            newGeneration.Dead[i, j] = true;
                    }
                }
            }

            return newGeneration;
        }
    }
}
