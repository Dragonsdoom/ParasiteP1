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
using ParasiteP1;

namespace ParasiteP1.Utility
{
    public class ImageButton
    {
        public delegate void Callback();
        Texture2D BackgroundImage, BackgroundImageHover;
        Callback ClickMethod;
        public Vector2 Position;
        public Rectangle CollisionRect;
        InputController input;
        public static ImageButton Selected;
        bool hover;
        

        public ImageButton(string BackgroundImageName,Vector2 Position, Callback ClickMethod)
        {
            
            Game1 g = ContentStorageManager.Get<Game1>("game");
            input = ContentStorageManager.Get<InputController>("input");
            BackgroundImage = g.Content.Load<Texture2D>(BackgroundImageName);
            this.Position = Position;
            CollisionRect = BackgroundImage.Bounds;
            CollisionRect.X = (int)Position.X;
            CollisionRect.Y = (int)Position.Y;
            this.ClickMethod = ClickMethod;
        }
        public void Update()
        {

            CollisionRect.X = (int)Position.X;
            CollisionRect.Y = (int)Position.Y;
            byte State;

            //Mouse Check
            State = input.GuiMouseClick(CollisionRect);
            
            if (State == 1) Selected = this;
            hover = (Selected == this);
            if (State == 2) ClickMethod();

            // Keyboard Selection Mode
            if (hover && input.Fire) ClickMethod();
          
        }
        public void Draw(SpriteBatch sb)
        {
            if (hover)
            {
                sb.Draw(BackgroundImage, Position, Color.Red);
            }
            else
            {
                sb.Draw(BackgroundImage, Position, Color.White);
            }
        }
    }
}
