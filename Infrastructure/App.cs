// Copyright (c) 2010-2013 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Windows.Forms;
using SharpDX.Windows;

namespace GoL.frm.Infrastructure
{
    public abstract class App : IDisposable
    {
        private readonly Clock _clock = new Clock();
        private FormWindowState _currentFormWindowState;
        private bool _disposed;
        private Form _form;
        private float _frameAccumulator;
        private int _frameCount;

        ~App()
        {
            if (!_disposed)
            {
                Dispose(false);
                _disposed = true;
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                Dispose(true);
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposeManagedResources)
        {
            if (disposeManagedResources)
                _form?.Dispose();
        }

        protected IntPtr DisplayHandle => _form.Handle;

        public Configuration Configuration { get; private set; }
        public float FrameDelta { get; private set; }
        public float FramePerSecond { get; private set; }

        protected virtual Form CreateForm(Configuration config)
        {
            return new RenderForm(config.Title)
            {
                ClientSize = new System.Drawing.Size(config.Width, config.Height)
            };
        }

        public void Run()
        {
            Run(new Configuration());
        }

        public void Run(Configuration configuration)
        {
            Configuration = configuration ?? new Configuration();
            _form = CreateForm(Configuration);
            Initialize(Configuration);

            var isFormClosed = false;
            var formIsResizing = false;

            _form.MouseDown += HandleMouseDown;
            _form.MouseUp += HandleMouseUp;
            _form.MouseMove += HandleMouseMove;
            _form.MouseClick += HandleMouseClick;
            _form.MouseWheel += HandleMouseWheel;
            _form.KeyDown += HandleKeyDown;
            _form.KeyUp += HandleKeyUp;
            _form.Resize += (o, args) =>
            {
                if (_form.WindowState != _currentFormWindowState)
                {
                    HandleResize(o, args);
                }

                _currentFormWindowState = _form.WindowState;
            };

            _form.ResizeBegin += (o, args) => { formIsResizing = true; };
            _form.ResizeEnd += (o, args) =>
            {
                formIsResizing = false;
                HandleResize(o, args);
            };

            _form.Closed += (o, args) => { isFormClosed = true; };

            LoadContent();

            _clock.Start();
            BeginRun();
            RenderLoop.Run(_form, () =>
            {
                if (isFormClosed)
                {
                    return;
                }

                OnUpdate();
                if (!formIsResizing)
                    Render();
            });

            UnloadContent();
            EndRun();

            Dispose();
        }

        protected abstract void Initialize(Configuration configuration);

        protected virtual void LoadContent()
        {
        }

        protected virtual void UnloadContent()
        {
        }

        protected virtual void Update(Clock time)
        {
        }

        protected virtual void Draw(Clock time)
        {
        }

        protected virtual void BeginRun()
        {
        }

        protected virtual void EndRun()
        {
        }

        protected virtual void BeginDraw()
        {
        }

        protected virtual void EndDraw()
        {
        }

        public void Exit()
        {
            _form.Close();
        }

        private void OnUpdate()
        {
            FrameDelta = (float)_clock.Update();
            Update(_clock);
        }

        protected System.Drawing.Size RenderingSize => _form.ClientSize;

        private void Render()
        {
            _frameAccumulator += FrameDelta;
            ++_frameCount;
            if (_frameAccumulator >= 1.0f)
            {
                FramePerSecond = _frameCount / _frameAccumulator;

                _form.Text = Configuration.Title + @" - FPS: " + FramePerSecond;
                _frameAccumulator = 0.0f;
                _frameCount = 0;
            }

            BeginDraw();
            Draw(_clock);
            EndDraw();
        }

        #region window events
        protected virtual void MouseWheel(MouseEventArgs e)
        {
        }

        protected virtual void MouseClick(MouseEventArgs e)
        {
        }

        protected virtual void MouseDown(MouseEventArgs e)
        {
        }

        protected virtual void MouseUp(MouseEventArgs e)
        {
        }

        protected virtual void MouseMove(MouseEventArgs e)
        {
        }

        protected virtual void KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Exit();
        }

        protected virtual void KeyUp(KeyEventArgs e)
        {
        }

        protected virtual void WindowResize(int width, int height)
        {
            Configuration.Width = width;
            Configuration.Height = height;
        }

        private void HandleMouseWheel(object sender, MouseEventArgs mouseEventArgs)
        {
            MouseWheel(mouseEventArgs);
        }

        private void HandleMouseClick(object sender, MouseEventArgs e)
        {
            MouseClick(e);
        }

        private void HandleMouseDown(object sender, MouseEventArgs e)
        {
            MouseDown(e);
        }

        private void HandleMouseUp(object sender, MouseEventArgs e)
        {
            MouseUp(e);
        }

        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            MouseMove(e);
        }

        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);
        }

        private void HandleKeyUp(object sender, KeyEventArgs e)
        {
            KeyUp(e);
        }

        private void HandleResize(object sender, EventArgs e)
        {
            if (_form.WindowState == FormWindowState.Minimized)
            {
                return;
            }
            WindowResize(((Control)sender).ClientSize.Width, ((Control)sender).ClientSize.Height);
        }
        #endregion
    }
}