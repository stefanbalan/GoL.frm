using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GoL.Infrastructure;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using Configuration = GoL.Infrastructure.Configuration;

namespace GoL
{
    public class GameOfLifeGraphicsApp :
        Direct2D1App
    //Direct2D1WinFormApp
    {
        private SolidColorBrush _backColorBrush;
        private SolidColorBrush _liveColorBrush;
        private SolidColorBrush _bornColorBrush;
        private SolidColorBrush _deadColorBrush;
        private readonly int _cellSize = 10;
        private readonly int _cellCornerRadius = 2;

        private List<Position> _testMap = new List<Position>();
        private List<Position> _testBornMap = new List<Position>();

        public GameOfLifeGraphicsApp(Configuration configuration) : base(configuration)
        {
        }

        protected override void Initialize(Configuration configuration)
        {
            base.Initialize(configuration);
            configuration.OnChange += UpdateConfiguration;
            UpdateConfiguration();

            _testMap = new List<Position>
            {
                new Position { X = 0, Y = 0 },
                new Position { X = 0, Y = 1 }
            };
            var r = new Random();

            for (var i = 0; i < 10000; i++)
            {
                var x = r.Next(1000);
                var y = r.Next(1000);

                _testMap.Add(new Position { X = x, Y = y });
            }

            _testBornMap = new List<Position>
            {
                new Position { X = 1, Y = 0 },
                new Position { X = 1, Y = 1 }
            };
            for (var i = 0; i < 1000; i++)
            {
                var x = r.Next(1000);
                var y = r.Next(1000);

                if (_testMap.Any(position => position.X == x && position.Y == y))
                    i += 1;
                else
                    _testBornMap.Add(new Position { X = x, Y = y });
            }
        }

        private void UpdateConfiguration()
        {
            _backColorBrush = new SolidColorBrush(RenderTarget2D, Configuration.BackColor);
            _liveColorBrush = new SolidColorBrush(RenderTarget2D, Configuration.LiveColor);
            _bornColorBrush = new SolidColorBrush(RenderTarget2D, Configuration.BornColor);
            _deadColorBrush = new SolidColorBrush(RenderTarget2D, Configuration.DeadColor);
        }


        protected override void Draw(Clock time)
        {
            base.Draw(time);

            //RenderTarget2D.Clear(new RawColor4(1F, 01F, 01F, 10));
            RenderTarget2D.FillRectangle(new RawRectangleF(0, 0, Configuration.Width, Configuration.Height), _backColorBrush);

            foreach (var position in _testMap)
            {
                DrawLiveCellAt(position.X, position.Y);
            }
            foreach (var position in _testBornMap)
            {
                DrawBornCellAt(position.X, position.Y);
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
