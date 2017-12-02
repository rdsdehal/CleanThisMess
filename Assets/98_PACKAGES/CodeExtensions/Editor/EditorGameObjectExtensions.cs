using UnityEngine;
using UnityEditor;

namespace bTools.CodeExtensions
{
	/// <summary>
	/// Extensions for the GameObject type to be used within the Editor.
	/// </summary>
	public static class EditorGameObjectExtensions
	{
		/// <summary>
		/// <p>Changes the visibility of this GameObject and all of it's children and sub-children.</p>
		/// <p>Also creates an Undo state.</p>
		/// </summary>
		public static void SetVisibleRecursively( this GameObject gameObject, bool newState )
		{
			Undo.RecordObject( gameObject, "Visibility Change" );
			gameObject.SetActive( newState );

			if ( gameObject.transform.childCount > 0 )
			{
				foreach ( Transform child in gameObject.transform )
				{
					child.gameObject.SetVisibleRecursively( newState );
				}
			}
		}

		/// <summary>
		/// Changes the static flags of this GameObject and all of it's children and sub-children.
		/// <p>Also creates an Undo state.</p>
		/// </summary>
		public static void SetStaticFlagsRecursively( this GameObject gameObject, StaticEditorFlags flags )
		{
			Undo.RecordObject( gameObject, "Changed static flags" );
			GameObjectUtility.SetStaticEditorFlags( gameObject, flags );

			if ( gameObject.transform.childCount > 0 )
			{
				foreach ( Transform child in gameObject.transform )
				{
					child.gameObject.SetStaticFlagsRecursively( flags );
				}
			}
		}

		/// <summary>
		/// Changes the tag of this GameObject and all of it's children and sub-children.
		/// <p>Also creates an Undo state.</p>
		/// </summary>
		public static void SetTagRecursively( this GameObject gameObject, string tag )
		{
			Undo.RecordObject( gameObject, "Changed tag" );
			gameObject.tag = tag;

			if ( gameObject.transform.childCount > 0 )
			{
				foreach ( Transform child in gameObject.transform )
				{
					child.gameObject.SetTagRecursively( tag );
				}
			}
		}

		/// <summary>
		/// <p>Changes the layer of this GameObject and all of it's children and sub-children.</p> 
		/// <p>Also creates an Undo state.</p>
		/// </summary>
		public static void SetLayerRecursively( this GameObject gameObject, int layer )
		{
			Undo.RecordObject( gameObject, "Changed layer" );
			gameObject.layer = layer;

			if ( gameObject.transform.childCount > 0 )
			{
				foreach ( Transform child in gameObject.transform )
				{
					child.gameObject.SetLayerRecursively( layer );
				}
			}
		}

		/// <summary>
		/// Returns true if the object has the NotEditable <a href = "https://docs.unity3d.com/ScriptReference/HideFlags.html">HideFlag</a> set.
		/// </summary>
		public static bool IsLocked( this GameObject gameObject )
		{
			return ( gameObject.hideFlags & HideFlags.NotEditable ) != 0;
		}

		/// <summary>
		/// Sets the NotEditable flag on the GameObject while preserving other <a href = "https://docs.unity3d.com/ScriptReference/HideFlags.html"> HideFlags</a>.
		/// </summary>
		public static void SetLocked( this GameObject gameObject, bool newStatus )
		{
			if ( newStatus )
			{
				gameObject.hideFlags |= HideFlags.NotEditable;
				SceneView.RepaintAll();
			}
			else
			{
				gameObject.hideFlags &= ~HideFlags.NotEditable;
				SceneView.RepaintAll();
			}
		}

		/// <summary>
		/// <p>Changes the lock of this GameObject and all of it's children and sub-children.</p>
		/// <p>Also creates an Undo state.</p>
		/// </summary>
		public static void SetLockedRecursively( this GameObject gameObject, bool state )
		{
			Undo.RecordObject( gameObject, "Set Locked" );
			gameObject.SetLocked( state );

			if ( gameObject.transform.childCount > 0 )
			{
				foreach ( Transform child in gameObject.transform )
				{
					child.gameObject.SetLockedRecursively( state );
				}
			}
		}
	}
}