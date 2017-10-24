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
    public class Background
    {
        Texture2D texture;
        Rectangle box;
        public Vector2 position;
        int speed;

        public Background()
        {
            texture = null;
            position = new Vector2(0, 0);
            speed = Settings.background_speed;
        }
        public void LoadConent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("background");
            box = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
        public void Update(GameTime gameTime)
        {
            switch (Settings.state)
            {
                case State.Menu:
                    speed = Settings.menu_background_speed;
                    break;
                case State.Playing:
                    speed = Settings.background_speed;
                    break;
                case State.GameOver:
                    speed = 0;
                    break;
                default:
                    break;
            }
            

            position.Y += speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
