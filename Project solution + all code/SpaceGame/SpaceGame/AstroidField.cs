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
    class AstroidField
    {
        //Fields
        public List<astroid> field;
        Random rand;
        Texture2D texture;
        int maxAstroids, spawnDelay, spawn_timer;


        //Ctor
        public AstroidField()
        {
            rand = new Random();
            maxAstroids = Settings.astroids_on_screen;
            spawnDelay = Settings.astroid_spawn_delay;
            spawn_timer = 0;
            field = new List<astroid>();
        }



        //XNA functions
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("astroid");
        }
        public void Update(GameTime gameTime)
        {

            DeleteDead();
            GenerateAstroids();
            spawn_timer++;

            //Update active astroids
            foreach (astroid item in field)
            {
                item.Update(gameTime);
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (astroid item in field)
            {
                if (item.isAlive)
                {
                    item.Draw(spriteBatch);
                }
            }
        }

        //Class Functions
        public astroid CreateAstroid()
        {

           
            int x = rand.Next(10, Settings.screen_width-texture.Width-10);
            int y = rand.Next(0, Settings.screen_height);

            astroid a = new astroid(x, -y, texture);
            a.box = new Rectangle(x, y, texture.Width, texture.Height);

            return a;
        }
        public void DeleteDead()
        {
            for (int i = 0; i < field.Count(); i++)
            {
                if (field.ElementAt(i) != null && !field.ElementAt(i).isAlive)
                {
                    field.RemoveAt(i);
                }
            }
        }
        public void GenerateAstroids()
        {
            if (spawn_timer < spawnDelay)
                return;


            if (field.Count() < maxAstroids)
            {
                field.Add(CreateAstroid());
                spawn_timer = 0;
            }
        }




    }
}
