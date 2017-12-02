using System.Collections.Generic;

namespace bTools.CodeExtensions
{
	/// <summary>
	/// Extension for Generic Lists.
	/// </summary>
	public static class ListExtensions
	{
		/// <summary>
		/// Moves element at oldIndex to newIndex
		/// </summary>
		/// <param name="oldIndex">Index of the element before moving</param>
		/// <param name="newIndex">Index of the element after moving</param>
		public static void Move<T>( this IList<T> list, int oldIndex, int newIndex )
		{
			if ( 0 > newIndex || oldIndex > list.Count || 0 > oldIndex || newIndex > list.Count )
			{
				throw new System.IndexOutOfRangeException();
			}

			T item = list[oldIndex];

			list.RemoveAt( oldIndex );
			list.Insert( newIndex, item );
		}

		/// <summary>
		/// Returns a random item from the list using UnityEngine.Random.
		/// </summary>
		/// <param name="list">list to get the item from</param>
		/// <returns>Random item T from the list</returns>
		public static T RandomItem<T>( this IList<T> list )
		{
			if ( list.Count == 0 ) throw new System.IndexOutOfRangeException( "Can't get a random item from an empty list !" );
			return list[UnityEngine.Random.Range( 0, list.Count )];
		}
	}
}