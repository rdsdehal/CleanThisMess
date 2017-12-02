using UnityEngine;

namespace bTools.CodeExtensions
{
	/// <summary>
	/// Extensions for the Unity Vector3 Type.
	/// </summary>
	public static class Vector3Extensions
	{
		/// <summary>
		/// Returns this vector with the specified x component.
		/// </summary>
		public static Vector3 WithX( this Vector3 original, float x )
		{
			original.x = x;
			return original;
		}

		/// <summary>
		/// Returns this vector with the specified y component.
		/// </summary>
		public static Vector3 WithY( this Vector3 original, float y )
		{
			original.y = y;
			return original;
		}

		/// <summary>
		/// Returns this vector with the specified z component.
		/// </summary>
		public static Vector3 WithZ( this Vector3 original, float z )
		{
			original.z = z;
			return original;
		}

		/// <summary>
		/// Returns this vector with the specified x and y component.
		/// </summary>
		public static Vector3 WithXY( this Vector3 original, Vector2 XY )
		{
			original.x = XY.x;
			original.y = XY.y;

			return original;
		}

		/// <summary>
		/// Returns this vector with the specified x and y component.
		/// </summary>
		public static Vector3 WithXY( this Vector3 original, float x, float y )
		{
			original.x = x;
			original.y = y;

			return original;
		}

		/// <summary>
		/// Returns this vector with the specified x and zcomponent.
		/// </summary>
		public static Vector3 WithXZ( this Vector3 original, Vector2 XZ )
		{
			original.x = XZ.x;
			original.z = XZ.y;

			return original;
		}

		/// <summary>
		/// Returns this vector with the specified x and z component.
		/// </summary>
		public static Vector3 WithXZ( this Vector3 original, float x, float z )
		{
			original.x = x;
			original.z = z;

			return original;
		}

		/// <summary>
		/// Returns this vector with the specified y and z component.
		/// </summary>
		public static Vector3 WithYZ( this Vector3 original, Vector2 YZ )
		{
			original.y = YZ.x;
			original.z = YZ.y;

			return original;
		}

		/// <summary>
		/// Returns this vector with the specified y and z component.
		/// </summary>
		public static Vector3 WithYZ( this Vector3 original, float y, float z )
		{
			original.y = y;
			original.z = z;

			return original;
		}

		/// <summary>
		/// Returns a vector that points from A towards B.
		/// </summary>
		public static Vector3 DirectionTo( this Vector3 A, Vector3 B )
		{
			return B - A;
		}

		/// <summary>
		/// Returns this vector with the specified magnitude.
		/// </summary>
		public static Vector3 WithMagnitude( this Vector3 v, float magnitude )
		{
			return v.normalized * magnitude;
		}

		/// <summary>
		/// Returns this vector's X and Y components as a Vector2
		/// </summary>
		public static Vector2 XY( this Vector3 v )
		{
			return new Vector2( v.x, v.y );
		}

		/// <summary>
		/// Returns this vector's X and Z components as a Vector2
		/// </summary>
		public static Vector2 XZ( this Vector3 v )
		{
			return new Vector2( v.x, v.z );
		}

		/// <summary>
		/// Returns this vector's Y and Z components as a Vector2
		/// </summary>
		public static Vector2 YZ( this Vector3 v )
		{
			return new Vector2( v.y, v.z );
		}
	}
}