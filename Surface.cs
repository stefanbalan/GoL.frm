using System;
using System.Drawing;
using SharpDX.Windows;
using SharpDX.DXGI;
using D2D1 = SharpDX.Direct2D1;

namespace GoL.frm
{
    public class GameSurface : IDisposable
    {
        private RenderForm renderForm;

        private const int Width = 1280;
        private const int Height = 720;

        private D2D1.Device d2dDevice;
        private D2D1.DeviceContext d2dDeviceContext;
        private SwapChain swapChain;

        public GameSurface()
        {
            renderForm = new RenderForm("My first SharpDX game");
            renderForm.ClientSize = new Size(Width, Height);
            renderForm.AllowUserResizing = false;
        }


        public void Run()
        {
            RenderLoop.Run(renderForm, RenderCallback);
        }

        private void RenderCallback()
        {

        }

        private void InitializeDeviceResources()
        {
            var backBufferDesc = new ModeDescription(Width, Height, new Rational(60, 1), Format.R8G8B8A8_UNorm);

            var swapChainDesc = new SwapChainDescription()
            {
                ModeDescription = backBufferDesc,
                SampleDescription = new SampleDescription(1, 0),
                Usage = Usage.RenderTargetOutput,
                BufferCount = 1,
                OutputHandle = renderForm.Handle,
                IsWindowed = true
            };

            //var device = new D2D1.Device();
            /*.CreateWithSwapChain(DriverType.Hardware, D3D11.DeviceCreationFlags.None, swapChainDesc, out d3dDevice, out swapChain);*/
            //d3dDeviceContext = d3dDevice.ImmediateContext;
        }



        public void Dispose()
        {
            renderForm.Dispose();
        }
    }
}
