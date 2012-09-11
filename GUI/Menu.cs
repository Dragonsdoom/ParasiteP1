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
using ParasiteP1.Utility;


namespace ParasiteP1
{
    
    public enum HLayout { Left, Right, Center }
    public enum VLayout { Top, Middle, Bottom }

    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Menu
    {
        InputController input;
        
        List<ImageButton> Buttons = new List<ImageButton>();


        public Menu(Game game,Dictionary<string,ImageButton.Callback> NameMethods,HLayout HorizonalLayout,VLayout VerticalLayout,Vector2 Offset, int VerticalPadding)
           
        {
            //Need to move Mouse visability and mouse nav to state manager.

            game.IsMouseVisible = true;
            input = ContentStorageManager.Get<InputController>("input");
            int TotalHeight = -1 * VerticalPadding;
            foreach (string item in NameMethods.Keys)
            {
                ImageButton b = new ImageButton(item, Vector2.Zero, NameMethods[item]);
                Buttons.Add(b);
                
                b.Position.Y = TotalHeight + Offset.Y;
                TotalHeight += b.CollisionRect.Height + VerticalPadding;
                if (HorizonalLayout == HLayout.Left)
                    b.Position.X = Offset.X;
                if (HorizonalLayout == HLayout.Center)
                    b.Position.X -=b.CollisionRect.Width / 2 - game.GraphicsDevice.PresentationParameters.BackBufferWidth / 2;
                if (HorizonalLayout == HLayout.Right)
                    b.Position.X -= b.CollisionRect.Width - game.GraphicsDevice.PresentationParameters.BackBufferWidth+(int)Offset.X;
            }
            switch (VerticalLayout)
            {
                case VLayout.Top:
                    break;
                case VLayout.Middle:
                    int mid = TotalHeight / 2 - game.GraphicsDevice.PresentationParameters.BackBufferHeight / 2;
                    foreach (ImageButton item in Buttons)
                    {
                        item.Position.Y -= mid;
                    }
                    break;
                case VLayout.Bottom:
                     int mid1 = TotalHeight - game.GraphicsDevice.PresentationParameters.BackBufferHeight+ (int)Offset.Y;
                    foreach (ImageButton item in Buttons)
                    {
                        item.Position.Y -= mid1;
                    }
                    break;
                    
                default:
                    break;
            }
            
                    
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public  void Initialize()
        {
            // TODO: Add your initialization code here
         
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public  void Update()
        {
            if (input.Up)
            {
                ImageButton.Selected = Buttons[(Buttons.IndexOf(ImageButton.Selected) + 1 + Buttons.Count) % Buttons.Count];
            }
            if (input.Down)
            {
                ImageButton.Selected = Buttons[(Buttons.IndexOf(ImageButton.Selected) - 1 + Buttons.Count) % Buttons.Count];
            }
            // TODO: Add your update code here
            foreach (ImageButton item in Buttons)
            {
                item.Update();
            }         
        }
        public void Draw(SpriteBatch sb)
        {
            // TODO: Add your update code here
            foreach (ImageButton item in Buttons)
            {
                item.Draw(sb);
            }
        }
    }
}
