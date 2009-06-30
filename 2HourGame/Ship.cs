﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame {
  class Ship : DrawableGameComponent {
    public Color Color { get; set; }
    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    public float Radius { get; set; }

    SpriteBatch spriteBatch;
    Texture2D shipTexture;
    Vector2 origin;

    public Ship(Game game) : base(game) { }

    public void Offset(float dx, float dy) {
      this.Position = new Vector2(this.Position.X + dx, this.Position.Y + dy);
    }

    public void Offset(Vector2 o) {
      this.Position += o;
    }

    public void RotateRadians(float radians) {
      this.Rotation += radians;
      this.Rotation %= MathHelper.TwoPi;
    }

    public void RotateDegrees(float degrees) {
      this.RotateRadians(MathHelper.ToRadians(degrees));
    }

    protected override void LoadContent() {
      spriteBatch = new SpriteBatch(this.GraphicsDevice);
      shipTexture = this.Game.Content.Load<Texture2D>("Ship");
      origin = new Vector2(shipTexture.Width / 2, shipTexture.Height / 2);
      base.LoadContent();
    }

    public override void Draw(GameTime gameTime) {
      spriteBatch.Begin();
      spriteBatch.Draw(shipTexture, Position, null, Color, Rotation, origin, 1.0f, SpriteEffects.None, 0);
      spriteBatch.End();

      base.Draw(gameTime);
    }

  }
}
