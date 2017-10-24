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
    public class Player
    {
        //Class Elements
        public Texture2D texture, texture_health,texture_bullet_red, texture_bullet_blue, texture_bullet_yellow;
        public Rectangle box;
        public HUD hud ;
        public Rectangle[] healthBar;
        public Vector2 position;
        public Weapon weapon;
        public SpriteFont spriteFont;

        //Class fields
        public int speed, hp, maxHP, damage, kills;
        public bool isAlive;
        public float timer,score, astroidsDestroyed,rushersDestroyed,fightersDestroyed,shotsFired;

        //Ctor
        public Player()
        {
            //Declare elements
            box = new Rectangle();
            
            position = Settings.player_starting_position;
            weapon = new Weapon(this);
            hud = new HUD(this);

            //Declare fields
            isAlive = true;
            texture = null;
            speed = Settings.player_speed;
            maxHP = Settings.player_hp;
            damage = Settings.player_damage;
            hp = maxHP;
            kills = 0;
            astroidsDestroyed = 0;
            fightersDestroyed = 0;
            rushersDestroyed = 0;
            shotsFired = 0;
            score = 0;





        }

        //XNA functions
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Player");
            texture_health = Content.Load<Texture2D>("healthbar");
            weapon.LoadContent(Content);

            spriteFont = Content.Load<SpriteFont>("SpriteFont");

            //Configure BOX ! 
            box.Width = texture.Width;
            box.Height = texture.Height;

        }
        public void Update(GameTime gameTime)
        {
            CalculateScore();
            HealthCheck();


            timer += gameTime.ElapsedGameTime.Milliseconds;

            if (!isAlive)
                return;
            weapon.Update(gameTime);
            hud.Update(gameTime);
            BorderCollision();
            KeyboardInput();

            //Move box
            box.X = (int)position.X;
            box.Y = (int)position.Y;

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isAlive)
                return;
            
            spriteBatch.Draw(texture, position, Color.White); //player
            weapon.Draw(spriteBatch); 
            hud.Draw(spriteBatch);
            



        }

        //Class functions
        public void BorderCollision()
        {

            //Width
            if (position.X < 0)
            {
                position.X = 1;
            }
            else if (position.X + texture.Width > Settings.screen_width)
            {
                position.X = Settings.screen_width - texture.Width;
            }
            //Height
            if (position.Y < 0)
            {
                position.Y = 0;
            }
            else if (position.Y + texture.Height > Settings.screen_height)
            {
                position.Y = Settings.screen_height - texture.Height;
            }
        }
        public void HealthBarUpdate()
        {

            int _x = 0;
            int currentHP = hp / 25;
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
                healthBar[i] = new Rectangle(_x, 0, texture_health.Width, 20);
                _x += texture_health.Width;
            }

        }
        public void KeyboardInput()
        {
            //Get Keyboard State
            KeyboardState key = Keyboard.GetState();

            //Configure movement with WASD
            if (key.IsKeyDown(Keys.W))
            {
                position.Y -= speed;
            }
            if (key.IsKeyDown(Keys.A))
            {
                position.X -= speed;
            }
            if (key.IsKeyDown(Keys.S))
            {
                position.Y += speed * 1.5f;
            }
            if (key.IsKeyDown(Keys.D))
            {
                position.X += speed;
            }

            //Shoot
            if (key.IsKeyDown(Keys.Space))
            {
                weapon.FireShot();
                shotsFired++;
            }

            //Disable/Enable hud
            if (key.IsKeyDown(Keys.Tab)&&timer>100)
            {
                hud.show = !hud.show;
                timer = 0;
            }


        }
        public void HealthCheck()
        {
            if (hp<=0)
            {
                isAlive = false;
                
            
            }
            if (!isAlive&&timer>=3500)
            {
                Settings.state = State.GameOver;
               
            }
        }
        public void Restart()
        {
            position = Settings.player_starting_position;
            weapon = new Weapon(this);
            isAlive = true;
            hp = maxHP;
            astroidsDestroyed = 0;
            rushersDestroyed = 0;
            fightersDestroyed = 0;
            shotsFired = 0;
            kills = 0;
        }
        public void CalculateScore()
        {
            score = astroidsDestroyed * 30 + rushersDestroyed * 70 + fightersDestroyed * 140;
            kills = (int)(rushersDestroyed + fightersDestroyed);
        }



        











    }
}
