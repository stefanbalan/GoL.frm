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

using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Device = SharpDX.Direct3D11.Device;
using Resource = SharpDX.Direct3D11.Resource;

namespace GoL.frm.Infrastructure
{
    public class Direct3D11App : App
    {
        protected Device Device;
        protected SwapChain SwapChain;
        protected Texture2D BackBuffer;
        protected RenderTargetView RenderView;




        protected override void Initialize(Configuration configuration)
        {
            var desc = new SwapChainDescription
            {
                BufferCount = 1,
                //ModeDescription = new ModeDescription(configuration.Width, configuration.Height, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                ModeDescription = new ModeDescription(0, 0, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                IsWindowed = true,
                OutputHandle = DisplayHandle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput | Usage.ShaderInput,
                Flags = SwapChainFlags.None
            };

            //SharpDX.Direct3D11.Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.BgraSupport, new[] { FeatureLevel.Level_10_0 }, desc, out _device, out _swapChain);
            // above line is expanded to...
            Device = new Device(DriverType.Hardware, DeviceCreationFlags.BgraSupport | DeviceCreationFlags.Debug, FeatureLevel.Level_10_0);
            using (Factory1 factory1 = new Factory1())
                SwapChain = new SwapChain(factory1, Device, desc);
            // ...this!

            var factory = SwapChain.GetParent<Factory>();
            factory.MakeWindowAssociation(DisplayHandle, WindowAssociationFlags.IgnoreAll);

            BackBuffer = Resource.FromSwapChain<Texture2D>(SwapChain, 0);
            RenderView = new RenderTargetView(Device, BackBuffer);
        }

        protected override void BeginDraw()
        {
            base.BeginDraw();
            Device.ImmediateContext.Rasterizer.SetViewport(0, 0, Configuration.Width, Configuration.Height);
            Device.ImmediateContext.OutputMerger.SetTargets(RenderView);
        }

        protected override void EndDraw()
        {
            SwapChain.Present(Configuration.WaitVerticalBlanking ? 1 : 0, PresentFlags.None);
        }

    }
}