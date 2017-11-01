using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GoL.frm.Infrastructure;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using Configuration = GoL.frm.Infrastructure.Configuration;

namespace GoL.frm
{
    public class GameOfLifeGraphicsApp :
        Direct2D1App
    //Direct2D1WinFormApp
    {
        private SolidColorBrush _solidColorBrush;
        private List<Position> _map = new List<Position>();


        protected override void Initialize(Configuration configuration)
        {
            base.Initialize(configuration);

            _solidColorBrush = new SolidColorBrush(RenderTarget2D, Color.White);

            _map = new List<Position>();
            var r = new Random();

            for (int i = 0; i < 10000; i++)
            {
                var x = r.Next(1000);
                var y = r.Next(1000);

                _map.Add(new Position { X = x, Y = y });
            }
        }


        protected override void Draw(Clock time)
        {
            base.Draw(time);

            RenderTarget2D.Clear(new RawColor4(0.1F, 0.1F, 0.1F, 10));
            foreach (var position in _map)
            {
                DrawLiveCellAt(position.X, position.Y);
            }
        }

        private Position _offset;
        private void DrawLiveCellAt(int x, int y)
        {
            const int cellSize = 10;
            RenderTarget2D.FillRoundedRectangle(new RoundedRectangle
            {
                RadiusX = (float)(2 * _scale),
                RadiusY = (float)(2 * _scale),
                Rect = new RawRectangleF(
                    (_offset.X + x * cellSize) * _scale,
                    (_offset.Y + y * cellSize) * _scale,
                    (_offset.X + x * cellSize + cellSize) * _scale,
                    (_offset.Y + y * cellSize + cellSize) * _scale
                    )
            }, _solidColorBrush
            );
        }

        private float _scale = 1;
        protected override void MouseWheel(MouseEventArgs e)
        {
            base.MouseWheel(e);
            var zoomDelta = e.Delta / 1200F; // 0.1 increments

            var newval = _scale + zoomDelta;
            if (newval >= 0.1 && newval < 5)
            {
                var mousePosRatioX = (float)e.X / Configuration.Width;
                var mousePosRatioY = (float)e.Y / Configuration.Height;


                _offset.X -= (int)(zoomDelta * (_offset.X + e.X / _scale) * mousePosRatioX);
                _offset.Y -= (int)(zoomDelta * (_offset.Y + e.Y / _scale) * mousePosRatioY);

                _scale = newval;
            }
        }



        private bool _dragging;
        private Position _dragStartMousePosition;
        private Position _dragStartOffset;

        protected override void MouseDown(MouseEventArgs e)
        {
            _dragStartMousePosition = new Position { X = e.X, Y = e.Y };
            _dragStartOffset = _offset;
            _dragging = true;
        }

        protected override void MouseUp(MouseEventArgs e)
        {
            _dragStartMousePosition = new Position { X = e.X, Y = e.Y };
            _dragStartOffset = _offset;
            _dragging = false;
        }

        protected override void MouseMove(MouseEventArgs e)
        {
            if (!_dragging) return;

            _offset.X = _dragStartOffset.X + (int)((e.X - _dragStartMousePosition.X) / _scale);
            _offset.Y = _dragStartOffset.Y + (int)((e.Y - _dragStartMousePosition.Y) / _scale);
        }
    }
}
