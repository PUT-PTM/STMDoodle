using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace PTM
{
    public class PlayerAnimation : Animation
    {
        int frameCounter;
        int switchFrame;

        Vector2 frames;
        Vector2 currentFrame;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Rectangle PlayerRect
        { 
            get { return sourceRect; }
            set { sourceRect = value; }
        }
        public Vector2 Frames
        { 
            set { frames = value; } 
        }
        public Vector2 CurrentFrame
        {
            set { currentFrame = value; }
            get { return currentFrame; }
        }
        public int FrameWidth
        {
            get { return image.Width/(int)frames.X;}
        }
        public int FrameHeight
        {
            get { return image.Height / (int)frames.Y; }
        }
        public override float Alpha
        {
            get
            {
                return base.Alpha;
            }
            set
            {
                base.Alpha = value;
            }
        }
        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager Content, Microsoft.Xna.Framework.Graphics.Texture2D image, string text, Microsoft.Xna.Framework.Vector2 position)
        {
            base.LoadContent(Content, image, text, position);
            frameCounter = 0;
            switchFrame = 100;
            frames = new Vector2(2, 3);///do zmiany
            currentFrame = new Vector2(0,0);                       
        }
        public override void UnloadContent()
        {
            base.UnloadContent();
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {/*
            if(isActive)
            {
                frameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if(frameCounter >= switchFrame)
                {
                    frameCounter = 0;
                    currentFrame.X++;

                    if (currentFrame.X * FrameWidth >= image.Width)
                        currentFrame.X = 0;

                }

            }
            else
            {
                frameCounter = 0;
            }
            */
            sourceRect = new Rectangle((int)currentFrame.X * FrameWidth, (int)currentFrame.Y * FrameHeight,
                FrameWidth, FrameHeight);
        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}
