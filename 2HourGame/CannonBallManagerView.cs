using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame {
    class CannonBallManagerView : DrawableGameComponent {
        CannonBallManager CannonBallManager { get; set; }
        IEnumerable<CanonBall> CannonBalls {
            get { return CannonBallManager.CannonBalls; }
        }

        Texture2D Texture { get; set; }
        SpriteBatch SpriteBatch { get; set; }
        float LayerDepth { get; set; }
        Vector2 Origin { get; set; }
        float Scale { get; set; }

        public CannonBallManagerView(Game game, CannonBallManager cannonBallManager, SpriteBatch spriteBatch, float layerDepth) : base(game) {
            this.CannonBallManager = cannonBallManager;
            this.SpriteBatch = spriteBatch;
            this.LayerDepth = layerDepth;
            this.Scale = 1.0f;
        }

        protected override void LoadContent() {
            this.Texture = this.Game.Content.Load<Texture2D>("cannonBall");
            this.Origin = new Vector2(this.Texture.Width / 2.0f, this.Texture.Height / 2.0f);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime) {
            foreach (var cannonBall in this.CannonBalls) {
                SpriteBatch.Draw(this.Texture, cannonBall.Position, null, cannonBall.Color, cannonBall.Rotation, this.Origin, this.Scale, SpriteEffects.None, this.LayerDepth);
            }
            base.Draw(gameTime);
        }
    }
}
