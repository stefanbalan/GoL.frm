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
                RadiusX = 2,
                RadiusY = 2,
                Rect = new RawRectangleF(
                    _offset.X + x * cellSize,
                    _offset.Y + y * cellSize,
                    _offset.X + x * cellSize + cellSize,
                    _offset.Y + y * cellSize + cellSize
                    )
            }, _solidColorBrush
            );
        }

        private bool _dragging;
        private Position _dragStartPosition;
        private Position _dragStartOffset;
        protected override void MouseDown(MouseEventArgs e)
        {
            _dragStartPosition = new Position { X = e.X, Y = e.Y };
            _dragStartOffset = _offset;
            _dragging = true;
        }

        protected override void MouseUp(MouseEventArgs e)
        {
            _dragStartPosition = new Position { X = e.X, Y = e.Y };
            _dragStartOffset = _offset;
            _dragging = false;
        }

        protected override void MouseMove(MouseEventArgs e)
        {
            if (!_dragging) return;

            _offset = new Position
            {
                X = _dragStartOffset.X + (e.X - _dragStartPosition.X),
                Y = _dragStartOffset.Y + (e.Y - _dragStartPosition.Y)
            };
        }
    }
}
