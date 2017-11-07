using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

namespace GoL
{


    public abstract class GameOfLifeBase<TWorld> : IGame<TWorld> where TWorld : class, IWorld, new()
    {
        public const bool Alive = true;
        public const bool Dead = false;
        private readonly ConcurrentQueue<Generation<TWorld>> _iterations;
        private Generation<TWorld> _current;

        protected GameOfLifeBase()
        {
            _iterations = new ConcurrentQueue<Generation<TWorld>>();
            _current = new Generation<TWorld> { Live = new TWorld() };
            _iterations.Enqueue(_current);

            TargetTimeMs = 1000;
        }

        public void Initialize(Generation<TWorld> generation)
        {
            _current = generation;
            _iterations.Enqueue(_current);
        }

        #region Game
        public abstract Generation<TWorld> ComputeNextGeneration(Generation<TWorld> previousGeneration);

        public abstract int GetNumberOfNeighbours(IWorld world, int cellPositionX, int cellPositionY);
        public abstract bool IsAlive(IWorld world, int cellPositionX, int cellPositionY);

        public abstract bool IsItLonely(int numberOfNeighbours);
        public abstract bool IsItJustRight(int numberOfNeighbours);
        public abstract bool IsItPerfect(int numberOfNeighbours);
        public abstract bool IsItOvercrowded(int numberOfNeighbours);

        public abstract bool WillItBeBorn(int numberOfNeighbours);
        public abstract bool WillItLive(int numberOfNeighbours);
        public abstract bool WillItDie(int numberOfNeighbours);
        #endregion


        public long TargetTimeMs { get; set; }
        public bool Stop { get; set; }

        public void Run()
        {
            var sw = new Stopwatch();
            do
            {
                sw.Start();
                var newGeneration = ComputeNextGeneration(_current);
                _iterations.Enqueue(newGeneration);
                _current = newGeneration;
                sw.Stop();
                if (sw.ElapsedMilliseconds < TargetTimeMs)
                    Thread.Sleep((int)(TargetTimeMs - sw.ElapsedMilliseconds));
                sw.Reset();

            } while (!Stop);
        }

        public Generation<TWorld> TryGetNext()
        {
            return _iterations.TryDequeue(out var result)
                ? result
                : null;
        }

    }
}
