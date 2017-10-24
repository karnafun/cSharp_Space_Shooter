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
    class NormalBullet : Ammo
    {

        //ctor
        public NormalBullet()
        {
            damage = Settings.normalBullet_damage;
            speed = Settings.normalBullet_speed;
            range = Settings.normalBullet_range;

        }

        //XNA functions
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("bulletRed");

        }
        public override void Update(GameTime gameTime)
        {
            position.Y -= speed- extraSpeed ;
            box.X = (int)position.X;
            box.Y = (int)position.Y;
            if (origion.Y - range > position.Y)
            {
                isAlive = false;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isAlive)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
        }
    }
}
