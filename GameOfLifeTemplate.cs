namespace GoL
{
    public class GameOfLifeTemplate : GameOfLifeBase<CellWorld>
    {
        public override bool IsAlive(int cellPositionX, int cellPositionY)
        {
            if (  /*condition*/ true /**/ )
                return true;
            else
                return false;
        }

        public override int GetNumberOfNeighbours(int cellPositionX, int cellPositionY)
        {
            //  1,1     1,2     1,3
            //  2,1    (2,2)    2,3
            //  3,1     3,2     3,3

            var neighbours = 0;

            //  1,1     1,2     1,3
            if (/*condition*/ true /**/)
                neighbours = neighbours + 1;
            // ...

            //  2,1    (2,2)    2,3
            // ...

            //  3,1     3,2     3,3
            // ...
            return neighbours;
        }

        public override void ComputeNextGeneration()
        {
            // currentGeneration.Rows
            // currentGeneration.Columns
        
            //nextGeneration
        }
    }
}
