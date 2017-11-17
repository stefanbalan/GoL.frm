using System.Windows.Forms;

namespace GoLife
{
    public class GameOfLife : GameOfLifeBase<CellWorld>
    {
        // currentGeneration [ row, column ] == Alive => nextGeneration[ row, column ] == Alive/Dead?

        public override int GetNumberOfAliveNeighbours(int row, int column)
        {
            var neighbours = 0;
            if (currentGeneration[row - 1, column - 1] == Alive) neighbours++;
            if (currentGeneration[row - 1, column] == Alive) neighbours++;
            if (currentGeneration[row - 1, column + 1] == Alive) neighbours++;
            if (currentGeneration[row, column - 1] == Alive) neighbours++;
            if (currentGeneration[row + 1, column - 1] == Alive) neighbours++;
            if (currentGeneration[row + 1, column] == Alive) neighbours++;
            if (currentGeneration[row, column + 1] == Alive) neighbours++;
            if (currentGeneration[row + 1, column + 1] == Alive) neighbours++;
            return neighbours;
        }

        public override void ComputeNextGeneration()
        {
            // currentGeneration ( StartRow, StartColumn )
            // currentGeneration ( EndRow, EndColumn )


            //for every row from the minimum value to the maximum, increase row
            for (int row = currentGeneration.StartRow; row <= currentGeneration.EndRow; row++)
            {
                // every column from the minimum value to the maximum increase column
                for (int column = currentGeneration.StartColumn; column <= currentGeneration.EndColumn; column++)
                {

                    // apply the rules to compute nextGeneration

                    // RULE: if the cell at row and column is Alive and NumberOfNeighbours for cell at row and column  is smaller than 2 
                    // nextGeneration at row and column becomes Dead
                    if (GetNumberOfAliveNeighbours(row, column) <2 && currentGeneration[row, column] == Alive)
                        nextGeneration[row, column] = Dead;


                    // RULE: if the cell at row and column is Alive and 
                    //    NumberOfNeighbours for cell at row and column is equal to 2  or 
                    //    NumberOfNeighbours for cell at row and column is equal to 3
                    // nextGeneration at row and column stays ALive
                    if ((GetNumberOfAliveNeighbours(row, column) == 2 || GetNumberOfAliveNeighbours(row, column) == 3) && currentGeneration[row, column] == Alive)
                        nextGeneration[row, column] = Alive;

                    


                    // RULE: if the cell at row and column is Alive and NumberOfNeighbours for cell at row and column  is greater than 3
                    // nextGeneration at row and column becomes Dead
                    if (currentGeneration[row, column] == Alive && GetNumberOfAliveNeighbours(row, column) > 3)
                        nextGeneration[row, column] = Dead;



                    // RULE: if the cell at row and column is Dead and NumberOfNeighbours for cell at row and column is equal to 3
                    // nextGeneration at row and column becomes Alive
                    if (currentGeneration[row, column] == Dead && GetNumberOfAliveNeighbours(row, column) == 3)
                        nextGeneration[row, column] = Alive;



                }
            }
        }

    }
}
