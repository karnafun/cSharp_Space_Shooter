using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceGame
{
    public class SoundManager
    {
        SoundEffect playerShot, enemyShot, explosion,playerDestroyed,rusherDestroyed;
        

        public SoundManager()
        {
            
        }

        public void LoadContent(ContentManager content)
        {
            playerShot = content.Load<SoundEffect>("playerShot");
            //enemyShot = content.Load<SoundEffect>("enemyShot");
            explosion = content.Load<SoundEffect>("explosionSound");
            playerDestroyed = content.Load<SoundEffect>("playerDestroyed");
            rusherDestroyed = content.Load<SoundEffect>("shipDestroyed");
        }

        public void PlaySound(Sound sound)
        {
            switch (sound)
            {
                case Sound.explosion:
                    explosion.Play(0.5f, 0, 0);
                    break;
                case Sound.RusherExplosion:
                    explosion.Play(0.7f, 0, 0);
                    break;
                case Sound.FighterExplosion:
                    rusherDestroyed.Play(0.7f, 0, 0);
                    break;
                case Sound.PlayerShot:
                    playerShot.Play(0.2f, 0, 0);
                    break;
                case Sound.playerDestroyed:
                    playerDestroyed.Play(0.8f, 0, 0);
                    break;


                default:
                    break;
            }
        }
    }
}
