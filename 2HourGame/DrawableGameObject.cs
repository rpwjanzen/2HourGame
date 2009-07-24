using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame {
    class DrawableGameObject : GameObject {
        public virtual Rectangle SourceRectangle { get; private set; }
        public virtual Texture2D Texture { get; private set; }
        public virtual Color Color { get; private set; }
        public virtual Vector2 Origin { get; private set; }        
        public virtual float Scale { get; private set; }
        public virtual SpriteEffects SpriteEffect { get; private set; }
        public virtual float LayerDepth { get; private set; }
        
        public DrawableGameObject(Vector2 position, float rotation, Rectangle sourceRectangle, Texture2D texture, Color color, Vector2 origin, float scale, SpriteEffects spriteEffect, float layerDepth) : base(position, rotation) {
            this.SourceRectangle = sourceRectangle;
            this.Texture = texture;
            this.Color = color;
            this.Origin = origin;
            this.Scale = scale;
            this.SpriteEffect = spriteEffect;
            this.LayerDepth = layerDepth;
        }

        public virtual void DrawWith(SpriteBatch spriteBatch) {
            spriteBatch.Draw(
                this.Texture,
                this.Position,
                this.SourceRectangle,
                this.Color,
                this.Rotation,
                this.Origin,
                this.Scale,
                this.SpriteEffect,
                this.LayerDepth);
        }
    }
}
