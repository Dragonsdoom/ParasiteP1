using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ParasiteP1.Utility;
using Microsoft.Xna.Framework.Graphics;

namespace ParasiteP1
{
    public class Parasite : ICollidable
    {
        Vector2 position;
        public Vector2 Position { get { return position; } set { position = value; s.position = value; } }
        public Vector2 velocity;
        public Retractor R;
        public Vector2 targetPoint;
        public static Parasite Me;
        public int bodyTimer = 900;
        public bool isDetached = false;
        public bool IsDying = false;
        public bool isDying { get { return IsDying; } set { IsDying = value; } }
        Sprite s;
        InputController ic = ContentStorageManager.Get<InputController>("input");
        public ControllableShip controlShip;
        public short OldGrid { get { return oldGrid; } set { oldGrid = value; } }
        protected short oldGrid = -1;
        public bool team { get { return false; } set { ;} }
        public List<ICollidable> coll;

        public Parasite(Vector2 position, Vector2 velocity)
        {
            this.velocity = velocity;
            s = new Sprite(ContentStorageManager.Get<Texture2D>("Parasite"));
            s.CenterOriginOnTexture();
            s.Scale = 0.5f;
            s.position = position;
            s.Color = Color.Red;
            Me = this;
        }

        public void Update()
        {
            if (R != null) R.Update();
            if (ic.Detach)
            {
                Position = controlShip.Position;
                controlShip.Die();
                isDetached = true;
                AudioManager.QueueSound("explosion", false);
            }

            if (isDetached)
            {
                if (isDying)
                {
                    GUIVariables.lives--;
                    Position = new Vector2(Game1.ScreenSize.X / 2, Game1.ScreenSize.Y-100);
                }
                CheckMoveInput(ic);
                if (ic.Fire)
                {
                    ChooseNewShip();
                }
            }
            else
            {
                //Step to point behind ship
                targetPoint = new Vector2(controlShip.Position.X, controlShip.Position.Y + 30);
                Position += (targetPoint - Position) * 1 / 60 * 6;
                
                /*
                if (bodyTimer < 0) //change later
                {
                    Position = controlShip.Position;
                    controlShip.Die();
                    bodyTimer = 900;
                    isDetached = true;
                    //AudioManager.QueueSound("explosion", false);
                }
                bodyTimer--;
                */ 
                
            }
        }

        private void ChooseNewShip()
        {
            if (R == null) 
            R=new Retractor(Position);
        }
        public void UseNewShip(Enemy closest)
        {
            bodyTimer = 900;
            //// TODO: Remove enemy
            controlShip.isDying = false;
            controlShip.s.texture = closest.sprite.texture;
            controlShip.s.Color = Color.Red;
            controlShip.isInvuln = 180;
            isDying = false;
            controlShip.Position =Position;
            //controlShip.lastFired = 0;
            //controlShip.bulletType.Clear();
            isDetached = false;
        }

        public void Die()
        {
            ;
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
            Position += tempVect * velocity;
        }

        public void Draw(SpriteBatch sb)
        {
            if (R != null) R.Draw(sb);
            s.Draw(sb);
        }
    }
}
