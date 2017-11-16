namespace GoLife
{
    public class GameOfLife : GameOfLifeBase<CellWorld>
    {
        // currentGeneration [ row, column ] == Alive => nextGeneration[ row, column ] == Alive/Dead?

        public override int GetNumberOfAliveNeighbours(int row, int column)
        {
            var neighbours = 0;

            return neighbours;
        }

        public override void ComputeNextGeneration()
        {



        }
    }
}
