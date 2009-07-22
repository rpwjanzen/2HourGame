using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerGames.FarseerPhysics;
using FarseerGames.SimpleSamplesXNA;

using Microsoft.Xna.Framework.Input;

namespace _2HourGame {
    class PhysicsComponent : DrawableGameComponent {
        PhysicsSimulator physicsSimulator;
        PhysicsSimulatorView physicsSimulatorView;        
        SpriteBatch spriteBatch;

        public bool Debug { get; set; }

        public PhysicsComponent(Game game, PhysicsSimulator physicsSimulator)
            : base(game) {
            physicsSimulatorView = new PhysicsSimulatorView(physicsSimulator);
            this.physicsSimulator = physicsSimulator;
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(this.GraphicsDevice);
            physicsSimulatorView.LoadContent(this.GraphicsDevice, this.Game.Content);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime) {
            if (Debug) {
                spriteBatch.Begin();
                physicsSimulatorView.Draw(spriteBatch);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        GamePadState LastState;

        public override void Update(GameTime gameTime) {
            this.physicsSimulator.Update(0.1f);

            GamePadState gs = GamePad.GetState(PlayerIndex.One);
            if (gs.IsConnected) {
                if (gs.IsButtonDown(Buttons.Y) && LastState.IsButtonUp(Buttons.Y)) {
                    this.Debug = !this.Debug;
                }
                LastState = gs;
            }
            base.Update(gameTime);
        }
    }
}
