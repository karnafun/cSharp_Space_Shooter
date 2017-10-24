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

    public static class Settings
    {
        static public State state = State.Menu;

        //Configurations for Menu State
        public static int menu_background_speed=1;
        public static SoundManager sounds;
        public static Animator animator;
       

        //General Configurations - Used in Playing mode.
        #region General, background and Texture


        //General
        public static int screen_width = 800;
        public static int screen_height = 950;

        //Background
        public static int background_speed = 5;

        //textures
        public static Texture2D texture_bullet_blue, texture_bullet_red, texture_bullet_yellow;
        public static Texture2D texture_Menu;


        #endregion

        #region Player


        //Player:
        public static Vector2 player_starting_position = new Vector2(screen_width/2-50, screen_height-100);
        public static int player_hp = 500;
        public static int healthbar_rectangles = 20;
        public static int player_speed = 7;
        public static int player_damage = 100;


        #endregion

        #region Weapons and ammo


        //Weapon
        public static int player_weapon_shot_delay = 7;
        public static int enemy_weapon_shot_delay = 13;
        
        //Normal Bullet
        public static int normalBullet_damage = 10;
        public static int normalBullet_speed = 10;
        public static int normalBullet_range = 600;


        #endregion

        #region Enemies and map objects


        //Astroids
        public static int astroids_on_screen = 10;
        public static int astroid_spawn_delay = 15;
        public static int astroid_damage = 15;
        public static int astroid_speed = 4;
        public static int astroid_hp = 100;

       
        //Enemies
        public static int enemy_AI_delay = 70;
        public static int enemy_spawn_delay = 50;
        public static int rushers_on_screen = 3;
        public static int fighters_on_screen = 2;
        public static int enemy_collision_damage = 50;
       
        //Rushers
        public static int rusher_speed = 10;
        public static int rusher_hp = 100;
        public static int rusher_damage= 10;
        public static int rusher_bullet_extra_speed = 4;

        //Fighters
        public static int fighter_speed = 3;
        public static int fighter_hp = 200;
        public static int fighter_damage = 30;


        #endregion

        public static void Configure()
        {
            switch (state)
            {
                case State.Menu:
                    break;
                case State.Playing:
                    break;
                case State.GameOver:
                    break;
                default:
                    break;
            }
        }
        public static void restart()
        {
            
        }

        
    }
}

