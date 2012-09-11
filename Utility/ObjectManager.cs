using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParasiteP1
{
    public static class ObjectManager
    {
        public static List<Bullet> Bullets = new List<Bullet>();

        public static void Update()
        {
            UpdateBullets();
        }
        public static void AddBullet(Bullet bu1,bool team)
        {
            bu1.team = team;
            if (!Bullets.Contains(bu1))
                Bullets.Add(bu1);
        }
        public static void RemoveBullet(Bullet bu1)
        {
            if (Bullets.Contains(bu1))
                Bullets.Remove(bu1);
        }
        public static void UpdateBullets()
        {
            for (int i = 0; i < Bullets.Count; i++)
            {
                if (Bullets[i].isDying)
                {
                    Bullets.RemoveAt(i);
                    continue;
                }
                if (!Bullets[i].team)
                if ((Bullets[i].Position.X - ControllableShip.Me.Position.X) < 50 && (Bullets[i].Position.Y - ControllableShip.Me.Position.Y) < 50)
                {
                    if (ControllableShip.Me.isInvuln < 1)
                    {
                        ControllableShip.Me.isDying = true;
                        Parasite.Me.isDetached = true;
                    }
                    Bullets[i].isDying=true;
                    Bullets.RemoveAt(i);
                    continue;
                }
                Bullets[i].Update();
            }
        }

        public static void Draw(SpriteBatch sb)
        {
            DrawBullets(sb);
        }

        public static void DrawBullets(SpriteBatch sb)
        {
            for (int i = 0; i < Bullets.Count; i++)
            {
                
                if (Bullets[i].isDying)
                {
                    Bullets.RemoveAt(i);
                    continue;
                }
                
                Bullets[i].Draw(sb);
            }
        }
    }
}
