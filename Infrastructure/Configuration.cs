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
using SharpDX;

namespace GoL.frm.Infrastructure
{
    public class Configuration
    {
        private Color _backColor;
        public Configuration() : this("...") { }
        public Configuration(string title) : this(title, 800, 600) { }

        public Configuration(string title, int width, int height)
        {
            Title = title; Width = width; Height = height; WaitVerticalBlanking = false;

            BackColor = Color.FromAbgr(0x333333FF);
            LiveColor = Color.FromAbgr(0xf99d1cFF);
            BornColor = Color.FromAbgr(0xfdb913FF);
            DeadColor = Color.FromAbgr(0x000000FF);
        }
        public string Title { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool WaitVerticalBlanking { get; set; }

        public Color BackColor
        {
            get => _backColor;
            set
            {
                _backColor = value;
                OnChange?.Invoke();
            }
        }

        public Color LiveColor { get; set; }
        public Color BornColor { get; set; }
        public Color DeadColor { get; set; }

        public event Action OnChange;
    }
}