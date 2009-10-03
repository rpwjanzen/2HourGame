using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;

using Microsoft.Xna.Framework;
using _2HourGame.Model.GameServices;

namespace _2HourGame.Model
{
    static class WorldBorder {
        const int buffer = 100;

        public static void AddWorldBorder(Rectangle innerBorder, PhysicsSimulator physicsSimulator, Game game) {
            var outerBorder = new Rectangle(innerBorder.X, innerBorder.X, innerBorder.Width, innerBorder.Height);
            outerBorder.Inflate(buffer, buffer);

            AddLeftBorder(physicsSimulator, innerBorder, outerBorder, game);
            AddRightBorder(physicsSimulator, innerBorder, outerBorder, game);
            AddTopBorder(physicsSimulator, innerBorder, outerBorder, game);
            AddBottomBorder(physicsSimulator, innerBorder, outerBorder, game);
        }

        private static void AddBottomBorder(PhysicsSimulator physicsSimulator, Rectangle innerBorder, Rectangle outerBorder, Game game) {
            var r = CreateBottomBorderRectangle(innerBorder, outerBorder);
            CreateBorder(r, physicsSimulator, game);
        }

        private static void AddTopBorder(PhysicsSimulator physicsSimulator, Rectangle innerBorder, Rectangle outerBorder, Game game) {
            var r = CreateTopBorderRectangle(innerBorder, outerBorder);
            CreateBorder(r, physicsSimulator, game);
        }

        private static void AddRightBorder(PhysicsSimulator physicsSimulator, Rectangle innerBorder, Rectangle outerBorder, Game game) {
            var r = CreateRightBorderRectangle(innerBorder, outerBorder);
            CreateBorder(r, physicsSimulator, game);
        }

        private static void AddLeftBorder(PhysicsSimulator physicsSimulator, Rectangle innerBorder, Rectangle outerBorder, Game game) {
            var r = CreateLeftBorderRectangle(innerBorder, outerBorder);
            CreateBorder(r, physicsSimulator, game);
        }

        private static Rectangle CreateLeftBorderRectangle(Rectangle innerBorder, Rectangle outerBorder) {
            var width = outerBorder.Width - innerBorder.Width;
            var height = outerBorder.Height;
            var x = outerBorder.X - width / 2;
            var y = outerBorder.Y;
            return new Rectangle(x, y, width, height);
        }

        private static Rectangle CreateRightBorderRectangle(Rectangle innerBorder, Rectangle outerBorder) {
            var width = outerBorder.Width - innerBorder.Width;
            var height = outerBorder.Height;
            var x = outerBorder.X + outerBorder.Width - width / 2;
            var y = outerBorder.Y;
            return new Rectangle(x, y, width, height);
        }

        private static Rectangle CreateTopBorderRectangle(Rectangle innerBorder, Rectangle outerBorder) {
            var width = outerBorder.Width;
            var height = outerBorder.Height - innerBorder.Height;
            var x = outerBorder.X;
            var y = outerBorder.Y - height / 2;
            return new Rectangle(x, y, width, height);
        }
        
        private static Rectangle CreateBottomBorderRectangle(Rectangle innerBorder, Rectangle outerBorder) {
            var width = outerBorder.Width;
            var height = outerBorder.Height - innerBorder.Height;
            var x = outerBorder.X;
            var y = outerBorder.Y + outerBorder.Height - height / 2;
            return new Rectangle(x, y, width, height);
        }

        private static void CreateBorder(Rectangle rectangle, PhysicsSimulator physicsSimulator, Game game) {
            var borderWidth = rectangle.Width;
            var borderHeight = rectangle.Height;
            var borderCenter = CalculateCenter(rectangle);

            var borderBody = BodyFactory.Instance.CreateRectangleBody(borderWidth, borderHeight, 1.0f);
            borderBody.IsStatic = true;
            borderBody.Position = borderCenter;
            physicsSimulator.Add(borderBody);

            var borderGeometry = GeomFactory.Instance.CreateRectangleGeom(borderBody, borderWidth, borderHeight);
            // prevent collisions with cannon balls
            borderGeometry.CollisionCategories = ((CollisionCategoryManager)game.Services.GetService(typeof(CollisionCategoryManager))).getCollisionCategory(typeof(WorldBorder));
            borderGeometry.CollidesWith = ((CollisionCategoryManager)game.Services.GetService(typeof(CollisionCategoryManager))).getCollidesWith(typeof(WorldBorder));
            physicsSimulator.Add(borderGeometry);
        }

        private static Vector2 CalculateCenter(Rectangle rectangle) {
            return new Vector2(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2);
        }
    }
}
