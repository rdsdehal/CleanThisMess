using System.IO;
using UnityEngine;
using UnityEditor;

namespace bTools
{
	/// <summary>
	/// Ressources used by the bTools modules, such as file paths.
	/// </summary>
	[InitializeOnLoad]
	public static class Ressources
	{
		static GUISkin m_bToolsSkin;

		/// <summary>
		/// Returns the current GUISkin used by bTools (Personal or Pro skin version)
		/// </summary>
		public static GUISkin bToolsSkin
		{
			get
			{
				if ( m_bToolsSkin == null )
				{
					// Not using the bExtension method to allow removal of bExtensions
					string name = EditorGUIUtility.isProSkin ? "bToolsSkin" : "bToolsSkin";

					var assetPath = AssetDatabase.GUIDToAssetPath( AssetDatabase.FindAssets( name )[0] );
					m_bToolsSkin = AssetDatabase.LoadAssetAtPath( assetPath, typeof( GUISkin ) ) as GUISkin;

					if ( m_bToolsSkin == null )
					{
						m_bToolsSkin = new GUISkin();
						Debug.LogError( "[bTools]Could not find " + name + ".guiskin, please reinstall" );
					}
				}

				return m_bToolsSkin;
			}
		}

		/// <summary>
		/// Returns the path to the root bTools folder relative to "Assets/" and ending with "/"
		/// </summary>
		public static string PathTo_bTools
		{
			get
			{
				string path;

				path = Directory.GetDirectories( Application.dataPath, "bTools", SearchOption.AllDirectories )[0];

				path = path.Replace( Application.dataPath, string.Empty );
				path = path.Replace( '\\', '/' );
				path = @"Assets" + path + "/";

				return path;
			}
		}

		/// <summary>
		/// Returns the path to bTools + "General/ToolResources/Data/"
		/// </summary>
		public static string PathTo_bData
		{
			get
			{
				return PathTo_bTools + "General/ToolResources/Data/";
			}
		}

		/// <summary>
		///  Returns the path to bTools + "General/ToolResources/Settings/"
		/// </summary>
		public static string PathTo_bSettings
		{
			get
			{
				return PathTo_bTools + "General/ToolResources/Settings/";
			}
		}
	}
}