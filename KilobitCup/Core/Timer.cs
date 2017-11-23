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
		private Action<float> tick;
		private Action<float> nonRepeatingTrigger;
		private Func<float, bool> repeatingTrigger;

		/// <summary>
		/// Constructs the timer.
		/// </summary>
		public Timer(float duration, Action<float> nonRepeatingTrigger, float initialElapsed = 0) :
			this(duration, null, nonRepeatingTrigger)
		{
		}

		/// <summary>
		/// Constructs the timer.
		/// </summary>
		public Timer(float duration, Action<float> tick, Action<float> nonRepeatingTrigger, float initialElapsed = 0)
		{
			this.tick = tick;
			this.nonRepeatingTrigger = nonRepeatingTrigger;

			Duration = duration;
			Elapsed = initialElapsed;
		}

		/// <summary>
		/// Constructs the timer.
		/// </summary>
		public Timer(float duration, Func<float, bool> repeatingTrigger, float initialElapsed = 0) :
			this(duration, null, repeatingTrigger)
		{
		}

		/// <summary>
		/// Constructs the timer.
		/// </summary>
		public Timer(float duration, Action<float> tick, Func<float, bool> repeatingTrigger, float initialElapsed = 0)
		{
			this.tick = tick;
			this.repeatingTrigger = repeatingTrigger;

			Duration = duration;
			Elapsed = initialElapsed;
		}

		/// <summary>
		/// Elapsed time.
		/// </summary>
		public float Elapsed { get; private set; }

		/// <summary>
		/// Timer duration.
		/// </summary>
		public float Duration { get; set; }

		/// <summary>
		/// Whether the timer is complete.
		/// </summary>
		public bool Complete { get; private set; }

		/// <summary>
		/// Whether the timer is paused.
		/// </summary>
		public bool Paused { get; set; }

		/// <summary>
		/// Resets the timer.
		/// </summary>
		public void Reset()
		{
			Elapsed = 0;
			Complete = false;
		}

		/// <summary>
		/// Updates the class.
		/// </summary>
		public void Update(float dt)
		{
			if (Paused)
			{
				return;
			}

			Elapsed += dt * 1000;

			while (Elapsed > Duration)
			{
				Elapsed -= Duration;

				if (nonRepeatingTrigger != null)
				{
					// Calling the tick function in this way prevents having to handle the final tick in the trigger function.
					tick?.Invoke(1);
					nonRepeatingTrigger(Elapsed);
					Complete = true;

					return;
				}

				// Repeating triggers can return false to end the timer early.
				if (!repeatingTrigger(Elapsed))
				{
					Complete = true;

					return;
				}
			}

			tick?.Invoke(Elapsed / Duration);
		}
	}
}
