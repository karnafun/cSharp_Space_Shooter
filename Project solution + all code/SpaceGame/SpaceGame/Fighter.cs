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
    class Fighter : Enemy
    {
        //Ctor
        public Fighter(float x, float y, Texture2D texture):base(x,y)
        {
            //Declare fields
            this.texture = texture;
            speed = Settings.fighter_speed;
            hp = Settings.fighter_hp;
            damage = Settings.fighter_damage;

            GetMoveOrderFloat();

        }

        //XNA functions
        public override void Update(GameTime gameTime)
        {
            if (!isAlive)
            {
                return;
            }
            AngerManagement();
            CheckIfMoving();
            GetMoveOrderFloat();

            base.Update(gameTime);
        }
    }
}
