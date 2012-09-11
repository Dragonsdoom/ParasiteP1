using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
//using System.Diagnostics;


namespace ParasiteP1
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public static class CollisionManager
    {
        public static List<ICollidable>[] GoodGuys = new List<ICollidable>[64];
        public static List<ICollidable>[] BadGuys  = new List<ICollidable>[64];

        public static void PopulateArray()
        {
            for (int i = 0; i < 64; i++)
            {
                GoodGuys[i] = new List<ICollidable>();
                BadGuys[i] = new List<ICollidable>();

            }
        }

        public static void DeleteMe(ICollidable Obj)
        {
            if (Obj.OldGrid > -1 && GoodGuys[Obj.OldGrid].Contains(Obj)) GoodGuys[Obj.OldGrid].Remove(Obj);
            if (Obj.OldGrid > -1 && BadGuys[Obj.OldGrid].Contains(Obj)) BadGuys[Obj.OldGrid].Remove(Obj);
        }

        public static List<ICollidable> GoodMoved(ICollidable Obj)
        {
            short newGrid = (short)((int)(Obj.Position.X / 100) * 8 + (int)(Obj.Position.Y / 100));
            if (newGrid > 63 || newGrid < 0) return new List<ICollidable>();
            if (newGrid != Obj.OldGrid && Obj.OldGrid > -1 && GoodGuys[Obj.OldGrid].Contains(Obj)) GoodGuys[Obj.OldGrid].Remove(Obj);
            GoodGuys[newGrid].Add(Obj);
            int tmpX = newGrid / 8 * 100; int tmpY = newGrid % 8*100;
            
            return BadGuys[newGrid];
        }
        public static List<ICollidable> BadMoved(Enemy Obj)
        {
            if(Obj.isDying && BadGuys[Obj.OldGrid].Contains(Obj)) BadGuys[Obj.OldGrid].Remove(Obj);
            short newGrid = (short)((int)(Obj.Position.X / 100) * 8 + (int)(Obj.Position.Y / 100));
            if (newGrid > 63 || newGrid < 0) return new List<ICollidable>();
            if (newGrid != Obj.OldGrid && Obj.OldGrid > -1 && BadGuys[Obj.OldGrid].Contains(Obj)) BadGuys[Obj.OldGrid].Remove(Obj);
            BadGuys[newGrid].Add(Obj);
            //Debug.Print("Hi");
            return GoodGuys[newGrid];
        }
               
       
        
    }
}
