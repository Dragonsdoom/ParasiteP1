using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParasiteP1.Utility;
//using System.Diagnostics;

namespace ParasiteP1
{

    public class Retractor
    {
        Vector2 position;
        
        public Vector2 Position { get { return position; } set { position = value;} }
        public Vector2 velocity = new Vector2 (0,-3);
        private Texture2D tex = ContentStorageManager.Get<Texture2D>("Bullet00");
        public bool isDying = false;
        List<ICollidable> coll;

        public Retractor(Vector2 p)
        {
            position = p;
        }

        public virtual void Update()
        {
            position += velocity;

            short newGrid = (short)((int)(position.X / 100) * 8 + (int)(position.Y / 100));
            coll = CollisionManager.BadGuys[newGrid];
            foreach (Enemy item in coll)
            {
                if ((item.Position - position).LengthSquared() < 2000 && !item.isDying)
                {
                    Parasite.Me.UseNewShip(item);

                    // choose type of bullet
                    if (item is OrbitEnemy)
                    {
                        Parasite.Me.controlShip.bulletTypeFlag = 2;
                    }
                    else if (item is SweepEnemy)
                    {
                        Parasite.Me.controlShip.bulletTypeFlag = 1;
                    }
                    else
                    {
                        Parasite.Me.controlShip.bulletTypeFlag = 0;
                    }

                    isDying = true;
                }
            }
        
            if (isDying || position.Y<-20)
            Parasite.Me.R = null;
           
        }

     
        public void Draw(SpriteBatch sb)
        {
            if (isDying) return;
            sb.Draw(tex,position,Color.White);
           
        }
    }
}
