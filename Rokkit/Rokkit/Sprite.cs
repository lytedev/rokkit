using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rokkit
{
    public class Sprite
    {
        public Texture2D Texture { get; set; }
        public Vector2 Size { get; set; }
        public int Frame { get; set; }
        public int Frames { get; set; }
        public int Time { get; set; }
        public float Rotation { get; set; }
        public Color Overlay { get; set; }
        public SpriteEffects Effects { get; set; }
        public int Layer { get; set; }
        private int timeSince = 0;

        public Sprite(Texture2D texture, float height, float width, float rotation, int frames, int time)
        {
            Texture = texture;
            Size = new Vector2(height, width);
            Rotation = rotation;
            Frame = 0;
            Frames = frames;
            Time = time;
            Overlay = Color.White;
            Effects = SpriteEffects.None;
        }

        public void Update(GameTime gameTime)
        {
            timeSince += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSince > Time)
            {
                timeSince = timeSince - Time;
            }
            Frame++;
            if (Frame > Frames)
            {
                Frame = 0;
            }
        }

        public Vector2 SpriteCoordinates()
        {
            int sheetWidth = (int)Size.X / Texture.Width;
            int sheetHeight = (int)Size.Y / Texture.Height;
            int y = (Frame / sheetWidth) * (int)Size.Y;
            int x = (Frame % sheetWidth) * (int)Size.X;
            return new Vector2(x, y);
        }

        static public Sprite Blank
        {
            get
            {
                return new Sprite(null, 0, 0, 0, 0, 0);
            }
        }
    }
}
