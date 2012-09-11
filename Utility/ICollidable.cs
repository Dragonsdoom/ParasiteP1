using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ParasiteP1
{
    public interface ICollidable
    {
        short OldGrid{get;set;}
        Vector2 Position {get;set;}
        bool team { get; set; }
        void Die();
        bool isDying { get; set; }


    }
}
