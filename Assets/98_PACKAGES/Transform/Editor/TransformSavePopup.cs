using UnityEngine;
using UnityEditor;
using bTools.CodeExtensions;

namespace bTools.TransformComponent
{
	public class TransformSavePopup : PopupWindowContent
	{
		string newSaveName;
		GameObject refObject;

		ObjectData objectData
		{
			get
			{
				var objectData = refObject.GetComponent<ObjectData>();

				if ( objectData == null )
				{
					objectData = refObject.AddComponent<ObjectData>();
				}

				return objectData;
			}
		}

		public TransformSavePopup( GameObject refObject )
		{
			this.refObject = refObject;
		}

		public override void OnGUI( Rect rect )
		{
			if ( Selection.gameObjects.Length > 1 )
			{
				EditorGUILayout.HelpBox( "Multiple Object Transform Saving is not supported", MessageType.Error );
				return;
			}

			EditorGUILayout.LabelField( "Transform Save/Load", EditorStyles.boldLabel, GUILayout.Height( 17 ) );

			// Save.

			newSaveName = EditorGUILayout.TextField( newSaveName, GUILayout.Height( 17 ) );
			GUILayout.Space( 4 );
			EditorGUILayout.BeginHorizontal();
			if ( GUILayout.Button( "New Save", GUILayout.Height( 17 ) ) )
			{
				if ( newSaveName == string.Empty )
				{
					newSaveName = "Name can't be empty";
				}
				else
				{
					// Keys and Values are coupled manually because Unity does not support dictionary serialization.
					Undo.RecordObject( objectData, "Added a saved transform" );
					if ( objectData.m_savedTransformKeys.Contains( newSaveName ) )
					{

						objectData.m_savedTransformValues[objectData.m_savedTransformKeys.IndexOf( newSaveName )] =
							TransformComponent.targetTransform.GetTransformData();
					}
					else
					{

						objectData.m_savedTransformKeys.Add( newSaveName );
						objectData.m_savedTransformValues.Add( TransformComponent.targetTransform.GetTransformData() );
					}
				}
			}

			// Clear All.

			GUILayout.Space( 8 );
			if ( GUILayout.Button( "Clear All", GUILayout.Height( 17 ) ) )
			{
				Undo.RecordObject( objectData, "Cleared saved transform" );
				objectData.m_savedTransformKeys.Clear();
				objectData.m_savedTransformValues.Clear();
				EditorUtility.SetDirty( objectData );
			}
			EditorGUILayout.EndHorizontal();

			// Load/Remove.

			EditorGUI.BeginDisabledGroup( true );
			EditorGUILayout.TextArea( string.Empty, GUI.skin.horizontalSlider, GUILayout.Height( 16 ) );
			EditorGUI.EndDisabledGroup();

			for ( int i = 0 ; i < objectData.m_savedTransformKeys.Count ; i++ )
			{
				DrawLoadField( i );
			}
		}

		void DrawLoadField( int key )
		{
			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.LabelField( objectData.m_savedTransformKeys[key], GUILayout.Height( 16 ), GUILayout.MinWidth( 50 ) );
			if ( GUILayout.Button( "Load", GUILayout.Height( 16 ) ) )
			{

				Undo.RecordObject( TransformComponent.targetTransform, "Loaded transform" );
				TransformComponent.targetTransform.ApplyTransformData( objectData.m_savedTransformValues[key] );
				EditorUtility.SetDirty( objectData );
			}

			if ( GUILayout.Button( "Delete", GUILayout.Height( 16 ) ) )
			{

				Undo.RecordObject( objectData, "Deleted a saved transform" );
				objectData.m_savedTransformKeys.RemoveAt( key );
				objectData.m_savedTransformValues.RemoveAt( key );
				EditorUtility.SetDirty( objectData );
			}

			EditorGUILayout.EndHorizontal();
			GUILayout.Space( 4 );
		}

		public override Vector2 GetWindowSize()
		{
			return new Vector2( 250, 93 + 22 * ( objectData.m_savedTransformKeys.Count ) );
		}
	}
}