using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceGame
{
    public enum State { Menu, Playing, GameOver }
    public enum Animations { explosion, RusherExplosion, FighterExplosion, playerDestroyed }
    public enum Sound { explosion, RusherExplosion, FighterExplosion, playerDestroyed, PlayerShot }

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region fields
        State state;

        //XNA fields
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Element fields

        static public Player p;
        Background bg1, bg2;
        AstroidField astroidField;
        EnemyForce enemyForce;


        //Class fields
        Random rand;

        float CreditTimer;
        bool CreditsActive;
        Vector2 CreditPosition;


        #endregion

        public Game1()
        {
            Settings.Configure();

            this.Window.Title = "Karnafun - Space Shooter";
            Content.RootDirectory = "Content";

            rand = new Random();


            //graphics
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = Settings.screen_width;
            graphics.PreferredBackBufferHeight = Settings.screen_height;

            //Game Player
            p = new Player();

            //Backgrounds
            bg1 = new Background();
            bg2 = new Background();

            //Astroids
            astroidField = new AstroidField();
            //enemies
            enemyForce = new EnemyForce();

            CreditTimer = 0;
            CreditsActive = false;
            CreditPosition =  new Vector2(50, Settings.screen_height);










        }


        //XNA Functions
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Settings.sounds = new SoundManager();
            Settings.animator = new Animator();

            base.Initialize();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);



            Settings.texture_Menu = Content.Load<Texture2D>("Menu");
            Settings.texture_bullet_blue = Content.Load<Texture2D>("bulletBlue");
            Settings.texture_bullet_red = Content.Load<Texture2D>("bulletRed");
            Settings.texture_bullet_yellow = Content.Load<Texture2D>("bulletYellow");


            Settings.animator.LoadContent(Content);
            Settings.sounds.LoadContent(Content);
            Settings.animator.LoadContent(Content);
            //Player
            p.LoadContent(Content);

            //Backgrounds
            bg1.LoadConent(Content);
            bg2.LoadConent(Content);

            //Astroids
            astroidField.LoadContent(Content);

            //Enemies
            enemyForce.LoadContent(Content);


        }
        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();



            //Allways run background update!
            bg1.Update(gameTime);
            bg2.Update(gameTime);
            if (bg1.position.Y > Settings.screen_height)
            {
                bg1.position.Y = 0;
            }
            bg2.position.Y = bg1.position.Y - Settings.screen_height;


            state = Settings.state;
            //Update by state
            switch (state)
            {
                case State.Menu:

                    MenuUpdate();
                    //Settings.GameStartCounter = (float)gameTime.ElapsedGameTime.Seconds;
                    break;
                case State.Playing:


                    //Player
                    p.Update(gameTime);

                    //Astroids
                    astroidField.Update(gameTime);

                    //Enemies
                    enemyForce.Update(gameTime);

                    //Animator
                    Settings.animator.Update(gameTime);

                    //Collision
                    MovementCollision();
                    ShotCollisionPlayer();
                    ShotCollisionEnemy();


                    //Check game ending conditions
                    if (!p.isAlive)
                    {
                        Settings.state = State.GameOver;

                    }

                    break;


                case State.GameOver:
                    GameOverUpdate();
                    Settings.animator.Update(gameTime);
                    break;
                default:
                    break;
            }









            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            //Background
            bg1.Draw(spriteBatch);
            bg2.Draw(spriteBatch);


            //Draw by state
            switch (state)
            {
                case State.Menu:

                    MenuDraw(gameTime);
                    break;
                case State.Playing:


                    Settings.animator.Draw(spriteBatch);
                    astroidField.Draw(spriteBatch);
                    enemyForce.Draw(spriteBatch);
                    p.Draw(spriteBatch);





                    break;
                case State.GameOver:
                    
                    float tmpWidth = Settings.screen_width /4;
                    float tmpHeight = Settings.screen_height / 4;
                    float interval = 50;

                    spriteBatch.DrawString(p.spriteFont, $"Score:{p.score}",
                        new Vector2(tmpWidth,tmpHeight), Color.Red);

                    spriteBatch.DrawString(p.spriteFont, $"Total kills:={p.kills}",
                        new Vector2(tmpWidth, tmpHeight+interval), Color.White);

                    spriteBatch.DrawString(p.spriteFont, $"Astroids Destroyed:={p.astroidsDestroyed}",
                        new Vector2(tmpWidth, tmpHeight + interval*2), Color.White);

                    spriteBatch.DrawString(p.spriteFont, $"Rushers Destroyed:={p.rushersDestroyed}",
                        new Vector2(tmpWidth, tmpHeight + interval*3), Color.White);

                    spriteBatch.DrawString(p.spriteFont, $"Fighters Destroyed:={p.fightersDestroyed}",
                        new Vector2(tmpWidth, tmpHeight + interval*4), Color.White);


                    spriteBatch.DrawString(p.spriteFont, $"Press ENTER to continue ",
                      new Vector2(tmpWidth-interval, tmpHeight + interval * 6), Color.Blue);






                    break;
                default:
                    break;
            }



            spriteBatch.End();

            // base.Draw(gameTime);

        }


        //State functions
        private void MenuUpdate()
        {
            KeyboardState key = Keyboard.GetState();

            if (key.IsKeyDown(Keys.Enter))
            {
                if (!CreditsActive)
                {
                Settings.state = State.Playing;
                Settings.Configure();
                }
                else
                {
                    CreditsActive = false;
                    System.Threading.Thread.Sleep(250);
                    CreditPosition = new Vector2(50, Settings.screen_height);
                }
            }

            if (key.IsKeyDown(Keys.C))
            {
                CreditsActive = true;
            }
        }
        private void MenuDraw(GameTime gameTime)
        {
            if (CreditsActive)
            {
                Credits(gameTime);
                
            }
            else
            {
                spriteBatch.Draw(Settings.texture_Menu,
                new Rectangle(0, 0, Settings.screen_width, Settings.screen_height),
                Color.White);
            }
            
        }
        private void GameOverUpdate()
        {
            KeyboardState key = Keyboard.GetState();


            if (key.IsKeyDown(Keys.Enter))
            {
                Settings.state = State.Menu;
                System.Threading.Thread.Sleep(250);
                Settings.Configure();
                enemyForce.force.Clear();
                astroidField.field.Clear();
                p.Restart();
            }
        }
     


        //Class Functions
        private void MovementCollision()
        {
            //Collision with astroids
            foreach (astroid item in astroidField.field)
            {
                if (p.box.Intersects(item.box))
                {
                    p.hp -= item.damage;
                    p.astroidsDestroyed++;
                    Settings.animator.AddAnimation(Animations.explosion,item.position);
                    item.isAlive = false;
                }
            }

            //Collision with enemies
            foreach (Enemy item in enemyForce.force)
            {
                if (p.box.Intersects(item.box))
                {
                    item.isAlive = false;
                    if (item is Rusher)
                    {
                        Settings.animator.AddAnimation(Animations.RusherExplosion, item.position);
                        p.rushersDestroyed++; ;
                    }
                    else if (item is Fighter)
                    {
                        Settings.animator.AddAnimation(Animations.FighterExplosion, item.position);
                        p.fightersDestroyed++;
                    }
                    p.hp -= item.collisionDamage;
                }
            }

        }
        private void ShotCollisionPlayer()
        {
            //player shots vs astroids
            foreach (Ammo ammo in p.weapon.magazine)
            {
                foreach (astroid astroid in astroidField.field)
                {
                    if (ammo.box.Intersects(astroid.box))
                    {
                        astroid.hp -= ammo.damage;
                        if (astroid.hp <= 0)
                        {
                            p.astroidsDestroyed++;
                            Settings.animator.AddAnimation(Animations.explosion, ammo.position);
                        }
                        ammo.isAlive = false;
                    }
                }
            }

            //Player shots vs enemies
            foreach (Ammo ammo in p.weapon.magazine)
            {
                foreach (Enemy enemy in enemyForce.force)
                {
                    if (ammo.box.Intersects(enemy.box))
                    {


                        enemy.hp -= ammo.damage;

                        if (enemy.hp <= 0)
                        {
                           
                            enemy.isAlive = false;
                            if (enemy is Rusher)
                            {
                                Settings.animator.AddAnimation(Animations.RusherExplosion, ammo.position);
                                p.rushersDestroyed++;
                            }
                            else if (enemy is Fighter)
                            {
                                Settings.animator.AddAnimation(Animations.FighterExplosion, ammo.position);
                                p.fightersDestroyed++;
                            }
                        }
                        else
                        {
                            Settings.animator.AddAnimation(Animations.explosion, ammo.position);
                        }
                        ammo.isAlive = false;
                    }
                }
            }



        }
        private void ShotCollisionEnemy()
        {
            foreach (Enemy e in enemyForce.force)
                foreach (Ammo a in e.weapon.magazine)
                {
                    if (a.box.Intersects(p.box))
                    {
                        p.hp -= a.damage;
                        Settings.animator.AddAnimation(Animations.explosion, a.position);
                        a.isAlive = false;
                    }
                }


        }
        private void Credits(GameTime gameTime)
        {



                string credits = "This Game was build using Microsoft XNA engine"+
                    "\n\nYoutube Channels used for information: " +
                "\n\n \"Gamerdad81\" Youtube Page" +
                "\n\n \"Programming with Mosh\" Youtube Page" +
                "\n\n and TONS of fucking google searches";

           
                float interval = 45;
            CreditTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (CreditTimer > interval)
            {
                CreditPosition.Y -= 3;
                CreditTimer = 0;
            }
            if (CreditPosition .Y== 0)
            {
                CreditsActive = false;
            }
            spriteBatch.DrawString(p.spriteFont, credits, CreditPosition, Color.Green);

        }








    }//End Game1
}//end namespace
