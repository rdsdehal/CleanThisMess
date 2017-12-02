using UnityEngine;

namespace bTools.CodeExtensions
{
	public class RandomExtensions
	{
		/// <summary>
		/// Returns true or false with a 50/50 chance
		/// </summary>
		public static bool randomBool
		{
			get
			{
				return Random.Range( 0.0f, 1.0f ) >= 0.5f;
			}
		}
	}
}