using System.Windows.Forms;

namespace GoL
{
    public interface IGame<TWorld> where TWorld : class, IWorld, new()
    {
        void Initialize(Generation<TWorld> generation);

        bool IsAlive(IWorld world, int cellPositionX, int cellPositionY);

        int GetNumberOfNeighbours(IWorld world, int cellPositionX, int cellPositionY);

        bool IsItLonely(int numberOfNeighbours);
        bool IsItJustRight(int numberOfNeighbours);
        bool IsItPerfect(int numberOfNeighbours);
        bool IsItOvercrowded(int numberOfNeighbours);

        bool WillItBeBorn(int numberOfNeighbours);
        bool WillItLive(int numberOfNeighbours);
        bool WillItDie(int numberOfNeighbours);

        Generation<TWorld> ComputeNextGeneration(Generation<TWorld> previousGeneration);
    }
}