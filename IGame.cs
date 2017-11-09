namespace GoL
{
    public interface IGame<TWorld> where TWorld : class, IWorld, new()
    {
        void Initialize(Generation<TWorld> generation);
        void AddCellAt(int x, int y);


        bool IsAlive(int x, int y);
        int GetNumberOfNeighbours(int x, int y);

        void ComputeNextGeneration();
    }
}