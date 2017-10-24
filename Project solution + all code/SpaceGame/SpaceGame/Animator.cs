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
    public class Animator
    {
        Texture2D texture, texture_explosion, texture_fighter_explosion, texure_rusher_explosion;
        Vector2 position;
        List<Animation> list;

        public Animator()
        {

            list = new List<Animation>();
        }
        public void LoadContent(ContentManager Content)
        {
            texture_explosion = Content.Load<Texture2D>("explosion1");
            texture_fighter_explosion = Content.Load<Texture2D>("explosion9");
            texure_rusher_explosion = Content.Load<Texture2D>("explosion34");
        }
        public void Update(GameTime gameTime)
        {
            foreach (Animation item in list)
            {
                item.Update(gameTime);
            }


            if (list.Count()==20)// To prevent game lag, run after X
            {

                for (int i = 0; i < list.Count(); i++)
                {

                    if (!list.ElementAt(i).isVisible)//null not visible animations
                    {
                        list.RemoveAt(i);
                        i--;
                        
                    }
                }


            }
            

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Animation item in list)
            {
                if (item.isVisible)
                item.Draw(spriteBatch);
            }
        }
        public void AddAnimation(Animations animation, Vector2 position)
        {
            this.position = position;
            switch (animation)
            {
                case Animations.explosion:
                    texture = texture_explosion;
                    list.Add(new Animation(texture, position, 16));
                    Settings.sounds.PlaySound(Sound.explosion);

                    break;


                case Animations.RusherExplosion:
                    texture = texure_rusher_explosion;
                     list.Add(new Animation(texture, position, 3,4,20));
                    Settings.sounds.PlaySound(Sound.RusherExplosion);
                    break;


                case Animations.FighterExplosion:
                    texture = texture_fighter_explosion;
                    list.Add(new Animation(texture, position, 9,9,1));
                    Settings.sounds.PlaySound(Sound.FighterExplosion);
                    break;
                default:
                    break;
            }
        }

        
    }
}
