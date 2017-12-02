
/// <summary>
/// Extensions for Arrays.
/// </summary>
public static class ArrayExtensions
{
	/// <summary>
	/// Returns a random item from the array using UnityEngine.Random.
	/// </summary>
	/// <param name="array">array to get the item from</param>
	/// <returns>Random item T from the array</returns>
	public static T RandomItem<T>( this T[] array )
	{
		return array[UnityEngine.Random.Range( 0, array.Length + 1 )];
	}

	/// <summary>
	/// Fills the entire array with <c>value</c>
	/// </summary>
	/// <param name="value">value to fill the array with</param>
	public static void Populate<T>( this T[] arr, T value )
	{
		for ( int i = 0 ; i < arr.Length ; i++ )
		{
			arr[i] = value;
		}
	}
}
