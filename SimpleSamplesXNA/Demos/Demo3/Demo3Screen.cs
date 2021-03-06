using System.Text;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;
using FarseerGames.SimpleSamplesXNA.Demos.DemoShare;
using FarseerGames.SimpleSamplesXNA.DrawingSystem;
using FarseerGames.SimpleSamplesXNA.ScreenSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FarseerGames.SimpleSamplesXNA.Demos.Demo3
{
    public class Demo3Screen : GameScreen
    {
        private Agent _agent;

        private Body[] _obstacleBodies;
        private Geom[] _obstacleGeoms;
        private RectangleBrush _obstacleBrush;

        public override void Initialize()
        {
            PhysicsSimulator = new PhysicsSimulator(new Vector2(0, 100));
            PhysicsSimulatorView = new PhysicsSimulatorView(PhysicsSimulator);

            base.Initialize();
        }

        public override void LoadContent()
        {
            _agent = new Agent(new Vector2(ScreenManager.ScreenCenter.X, 100));
            _agent.Load(ScreenManager.GraphicsDevice, PhysicsSimulator);

            LoadObstacles();

            base.LoadContent();
        }

        public void LoadObstacles()
        {
            _obstacleBrush = new RectangleBrush(128, 32, Color.White, Color.Black);
            _obstacleBrush.Load(ScreenManager.GraphicsDevice);

            _obstacleBodies = new Body[5];
            _obstacleGeoms = new Geom[5];
            for (int i = 0; i < _obstacleBodies.Length; i++)
            {
                _obstacleBodies[i] = BodyFactory.Instance.CreateRectangleBody(PhysicsSimulator, 128, 32, 1);
                _obstacleBodies[i].IsStatic = true;

                if (i == 0)
                {
                    _obstacleGeoms[i] = GeomFactory.Instance.CreateRectangleGeom(PhysicsSimulator, _obstacleBodies[i], 128,
                                                                                32);
                    _obstacleGeoms[i].RestitutionCoefficient = .2f;
                    _obstacleGeoms[i].FrictionCoefficient = .2f;
                }
                else
                {
                    _obstacleGeoms[i] = GeomFactory.Instance.CreateGeom(PhysicsSimulator, _obstacleBodies[i],
                                                                       _obstacleGeoms[0]);
                }
            }

            _obstacleBodies[0].Position = ScreenManager.ScreenCenter + new Vector2(-50, -200);
            _obstacleBodies[1].Position = ScreenManager.ScreenCenter + new Vector2(150, -100);
            _obstacleBodies[2].Position = ScreenManager.ScreenCenter + new Vector2(100, 50);
            _obstacleBodies[3].Position = ScreenManager.ScreenCenter + new Vector2(-100, 200);
            _obstacleBodies[4].Position = ScreenManager.ScreenCenter + new Vector2(-170, 0);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            DrawObstacles();

            _agent.Draw(ScreenManager.SpriteBatch);

            ScreenManager.SpriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawObstacles()
        {
            for (int i = 0; i < _obstacleBodies.Length; i++)
            {
                _obstacleBrush.Draw(ScreenManager.SpriteBatch, _obstacleBodies[i].Position, _obstacleBodies[i].Rotation);
            }
        }

        public override void HandleInput(InputState input)
        {
            if (firstRun)
            {
                ScreenManager.AddScreen(new PauseScreen(GetTitle(), GetDetails(), this));
                firstRun = false;
            }
            if (input.PauseGame)
            {
                ScreenManager.AddScreen(new PauseScreen(GetTitle(), GetDetails(), this));
            }

            if (input.CurrentGamePadState.IsConnected)
            {
                HandleGamePadInput(input);
            }
            else
            {
                HandleKeyboardInput(input);
            }
            base.HandleInput(input);
        }

        private void HandleGamePadInput(InputState input)
        {
            Vector2 force = 800 * input.CurrentGamePadState.ThumbSticks.Left;
            force.Y = -force.Y;
            _agent.Body.ApplyForce(force);

            float rotation = -8000 * input.CurrentGamePadState.Triggers.Left;
            _agent.Body.ApplyTorque(rotation);

            rotation = 8000 * input.CurrentGamePadState.Triggers.Right;
            _agent.Body.ApplyTorque(rotation);
        }

        private void HandleKeyboardInput(InputState input)
        {
            const float forceAmount = 800;
            Vector2 force = Vector2.Zero;
            force.Y = -force.Y;

            if (input.CurrentKeyboardState.IsKeyDown(Keys.A)) { force += new Vector2(-forceAmount, 0); }
            if (input.CurrentKeyboardState.IsKeyDown(Keys.S)) { force += new Vector2(0, forceAmount); }
            if (input.CurrentKeyboardState.IsKeyDown(Keys.D)) { force += new Vector2(forceAmount, 0); }
            if (input.CurrentKeyboardState.IsKeyDown(Keys.W)) { force += new Vector2(0, -forceAmount); }

            _agent.Body.ApplyForce(force);

            const float torqueAmount = 8000;
            float torque = 0;

            if (input.CurrentKeyboardState.IsKeyDown(Keys.Left)) { torque -= torqueAmount; }
            if (input.CurrentKeyboardState.IsKeyDown(Keys.Right)) { torque += torqueAmount; }

            _agent.Body.ApplyTorque(torque);
        }

        public static string GetTitle()
        {
            return "Demo3: Multiple geometries and static bodies";
        }

        public static string GetDetails()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("This demo shows a single body with multiple geometry");
            sb.AppendLine("objects attached.  The yellow circles are offset");
            sb.AppendLine("from the bodies center. The body itself is created");
            sb.AppendLine("using 'CreateRectangleBody' so that it's moment of");
            sb.AppendLine("inertia is that of a rectangle.");
            sb.AppendLine(string.Empty);
            sb.AppendLine("This demo also shows the use of static bodies.");
            sb.AppendLine(string.Empty);
            sb.AppendLine("GamePad:");
            sb.AppendLine("  -Rotate: left and right triggers");
            sb.AppendLine("  -Move: left thumbstick");
            sb.AppendLine(string.Empty);
            sb.AppendLine("Keyboard:");
            sb.AppendLine("  -Rotate: left and right arrows");
            sb.AppendLine("  -Move: A,S,D,W");
            sb.AppendLine(string.Empty);
            sb.AppendLine("Mouse");
            sb.AppendLine("  -Hold down left button and drag");
            return sb.ToString();
        }
    }
}