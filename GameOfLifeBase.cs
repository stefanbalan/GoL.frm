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
        protected TWorld currentGeneration;
        protected Generation<TWorld> current;
        protected TWorld nextGeneration;
        protected Generation<TWorld> next;

        public bool KeepChanges { get; set; }


        protected GameOfLifeBase()
        {
            _iterations = new ConcurrentQueue<Generation<TWorld>>();
            current = new Generation<TWorld> { Live = new TWorld() };
            _iterations.Enqueue(current);

            TargetTimeMs = 1000;
        }

        public void Initialize(Generation<TWorld> generation)
        {
            current = generation;
            _iterations.Enqueue(current);
        }

        public void AddCellAt(int x, int y)
        {
            current.Live[x, y] = true;
        }

        #region Game
        public abstract bool IsAlive(int i, int cellPositionX);

        public abstract int GetNumberOfNeighbours(int i, int cellPositionX);

        public abstract void ComputeNextGeneration();
        #endregion


        public long TargetTimeMs { get; set; }
        public bool Stop { get; set; }

        public void Run()
        {
            var sw = new Stopwatch();
            do
            {
                sw.Start();
                currentGeneration = (TWorld)current.Live
                    .Add(current.Born)
                    .Remove(current.Dead);
                next = new Generation<TWorld>();
                nextGeneration = next.Live;

                ComputeNextGeneration();

                _iterations.Enqueue(next);
                current = next;

                sw.Stop();
                if (sw.ElapsedMilliseconds < TargetTimeMs || _iterations.Count > 8)
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
