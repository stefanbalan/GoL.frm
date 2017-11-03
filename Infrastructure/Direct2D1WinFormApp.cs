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

using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Factory = SharpDX.Direct2D1.Factory;

namespace GoL.Infrastructure
{
    public class Direct2D1WinFormApp : App
    {
        public Factory Factory2D { get; private set; }
        public SharpDX.DirectWrite.Factory FactoryDWrite { get; private set; }
        public WindowRenderTarget RenderTarget2D { get; private set; }
        public SolidColorBrush SceneColorBrush { get; private set; }


        public Direct2D1WinFormApp(Configuration configuration) : base(configuration)
        {
        }

        protected override void Initialize(Configuration configuration)
        {
            Factory2D = new Factory();
            FactoryDWrite = new SharpDX.DirectWrite.Factory();

            var properties = new HwndRenderTargetProperties
            {
                Hwnd = DisplayHandle,
                PixelSize = new Size2(configuration.Width, configuration.Height),
                PresentOptions = PresentOptions.None
            };

            RenderTarget2D = new WindowRenderTarget(Factory2D,
                new RenderTargetProperties(new PixelFormat(Format.Unknown, AlphaMode.Premultiplied)), properties)
            {
                AntialiasMode = AntialiasMode.PerPrimitive
            };

            SceneColorBrush = new SolidColorBrush(RenderTarget2D, Color.White);
        }

        protected override void BeginDraw()
        {
            base.BeginDraw();
            RenderTarget2D.BeginDraw();
        }

        protected override void EndDraw()
        {
            RenderTarget2D.EndDraw();
            base.EndDraw();
        }

        protected override void WindowResize(int width, int height)
        {
            base.WindowResize(width, height);

            //RenderTarget2D.Resize(new Size2(width, height));
        }

    }
}
