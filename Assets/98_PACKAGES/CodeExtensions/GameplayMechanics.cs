using UnityEngine;

namespace bTools.CodeExtensions
{
	/// <summary>
	/// <p>Abstraction of the common cooldown pattern using Time.time.</p>
	/// <p>Cooldowns initialised in the inspector are started when the game is started.</p>
	/// </summary>
	[System.Serializable]
	public class Cooldown
	{
		[SerializeField, HideInInspector]
		private float m_duration;

		/// <summary>
		/// The current duration of this Cooldown.
		/// </summary>
		public float duration
		{
			get
			{
				return m_duration;
			}
		}

		/// <summary>
		/// Returns true if this cooldown has elapsed.
		/// </summary>
		public bool isDone
		{
			get
			{
				return Time.time > timestamp;
			}
		}

		private float timestamp = 0.0f;

		/// <summary>
		/// Creates a new Cooldown without starting it.
		/// </summary>
		public Cooldown() { }

		/// <summary>
		/// Creates a new Cooldown and starts it.
		/// </summary>
		public Cooldown( float duration )
		{
			m_duration = duration;
			timestamp = Time.time;
		}

		/// <summary>
		/// Starts this cooldown with a new duration.
		/// </summary>
		public void Start( float duration )
		{
			this.m_duration = duration;
			timestamp = Time.time + duration;
		}

		/// <summary>
		/// Restarts this cooldown using the last specified duration.
		/// </summary>
		public void Restart()
		{
			timestamp = Time.time + duration;
		}
	}

	/// <summary>
	/// <p>Abstraction of the common cooldown pattern using Time.unscaledTime.</p>
	/// <p>Cooldowns initialised in the inspector are started when the game is started.</p>
	/// </summary>
	[System.Serializable]
	public class UnscaledCooldown
	{
		[SerializeField, HideInInspector]
		private float m_duration;

		/// <summary>
		/// The current duration of this Cooldown.
		/// </summary>
		public float duration
		{
			get
			{
				return m_duration;
			}
		}

		/// <summary>
		/// Returns true if this cooldown has elapsed.
		/// </summary>
		public bool isDone
		{
			get
			{
				return Time.unscaledTime > timestamp;
			}
		}

		private float timestamp = 0.0f;

		/// <summary>
		/// Creates a new Cooldown without starting it.
		/// </summary>
		public UnscaledCooldown() { }

		/// <summary>
		/// Creates a new Cooldown and starts it.
		/// </summary>
		public UnscaledCooldown( float duration )
		{
			m_duration = duration;
			timestamp = Time.unscaledTime;
		}

		/// <summary>
		/// Starts this cooldown with a new duration.
		/// </summary>
		public void Start( float duration )
		{
			this.m_duration = duration;
			timestamp = Time.unscaledTime + duration;
		}

		/// <summary>
		/// Restarts this cooldown using the last specified duration.
		/// </summary>
		public void Restart()
		{
			timestamp = Time.unscaledTime + duration;
		}
	}

	/// <summary>
	/// Returns a new random int from the specified (inclusive) range.
	/// </summary>
	[System.Serializable]
	public struct RandomInt
	{
		/// <summary>
		/// The current minimum.
		/// </summary>

		public int min;
		/// <summary>
		/// The current maximum.
		/// </summary>
		public int max;

		/// <summary>
		/// Last int generated using ```Next()```.
		/// </summary>
		public int last { get; private set; }

		/// <param name="min">Inclusive minimum</param>
		/// <param name="max">Inclusive maximum</param>
		public RandomInt( int min, int max ) : this()
		{
			this.min = min;
			this.max = max;
		}

		/// <summary>
		/// Returns a new int within the range and stores it in ```last```.
		/// </summary>
		public int Next()
		{
			last = Random.Range( min, max + 1 );
			return last;
		}
	}

	/// <summary>
	/// Returns a new random float from the specified (inclusive) range.
	/// </summary>
	[System.Serializable]
	public struct RandomFloat
	{
		/// <summary>
		/// The current minimum.
		/// </summary>
		public float min;

		/// <summary>
		/// The current maximum.
		/// </summary>
		public float max;

		/// <summary>
		/// Last float generated using ```Next()```.
		/// </summary>
		public float last { get; private set; }

		/// <param name="min">Inclusive minimum</param>
		/// <param name="max">Inclusive maximum</param>
		public RandomFloat( float min, float max ) : this()
		{
			this.min = min;
			this.max = max;
		}

		/// <summary>
		/// Returns a new float within the range and stores it in ```last```.
		/// </summary>
		public float Next()
		{
			last = Random.Range( min, max );
			return last;
		}
	}
}