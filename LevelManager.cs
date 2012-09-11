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
    static class LevelManager
    {
        public static Texture2D GiveBackground(int current_level)
        {
            int current = current_level;
            Texture2D texture = ContentStorageManager.Get<Texture2D>("Background00");

            switch (current)
            {
                case 1:
                    texture = ContentStorageManager.Get<Texture2D>("Background00");
                    break;

                default:
                    texture = ContentStorageManager.Get<Texture2D>("Background00");
                    break;

            }
            return texture; 
        }

        public static Texture2D GiveObject(int current_level, int random)
        {
            int current = current_level;
            int randnum = random;
            Texture2D texture = ContentStorageManager.Get<Texture2D>("Shape02");

            switch (current)
            {
                case 1:
                    
                    switch (randnum)
                    {
                        case 1:
                            texture = ContentStorageManager.Get<Texture2D>("Shape00");
                            break;

                        case 2:
                            texture = ContentStorageManager.Get<Texture2D>("Shape02");
                            break;


                        default:
                            texture = ContentStorageManager.Get<Texture2D>("Shape00");
                            break;
                    }
                    break;

                default:
                    texture = ContentStorageManager.Get<Texture2D>("Shape00");
                    break;
            }
            return texture;
        }

    }
}
