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
    public class astroid
    {
        //Fields
        Texture2D texture;
        public Rectangle box;
        public Vector2 position;
        public bool isAlive;
        public int hp, speed, damage;

        //Ctors
        public astroid(float x, float y)
        {
            damage = Settings.astroid_damage;
            speed = Settings.astroid_speed;
            isAlive = true;
            hp = Settings.astroid_hp;
            texture = null;
            position = new Vector2(x, y);
            box = new Rectangle();
        }
        public astroid(float x, float y, Texture2D tx) : this(x, y)
        {
            texture = tx;
        }

        //XNA functions
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("astroid");
        }
        public void Update(GameTime gameTime)
        {

            position.Y += speed;
            box.X = (int)position.X;
            box.Y = (int)position.Y;
            if (position.Y + texture.Height >Settings.screen_height || hp <= 0)
            {
                isAlive = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isAlive)
            {
                spriteBatch.Draw(texture, box, Color.White);

            }
        }
    }
}
