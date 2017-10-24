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
    public class HUD
    {
        Rectangle[] healthBar;
        Player p;
        public bool show;

        

        //ctor
        public HUD(Player p)
        {
            healthBar = new Rectangle[Settings.healthbar_rectangles];
            this.p = p;
            show = true;


        }

        //XNA functions
        public void Update(GameTime gameTime)
        {
            HealthBarUpdate();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!show)
            {
                return;
            }
            //Draw Health Bar
            foreach (Rectangle item in healthBar)
            {
                if (!item.IsEmpty)
                {
                    spriteBatch.Draw(p.texture_health, item, Color.White);
                }
            }


            spriteBatch.DrawString(p.spriteFont, $"p.hp={p.hp}", new Vector2(0, Settings.screen_height - 50), Color.White);
            spriteBatch.DrawString(p.spriteFont, $"Kills:{p.kills}", new Vector2(0, 25), Color.Red);
            
        }

        //Class functions
        public void HealthBarUpdate()
        {
            int _x = 0;
            int currentHP = p.hp /25;
            if (currentHP <= 0) // making sure array length isnt negative
            {
                currentHP = 0;
            }
            if (healthBar.Length != currentHP)
            {
                healthBar = new Rectangle[currentHP];
            }

            for (int i = 0; i < healthBar.Length; i++)
            {
                healthBar[i] = new Rectangle(_x, 0, p.texture_health.Width, 20);
                _x += p.texture_health.Width;
            }
        }

    }
}
