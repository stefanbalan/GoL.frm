namespace GoL
{
    public interface IGame<TWorld> where TWorld : class, IWorld, new()
    {
        void Initialize(Generation<TWorld> generation);
        void AddCellAt(int x, int y);


        bool IsAlive(int cellPositionX, int cellPositionY);
        int GetNumberOfNeighbours(int cellPositionX, int cellPositionY);

        void ComputeNextGeneration();
    }
}