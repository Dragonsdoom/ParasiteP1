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
    public class TextButton
    {
        public delegate void Callback(object sender);
        Texture2D BackgroundImage;
        SpriteFont Font;
        string Text;
        Callback ClickMethod;
        Vector2 Position;
        Rectangle CollisionRect;
        InputController input;
        bool hover;
        Vector2 TextOffset;
       
        public TextButton(string BackgroundImageName, string FontName, string Text, Vector2 Position, Vector2 TextOffset, Callback ClickMethod)
        {

            Game1 g =ContentStorageManager.Get<Game1>("game");
            input=ContentStorageManager.Get<InputController>("input");
            this.Text = Text;
            Font=g.Content.Load<SpriteFont>(FontName);
            BackgroundImage=g.Content.Load<Texture2D>(BackgroundImageName);
            this.Position = Position;
            CollisionRect = BackgroundImage.Bounds;
                CollisionRect.X += (int)Position.X;
                CollisionRect.Y += (int)Position.Y;
                this.TextOffset = TextOffset;
                this.ClickMethod = ClickMethod;
        }
        public void Update()
        {
            byte State;
            State = input.GuiMouseClick(CollisionRect);
            hover = (State == 1);
            if (State == 2) ClickMethod(this);

        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(BackgroundImage, Position, (hover) ? Color.Red : Color.White);
            sb.DrawString(Font, Text, Position + TextOffset, Color.White);

        }
    }
}
