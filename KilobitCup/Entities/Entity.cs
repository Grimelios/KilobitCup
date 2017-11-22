using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KilobitCup.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KilobitCup.Entities
{
	/// <summary>
	/// Enumeration storing entity types. Used to organize entities within the scene.
	/// </summary>
	public enum EntityTypes
	{
		Cheer,
		Message
	}

	/// <summary>
	/// Represents an abstract entity. As entity is anything that can be updated and drawn on the screen.
	/// </summary>
	public abstract class Entity : IPositionable, IDynamic, IRenderable
	{
		private Vector2 position;

		/// <summary>
		/// Constructs the entity.
		/// </summary>
		protected Entity(EntityTypes entityType)
		{
			EntityType = entityType;
		}

		/// <summary>
		/// Entity position.
		/// </summary>
		public virtual Vector2 Position
		{
			get { return position; }
			set { position = value; }
		}

		/// <summary>
		/// Entity type.
		/// </summary>
		public EntityTypes EntityType { get; }

		/// <summary>
		/// Reference to the scene. Used to add and remove entities from the scene more easily.
		/// </summary>
		public Scene Scene { get; set; }

		/// <summary>
		/// Updates the entity.
		/// </summary>
		public virtual void Update(float dt)
		{
		}

		/// <summary>
		/// Draws the entity.
		/// </summary>
		public virtual void Draw(SpriteBatch sb)
		{
		}
	}
}
