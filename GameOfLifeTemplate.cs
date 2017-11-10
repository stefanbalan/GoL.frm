namespace GoL
{
    public class GameOfLifeTemplate : GameOfLifeBase<CellWorld>
    {
        public override bool IsAlive(int x, int y)
        {
            // currentGeneration
            if ( /* condition */ true /**/ )
                return true;
            else
                return false;
        }

        public override int GetNumberOfNeighbours(int x, int y)
        {
            // x-1,y-1  x-1,y  x-1,y+1
            //   x,y-1    x,y    x,y+1
            // x+1,y-1  x+1,y  x+1,y+1

            //   1,1      1,2    1,3
            //   2,1     (2,2)   2,3
            //   3,1      3,2    3,3

            var neighbours = 0;

            //  1,1     1,2     1,3
            //...

            //  2,1    (2,2)    2,3
            //...

            //  3,1     3,2     3,3
            //...

            return neighbours;
        }

        public override void ComputeNextGeneration()
        {
            // currentGeneration ( MinX, MinY )
            // currentGeneration ( MaxX, MaxY )

            for (/* every x from the minimum value  */; /* to the maximum */; /* increase x */)
            {
                for (/* every y from the minimum value  */; /* to the maximum */; /* increase y */)
                {

                    // apply the rules to compute nextGeneration

                }
            }
        }
    }
}
