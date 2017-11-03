using System;
using System.Collections.Generic;

namespace GoL
{
    public interface IWorld : IEnumerable<Position>, ICloneable
    {
        bool this[int x, int y] { get; set; }
    }
}