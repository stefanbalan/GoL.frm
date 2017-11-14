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
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Factory = SharpDX.Direct2D1.Factory;
using Resource = SharpDX.Direct3D11.Resource;

namespace GoLife.Infrastructure
{
    public class Direct2D1App : Direct3D11App
    {
        public Factory Factory2D { get; private set; }
        public SharpDX.DirectWrite.Factory FactoryDWrite { get; private set; }
        public SolidColorBrush SceneColorBrush { get; private set; }
        private Surface _surface;
        public RenderTarget RenderTarget2D => _renderTarget2D;
        private RenderTarget _renderTarget2D;


        public Direct2D1App(Configuration configuration) : base(configuration)
        {
        }

        protected override void Initialize(Configuration configuration)
        {
            base.Initialize(configuration);
            Factory2D = new Factory();
            using (var surface = BackBuffer.QueryInterface<Surface>())
            {
                _renderTarget2D = new RenderTarget(Factory2D, surface, new RenderTargetProperties(new PixelFormat(Format.Unknown, AlphaMode.Premultiplied)));
            }
            _renderTarget2D.AntialiasMode = AntialiasMode.PerPrimitive;

            FactoryDWrite = new SharpDX.DirectWrite.Factory();

            SceneColorBrush = new SolidColorBrush(_renderTarget2D, Color.White);
        }

        protected override void BeginDraw()
        {
            base.BeginDraw();
            _renderTarget2D.BeginDraw();
        }

        protected override void EndDraw()
        {
            _renderTarget2D.EndDraw();
            base.EndDraw();
        }

        protected override void WindowResize(int width, int height)
        {
            base.WindowResize(width, height);


            Device.ImmediateContext.ClearState();

            _renderTarget2D.Dispose();
            BackBuffer.Dispose();
            RenderView.Dispose();
            _surface?.Dispose();

            SwapChain.ResizeBuffers(1, 0, 0, Format.R8G8B8A8_UNorm, SwapChainFlags.None);

            BackBuffer = Resource.FromSwapChain<Texture2D>(SwapChain, 0);
            RenderView = new RenderTargetView(Device, BackBuffer);
            _surface = BackBuffer.QueryInterface<Surface>();
            _renderTarget2D = new RenderTarget(Factory2D, _surface,
                new RenderTargetProperties(new PixelFormat(Format.R8G8B8A8_UNorm, AlphaMode.Premultiplied)));

        }


    }
}
