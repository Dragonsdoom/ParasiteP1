using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;


namespace ParasiteP1.Utility
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class InputController
    {
        private KeyboardState OldKeyState = new KeyboardState();
        private MouseState OldMouse = new MouseState();
        private TouchLocation OldTl = new TouchLocation();
        private GamePadState OldGp = new GamePadState();

        // Set to false to disable mouse input.
        public bool UseMouseNav = true;
        // Mouse Change
        private int dx = 0;
        private int dy = 0;

        //Key Down
        public bool FireDown = false;
        public bool UpDown = false;
        public bool DownDown = false;
        public bool LeftDown = false;
        public bool RightDown = false;
        public bool AttachDown = false;
        public bool DetachDown = false;
        public bool EscDown = false;

        //Key Released i.e. 
        public bool Fire = false;
        public bool Up = false;
        public bool Down = false;
        public bool Left = false;
        public bool Right = false;
        public bool UpMouse = false;
        public bool DownMouse = false;
        public bool LeftMouse = false;
        public bool RightMouse = false;
        public bool Attach = false;
        public bool Detach = false;
        public bool Esc = false;

        // Default Key Bindings
        public Keys FireKey = Keys.Space;
        public Keys AltFireKey = Keys.Space;

        public Keys AttachKey = Keys.E;
        public Keys AltAttachKey = Keys.E;
        public Keys DetachKey = Keys.P;
        public Keys AltDetachKey = Keys.P;

        public Keys UpKey = Keys.Up;
        public Keys DownKey = Keys.Down;
        public Keys LeftKey = Keys.Left;
        public Keys RightKey = Keys.Right;

        public Keys AltUpKey = Keys.W;
        public Keys AltDownKey = Keys.S;
        public Keys AltLeftKey = Keys.A;
        public Keys AltRightKey = Keys.D;

        public Keys EscKey = Keys.Escape;

        //temp
        SpriteFont sf;
        //temp
        public InputController(Game game)
        {
            TouchPanel.EnabledGestures = GestureType.HorizontalDrag | GestureType.VerticalDrag | GestureType.PinchComplete | GestureType.DoubleTap;
            CollisionManager.PopulateArray();
            KeyboardState ks = Keyboard.GetState();
            MouseState ms = Mouse.GetState();
            GamePadState gp = GamePad.GetState(0);
            TouchCollection tlc = TouchPanel.GetState();
            TouchLocation tl = (tlc.Count > 0) ? tlc[0] : OldTl;
            OldMouse = ms;
            OldKeyState = ks;
            OldTl = tl;
            OldGp = gp;
            //sf = game.Content.Load<SpriteFont>("Arial");
            // TODO: Construct any child components here
        }

        public byte GuiMouseClick(Rectangle CheckRect)
        {
            byte retVal = 0;

            if (CheckRect.Intersects(new Rectangle(OldMouse.X, OldMouse.Y, 20, 20)) || CheckRect.Intersects(new Rectangle((int)OldTl.Position.X - 10, (int)OldTl.Position.Y - 10, 30, 30)))
            {
                retVal += 1;
                if (Fire)
                    retVal += 1;

            }

            if (OldGp.Buttons.Start == ButtonState.Pressed || OldGp.Buttons.A == ButtonState.Pressed) retVal = 2;
            return retVal;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update()
        {


            KeyboardState ks = Keyboard.GetState();
            MouseState ms = Mouse.GetState();
            GamePadState gp = GamePad.GetState(0);
            TouchCollection tlc = TouchPanel.GetState();
            TouchLocation tl = (tlc.Count > 0) ? tlc[0] : OldTl;

            FireDown = (ks.IsKeyDown(FireKey) || ks.IsKeyDown(AltFireKey) || (ms.LeftButton == ButtonState.Pressed) || gp.IsButtonDown(Buttons.A));
            Fire = ((ks.IsKeyDown(FireKey) && !OldKeyState.IsKeyDown(FireKey)) || (ks.IsKeyDown(AltFireKey) && !OldKeyState.IsKeyDown(AltFireKey)) || (OldMouse.LeftButton != ButtonState.Pressed && ms.LeftButton == ButtonState.Pressed) || (!OldGp.IsButtonDown(Buttons.A) && gp.IsButtonDown(Buttons.A)));


            if (gp.IsButtonDown(Buttons.B)) 



            AttachDown = (ks.IsKeyDown(AttachKey) || ks.IsKeyDown(AltAttachKey) || (ms.RightButton == ButtonState.Pressed));
            Attach = ((ks.IsKeyDown(AttachKey) && !OldKeyState.IsKeyDown(AttachKey)) || (ks.IsKeyDown(AltAttachKey) && !OldKeyState.IsKeyDown(AltAttachKey)) || (OldMouse.RightButton != ButtonState.Pressed && ms.RightButton == ButtonState.Pressed));

            UpDown = (ks.IsKeyDown(UpKey) || ks.IsKeyDown(AltUpKey) || gp.ThumbSticks.Left.Y > .90 && gp.ThumbSticks.Left.Y < OldGp.ThumbSticks.Left.Y);
            Up = ((ks.IsKeyDown(UpKey) && !OldKeyState.IsKeyDown(UpKey)) || (ks.IsKeyDown(AltUpKey) && !OldKeyState.IsKeyDown(AltUpKey)) || (gp.ThumbSticks.Left.Y > .80 && gp.ThumbSticks.Left.Y < OldGp.ThumbSticks.Left.Y));

            DownDown = (ks.IsKeyDown(DownKey) || ks.IsKeyDown(AltDownKey) || (gp.ThumbSticks.Left.Y < -.90 && gp.ThumbSticks.Left.Y < OldGp.ThumbSticks.Left.Y));
            Down = ((ks.IsKeyDown(DownKey) && !OldKeyState.IsKeyDown(DownKey)) || (ks.IsKeyDown(AltDownKey) && !OldKeyState.IsKeyDown(AltDownKey))|| (gp.ThumbSticks.Left.Y < -.90 && gp.ThumbSticks.Left.Y < OldGp.ThumbSticks.Left.Y));

            LeftDown = (ks.IsKeyDown(LeftKey) || ks.IsKeyDown(AltLeftKey) || (gp.ThumbSticks.Left.X < -.90 && gp.ThumbSticks.Left.X < OldGp.ThumbSticks.Left.X));
            Left = ((ks.IsKeyDown(LeftKey) && !OldKeyState.IsKeyDown(LeftKey)) || (ks.IsKeyDown(AltLeftKey) && !OldKeyState.IsKeyDown(AltLeftKey)) || (gp.ThumbSticks.Left.X < -.90 && gp.ThumbSticks.Left.X < OldGp.ThumbSticks.Left.X));


            RightDown = (ks.IsKeyDown(RightKey) || ks.IsKeyDown(AltRightKey) || (gp.ThumbSticks.Left.X > .90 && gp.ThumbSticks.Left.X > OldGp.ThumbSticks.Left.X));
            Right = ((ks.IsKeyDown(RightKey) && !OldKeyState.IsKeyDown(RightKey)) || (ks.IsKeyDown(AltRightKey) && !OldKeyState.IsKeyDown(AltRightKey)));


            DetachDown = (ks.IsKeyDown(DetachKey) || ks.IsKeyDown(AltDetachKey));
            Detach = ((ks.IsKeyDown(DetachKey) && !OldKeyState.IsKeyDown(DetachKey)) || (ks.IsKeyDown(AltDetachKey) && !OldKeyState.IsKeyDown(AltDetachKey)));

            EscDown = (ks.IsKeyDown(EscKey) || gp.IsButtonDown(Buttons.Back));
            Esc = ((ks.IsKeyDown(EscKey) && !OldKeyState.IsKeyDown(EscKey)));

            if (TouchPanel.IsGestureAvailable)
            {
                GestureSample gs = TouchPanel.ReadGesture();
                switch (gs.GestureType)
                {
                    case GestureType.DoubleTap:
                        Fire = true;
                        break;
                    case GestureType.DragComplete:
                        break;
                    case GestureType.Flick:
                        break;
                    case GestureType.FreeDrag:
                        break;
                    case GestureType.Hold:
                        break;
                    case GestureType.HorizontalDrag:
                        if (gs.Delta.X < gs.Delta2.X) LeftDown = true; else RightDown = true;
                        break;
                    case GestureType.None:
                        break;
                    case GestureType.Pinch:
                        DetachDown = true;
                        break;
                    case GestureType.PinchComplete:
                        break;
                    case GestureType.Tap:

                        break;
                    case GestureType.VerticalDrag:
                        if (gs.Delta.Y < gs.Delta2.Y) UpDown = true; else DownDown = true;
                        break;
                    default:
                        break;
                }
            }
            if (UseMouseNav)
            {
                //Saved just in case :)
                //if (ms.X - OldMouse.X > 5) Right = true;
                //if (ms.X - OldMouse.X < -5) Left = true;

                if (ms.Y - OldMouse.Y > 5) DownMouse = true;
                if (ms.Y - OldMouse.Y < -5) UpMouse = true;
                if (ms.X - OldMouse.X > 5) RightMouse = true;
                if (ms.X - OldMouse.X < -5) LeftMouse = true;

            }
            OldTl = tl;
            OldMouse = ms;
            OldKeyState = ks;
            OldGp = gp;
        }
        //public void Draw(SpriteBatch sb)
        //{

        //    sb.DrawString(sf, "UP:" + Up + ", " + UpDown, new Vector2(10, 10), Color.White);
        //    sb.DrawString(sf, "Down:" + Down + ", " + DownDown, new Vector2(10, 40), Color.White);
        //    sb.DrawString(sf, "Left:" + Left + ", " + LeftDown, new Vector2(10, 70), Color.White);
        //    sb.DrawString(sf, "Right:" + Right + ", " + RightDown, new Vector2(10, 100), Color.White);
        //    sb.DrawString(sf, "Fire:" + Fire + ", " + FireDown, new Vector2(10, 130), Color.White);
        //    sb.DrawString(sf, "Attach:" + Attach + ", " + AttachDown, new Vector2(10, 160), Color.White);
        //}
    }
}
