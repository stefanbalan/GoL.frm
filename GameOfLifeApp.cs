using System;
using System.Diagnostics;
using System.Windows.Forms;
using GoL.Infrastructure;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using Configuration = GoL.Infrastructure.Configuration;

namespace GoL
{
    public class GameOfLifeApp :
        Direct2D1App
    //Direct2D1WinFormApp
    {
        private SolidColorBrush _backColorBrush;
        private SolidColorBrush _liveColorBrush;
        private SolidColorBrush _bornColorBrush;
        private SolidColorBrush _deadColorBrush;
        private readonly int _cellSize = 10;
        private readonly int _cellCornerRadius = 2;

        private Generation<CellWorld> current;
        private readonly GameOfLife game;

        public GameOfLifeApp(Configuration configuration, GameOfLife game) : base(configuration)
        {
            this.game = game;
        }

        protected override void Initialize(Configuration configuration)
        {
            base.Initialize(configuration);
            configuration.OnChange += UpdateConfiguration;
            UpdateConfiguration();

            current = new Generation<CellWorld> { Live = GetTestInitialMap() };
            game.Initialize(current);
        }

        private CellWorld GetTestInitialMap()
        {
            var _testMap = new CellWorld
            {
                //block
                [0, 0] = true,
                [0, 1] = true,
                [1, 0] = true,
                [1, 1] = true,
                //blinker
                [20, 0] = true,
                [20, 1] = true,
                [20, 2] = true,
                //blinker
                [40, 0] = true,
                [41, 0] = true,
                [42, 0] = true
            };

            for (var i = 50; i < 100; i++)
            {
                _testMap[i, i] = true;
            }

            var r = new Random();
            for (var i = 0; i < 100000; i++)
            {
                var x = r.Next(1000) + 50;
                var y = r.Next(1000) + 50;

                _testMap[x, y] = true;
            }
            return _testMap;
        }

        private void UpdateConfiguration()
        {
            _backColorBrush = new SolidColorBrush(RenderTarget2D, Configuration.BackColor);
            _liveColorBrush = new SolidColorBrush(RenderTarget2D, Configuration.LiveColor);
            _bornColorBrush = new SolidColorBrush(RenderTarget2D, Configuration.BornColor);
            _deadColorBrush = new SolidColorBrush(RenderTarget2D, Configuration.DeadColor);

            game.TargetTimeMs = Configuration.TargetMs;
        }


        protected override void Draw(Clock time)
        {
            base.Draw(time);

            var gen = game.TryGetNext();
            if (gen != null)
                current = gen;

            //RenderTarget2D.Clear(new RawColor4(1F, 01F, 01F, 10));
            RenderTarget2D.FillRectangle(new RawRectangleF(0, 0, Configuration.Width, Configuration.Height), _backColorBrush);
            try
            {
                if (current.Live != null)
                    foreach (var position in current.Live)
                    {
                        DrawLiveCellAt(position.X, position.Y);
                    }
                if (current.Born != null)
                    foreach (var position in current.Born)
                    {
                        DrawBornCellAt(position.X, position.Y);
                    }
                if (current.Dead != null)
                    foreach (var position in current.Dead)
                    {
                        DrawDeadCellAt(position.X, position.Y);
                    }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        private float _offsetX;
        private float _offsetY;
        private void DrawLiveCellAt(int x, int y)
        {
            var scale = (float)_scale;
            const int cellSize = 10;
            RenderTarget2D.FillRoundedRectangle(new RoundedRectangle
            {
                RadiusX = (float)(_cellCornerRadius * _scale),
                RadiusY = (float)(_cellCornerRadius * _scale),
                Rect = new RawRectangleF(
                    (_offsetX + x * cellSize) * scale,
                    (_offsetY + y * cellSize) * scale,
                    (_offsetX + x * cellSize + cellSize) * scale,
                    (_offsetY + y * cellSize + cellSize) * scale
                    )
            }, _liveColorBrush
            );
        }
        private void DrawBornCellAt(int x, int y)
        {
            var scale = (float)_scale;
            RenderTarget2D.FillRoundedRectangle(new RoundedRectangle
            {
                RadiusX = (float)(_cellCornerRadius * _scale),
                RadiusY = (float)(_cellCornerRadius * _scale),
                Rect = new RawRectangleF(
                        (_offsetX + x * _cellSize + 2) * scale,
                        (_offsetY + y * _cellSize + 2) * scale,
                        (_offsetX + x * _cellSize + _cellSize - 2) * scale,
                        (_offsetY + y * _cellSize + _cellSize - 2) * scale
                    )
            }, _bornColorBrush
            );
        }
        private void DrawDeadCellAt(int x, int y)
        {
            var scale = (float)_scale;
            const int cellSize = 10;
            RenderTarget2D.FillRoundedRectangle(new RoundedRectangle
            {
                RadiusX = (float)(_cellCornerRadius * _scale),
                RadiusY = (float)(_cellCornerRadius * _scale),
                Rect = new RawRectangleF(
                        (_offsetX + x * cellSize) * scale,
                        (_offsetY + y * cellSize) * scale,
                        (_offsetX + x * cellSize + cellSize) * scale,
                        (_offsetY + y * cellSize + cellSize) * scale
                    )
            }, _deadColorBrush
            );
        }

        private decimal _scale = 1;
        protected override void MouseWheel(MouseEventArgs e)
        {
            base.MouseWheel(e);
            var zoomDelta = (decimal)(e.Delta / 1200F); // 0.1 increments

            var newscale = _scale + zoomDelta;

            if (newscale >= 0.1M && newscale < 5)
            {
                _offsetX -= (int)(e.X * (float)(zoomDelta / newscale));
                _offsetY -= (int)(e.Y * (float)(zoomDelta / newscale));

                _scale = newscale;
            }
        }

        private bool _dragging;
        private Position _dragStartMousePosition;
        private float _dragStartOffsetX;
        private float _dragStartOffsetY;
        protected override void MouseDown(MouseEventArgs e)
        {
            _dragStartMousePosition = new Position { X = e.X, Y = e.Y };
            _dragStartOffsetX = _offsetX;
            _dragStartOffsetY = _offsetY;
            _dragging = true;
        }

        protected override void MouseUp(MouseEventArgs e)
        {
            _dragStartMousePosition = new Position { X = e.X, Y = e.Y };
            _dragStartOffsetX = _offsetX;
            _dragStartOffsetY = _offsetY;
            _dragging = false;
        }

        protected override void MouseMove(MouseEventArgs e)
        {
            if (!_dragging) return;

            _offsetX = _dragStartOffsetX + (int)((e.X - _dragStartMousePosition.X) / _scale);
            _offsetY = _dragStartOffsetY + (int)((e.Y - _dragStartMousePosition.Y) / _scale);
        }


    }
}
