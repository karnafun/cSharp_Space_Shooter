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
    public class Animation
    {
        Texture2D texture;
        Rectangle sourceRect;
        Vector2 position, origion;
        int frames,currentFrame, currentFrameHigh, spriteWidth,spriteHeight , framesLong, framesHigh;
        float timer, interval;
       public  bool isVisible, multiLine;

        public Animation(Texture2D texture,Vector2 position,int frames)
        {
            this.texture = texture;
            interval = 20;
            this.frames = frames;
            currentFrame = 0;
            this.position = position;
            isVisible = true;
            spriteWidth = texture.Width / frames;
            spriteHeight = texture.Height;
            multiLine = false;

        }
        public Animation(Texture2D texture, Vector2 position, int framesLong,int framesHigh,float interval)
        {
            this.texture = texture;
            this.interval = interval;
            this.frames = framesLong + framesHigh;
            this.framesLong = framesLong;
            this.framesHigh = framesHigh;
            currentFrame = 0;
            currentFrameHigh = 0;
            this.position = position;
            isVisible = true;
            spriteWidth = texture.Width / framesLong;
            spriteHeight = texture.Height/framesHigh;
            multiLine = true;

        }


        public void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;

            if (multiLine)
            {
               
                if (timer >= interval)
                {
                    timer = 0;
                    currentFrame++;
                    if (currentFrame >= framesLong)
                    {
                        currentFrame = 0;
                        currentFrameHigh++;
                    }
                    if (currentFrameHigh >= framesHigh)
                    {
                        isVisible = false;
                    }


                    sourceRect = new Rectangle(currentFrame * spriteWidth, currentFrameHigh * spriteHeight, spriteWidth, spriteHeight);
                    
                }
            }
            else
            {
                if (timer >= interval)
                {
                    timer = 0;
                    currentFrame++;
                    if (currentFrame >= frames)
                    {
                        isVisible = false;
                        currentFrame = 0;
                    }
                }

                sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
                

            }

            origion = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);

        }

        public void MultiUpdate(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;

            if (timer >= interval)
            {
                timer = 0;
                currentFrame++;
                if (currentFrame >= frames)
                {
                    isVisible = false;
                    currentFrame = 0;
                }
            }

            sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
            origion = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRect, Color.White, 0f, origion,1,SpriteEffects.None, 0);
            
        }
    }
}
