namespace GoLife
{
    public class GameOfLifeTemplate : GameOfLifeBase<CellWorld>
    {
        // currentGeneration [ row, column ] == Alive => nextGeneration[ row, column ] == Alive/Dead?

        public override int GetNumberOfAliveNeighbours(int row, int column)
        {
            // row-1,column-1  row-1,column  row-1,column+1
            //   row,column-1    row,column    row,column+1
            // row+1,column-1  row+1,column  row+1,column+1

            //   1,1      1,2    1,3
            //   2,1     (2,2)   2,3
            //   3,1      3,2    3,3

            var neighbours = 0;

            //  1,1     1,2     1,3
            // if currentGeneration at row-1 and column-1 is Alive then increase neighbours
            // if currentGeneration at row-1 and column is Alive then increase neighbours
            // if currentGeneration at row-1 and column+1 is Alive then increase neighbours
            if (currentGeneration[row - 1, column - 1] == Alive) neighbours = neighbours + 1;

            //  2,1    (2,2)    2,3
            // if currentGeneration at row and column-1 is Alive then increase neighbours
            // if currentGeneration at row and column+1 is Alive then increase neighbours

            //  3,1     3,2     3,3
            // if currentGeneration at row+1 and column-1 is Alive then increase neighbours
            // if currentGeneration at row+1 and column is Alive then increase neighbours
            // if currentGeneration at row+1 and column+1 is Alive then increase neighbours

            return neighbours;
        }

        public override void ComputeNextGeneration()
        {
            // currentGeneration ( StartRow, StartColumn )
            // currentGeneration ( EndRow, EndColumn )

            for (/* every row from the minimum value  */; /* to the maximum */; /* increase row */)
            {
                for (/* every column from the minimum value  */; /* to the maximum */; /* increase column */)
                {
                    // apply the rules to compute nextGeneration

                    // if the cell at row and column is Alive and NumberOfNeighbours for cell at row and column  is smaller than 2 
                    // nextGeneration at row and column becomes Dead

                    // if the cell at row and column is Alive and NumberOfNeighbours for cell at row and column  is greater than 3
                    // nextGeneration at row and column becomes Dead

                    // if the cell at row and column is Alive and 
                    //    NumberOfNeighbours for cell at row and column is equal to 2  or 
                    //    NumberOfNeighbours for cell at row and column is equal to 3
                    // nextGeneration at row and column stays ALive

                    // if the cell at row and column is Dead and NumberOfNeighbours for cell at row and column is equal to 3
                    // nextGeneration at row and column becomes Alive

                }
            }
        }
    }
}
