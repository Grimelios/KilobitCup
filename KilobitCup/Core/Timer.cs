using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KilobitCup.Interfaces;

namespace KilobitCup.Core
{
	/// <summary>
	/// Represents a timer. Timers call a function when triggered and can optionally call a function on each tick.
	/// </summary>
	public class Timer : IDynamic
	{
		private float elapsed;
		private float duration;

		private Action<float> tick;
		private Action<float> nonRepeatingTrigger;
		private Func<float, bool> repeatingTrigger;

		/// <summary>
		/// Constructs the timer.
		/// </summary>
		public Timer(float duration, Action<float> nonRepeatingTrigger) :
			this(duration, null, nonRepeatingTrigger)
		{
		}

		/// <summary>
		/// Constructs the timer.
		/// </summary>
		public Timer(float duration, Action<float> tick, Action<float> nonRepeatingTrigger)
		{
			this.duration = duration;
			this.tick = tick;
			this.nonRepeatingTrigger = nonRepeatingTrigger;
		}

		/// <summary>
		/// Constructs the timer.
		/// </summary>
		public Timer(float duration, Func<float, bool> repeatingTrigger) :
			this(duration, null, repeatingTrigger)
		{
		}

		/// <summary>
		/// Constructs the timer.
		/// </summary>
		public Timer(float duration, Action<float> tick, Func<float, bool> repeatingTrigger)
		{
			this.duration = duration;
			this.tick = tick;
			this.repeatingTrigger = repeatingTrigger;
		}

		/// <summary>
		/// Whether the timer is complete.
		/// </summary>
		public bool Complete { get; private set; }

		/// <summary>
		/// Updates the class.
		/// </summary>
		public void Update(float dt)
		{
			elapsed += dt * 1000;

			while (elapsed > duration)
			{
				elapsed -= duration;

				if (nonRepeatingTrigger != null)
				{
					// Calling the tick function in this way prevents having to handle the final tick in the trigger function.
					tick?.Invoke(1);
					nonRepeatingTrigger(elapsed);
					Complete = true;

					return;
				}

				// Repeating triggers can return false to end the timer early.
				if (!repeatingTrigger(elapsed))
				{
					Complete = true;

					return;
				}
			}

			tick?.Invoke(elapsed / duration);
		}
	}
}
