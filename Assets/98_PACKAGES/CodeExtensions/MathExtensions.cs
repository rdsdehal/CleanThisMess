/// <summary>
/// Math Extensions
/// </summary>
public class MathExtensions
{
	/// <summary>
	/// Remaps a value from one range to another.
	/// </summary>
	/// <param name="value">Value to remap</param>
	/// <param name="inMin">Minimum of the first range (Containing value)</param>
	/// <param name="inMax">Maximum of the first range (Containing value)</param>
	/// <param name="outMin">Minimum of the second range (Containing the return)</param>
	/// <param name="outMax">Maximum of the second range (Containing the return)</param>
	/// <returns>value remapped to be between outMin and outMax</returns>
	/// <example> 0.5f from a range of 0-1 to a range of 0-100 returns 50</example>
	public static float Remap( float value, float inMin, float inMax, float outMin, float outMax )
	{
		return outMin + ( value - inMin ) * ( outMax - outMin ) / ( inMax - inMin );
	}

	/// <summary>
	/// Remaps value to be between 0 and 1.
	/// </summary>
	/// <param name="value">Value to remap</param>
	/// <param name="min">Minimum value for the input</param>
	/// <param name="max">Maximum value for the input</param>
	/// <returns>value remapped between 0 and 1</returns>
	public static float Remap01( float value, float min, float max )
	{
		return 0 + ( value - min ) * ( 1 - 0 ) / ( max - min );
	}

	/// <summary>
	/// Remaps a value from one range to another.
	/// </summary>
	/// <param name="value">Value to remap</param>
	/// <param name="inMin">Minimum of the first range (Containing value)</param>
	/// <param name="inMax">Maximum of the first range (Containing value)</param>
	/// <param name="outMin">Minimum of the second range (Containing the return)</param>
	/// <param name="outMax">Maximum of the second range (Containing the return)</param>
	/// <returns>value remapped to be between outMin and outMax</returns>
	public static int Remap( int value, int inMin, int inMax, int outMin, int outMax )
	{
		return outMin + ( value - inMin ) * ( outMax - outMin ) / ( inMax - inMin );
	}

	/// <summary>
	/// Remaps value to be between 0 and 1.
	/// </summary>
	/// <param name="value">Value to remap</param>
	/// <param name="min">Minimum value for the input</param>
	/// <param name="max">Maximum value for the input</param>
	/// <returns>value remapped between 0 and 1</returns>
	public static int Remap01( int value, int min, int max )
	{
		return 0 + ( value - min ) * ( 1 - 0 ) / ( max - min );
	}

	/// <summary>
	/// Returns the decimal part of the float.
	/// </summary>
	public static float Frac( float value )
	{
		return value - UnityEngine.Mathf.Floor( value );
	}
}
