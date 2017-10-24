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
    public class Ammo
    {
        public Player owner;
        public Texture2D texture;
        public Rectangle box;
        public Vector2 position, origion;
        public int speed, extraSpeed, damage, range;
        public bool isAlive;


        public Ammo()
        {

            box = new Rectangle();
            speed = 1;
            extraSpeed = 0;
            damage = 0;
            isAlive = true;
        }

        public virtual void LoadContent(ContentManager content) { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
