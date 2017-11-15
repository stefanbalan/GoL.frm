namespace GoLife
{
    public interface IGame<TWorld> where TWorld : class, IWorld, new()
    {
        void Initialize(Generation<TWorld> generation);
        bool GetCellAt(int x, int y);
        void SetCellAt(int x, int y, bool live);

        //bool IsAlive(int x, int y);
        int GetNumberOfAliveNeighbours(int x, int y);

        void ComputeNextGeneration();
    }
}