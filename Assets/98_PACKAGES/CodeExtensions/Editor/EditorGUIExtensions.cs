using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;
using UnityEditor;

namespace bTools.CodeExtensions
{
	/// <summary>
	/// Additional EditorGUI controls and methods.
	/// </summary>
	public static class EditorGUIExtensions
	{
		/// <summary>
		/// Returns true if the current event is a Ctrl + LeftClick in the specified rect.
		/// 
		/// **Does not use the event**.
		/// </summary>
		/// <param name="rect">Screen rect to compare against mouse position</param>
		public static bool CtrlLeftClickOnRect( Rect rect )
		{
			if (
				Event.current.button == 0
				&& Event.current.control == true
				&& Event.current.shift == false
				&& Event.current.alt == false
				&& rect.Contains( Event.current.mousePosition ) )
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Returns true if the current event is a Alt + LeftClick in the specified rect.
		/// 
		/// **Does not use the event**.
		/// </summary>
		/// <param name="rect">Screen rect to compare against mouse position</param>
		/// <example><code>
		/// EditorGUILayout.Label("MyLabel");
		/// if ( EditorGUIExtensions.AltLeftClickOnRect( GUILayoutUtility.GetLastRect() ) )
		/// {
		///	    DoStuff();
		///	}
		/// </code></example>
		public static bool AltLeftClickOnRect( Rect rect )
		{
			if (
				Event.current.button == 0
				&& Event.current.control == false
				&& Event.current.shift == false
				&& Event.current.alt == true
				&& rect.Contains( Event.current.mousePosition ) )
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Returns true if the current event is a Shift + LeftClick in the specified rect.
		/// 
		/// **Does not use the event**.
		/// </summary>
		/// <param name="rect">Screen rect to compare against mouse position</param>
		public static bool ShiftLeftClickOnRect( Rect rect )
		{
			if (
				Event.current.button == 0
				&& Event.current.control == false
				&& Event.current.shift == true
				&& Event.current.alt == false
				&& rect.Contains( Event.current.mousePosition ) )
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Returns true if the current event is a RightClick in the specified rect.
		/// 
		/// **Does not use the event**.
		/// </summary>
		/// <param name="rect">Screen rect to compare against mouse position</param>
		public static bool RightClickOnRect( Rect rect )
		{
			if (
				Event.current.button == 1
				&& Event.current.control == false
				&& Event.current.shift == false
				&& Event.current.alt == false
				&& rect.Contains( Event.current.mousePosition ) )
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Returns true if the current event is a LeftClick in the specified rect.
		/// 
		/// **Does not use the event**.
		/// </summary>
		/// <param name="rect">Screen rect to compare against mouse position</param>
		public static bool LeftClickOnRect( Rect rect )
		{
			if (
				Event.current.button == 0
				&& Event.current.control == false
				&& Event.current.shift == false
				&& Event.current.alt == false
				&& rect.Contains( Event.current.mousePosition ) )
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Returns true if the current event is a Ctrl + RightClick in the specified rect.
		/// 
		/// **Does not use the event**.
		/// </summary>
		/// <param name="rect">Screen rect to compare against mouse position</param>
		public static bool CtrlRightClickOnRect( Rect rect )
		{
			if (
				Event.current.button == 1
				&& Event.current.control == true
				&& Event.current.shift == false
				&& Event.current.alt == false
				&& rect.Contains( Event.current.mousePosition ) )
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Returns true if the current event is a Ctrl + Shift + RightClick in the specified rect.
		/// 
		/// **Does not use the event**.
		/// </summary>
		/// <param name="rect">Screen rect to compare against mouse position</param>
		public static bool CtrlShiftRightClickOnRect( Rect rect )
		{
			if (
				Event.current.button == 1
				&& Event.current.control == true
				&& Event.current.shift == true
				&& Event.current.alt == false
				&& rect.Contains( Event.current.mousePosition ) )
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Returns true if the current event is a MiddleClick in the specified rect.
		/// 
		/// **Does not use the event**.
		/// </summary>
		/// <param name="rect">Screen rect to compare against mouse position</param>
		public static bool ScrollWheelClickOnRect( Rect rect )
		{
			if (
				Event.current.button == 2
				&& Event.current.control == false
				&& Event.current.shift == false
				&& Event.current.alt == false
				&& rect.Contains( Event.current.mousePosition ) )
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Searches the whole project and attempts to load the first asset matching name (excluding extension).
		/// </summary>
		/// <param name="name">Name of the file without extension</param>
		public static T LoadAssetWithName<T>( string name ) where T : UnityEngine.Object
		{
			T asset = null;

			try
			{
				var assetPath = AssetDatabase.GUIDToAssetPath( AssetDatabase.FindAssets( name )[0] );
				asset = AssetDatabase.LoadAssetAtPath<T>( assetPath );
			}
			catch ( Exception ex )
			{
				Debug.LogError( "Could not load asset with name " + name + " | Error: " + ex.Message );
			}

			return asset;
		}

		/// <summary>
		/// Searches the whole projects for assets of type T and returns them as a list
		/// </summary>
		/// <typeparam name="T">Type to look for, derived from UnityEngine.Object</typeparam>
		/// <returns>List of assets of type T found</returns>
		public static List<T> LoadAssetsOfType<T>() where T : UnityEngine.Object
		{
			List<T> assets = new List<T>();
			string[] guids = AssetDatabase.FindAssets( string.Format( "t:{0}", typeof( T ).ToString().Replace( "UnityEngine.", string.Empty ) ) );

			for ( int i = 0 ; i < guids.Length ; i++ )
			{
				string assetPath = AssetDatabase.GUIDToAssetPath( guids[i] );
				T asset = AssetDatabase.LoadAssetAtPath<T>( assetPath );
				if ( asset != null )
				{
					assets.Add( asset );
				}
			}
			return assets;
		}

		/// <summary>
		/// Creates a layouted toggle switch
		/// </summary>
		/// <param name="labels">Labels for the on/off buttons</param>
		/// <returns></returns>
		public static bool LayoutToggleSwitch( GUIContent[] labels, bool state, params GUILayoutOption[] options )
		{
			int i;
			if ( state == true )
			{
				i = 0;
				i = GUILayout.Toolbar( i, labels, options );
			}
			else
			{
				i = 1;
				i = GUILayout.Toolbar( i, labels, options );
			}

			return i == 0;
		}

		[ExcludeFromDocs]
		public static bool LayoutToggleSwitch( GUIContent[] labels, bool state, GUIStyle style, params GUILayoutOption[] options )
		{
			int i;
			if ( state == true )
			{
				i = 0;
				i = GUILayout.Toolbar( i, labels, style, options );
			}
			else
			{
				i = 1;
				i = GUILayout.Toolbar( i, labels, style, options );
			}

			return i == 0;
		}

		[ExcludeFromDocs]
		public static bool LayoutToggleSwitch( string[] labels, bool state, params GUILayoutOption[] options )
		{
			int i;
			if ( state == true )
			{
				i = 0;
				i = GUILayout.Toolbar( i, labels, options );
			}
			else
			{
				i = 1;
				i = GUILayout.Toolbar( i, labels, options );
			}

			return i == 0;
		}

		[ExcludeFromDocs]
		public static bool LayoutToggleSwitch( string[] labels, bool state, GUIStyle style, params GUILayoutOption[] options )
		{
			int i;
			if ( state == true )
			{
				i = 0;
				i = GUILayout.Toolbar( i, labels, style, options );
			}
			else
			{
				i = 1;
				i = GUILayout.Toolbar( i, labels, style, options );
			}

			return i == 0;
		}

		/// <summary>
		/// Creates a new ScriptableObject of type T in the Assets folder with it's type as file name.
		/// </summary>
		/// <typeparam name="T">The ScriptableObject type to instanciate</typeparam>
		/// <returns>The generated ScriptableObject asset.</returns>
		public static T InstanciateScriptableObject<T>() where T : ScriptableObject
		{
			T asset = ScriptableObject.CreateInstance<T>();
			var path = AssetDatabase.GenerateUniqueAssetPath( "Assets/" + typeof( T ).ToString() + ".asset" );
			AssetDatabase.CreateAsset( asset, path );
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			var final = AssetDatabase.LoadAssetAtPath( path, typeof( T ) ) as T;
			return final;
		}

		/// <summary>
		/// Creates a new ScriptableObject of type T in the specified path with it's type as file name.
		/// </summary>
		/// <typeparam name="T">The ScriptableObject type to instanciate</typeparam>
		/// <param name="path">Path to the folder in which to instanciate the ScriptableObject</param>
		/// <returns>The generated ScriptableObject asset.</returns>
		public static T InstanciateScriptableObject<T>( string path ) where T : ScriptableObject
		{
			T asset = ScriptableObject.CreateInstance<T>();
			var uniquePath = AssetDatabase.GenerateUniqueAssetPath( path + typeof( T ).ToString() + ".asset" );
			AssetDatabase.CreateAsset( asset, uniquePath );
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			var final = AssetDatabase.LoadAssetAtPath( uniquePath, typeof( T ) ) as T;
			return final;
		}

		/// <summary>
		/// Creates a new ScriptableObject of type T in the specified path and file name.
		/// </summary>
		/// <typeparam name="T">The ScriptableObject type to instanciate</typeparam>
		/// <param name="path">Path to the folder in which to instanciate the ScriptableObject</param>
		/// <param name="fileName">File name without extension</param>
		public static T InstanciateScriptableObject<T>( string path, string fileName ) where T : ScriptableObject
		{
			T asset = ScriptableObject.CreateInstance<T>();
			var uniquePath = AssetDatabase.GenerateUniqueAssetPath( path + fileName + ".asset" );
			AssetDatabase.CreateAsset( asset, uniquePath );
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			var final = AssetDatabase.LoadAssetAtPath( uniquePath, typeof( T ) ) as T;
			return final;
		}

		/// <summary>
		/// Loads a Unity Object from it's GUID.
		/// </summary>
		/// <param name="guid">GUID of the object to load</param>
		public static UnityEngine.Object ObjectFromGUID( string guid )
		{
			return AssetDatabase.LoadAssetAtPath( AssetDatabase.GUIDToAssetPath( guid ), typeof( UnityEngine.Object ) ) as UnityEngine.Object;
		}
	}
}