namespace GoL
{
    public class GameOfLifeMine : GameOfLifeBase<CellWorld>
    {
        public override bool IsAlive(int cellPositionX, int cellPositionY)
        {
            return currentGeneration[cellPositionX, cellPositionY];
        }

        public override int GetNumberOfNeighbours(int cellPositionX, int cellPositionY)
        {
            var neighbours = 0;

            if (currentGeneration[cellPositionX - 1, cellPositionY - 1]) neighbours = neighbours + 1;
            if (currentGeneration[cellPositionX, cellPositionY - 1]) neighbours = neighbours + 1;
            if (currentGeneration[cellPositionX + 1, cellPositionY - 1]) neighbours = neighbours + 1;
            if (currentGeneration[cellPositionX - 1, cellPositionY]) neighbours = neighbours + 1;
            if (currentGeneration[cellPositionX + 1, cellPositionY]) neighbours = neighbours + 1;
            if (currentGeneration[cellPositionX - 1, cellPositionY + 1]) neighbours = neighbours + 1;
            if (currentGeneration[cellPositionX, cellPositionY + 1]) neighbours = neighbours + 1;
            if (currentGeneration[cellPositionX + 1, cellPositionY + 1]) neighbours = neighbours + 1;

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
                            var numberOfNeighbours = GetNumberOfNeighbours(column, row);
                            if (!currentGeneration[column, row] && numberOfNeighbours == 3)
                                next.Born[column, row] = true;

                            if (currentGeneration[column, row] && (numberOfNeighbours == 2 || numberOfNeighbours == 3))
                                next.Live[column, row] = true;

                            if (currentGeneration[column, row] && (numberOfNeighbours < 2 || numberOfNeighbours > 3))
                                next.Dead[column, row] = true;
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
                            var numberOfNeighbours = GetNumberOfNeighbours(column, row);
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
