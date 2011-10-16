using System.Collections.Generic;
using System.Collections.ObjectModel;
using _2HourGame.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame.Model
{
    internal class World
    {
        private ContentManager _contentManager;

        public World()
        {
            NewActors = new List<Actor>();
            actors = new List<Actor>();
            GarbageActors = new List<Actor>();

            NewActorViews = new List<ActorView>();
            _actorViews = new List<ActorView>();
            GarbageActorViews = new List<ActorView>();
        }

        private readonly List<Actor> actors;
        public ReadOnlyCollection<Actor> Actors
        {
            get { return new ReadOnlyCollection<Actor>(actors); }
        }
        public List<Actor> NewActors { get; private set; }
        public List<Actor> GarbageActors { get; private set; }

        private readonly List<ActorView> _actorViews;
        public ReadOnlyCollection<ActorView> ActorViews
        {
            get { return new ReadOnlyCollection<ActorView>(_actorViews); }
        }
        public List<ActorView> NewActorViews { get; private set; }
        public List<ActorView> GarbageActorViews { get; private set; }

        public void LoadContent(ContentManager contentManager)
        {
            _contentManager = contentManager;
            foreach (ActorView av in ActorViews)
            {
                av.LoadContent(contentManager);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (Actor actor in actors)
                actor.Update(gameTime);

            foreach (Actor actor in NewActors)
                actors.Add(actor);
            NewActors.Clear();

            foreach (Actor actor in GarbageActors)
                actors.Remove(actor);
            GarbageActors.Clear();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (ActorView av in _actorViews)
            {
                av.Draw(gameTime, spriteBatch);
            }

            foreach (ActorView nav in NewActorViews)
            {
                nav.LoadContent(_contentManager);
                _actorViews.Add(nav);
            }
            NewActorViews.Clear();

            foreach (ActorView av in GarbageActorViews)
            {
                _actorViews.Remove(av);
            }
            GarbageActorViews.Clear();
        }
    }
}