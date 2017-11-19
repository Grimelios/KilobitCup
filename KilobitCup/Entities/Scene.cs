using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KilobitCup.Data;
using KilobitCup.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KilobitCup.Entities
{
	/// <summary>
	/// Represents a collection of entities that are updated and drawn each frame in a preset order. This class also listens to certain
	/// events in order to create new entities.
	/// </summary>
	public class Scene : IMessageReceiver, IDynamic, IRenderable
	{
		private List<Entity>[] entities;
		private List<Entity> addList;
		private List<Entity> removeList;

		private EntityTypes[] updateOrder;
		private EntityTypes[] drawOrder;

		/// <summary>
		/// Constructs the class.
		/// </summary>
		public Scene()
		{
			updateOrder = new []
			{
				EntityTypes.Message,
				EntityTypes.Cheer
			};

			drawOrder = new []
			{
				EntityTypes.Message,
				EntityTypes.Cheer
			};

			entities = new List<Entity>[CoreFunctions.EnumCount<EntityTypes>()];
			addList = new List<Entity>();
			removeList = new List<Entity>();

			for (int i = 0; i < entities.Length; i++)
			{
				entities[i] = new List<Entity>();
			}

			MessageSystem.Subscribe(MessageTypes.Bits, this);
		}

		/// <summary>
		/// Receives messages.
		/// </summary>
		public void Receive(MessageTypes messageType, object data)
		{
			ProcessBits((BitData)data);
		}

		/// <summary>
		/// Processes bit events.
		/// </summary>
		private void ProcessBits(BitData data)
		{
			int messageCount = entities[(int)EntityTypes.Message].Count;

			Add(new ScrollingMessage(data.Message, messageCount));
		}

		/// <summary>
		/// Adds the given entity to the scene.
		/// </summary>
		public void Add(Entity entity)
		{
			addList.Add(entity);
		}

		/// <summary>
		/// Removes the given entity from the scene.
		/// </summary>
		public void Remove(Entity entity)
		{
			removeList.Add(entity);
		}

		/// <summary>
		/// Updates the scene.
		/// </summary>
		public void Update(float dt)
		{
			foreach (EntityTypes type in updateOrder)
			{
				entities[(int)type].ForEach(e => e.Update(dt));
			}

			removeList.ForEach(e =>
			{
				entities[(int)e.EntityType].Remove(e);
			});

			addList.ForEach(e =>
			{
				e.Scene = this;
				entities[(int)e.EntityType].Add(e);
			});

			addList.Clear();
			removeList.Clear();
		}

		/// <summary>
		/// Draws the scene.
		/// </summary>
		public void Draw(SpriteBatch sb)
		{
			foreach (EntityTypes type in drawOrder)
			{
				entities[(int)type].ForEach(e => e.Draw(sb));
			}
		}
	}
}
