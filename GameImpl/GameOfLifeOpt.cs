namespace GoLife
{
    public class GameOfLifeOpt : GameOfLifeBase<CellWorld>
    {
        public override int GetNumberOfAliveNeighbours(int x, int y)
        {
            var neighbours = 0;

            if (currentGeneration[x - 1, y - 1]) neighbours = neighbours + 1;
            if (currentGeneration[x, y - 1]) neighbours = neighbours + 1;
            if (currentGeneration[x + 1, y - 1]) neighbours = neighbours + 1;
            if (currentGeneration[x - 1, y]) neighbours = neighbours + 1;
            if (currentGeneration[x + 1, y]) neighbours = neighbours + 1;
            if (currentGeneration[x - 1, y + 1]) neighbours = neighbours + 1;
            if (currentGeneration[x, y + 1]) neighbours = neighbours + 1;
            if (currentGeneration[x + 1, y + 1]) neighbours = neighbours + 1;

            return neighbours;
        }

        public override void ComputeNextGeneration()
        {
            if (HighlightChanges)
                foreach (var cell in currentGeneration)
                {
                    for (var row = cell.Y - 1; row <= cell.Y + 1; row++)
                    {
                        for (var column = cell.X - 1; column <= cell.X + 1; column++)
                        {
                            var numberOfNeighbours = GetNumberOfAliveNeighbours(column, row);
                            if (!currentGeneration[column, row] && numberOfNeighbours == 3)
                                ng.Born[column, row] = true;

                            if (currentGeneration[column, row] && (numberOfNeighbours == 2 || numberOfNeighbours == 3))
                                ng.Live[column, row] = true;

                            if (currentGeneration[column, row] && (numberOfNeighbours < 2 || numberOfNeighbours > 3))
                                ng.Dead[column, row] = true;
                        }
                    }
                }
            else
                foreach (var cell in currentGeneration)
                {
                    for (var row = cell.Y - 1; row <= cell.Y + 1; row++)
                    {
                        for (var column = cell.X - 1; column <= cell.X + 1; column++)
                        {
                            var numberOfNeighbours = GetNumberOfAliveNeighbours(column, row);
                            if (!currentGeneration[column, row] && numberOfNeighbours == 3)
                                nextGeneration[column, row] = true;

                            if (currentGeneration[column, row] && (numberOfNeighbours == 2 || numberOfNeighbours == 3))
                                nextGeneration[column, row] = true;

                            if (currentGeneration[column, row] && (numberOfNeighbours < 2 || numberOfNeighbours > 3))
                                nextGeneration[column, row] = false;
                        }
                    }
                }
        }

    }
}
