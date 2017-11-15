namespace GoLife
{
    public class GameOfLifeFinal : GameOfLifeBase<CellWorld>
    {
        // currentGeneration [ row, column ] == Alive => nextGeneration[ row, column ] == Alive/Dead?

        public override int GetNumberOfAliveNeighbours(int row, int column)
        {
            // row-1,column-1  row-1,column  row-1,column+1
            //   row,column-1    row,column    row,column+1
            // row+1,column-1  row+1,column  row+1,column+1

            var neighbours = 0;

            // if currentGeneration at row-1 and column-1 is Alive then increase neighbours
            if (currentGeneration[row - 1, column - 1] == Alive) neighbours = neighbours + 1;
            // if currentGeneration at row-1 and column is Alive then increase neighbours
            if (currentGeneration[row - 1, column] == Alive) neighbours = neighbours + 1;
            // if currentGeneration at row-1 and column+1 is Alive then increase neighbours
            if (currentGeneration[row - 1, column + 1] == Alive) neighbours = neighbours + 1;

            // if currentGeneration at row and column-1 is Alive then increase neighbours
            if (currentGeneration[row, column - 1] == Alive) neighbours++;
            // if currentGeneration at row and column+1 is Alive then increase neighbours
            if (currentGeneration[row, column + 1] == Alive) neighbours++;

            // if currentGeneration at row+1 and column-1 is Alive then increase neighbours
            if (currentGeneration[row + 1, column - 1] == Alive) neighbours = neighbours + 1;
            // if currentGeneration at row+1 and column is Alive then increase neighbours
            if (currentGeneration[row + 1, column] == Alive) neighbours++;
            // if currentGeneration at row+1 and column+1 is Alive then increase neighbours
            if (currentGeneration[row + 1, column + 1] == Alive) neighbours++;

            return neighbours;
        }

        public override void ComputeNextGeneration()
        {
            // currentGeneration ( StartRow, StartColumn )
            // currentGeneration ( EndRow, EndColumn )

            for (int row = currentGeneration.StartRow; row < currentGeneration.EndRow; row++)
            {
                for (int column = currentGeneration.StartColumn; column < currentGeneration.EndColumn; column++)
                {
                    // apply the rules to compute nextGeneration

                    // if the cell at row and column is Alive and NumberOfNeighbours for cell at row and column  is smaller than 2 
                    // nextGeneration at row and column becomes Dead
                    if (currentGeneration[row, column] == Alive && GetNumberOfAliveNeighbours(row, column) < 2)
                        nextGeneration[row, column] = Dead;

                    // if the cell at row and column is Alive and 
                    //    NumberOfNeighbours for cell at row and column is equal to 2  or 
                    //    NumberOfNeighbours for cell at row and column is equal to 3
                    // nextGeneration at row and column stays ALive

                    if (currentGeneration[row, column] == Alive && (GetNumberOfAliveNeighbours(row, column) == 2 ||
                        GetNumberOfAliveNeighbours(row, column) == 3))
                        nextGeneration[row, column] = Alive;


                    // if the cell at row and column is Alive and NumberOfNeighbours for cell at row and column  is greater than 3
                    // nextGeneration at row and column becomes Dead

                    if (currentGeneration[row, column] == Alive && GetNumberOfAliveNeighbours(row, column) > 3)
                        nextGeneration[row, column] = Dead;

                    // if the cell at row and column is Dead and NumberOfNeighbours for cell at row and column is equal to 3
                    // nextGeneration at row and column becomes Alive
                    if (currentGeneration[row, column] == Dead && GetNumberOfAliveNeighbours(row, column) == 3)
                        nextGeneration[row, column] = Alive;
                
                }
            }
        }
    }
}
