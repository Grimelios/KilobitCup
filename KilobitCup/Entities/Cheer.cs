using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using KilobitCup.Core;
using KilobitCup.Physics;
using KilobitCup.Twitch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KilobitCup.Entities
{
	/// <summary>
	/// Enumeration storing default cheer types (custom cheers not included).
	/// </summary>
	public enum CheerTypes
	{
		Cheer,
		RIPCheer,
		Kappa,
		Kreygasm,
		SwiftRage,
		FourHead,
		PJSalt,
		MrDestructoid,
		TriHard,
		NotLikeThis,
		FailFish,
		VoHiYo,
		Streamlabs,
		Muxy,
		BDay,
		BitBoss,
		DoodleCheer,
		Invalid
	}

	/// <summary>
	/// Represents an animated cheermote with physics data.
	/// </summary>
	public class Cheer : Entity
	{
		private const int VerticalOffset = 8;

		private static readonly int[] BitThresholds =
		{
			1,
			100,
			1000,
			5000,
			10000
		};

		private Gif gif;
		private Body body;
		private Vector2 positionOffset;

		private bool selfUpdate;

		/// <summary>
		/// Constructs the cheer. Bit value is used to determine which image to use.
		/// </summary>
		public Cheer(CheerTypes type, int bitValue) : base(EntityTypes.Cheer)
		{
			int threshold = 0;

			while (threshold < BitThresholds.Length - 1 && bitValue >= BitThresholds[threshold + 1])
			{
				threshold++;
			}

			gif = new Gif(type.ToString() + threshold);
			positionOffset = new Vector2(Width / 2, VerticalOffset);

			body = PhysicsFactory.CreateRectangle(gif.Width, gif.Height, Units.Pixels, BodyType.Dynamic, this);
			body.Enabled = false;
		}

		/// <summary>
		/// Cheer width (based on the active cheermote).
		/// </summary>
		public int Width => gif.Width;

		/// <summary>
		/// Cheer position.
		/// </summary>
		public override Vector2 Position
		{
			set
			{
				gif.Position = value + positionOffset;

				if (!selfUpdate)
				{
					body.Position = PhysicsConvert.ToMeters(value);
				}

				base.Position = value;
			}
		}

		/// <summary>
		/// Cheer rotation.
		/// </summary>
		public override float Rotation
		{
			// Gif rotation will never be set if not from the body.
			set { gif.Rotation = value; }
		}

		/// <summary>
		/// Enables the physics body.
		/// </summary>
		public void EnablePhysics()
		{
			const float ForceLimit = 3;
			const float AngleLimit = 2;

			Random random = new Random();

			float forceX = (float)random.NextDouble() * ForceLimit - ForceLimit / 2;
			float forceY = (float)random.NextDouble() * ForceLimit / -2;
			float impulse = (float)random.NextDouble() * AngleLimit - AngleLimit / 2;

			body.Enabled = true;
			body.ApplyLinearImpulse(new Vector2(forceX, forceY));
			body.ApplyAngularImpulse(impulse);
		}

		/// <summary>
		/// Updates the cheer.
		/// </summary>
		public override void Update(float dt)
		{
			gif.Update(dt);

			if (body.Enabled)
			{
				selfUpdate = true;
				Position = PhysicsConvert.ToPixels(body.Position);
				Rotation = body.Rotation;
				selfUpdate = false;
			}
		}

		/// <summary>
		/// Draws the sprite.
		/// </summary>
		public override void Draw(SpriteBatch sb)
		{
			gif.Draw(sb);
		}
	}
}
