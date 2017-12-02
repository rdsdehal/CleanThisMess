using UnityEngine;

namespace bTools.CodeExtensions
{
	/// <summary>
	/// Extensions for the Unity Rect type.
	/// </summary>
	public static class RectExtensions
	{
		/// <summary>
		/// Contracts or Expands the selected rect by <c>padding</c> amount, leaving the center point unmoved.
		/// </summary>
		public static Rect WithPadding( this Rect rect, float padding )
		{
			rect.x += padding;
			rect.xMax -= padding * 2;
			rect.y += padding;
			rect.yMax -= padding * 2;

			return rect;
		}

		/// <summary>
		/// Returns this rect with the specified X
		/// </summary>
		public static Rect WithX( this Rect rect, float newX )
		{
			rect.x = newX;
			return rect;
		}

		/// <summary>
		/// Returns this rect with the specified Y
		/// </summary>
		public static Rect WithY( this Rect rect, float newY )
		{
			rect.y = newY;
			return rect;
		}

		/// <summary>
		/// Returns this rect with the specified xMax
		/// </summary>
		public static Rect WithXMax( this Rect rect, float newXMax )
		{
			rect.xMax = newXMax;
			return rect;
		}

		/// <summary>
		/// Returns this rect with the specified yMax
		/// </summary>
		public static Rect WithYMax( this Rect rect, float newYMax )
		{
			rect.yMax = newYMax;
			return rect;
		}

		/// <summary>
		/// Returns this rect with the specified width
		/// </summary>
		public static Rect WithW( this Rect rect, float newW )
		{
			rect.width = newW;
			return rect;
		}

		/// <summary>
		/// Returns this rect with the specified height
		/// </summary>
		public static Rect WithH( this Rect rect, float newH )
		{
			rect.height = newH;
			return rect;
		}
	}
}