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
    public class Weapon
    {
        //Fields
        Player p;
        Enemy e;

        public Texture2D texture;
        public List<Ammo> magazine;
        public int shotDelay, shotTimer,extraDamage;



        //ctor
        public Weapon(Player p)
        {
            this.p = p;
            magazine = new List<Ammo>();
            shotDelay = Settings.player_weapon_shot_delay;
            shotTimer = 0;
            extraDamage = Settings.player_damage;
            texture = Settings.texture_bullet_blue;
            

        }
        public Weapon(Enemy e)
        {
            this.e = e;
            magazine = new List<Ammo>(); 
            shotDelay = Settings.enemy_weapon_shot_delay;
            shotTimer = 0;
            extraDamage = Settings.rusher_damage;
            texture = Settings.texture_bullet_red;
            
        }
        //XNA functions
        public void LoadContent(ContentManager content)
        {

        }
        public void Update(GameTime gameTime)
        {

            
            //Update Active Shots
            foreach (Ammo item in magazine)
            {
                item.Update(gameTime);
            }


            shotTimer++;
            RemoveDeads();




        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Ammo item in magazine)
            {
                if (item.isAlive)
                {
                    item.Draw(spriteBatch);
                }
            }
        }

        //Class functions
        public void FireShot()
        {
            //Check if ready to shoot;
            if (shotTimer <= shotDelay)
            {
                return;
            }
            shotTimer = 0;

            //Declare variables
            Ammo f = ConfigureBullet();
            f.texture = texture;
            int width = texture.Width;
            int height = texture.Height;
            
            


            float x, y;
            if (p!=null)
            {
                x = p.position.X + p.texture.Width / 2 - width / 2;
                 y = p.position.Y;
                
            }
            else
            {
                 x = e.position.X + e.texture.Width / 2 - width / 2;
                 y = e.position.Y;
                f.speed = -f.speed;
            }
            


            //Configure Bullet
            
            f.box = new Rectangle((int)x, (int)y, width, height);
            f.position = new Vector2(x, y );
            f.origion = f.position;
            f.damage += extraDamage;
            if (e!=null&&e is Rusher)
            {
                f.extraSpeed = Settings.rusher_bullet_extra_speed;
            }

            //Add Bullet 
            magazine.Add(f);

            //Play sound

        }
        public void RemoveDeads()
        {
            for (int i = 0; i < magazine.Count(); i++)
            {
                if (!magazine.ElementAt(i).isAlive)
                {
                    magazine.RemoveAt(i);
                }
            }
        }

        public void AssignAmmo()
        {
            if (e is Rusher)
            {

            }
            else if (e is Fighter)
            {
                extraDamage = Settings.fighter_damage;
                texture = Settings.texture_bullet_red;
            }

            
        }

        public Ammo ConfigureBullet()
        {
            if (p!=null)
            {
                texture = Settings.texture_bullet_blue;
                Settings.sounds.PlaySound(Sound.PlayerShot);
                return new NormalBullet();
            }
            else if (e is Rusher)
            {
                return new NormalBullet();
            }
            else if (e is Fighter)
            {
                return new NormalBullet();
            }
            else
            {
                return new NormalBullet();
            }
        }
        

    }
}
