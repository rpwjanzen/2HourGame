using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    internal class WorldBorder
    {
        private const int buffer = 100;

        public WorldBorder(Rectangle innerBorder, PhysicsSimulator physicsSimulator)
        {
            var outerBorder = new Rectangle(innerBorder.X, innerBorder.X, innerBorder.Width, innerBorder.Height);
            outerBorder.Inflate(buffer, buffer);

            AddLeftBorder(physicsSimulator, innerBorder, outerBorder);
            AddRightBorder(physicsSimulator, innerBorder, outerBorder);
            AddTopBorder(physicsSimulator, innerBorder, outerBorder);
            AddBottomBorder(physicsSimulator, innerBorder, outerBorder);
        }

        private void AddBottomBorder(PhysicsSimulator physicsSimulator, Rectangle innerBorder, Rectangle outerBorder)
        {
            Rectangle r = CreateBottomBorderRectangle(innerBorder, outerBorder);
            CreateBorder(r, physicsSimulator);
        }

        private void AddTopBorder(PhysicsSimulator physicsSimulator, Rectangle innerBorder, Rectangle outerBorder)
        {
            Rectangle r = CreateTopBorderRectangle(innerBorder, outerBorder);
            CreateBorder(r, physicsSimulator);
        }

        private void AddRightBorder(PhysicsSimulator physicsSimulator, Rectangle innerBorder, Rectangle outerBorder)
        {
            Rectangle r = CreateRightBorderRectangle(innerBorder, outerBorder);
            CreateBorder(r, physicsSimulator);
        }

        private void AddLeftBorder(PhysicsSimulator physicsSimulator, Rectangle innerBorder, Rectangle outerBorder)
        {
            Rectangle r = CreateLeftBorderRectangle(innerBorder, outerBorder);
            CreateBorder(r, physicsSimulator);
        }

        private Rectangle CreateLeftBorderRectangle(Rectangle innerBorder, Rectangle outerBorder)
        {
            int width = outerBorder.Width - innerBorder.Width;
            int height = outerBorder.Height;
            int x = outerBorder.X - width/2;
            int y = outerBorder.Y;
            return new Rectangle(x, y, width, height);
        }

        private Rectangle CreateRightBorderRectangle(Rectangle innerBorder, Rectangle outerBorder)
        {
            int width = outerBorder.Width - innerBorder.Width;
            int height = outerBorder.Height;
            int x = outerBorder.X + outerBorder.Width - width/2;
            int y = outerBorder.Y;
            return new Rectangle(x, y, width, height);
        }

        private Rectangle CreateTopBorderRectangle(Rectangle innerBorder, Rectangle outerBorder)
        {
            int width = outerBorder.Width;
            int height = outerBorder.Height - innerBorder.Height;
            int x = outerBorder.X;
            int y = outerBorder.Y - height/2;
            return new Rectangle(x, y, width, height);
        }

        private Rectangle CreateBottomBorderRectangle(Rectangle innerBorder, Rectangle outerBorder)
        {
            int width = outerBorder.Width;
            int height = outerBorder.Height - innerBorder.Height;
            int x = outerBorder.X;
            int y = outerBorder.Y + outerBorder.Height - height/2;
            return new Rectangle(x, y, width, height);
        }

        private void CreateBorder(Rectangle rectangle, PhysicsSimulator physicsSimulator)
        {
            int borderWidth = rectangle.Width;
            int borderHeight = rectangle.Height;
            Vector2 borderCenter = CalculateCenter(rectangle);

            Body borderBody = BodyFactory.Instance.CreateRectangleBody(borderWidth, borderHeight, 1.0f);
            borderBody.IsStatic = true;
            borderBody.Position = borderCenter;
            physicsSimulator.Add(borderBody);

            Geom borderGeometry = GeomFactory.Instance.CreateRectangleGeom(borderBody, borderWidth, borderHeight);
            physicsSimulator.Add(borderGeometry);
        }

        private Vector2 CalculateCenter(Rectangle rectangle)
        {
            return new Vector2(rectangle.X + rectangle.Width/2, rectangle.Y + rectangle.Height/2);
        }
    }
}