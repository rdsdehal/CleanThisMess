using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using bTools.CodeExtensions;

namespace bTools
{
	/// <summary>
	/// Manages the different Settings assets used by bTools modules.
	/// </summary>
	public static class Settings
	{
		private static List<ToolsSettingsBase> m_SettingsAssets;

		/// <summary>
		/// <p>Contains all of the currently cached settings assets.</p>
		/// <p>Generates a new cache if the current one is null.</p>
		/// </summary>
		public static List<ToolsSettingsBase> SettingsAssets
		{
			get
			{
				if ( m_SettingsAssets == null || m_SettingsAssets.Count == 0 )
				{
					m_SettingsAssets = EditorGUIExtensions.LoadAssetsOfType<ToolsSettingsBase>();
					ReOrderCache();
				}

				return m_SettingsAssets;
			}
		}

		/// <summary>
		/// <p>Get the settings file for the provided derived ToolsSettings type.</p>
		/// <p>Generates one in the bTools Settings folder if none was found.</p>
		/// <p>WARNING: If a project is cloned or RemportAll is used, this might return null as the settings file exists but hasn't been loaded by unity yet</p>
		/// </summary>
		/// <typeparam name="T">The derived ToolsSettingsBase type to get settings from</typeparam>
		/// <returns>The ToolsSettingsBase asset of the specified derived type</returns>
		public static T Get<T>() where T : ToolsSettingsBase
		{
			var isItLoaded = SettingsAssets.FirstOrDefault( m => m is T );

			if ( isItLoaded != null )
			{
				return isItLoaded as T;
			}
			else
			{
				return GenerateSettingsFile<T>();
			}
		}

		/// <summary>
		/// <p>Get the settings file for the provided derived ToolsSettings type.</p>
		/// <p>Generates one in the bTools Settings folder if none was found.</p>
		/// <p>WARNING: If a project is cloned or RemportAll is used, this might return null as the settings file exists but hasn't been loaded by unity yet</p>
		/// </summary>
		/// <typeparam name="type">The derived ToolsSettingsBase type to get settings from</typeparam>
		/// <returns>The ToolsSettingsBase asset of the specified derived type</returns>
		public static ToolsSettingsBase Get( Type type )
		{
			var isItLoaded = SettingsAssets.FirstOrDefault( m => m.GetType() == type );

			if ( isItLoaded != null )
			{
				return isItLoaded;
			}
			else
			{
				return GenerateSettingsFile( type );
			}
		}

		private static T GenerateSettingsFile<T>() where T : ToolsSettingsBase
		{
			var path = Ressources.PathTo_bSettings + typeof( T ).ToString() + ".asset";

			var tryLoading = AssetDatabase.LoadAssetAtPath<ToolsSettingsBase>( path );
			if ( tryLoading != null )
			{
				m_SettingsAssets.Add( tryLoading );
				return tryLoading as T;
			}

			// In case the asset is present but not loaded by unity (The FindAssetOfType failed to find it)
			if ( File.Exists( path ) )
			{
				Debug.LogError( "Cannot access settings during editor loading !!" );
				return null;
			}

			T asset = ScriptableObject.CreateInstance<T>();

			AssetDatabase.CreateAsset( asset, path );
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			var final = AssetDatabase.LoadAssetAtPath<T>( path );
			m_SettingsAssets.Add( final );
			ReOrderCache();
			return final;
		}

		private static ToolsSettingsBase GenerateSettingsFile( Type type )
		{
			var path = Ressources.PathTo_bSettings + type.ToString() + ".asset";

			var tryLoading = AssetDatabase.LoadAssetAtPath<ToolsSettingsBase>( path );
			if ( tryLoading != null )
			{
				m_SettingsAssets.Add( tryLoading );
				return tryLoading;
			}

			// In case the asset is present but not loaded by unity (The FindAssetOfType failed to find it)
			if ( File.Exists( path ) )
			{
				Debug.LogError( "Cannot access settings during editor loading !!" );
				return null;
			}

			var asset = ScriptableObject.CreateInstance( type );

			AssetDatabase.CreateAsset( asset, path );
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			var final = AssetDatabase.LoadAssetAtPath( path, type ) as ToolsSettingsBase;
			m_SettingsAssets.Add( final );
			ReOrderCache();
			return final;
		}

		private static void ReOrderCache()
		{
			if ( m_SettingsAssets != null )
			{
				m_SettingsAssets = m_SettingsAssets.OrderBy( m => m.order ).ToList();
			}
		}
	}
}