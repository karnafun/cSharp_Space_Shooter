using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace SpaceGame
{
    class EnemyForce
    {
        //Fields
        public List<Enemy> force;
        Random rand;
        Texture2D texture, texture_rusher, texture_fighter;
        int rushers, fighters, spawnDelay, spawn_timer, lastX;


        //Ctor
        public EnemyForce()
        {
            rand = new Random();
            rushers = Settings.rushers_on_screen;
            fighters = Settings.fighters_on_screen;
            spawnDelay = Settings.enemy_spawn_delay;
            spawn_timer = 0;
            force = new List<Enemy>();
            lastX = 0;
        }


        //XNA functions
        public void LoadContent(ContentManager content)
        {
            texture_rusher = content.Load<Texture2D>("rusher");

            texture_fighter = content.Load<Texture2D>("fighter");

        }
        public void Update(GameTime gameTime)
        {

            DeleteDead();
            CreateEnemy();
            spawn_timer++;

            //Update active astroids
            foreach (Enemy item in force)
            {
                item.Update(gameTime);
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy item in force)
            {
                if (item.isAlive)
                {
                    item.Draw(spriteBatch);
                }
            }
        }

        //Class Functions
        public void CreateEnemy()
        {
            //Generate Location


            //Check which enemy type is missing

            int _rushers = 0;
            int _fighters = 0;
            Enemy res;

            foreach (Enemy e in force)
            {
                if (e is Rusher)
                {
                    _rushers++;
                }
                else if (e is Fighter)
                {
                    _fighters++;
                }

            }

            int x, y;

            if (_rushers < rushers)//Create rusher
            {
                texture = texture_rusher;
                do
                {
                    x = rand.Next(10, Settings.screen_width - texture.Width - 10);
                } while (Math.Abs(x - lastX) < 60);

                y = rand.Next(0, Settings.screen_height);
                res = new Rusher(x, -y, texture);
                res.box = new Rectangle(x, y, texture.Width, texture.Height);
            }
            else if (_fighters < fighters)// Create Fighter
            {
                texture = texture_fighter;
                do
                {
                    x = rand.Next(10, Settings.screen_width - texture.Width - 10);
                } while (Math.Abs(x - lastX) < 60);
                y = rand.Next(0, Settings.screen_height);
                res = new Fighter(x, -y, texture);
                res.box = new Rectangle(x, y, texture.Width, texture.Height);
            }
            else
            {
                return;
            }


            force.Add(res);

        }
        public void DeleteDead()
        {
            for (int i = 0; i < force.Count(); i++)
            {
                
                    if (!force.ElementAt(i).isAlive || force.ElementAt(i).hp < 0)
                    {
                        force.RemoveAt(i);
                    }
                
                
            }
        }










    }
}
