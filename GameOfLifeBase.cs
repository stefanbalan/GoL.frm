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
        protected Generation<TWorld> cg;
        protected TWorld nextGeneration;
        protected Generation<TWorld> ng;

        public bool HighlightChanges { get; set; }


        protected GameOfLifeBase()
        {
            _iterations = new ConcurrentQueue<Generation<TWorld>>();
            cg = new Generation<TWorld> { Live = new TWorld() };
            _iterations.Enqueue(cg);

            TargetTimeMs = 1000;
        }

        public void Initialize(Generation<TWorld> generation)
        {
            cg = generation;
            _iterations.Enqueue(cg);
        }

        public void AddCellAt(int x, int y)
        {
            cg.Live[x, y] = true;
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
                currentGeneration = (TWorld)((CellWorld)cg.Live.Clone())
                    .Add(cg.Born)
                    .Remove(cg.Dead);
                ng = new Generation<TWorld>();
                nextGeneration = ng.Live;

                ComputeNextGeneration();

                _iterations.Enqueue(ng);
                cg = ng;

                sw.Stop();
                if (sw.ElapsedMilliseconds < TargetTimeMs)
                    Thread.Sleep((int)(TargetTimeMs - sw.ElapsedMilliseconds));
                if (_iterations.Count > 8)
                {
                    Debug.WriteLine($"{_iterations.Count} itterations; sleeping");
                    Thread.Sleep((int)(TargetTimeMs - sw.ElapsedMilliseconds));
                }
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
