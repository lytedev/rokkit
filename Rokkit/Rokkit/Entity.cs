using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Collision;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Controllers;
using FarseerPhysics.Collision.Shapes;

namespace Rokkit
{
    public class Entity
    {
        #region Private Properties
        Sprite _sprite = Sprite.Blank;
        EntityFlags _flags = EntityFlags.None;
        #endregion

        #region Public Properties
        /// <summary>
        /// Returns the Position of the Entity or sets a new Position.
        /// </summary>
        public Vector2 Position 
        {
            set
            {
                Body.Position = value;
            }
            get
            {
                return Body.Position;
            }
        }

        /// <summary>
        /// Returns the Velocity of the Entity or sets a new Velocity. 
        /// </summary>
        public Vector2 Velocity 
        {
            set
            {
                Body.LinearVelocity = value;
            }
            get
            {
                return Body.LinearVelocity;
            }
        }

        /// <summary>
        /// Returns the bounded Size of the Entity or sets a new bounded Size.
        /// </summary>
        public Vector2 Size 
        {
            set
            {
                Sprite.Size = value;
            }
            get
            {
                return Sprite.Size;
            }
        }

        /// <summary>
        /// Returns the Fixture of the Entity or sets a new Body. 
        /// </summary>
        public Body Body
        {
            set;
            get;
        }

        /// <summary>
        /// Returns the Entity's Sprite or sets a new Sprite. 
        /// </summary>
        public Sprite Sprite 
        {
            set
            {
                _sprite = value;
            }
            get
            {
                return _sprite;
            }
        }

        /// <summary>
        /// Returns the Entity's flags or sets new flags.
        /// </summary>
        public EntityFlags Flags
        {
            set
            {
                _flags = value;
            }
            get
            {
                return _flags;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Create an Entity with a Sprite only.
        /// </summary>
        /// <param name="sprite">Any initialized Sprite.</param>
        public Entity(Sprite sprite)
        {
            Sprite = sprite;
            Initialize();
        }

        /// <summary>
        /// Create an Entity with a Sprite and Position. 
        /// </summary>
        /// <param name="sprite">Any initialized Sprite.</param>
        /// <param name="position">Initial Entity position.</param>
        public Entity(Sprite sprite, Vector2 position)
        {
            Sprite = sprite;
            Initialize();
            Position = position;
        }

        /// <summary>
        /// Create an Entity with a Sprite and Position. 
        /// </summary>
        /// <param name="sprite">Any initialized Sprite.</param>
        /// <param name="position">Initial Entity position.</param>
        /// <param name="velocity">Initial Entity velocity.</param>
        public Entity(Sprite sprite, Vector2 position, Vector2 velocity)
        {
            Sprite = sprite;
            Initialize();
            Position = position;
            Velocity = velocity;
        }

        /// <summary>
        /// Create an Entity with a Sprite and Position. 
        /// </summary>
        /// <param name="sprite">Any initialized Sprite.</param>
        /// <param name="position">Initial Entity position.</param>
        /// <param name="velocity">Initial Entity velocity.</param>
        /// <param name="flags">Any specified set of EntityFlags.</param>
        public Entity(Sprite sprite, Vector2 position, Vector2 velocity, EntityFlags flags)
        {
            Sprite = sprite;
            Initialize();
            Position = position;
            Velocity = velocity;
            Flags = flags;
        }
        #endregion

        #region Private Functions
        public bool Collision(Fixture f1, Fixture f2, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            return true;
        }

        private void Initialize()
        {
            Body = new Body(Main.World);
            Body.BodyType = BodyType.Dynamic;
            float width = (Size.X / 2);
            float height = (Size.Y / 2);

            PolygonShape ps = new PolygonShape(new Vertices(new Vector2[4] {
                new Vector2(-width, -height),
                new Vector2(width, -height),
                new Vector2(width, height),
                new Vector2(-width, height)
            }), 1);

            Body.CreateFixture(ps);
            
            /*Texture2D polygonTexture = Sprite.Texture;
            uint[] data = new uint[polygonTexture.Width * polygonTexture.Height];
            polygonTexture.GetData(data);
            Vertices verts = PolygonTools.CreatePolygon(data, polygonTexture.Width, polygonTexture.Height, true);
            Vector2 scale = new Vector2(1, 1);
            verts.Scale(ref scale);
            List<Vertices> v = FarseerPhysics.Common.Decomposition.BayazitDecomposer.ConvexPartition(verts);
            List<Fixture> compound = FixtureFactory.CreateCompoundPolygon(Main.World, v, 1);*/
            
            // Body.FixtureList.AddRange(compound);

            foreach (Fixture f in Body.FixtureList)
            {
                f.OnCollision += Collision;
                f.CollisionFilter.CollidesWith = Category.All;
            }

            if (HasFlags(EntityFlags.CanCollide))
            {
                
            }
            if (HasFlags(EntityFlags.CanFall))
            {

            }
            if (HasFlags(EntityFlags.CanMove))
            {

            }
            if (HasFlags(EntityFlags.CanRotate))
            {

            }
        }
        #endregion

        #region Public Functions
        public void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite.Texture, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), null, Sprite.Overlay, Body.Rotation, Sprite.SpriteCoordinates(), Sprite.Effects, Sprite.Layer);
        }
        #endregion

        #region Static Functions
        public bool HasFlags(EntityFlags flag)
        {
            return (Flags & flag) == flag;
        }
        #endregion
    }

    [Flags]
    public enum EntityFlags : byte
    {
        None,
        CanCollide,
        CanDie,
        CanFall,
        CanMove, 
        CanRotate
    }
}
