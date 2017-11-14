using System;
using System.Collections.Generic;

namespace GoLife
{
    public interface IWorld : IEnumerable<Cell>, ICloneable
    {
        bool this[int x, int y] { get; set; }

        IWorld Add(IWorld toAdd);
        IWorld Remove(IWorld toRemove);
    }
}