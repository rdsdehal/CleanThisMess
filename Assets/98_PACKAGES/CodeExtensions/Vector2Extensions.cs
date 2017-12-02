using UnityEngine;

namespace bTools.CodeExtensions
{
	/// <summary>
	/// Extensions for the Unity Vector2 type.
	/// </summary>
	public static class Vector2Extensions
	{
		/// <summary>
		/// Returns this vector with the specified x component.
		/// </summary>
		public static Vector2 WithX( this Vector2 original, float x )
		{
			original.x = x;
			return original;
		}

		/// <summary>
		/// Returns this vector with the specified y component.
		/// </summary>
		public static Vector2 WithY( this Vector2 original, float y )
		{
			original.y = y;
			return original;
		}

		/// <summary>
		/// Rotates a Vector2 by degrees.
		/// </summary>
		/// <param name="vector">Vector to rotate</param>
		/// <param name="degrees">Degrees to rotate the vector by.</param>
		public static Vector2 Rotate( this Vector2 vector, float degrees )
		{
			float sin = Mathf.Sin( degrees * Mathf.Deg2Rad );
			float cos = Mathf.Cos( degrees * Mathf.Deg2Rad );

			float tx = vector.x;
			float ty = vector.y;
			vector.x = ( cos * tx ) - ( sin * ty );
			vector.y = ( sin * tx ) + ( cos * ty );
			return vector;
		}
	}
}