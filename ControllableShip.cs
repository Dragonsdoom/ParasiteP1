using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using ParasiteP1.Utility;
using Microsoft.Xna.Framework.Graphics;

namespace ParasiteP1
{
    public class ControllableShip : ICollidable
    {
        public Vector2 velocity;
        public Sprite s;
        public static ControllableShip Me;
        public bool IsDying = false;
        public bool isDying { get { return IsDying; } set { IsDying = value; } }
        public int isInvuln = 0;
        public Vector2 Position { get { return s.position; } set { s.position = value; } }
        public short OldGrid { get { return oldGrid; } set { oldGrid = value; } }
        protected short oldGrid = -1;
        public bool team { get { return false; } set { ;} }
        public List<ICollidable> coll;
        public int bulletTypeFlag = 0;
        public List<Bullet> bulletType = new List<Bullet>();
        public uint fireDelay = 5;
        public uint lastFired = 0;
        InputController ic;
        public ControllableShip(Vector2 position, Vector2 velocity)
        {
            Me = this;
            this.velocity = velocity;
            s = new Sprite(ContentStorageManager.Get<Texture2D>("Ship01"));
            s.CenterOriginOnTexture();
            s.position = position;
            ic = ContentStorageManager.Get<InputController>("input");

        }

        public void Update()
        {
            if (isDying)
            {
                if (isInvuln > 0)
                {
                    isInvuln--;
                }
                else return;
            }

            CheckMoveInput(ic);
            CheckFireInput(ic);
            CheckScreenBounds();
            if (isInvuln < 1)
            {
                coll = CollisionManager.GoodMoved(this);
            }
        }

        public void Draw(SpriteBatch sb)
        {
            if (isDying)
            {
                if (isInvuln > 0)
                {
                    isDying = false;
                    isInvuln--;
                }
                else return;
            }

            s.Draw(sb);
        }

        private void CheckScreenBounds()
        {
            if (s.position.X < (s.GetTextureSize().X / 2))
            {
                s.position.X = s.GetTextureSize().X / 2 + 1;
            }
            else if (s.position.X > Game1.ScreenSize.X - (s.GetTextureSize().X / 2))
            {
                s.position.X = Game1.ScreenSize.X - (s.GetTextureSize().X / 2);
            }

            if (s.position.Y > Game1.ScreenSize.Y - (s.GetTextureSize().Y / 2))
            {
                s.position.Y = Game1.ScreenSize.Y - (s.GetTextureSize().Y / 2);
            }
            else if (s.position.Y < (s.GetTextureSize().Y / 2))
            {
                s.position.Y = s.GetTextureSize().Y / 2 + 1;
            }
        }
        public void Die()
        {
            isDying = (isInvuln < 1);
        }
        private void CheckMoveInput(InputController ic)
        {
            Vector2 tempVect = new Vector2(0, 0);

            if (ic.UpDown)
            {
                tempVect.Y -= velocity.Y;
            }
            else if (ic.DownDown)
            {
                tempVect.Y += velocity.Y;
            }

            if (ic.LeftDown)
            {
                tempVect.X -= velocity.X;
            }
            else if (ic.RightDown)
            {
                tempVect.X += velocity.X;
            }

            if (tempVect.LengthSquared() != 0)
                tempVect.Normalize();
            s.position += tempVect * velocity;
        }

        private void CheckFireInput(InputController ic)
        {
            if (isDying) return;
            if (ic.FireDown && lastFired > fireDelay)
            {
                lastFired = 0;
                
                if (bulletTypeFlag == 3)
                {
                    bulletType.Add(new SinBullet(Position, new Vector2(0, -12), 0, null));
                    bulletType.Add(new SinBullet(Position, new Vector2(0, -12), 3.14f, null));
                }
                else if (bulletTypeFlag == 2)
                {
                    bulletType.Add(new SprayBullet(Position, new Vector2(0, -12), 4, null));
                }
                else
                {
                    bulletType.Add(new StraitBullet(Position, new Vector2(0, -12)));
                }
                //bulletType.Add(new StraitBullet(Position, new Vector2(0, -12)));
                //AudioManager.QueueSound("lasershoot", false);
                foreach (Bullet b in bulletType)
                {
                    ObjectManager.AddBullet(b, true);
                }
            }
            lastFired++;
        }
    }
}
