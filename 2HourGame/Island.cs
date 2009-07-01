using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame {
  class Island : DrawableGameComponent {
    public Color Color { get; set; }
    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    public float Radius { get; set; }

    Texture2D islandTexture;
    SpriteBatch spriteBatch;
    Vector2 origin;

    public Island(Game game) : base(game) 
    {
        this.Color = Color.White;
    }

    protected override void LoadContent() {
      spriteBatch = new SpriteBatch(this.GraphicsDevice);
      islandTexture = this.Game.Content.Load<Texture2D>("island");
      origin = new Vector2(islandTexture.Width / 2, islandTexture.Height / 2);
      base.LoadContent();
    }

    public override void Draw(GameTime gameTime) {
      spriteBatch.Begin();
      spriteBatch.Draw(islandTexture, Position, null, Color, Rotation, origin, 1.0f, SpriteEffects.None, 0);
      spriteBatch.End();
      base.Draw(gameTime);
    }
  }
}
