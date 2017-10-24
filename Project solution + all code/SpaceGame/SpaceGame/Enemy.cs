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
    public class Enemy
    {

        //Class Elements
        public Texture2D texture, texture_weapon;
        public Rectangle box;
        public Vector2 position, destination;
        public Weapon weapon;
        protected Random rand;
        public bool angry, moving, inMap;



        //Class fields
        public int speed, hp, damage, AI_delay, collisionDamage;
        public bool isAlive;

        //Ctor
        public Enemy(float x, float y)
        {
            position = new Vector2(x, y);
            box = new Rectangle();
            weapon = new Weapon(this);

            //Declare fields
            isAlive = true;

            speed = Settings.rusher_speed;
            hp = Settings.rusher_hp;
            damage = Settings.rusher_damage;
            AI_delay = Settings.enemy_AI_delay;
            rand = new Random();
            angry = false;
            collisionDamage = Settings.enemy_collision_damage;

        }


        public virtual void Update(GameTime gameTime)
        {
            if (!isAlive)
            {
                return;
            }
            weapon.Update(gameTime);
            box.X = (int)position.X;
            box.Y = (int)position.Y;

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw Player
            spriteBatch.Draw(texture, position, Color.White);

            //Draw weapon
            weapon.Draw(spriteBatch);
        }



        //Class Functions
        public void AngerManagement()
        {
            //Declare Variables
            float x = Game1.p.position.X;
            int width = Game1.p.texture.Width;


            //Check if Angry
            if (position.Y < Game1.p.position.Y)
            {
                if (x + width > position.X + 20 && position.X + width > x + 20)
                {
                    angry = true;
                }
                else
                {
                    angry = false;
                }
            }


            //If angry -> SHOOT! !!
            if (angry)
                weapon.FireShot();
        }
        public void MoveToDestination()
        {

            if (!moving)
            {
                return;
            }


            //Adjust X position
            if (position.X < destination.X)
            {
                if (position.X + speed > destination.X)
                {

                }
                else
                {

                    position.X += speed;
                }
            }
            else if (position.X > destination.X)
            {
                if (position.X - speed < destination.X)
                {

                }
                else
                {

                    position.X -= speed;
                }
            }

            //Adjust Y position
            if (position.Y < destination.Y)
            {

                if (position.Y + speed > destination.Y)
                {

                }
                else
                {

                    position.Y += speed;
                }
            }
            else if (position.Y > destination.Y)
            {
                if (position.Y - speed < destination.Y)
                {

                }
                else
                {

                    position.Y -= speed;
                }
            }
        }
        public void GetMoveOrderFloat()
        {
            //If not moving, get new destination
            if (!moving)
            {

                //Roam left Side
                if (position.X > Settings.screen_width / 2)
                {
                    destination = new Vector2(rand.Next(20, Settings.screen_width / 2)
                        , rand.Next(10, Settings.screen_height / 2));
                }
                else //Roam right side
                {
                    destination = new Vector2(rand.Next(Settings.screen_width / 2, Settings.screen_width - texture.Width - 20)
                        , rand.Next(10, Settings.screen_height / 2));
                }
                moving = true;
            }

        }
        public void GetMoveOrder()
        {

            if (!moving)
            {
                //Roam left Side
                if (position.X > Settings.screen_width / 2)
                {
                    destination = new Vector2(rand.Next(0, Settings.screen_width / 2)
                        , position.Y + 100);
                }
                else //Roam right side
                {
                    destination = new Vector2(rand.Next(Settings.screen_width / 2, Settings.screen_width - texture.Width)
                        , position.Y + 100);
                }
                moving = true;


            }
        }
        public void CheckIfMoving()
        {
            if (Math.Abs(position.X - destination.X) <= Settings.rusher_speed && Math.Abs(position.Y - destination.Y) <= Settings.rusher_speed)
            {
                moving = false;
            }
            else
            {
                moving = true;
                MoveToDestination();
            }
        }




    }
}
