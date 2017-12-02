using UnityEngine;

namespace bTools.CodeExtensions
{
	/// <summary>
	/// Extension methods for the unity Texture Type and derived Types.
	/// </summary>
	public class TextureExtensions
	{
		/// <summary>
		/// Generates a new Texture2D with the specified size and color
		/// </summary>
		/// <param name="width">Width of the new texture</param>
		/// <param name="height">Height of the new texture</param>
		/// <param name="color">Color to fill the texture with</param>
		public Texture2D GenerateColorTexture( int width, int height, Color color )
		{
			Texture2D tex = new Texture2D( width, height );
			Color[] pixels = new Color[width * height];

			for ( int i = 0 ; i < pixels.Length ; i++ )
			{
				pixels[i] = color;
			}

			tex.SetPixels( pixels );

			tex.Apply();
			return tex;
		}
	}

}