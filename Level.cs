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
    public class Level
    {
        Random rand = new Random();
        public int current_level;
        Sprite backgroundp1,backgroundp2, Bobject;
        List<Sprite> ObjectList = new List<Sprite>();
        List<SweepEnemy> sweepEnemyList = new List<SweepEnemy>();
        List<OrbitEnemy> orbitEnemyList = new List<OrbitEnemy>();
        List<StraitEnemy> straitEnemyList = new List<StraitEnemy>();
        SweepEnemy sweepEnemy;
        OrbitEnemy orbitEnemy;
        StraitEnemy straitEnemy;
        int ramp = 0, rampup = 0;
        
        
        int Timer = 0;
        public Level(int current)
        {
            current_level = current;
        }


        public void GenerateLevel(int current_level)
        {
            backgroundp1 = new Sprite(LevelManager.GiveBackground(current_level));
            backgroundp2 = new Sprite(LevelManager.GiveBackground(current_level));
            backgroundp1.position.Y = -1;
            backgroundp2.position.Y = backgroundp2.GetTextureSize().Y;
            
            
        }

        public void TitleUpdate()
        {
            float size1 = backgroundp1.GetTextureSize().Y;
            float size2 = backgroundp2.GetTextureSize().Y;
            backgroundp1.position.Y += 1;
            backgroundp2.position.Y += 1;
            if (backgroundp1.position.Y >= size1)
            {
                backgroundp1.position.Y = -1 * size1;
            }
            if (backgroundp2.position.Y >= size2)
            {
                backgroundp2.position.Y = -1 * size2;
            }
        }

        public void Update()
        {
            float size1 = backgroundp1.GetTextureSize().Y;
            float size2 = backgroundp2.GetTextureSize().Y;
            backgroundp1.position.Y += 1;
            backgroundp2.position.Y += 1;
            if (backgroundp1.position.Y >= size1)
            {
                backgroundp1.position.Y = -1 * size1;
            }
            if (backgroundp2.position.Y >= size2)
            {
                backgroundp2.position.Y = -1 * size2;
            }

            foreach (Sprite b in ObjectList)
            {
                b.position.Y += 1;
            }


            ramp++;
            Timer++;
            if (Timer % 1300 == 0)
            {

                ObjectList.Clear();

                int bnumber = rand.Next(10, 10);
                int randnumX, randnumY, size, objectrand;
                Texture2D texture;
                
            
                while (bnumber > 0)
                {
                    randnumX = rand.Next(-50, 1000);
                    randnumY = rand.Next(-500, -50);
                    objectrand = rand.Next(1,3);
                    texture = LevelManager.GiveObject(current_level, objectrand);
                    Bobject = new Sprite(texture, new Vector2(randnumX, randnumY));
                    size = rand.Next(1, 3);
                    Bobject.Scale = size;
                    ObjectList.Add(Bobject);
                    bnumber -= 1;
                }
            }
        
            if (!(ramp >= 50000))
            {
                for (int i = 0; i < sweepEnemyList.Count; i++)
                {
                    if (sweepEnemyList[i].isDying || sweepEnemyList[i].Position.X > Game1.ScreenSize.X + 102 || sweepEnemyList[i].Position.X < -100 || sweepEnemyList[i].Position.Y < -102 || sweepEnemyList[i].Position.Y > Game1.ScreenSize.Y)
                    {
                        sweepEnemyList.RemoveAt(i);
                        continue;
                    }
                    sweepEnemyList[i].Update();
                }
                for (int i = 0; i < orbitEnemyList.Count; i++)
                {
                    if (orbitEnemyList[i].isDying || orbitEnemyList[i].Position.X > Game1.ScreenSize.X +102 || orbitEnemyList[i].Position.X < -100|| orbitEnemyList[i].Position.Y < -100  || orbitEnemyList[i].Position.Y > Game1.ScreenSize.Y)
                    {
                        orbitEnemyList.RemoveAt(i);
                        continue;
                    }
                    orbitEnemyList[i].Update();
                }
                for (int i = 0; i < straitEnemyList.Count; i++)
                {
                    if (straitEnemyList[i].isDying || straitEnemyList[i].Position.X > Game1.ScreenSize.X + 102 || straitEnemyList[i].Position.X < -100 || straitEnemyList[i].Position.Y < -100 || straitEnemyList[i].Position.Y > Game1.ScreenSize.Y)
                    {
                        straitEnemyList.RemoveAt(i);
                        continue;
                    }
                    straitEnemyList[i].Update();
                }

                if (Timer % (200 - rampup) == 0)
                {

                    int spawnCount, formationType;
                    spawnCount = rand.Next(3, 7);
                    formationType = rand.Next(1, 12);

                    if (formationType == 1)
                    {
                        while (spawnCount > 0)
                        {
                            sweepEnemy = new SweepEnemy(new Vector2(spawnCount * 10, spawnCount * -50), new Vector2((.4f + (0.1f * spawnCount)), (1.2f + (1f * spawnCount))), null);
                            sweepEnemyList.Add(sweepEnemy);
                            if (spawnCount >= 5)
                            {
                                orbitEnemy = new OrbitEnemy(sweepEnemy, 1f, 30);
                                orbitEnemyList.Add(orbitEnemy);
                            }                            
                            spawnCount -= 1;
                        }
                    }

                    if (formationType == 2)
                    {
                        while (spawnCount > 0)
                        {
                            sweepEnemy = new SweepEnemy(new Vector2((spawnCount * 10) + 50, -10), new Vector2((spawnCount * .1f) + .8f, 2.4f), null);
                            sweepEnemyList.Add(sweepEnemy);
                            spawnCount -= 1;
                        }
                    }

                    if (formationType == 3)
                    {
                        while (spawnCount > 0)
                        {
                            sweepEnemy = new SweepEnemy(new Vector2((spawnCount * 10) + 800, -10), new Vector2(-((spawnCount * .3f) + .8f), 2.4f), null);
                            sweepEnemyList.Add(sweepEnemy);
                            spawnCount -= 1;
                        }
                    }

                    if (formationType == 4)
                    {
                        while (spawnCount > 0)
                        {
                            if (spawnCount < 4)
                            {
                                sweepEnemy = new SweepEnemy(new Vector2((spawnCount * 10) + 800, -10), new Vector2(-((spawnCount * .3f) + .8f), 2.4f), null);
                                sweepEnemyList.Add(sweepEnemy);
                            }

                            if (spawnCount > 3)
                            {
                                sweepEnemy = new SweepEnemy(new Vector2(spawnCount * 10, spawnCount * -50), new Vector2((.4f + (0.1f * spawnCount)), (1.2f + (1f * spawnCount))), null);
                                sweepEnemyList.Add(sweepEnemy);
                            }
                            spawnCount -= 1;
                        }

                    }

                    if (formationType == 5)
                    {
                        while (spawnCount > 0)
                        {
                            straitEnemy = new StraitEnemy(new Vector2((spawnCount * 10) + 800, 200), new Vector2(-(spawnCount * 1.4f) - .6f, 0));
                            straitEnemyList.Add(straitEnemy);
                            if (spawnCount > 5)
                            {
                                sweepEnemy = new SweepEnemy(new Vector2(spawnCount * 50, spawnCount * -50), new Vector2((.4f + (0.1f * spawnCount)), (1.2f + (1f * spawnCount))), null);
                                sweepEnemyList.Add(sweepEnemy);
                                if (spawnCount > 5)
                                {
                                    orbitEnemy = new OrbitEnemy(sweepEnemy, 1f, 30);
                                    orbitEnemyList.Add(orbitEnemy);
                                }
                            }
                            spawnCount -= 1;
                        }
                    }

                    if (formationType == 6)
                    {
                        if (spawnCount == 3)
                        {
                            while (spawnCount > 0)
                            {
                                straitEnemy = new StraitEnemy(new Vector2((spawnCount * 10) + 800, 600), new Vector2(-(spawnCount * 1.4f) - .6f, 0));
                                straitEnemyList.Add(straitEnemy);
                                orbitEnemy = new OrbitEnemy(straitEnemy, 1f, 30);
                                orbitEnemyList.Add(orbitEnemy);
                                spawnCount -= 1;
                            }
                        }
                        else
                        {
                            while (spawnCount > 0)
                            {
                                straitEnemy = new StraitEnemy(new Vector2((spawnCount * 10), 250), new Vector2((spawnCount * 1.4f) + .6f, 0));
                                straitEnemyList.Add(straitEnemy);
                                spawnCount -= 1;
                            }
                        }
                    }

                    if (formationType == 7)
                    {
                        while (spawnCount > 0)
                        {
                            sweepEnemy = new SweepEnemy(new Vector2((spawnCount * 100) + 100, (spawnCount * -50)), new Vector2(0, 3f), null);
                            sweepEnemyList.Add(sweepEnemy);
                            orbitEnemy = new OrbitEnemy(sweepEnemy, 1f, 30);
                            orbitEnemyList.Add(orbitEnemy);
                            orbitEnemy = new OrbitEnemy(sweepEnemy, 4f, 30);
                            orbitEnemyList.Add(orbitEnemy);
                            spawnCount -= 1;
                        }
                    }

                    if (formationType == 8)
                    {
                        while (spawnCount > 0)
                        {
                            if (spawnCount == 1)
                            {
                                sweepEnemy = new SweepEnemy(new Vector2(Game1.ScreenSize.X / 2, -50), new Vector2(0, 3f), null);
                                sweepEnemyList.Add(sweepEnemy);
                                orbitEnemy = new OrbitEnemy(sweepEnemy, 1f, 30);
                                orbitEnemyList.Add(orbitEnemy);
                                orbitEnemy = new OrbitEnemy(sweepEnemy, 2f, 30);
                                orbitEnemyList.Add(orbitEnemy);
                                orbitEnemy = new OrbitEnemy(sweepEnemy, 3f, 30);
                                orbitEnemyList.Add(orbitEnemy);
                                orbitEnemy = new OrbitEnemy(sweepEnemy, 4f, 30);
                                orbitEnemyList.Add(orbitEnemy);
                                orbitEnemy = new OrbitEnemy(sweepEnemy, 5f, 30);
                                orbitEnemyList.Add(orbitEnemy);
                                orbitEnemy = new OrbitEnemy(sweepEnemy, 6f, 30);
                                orbitEnemyList.Add(orbitEnemy);
                            }
                            spawnCount -= 1;
                        }
                    }

                    if (formationType == 9)
                    {
                        while (spawnCount > 0)
                        {
                            if (spawnCount < 4)
                            {
                                straitEnemy = new StraitEnemy(new Vector2((spawnCount * 10) + 800, 200), new Vector2(-((spawnCount * .5f) + .8f), 0));
                                straitEnemyList.Add(straitEnemy);
                                orbitEnemy = new OrbitEnemy(straitEnemy, 1f, 30);
                                orbitEnemyList.Add(orbitEnemy);
                                orbitEnemy = new OrbitEnemy(straitEnemy, 4f, 30);
                                orbitEnemyList.Add(orbitEnemy);

                                straitEnemy = new StraitEnemy(new Vector2((spawnCount * 10), 200), new Vector2(((spawnCount * .3f) + .8f), 0));
                                straitEnemyList.Add(straitEnemy);
                                orbitEnemy = new OrbitEnemy(straitEnemy, 1f, 30);
                                orbitEnemyList.Add(orbitEnemy);
                                orbitEnemy = new OrbitEnemy(straitEnemy, 4f, 30);
                                orbitEnemyList.Add(orbitEnemy);
                            }

                            spawnCount -= 1;
                        }
                    }

                    if (formationType == 10)
                    {
                        spawnCount = spawnCount * 2;
                        while (spawnCount > 0)
                        {
                            sweepEnemy = new SweepEnemy(new Vector2((spawnCount * 50), -50), new Vector2(0, .8f), null);
                            sweepEnemyList.Add(sweepEnemy);
                            spawnCount -= 1;
                        }
                    }

                    if (formationType == 11)
                    {
                        while (spawnCount > 0)
                        {
                            sweepEnemy = new SweepEnemy(new Vector2(-50, (spawnCount * 100) + 200), new Vector2(1.4f, 0), null);
                            sweepEnemyList.Add(sweepEnemy);
                            orbitEnemy = new OrbitEnemy(sweepEnemy, 1f, 30);
                            orbitEnemyList.Add(orbitEnemy);

                            spawnCount -= 1;
                        }
                    }

                    
                }
            }
            else
            {
                ramp = 0;
                if (!(rampup == 50))
                {
                    rampup += 10;
                }
            }

            
        }

        public void TitleDraw(SpriteBatch sb)
        {
            backgroundp1.Draw(sb);
            backgroundp2.Draw(sb);
            
        }

        public void Draw(SpriteBatch sb)
        {
            backgroundp1.Draw(sb);
            backgroundp2.Draw(sb);
            foreach (Sprite b in ObjectList)
            {
                b.Draw(sb);
            }
            foreach (SweepEnemy se in sweepEnemyList)
            {
                se.Draw(sb);
            }
            foreach (OrbitEnemy oe in orbitEnemyList)
            {
                oe.Draw(sb);
            }
            foreach (StraitEnemy se in straitEnemyList)
            {
                se.Draw(sb);
            }
        }
    }
}
