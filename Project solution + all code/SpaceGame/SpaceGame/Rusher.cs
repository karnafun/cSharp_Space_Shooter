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
    public class Rusher : Enemy
    {

        //Ctor
        public Rusher(float x, float y, Texture2D texture):base(x,y)
        {
            //Declare fields
            this.texture = texture;
            speed = Settings.rusher_speed;
            hp = Settings.rusher_hp;
            damage = Settings.rusher_damage;

        }



        
        public override void Update(GameTime gameTime)
        {

            if (!isAlive)
            {
                return;
            }
            EnemyAI();
            base.Update(gameTime);

        }
        
        //Class Functions



        public void EnemyAI()
        {




            AngerManagement();
            position.Y += speed;
            if (position.Y>=Settings.screen_height)
            {
                this.isAlive = false;
            }

            //CheckIfMoving();
            //GetMoveOrderFloat();



            #region commnet out logic




            ////If not moving, get new destination

            //if (!moving)
            //{
            //    //Roam left Side
            //    if (position.X > Settings.screen_width / 2)
            //    {
            //        destination = new Vector2(rand.Next(0, Settings.screen_width / 2)
            //            , rand.Next(0, Settings.screen_height / 2));
            //    }
            //    else //Roam right side
            //    {
            //        destination = new Vector2(rand.Next(Settings.screen_width / 2, Settings.screen_width - texture.Width)
            //            , rand.Next(0, Settings.screen_height / 2));
            //    }
            //    moving = true;


            //}


            ////Move to Player

            //if (x + width < position.X )
            //{
            //    position.X -= speed;
            //}
            //if (position.X + width < x + 20)
            //{
            //    position.X += speed;
            //}




            #endregion

        }
        
       

        


    }
}
