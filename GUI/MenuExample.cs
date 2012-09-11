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
   
    public class MenuExample
    {

        //Declare 

        Menu MainMenu;


        public MenuExample(Game game)
            
        {

        //Create a dictionary <string, ImageButton.Callback> With the name of the image in contents

  //Images must be a pair of images and the second one named the same as the first with Hover appended

            Dictionary<string, ImageButton.Callback> NameMethods = new Dictionary<string, ImageButton.Callback>();

            // The Q and Start is the name of the PUBLIC method to call when clicked
            NameMethods.Add("QuitButton", Q);
            NameMethods.Add("StartButton", Start);

            MainMenu = new Menu(game, NameMethods, HLayout.Left, VLayout.Bottom, new Vector2(50, 50), 40);
            
        }

        public void Q()
        {
            //  Do quit action when pressed.

        }
        public void Start()
        {
            //  Do Start action when pressed. Change state to start

        }

      
        public void Update(GameTime gameTime)
        {
           
            //update
            MainMenu.Update();
        }

        public void Draw(SpriteBatch sb)
        {

            //Draw
            MainMenu.Draw(sb);
        }
    }
}
