using UnityEngine;
using UnityEditor;
using bTools.CodeExtensions;

namespace bTools.TransformComponent
{
	[CustomEditor( typeof( UnityEngine.Transform ) )]
	[CanEditMultipleObjects]
	public class TransformComponent : Editor
	{
		// GameObject references.
		public static UnityEngine.Transform targetTransform;

		// Properties.
		SerializedProperty localEulerHint;
		SerializedProperty localRotation;
		SerializedProperty localPosition;
		SerializedProperty localScale;

		// GUI.
		static bool combinedScale;
		string[] displayToolbar = { "Local", "World" };
		int toolbarIndex = 0;

		private void OnEnable()
		{
			targetTransform = (UnityEngine.Transform)target;

			localEulerHint = serializedObject.FindProperty( "m_LocalEulerAnglesHint" );
			localRotation = serializedObject.FindProperty( "m_LocalRotation" );
			localPosition = serializedObject.FindProperty( "m_LocalPosition" );
			localScale = serializedObject.FindProperty( "m_LocalScale" );
		}

		public override void OnInspectorGUI()
		{
			// Check if not uneditable prefab.
			EditorGUI.BeginDisabledGroup( targetTransform != null && PrefabUtility.GetPrefabType( targetTransform ) == PrefabType.ModelPrefab );

			#region Refresh
			serializedObject.Update();
			combinedScale = EditorPrefs.GetBool( "bToolsCombinedScale" );
			#endregion

			#region Additional Menus
			GUILayout.Space( 4 ); // Save/Load
			EditorGUILayout.BeginHorizontal();
			if ( GUILayout.Button( string.Empty, Ressources.bToolsSkin.customStyles[2], GUILayout.Width( 16 ), GUILayout.Height( 16 ) ) )
			{
				Rect popupRect = new Rect( Event.current.mousePosition.x, -Event.current.mousePosition.y, 250, 110 );
				PopupWindow.Show( popupRect, new TransformSavePopup( targetTransform.gameObject ) );
				GUIUtility.ExitGUI();
			}
			GUILayout.Space( 8 ); // Global reset
			if ( GUILayout.Button( string.Empty, Ressources.bToolsSkin.customStyles[0], GUILayout.Width( 16 ), GUILayout.Height( 16 ) ) )
			{
				localPosition.vector3Value = Vector3.zero;
				localEulerHint.vector3Value = Vector3.zero;
				localRotation.quaternionValue = Quaternion.identity;
				localScale.vector3Value = Vector3.one;
			}
			GUILayout.Space( 8 ); // Transform tools
			if ( GUILayout.Button( string.Empty, Ressources.bToolsSkin.customStyles[3], GUILayout.Width( 16 ), GUILayout.Height( 16 ) ) )
			{
				TransformToolsWindow.OpenWindow();
				GUIUtility.ExitGUI();
			}

			serializedObject.ApplyModifiedProperties();

			toolbarIndex = GUILayout.Toolbar( toolbarIndex, displayToolbar, GUILayout.Height( 14 ) );

			EditorGUILayout.EndHorizontal();
			#endregion

			#region Default Controls
			if ( toolbarIndex == 0 )
			{
				EditorGUIUtility.labelWidth = 13;
				DrawLocalPositionGUI();
				DrawLocalRotationGUI();
				DrawLocalScaleGUI();
				EditorGUIUtility.labelWidth = 0;
			}
			else
			{
				EditorGUI.BeginDisabledGroup( true );
				EditorGUIUtility.labelWidth = 52;
				EditorGUIUtility.wideMode = true;
				DrawWorldPositionGUI();
				DrawWorldRotationGUI();
				DrawWorldScaleGUI();
				EditorGUIUtility.labelWidth = 0;
				EditorGUI.EndDisabledGroup();
			}
			#endregion

			// Final Checks.
			if ( IsPositionTooBig() )
			{
				EditorGUILayout.HelpBox( "Due to floating-point precision limitations, it is recommended to bring the world coordinates of the GameObject within a smaller range.", MessageType.Warning );
			}

			serializedObject.ApplyModifiedProperties();
			EditorGUI.EndDisabledGroup();
		}

		#region GUI Drawers
		void DrawLocalPositionGUI()
		{
			// Setup.
			GUILayout.Space( 6 );
			EditorGUI.BeginChangeCheck();
			EditorGUI.BeginProperty( GUILayoutUtility.GetLastRect(), new GUIContent( string.Empty ), localPosition );
			EditorGUILayout.BeginHorizontal();
			Vector3 positionCache = localPosition.vector3Value;

			// Label.
			EditorGUILayout.LabelField( "Position", GUILayout.Width( 59 ) );

			#region Label Shortcuts
			if ( EditorGUIExtensions.ScrollWheelClickOnRect( GUILayoutUtility.GetLastRect() ) )
			{
				GUIUtility.keyboardControl = 0;
				positionCache = Vector3.zero;
				GUI.changed = true;
			}
			if ( EditorGUIExtensions.CtrlShiftRightClickOnRect( GUILayoutUtility.GetLastRect() ) )
			{ // Save position into clipboard
				ClipboardData.clipboardPosition = targetTransform.localPosition;
			}
			if ( EditorGUIExtensions.CtrlRightClickOnRect( GUILayoutUtility.GetLastRect() ) )
			{ // Load position into clipboard
				positionCache = ClipboardData.clipboardPosition;
				GUI.changed = true;
			}

			if ( EditorGUI.EndChangeCheck() )
			{
				GUIUtility.keyboardControl = 0;
				foreach ( var obj in targets )
				{
					SerializedObject serial = new SerializedObject( obj );
					serial.Update();
					serial.FindProperty( "m_LocalPosition" ).vector3Value = positionCache;
					serial.ApplyModifiedProperties();
				}
			}
			#endregion

			#region X Value
			EditorGUI.BeginChangeCheck();
			EditorGUI.showMixedValue = IsEntireSelectionEqualInXPos() ? false : true;
			positionCache.x = EditorGUILayout.FloatField( "X", positionCache.x, GUILayout.MinWidth( 12 ) );

			if ( EditorGUIExtensions.ScrollWheelClickOnRect( GUILayoutUtility.GetLastRect() ) )
			{
				GUIUtility.keyboardControl = 0;
				foreach ( var obj in targets )
				{
					SerializedObject serial = new SerializedObject( obj );
					serial.Update();
					serial.FindProperty( "m_LocalPosition" ).vector3Value = serial.FindProperty( "m_LocalPosition" ).vector3Value.WithX( 0 );
					serial.ApplyModifiedProperties();
				}
			}
			if ( EditorGUI.EndChangeCheck() )
			{
				foreach ( var obj in targets )
				{
					SerializedObject serial = new SerializedObject( obj );
					serial.Update();
					serial.FindProperty( "m_LocalPosition" ).vector3Value = serial.FindProperty( "m_LocalPosition" ).vector3Value.WithX( positionCache.x );
					serial.ApplyModifiedProperties();
				}
			}
			#endregion

			#region Y Value
			EditorGUI.BeginChangeCheck();
			EditorGUI.showMixedValue = IsEntireSelectionEqualInYPos() ? false : true;
			positionCache.y = EditorGUILayout.FloatField( "Y", positionCache.y, GUILayout.MinWidth( 12 ) );

			if ( EditorGUIExtensions.ScrollWheelClickOnRect( GUILayoutUtility.GetLastRect() ) )
			{
				GUIUtility.keyboardControl = 0;
				foreach ( var obj in targets )
				{
					SerializedObject serial = new SerializedObject( obj );
					serial.Update();
					serial.FindProperty( "m_LocalPosition" ).vector3Value = serial.FindProperty( "m_LocalPosition" ).vector3Value.WithY( 0 );
					serial.ApplyModifiedProperties();
				}
			}
			if ( EditorGUI.EndChangeCheck() )
			{
				foreach ( var obj in targets )
				{
					SerializedObject serial = new SerializedObject( obj );
					serial.Update();
					serial.FindProperty( "m_LocalPosition" ).vector3Value = serial.FindProperty( "m_LocalPosition" ).vector3Value.WithY( positionCache.y );
					serial.ApplyModifiedProperties();
				}
			}
			#endregion

			#region Z Value
			EditorGUI.BeginChangeCheck();
			EditorGUI.showMixedValue = IsEntireSelectionEqualInZPos() ? false : true;
			positionCache.z = EditorGUILayout.FloatField( "Z", positionCache.z, GUILayout.MinWidth( 12 ) );
			if ( EditorGUIExtensions.ScrollWheelClickOnRect( GUILayoutUtility.GetLastRect() ) )
			{
				GUIUtility.keyboardControl = 0;
				foreach ( var obj in targets )
				{
					SerializedObject serial = new SerializedObject( obj );
					serial.Update();
					serial.FindProperty( "m_LocalPosition" ).vector3Value = serial.FindProperty( "m_LocalPosition" ).vector3Value.WithZ( 0 );
					serial.ApplyModifiedProperties();
				}
			}
			if ( EditorGUI.EndChangeCheck() )
			{
				foreach ( var obj in targets )
				{
					SerializedObject serial = new SerializedObject( obj );
					serial.Update();
					serial.FindProperty( "m_LocalPosition" ).vector3Value = serial.FindProperty( "m_LocalPosition" ).vector3Value.WithZ( positionCache.z );
					serial.ApplyModifiedProperties();
				}
			}

			#endregion

			// End block.
			EditorGUI.showMixedValue = false;
			EditorGUILayout.EndHorizontal();
			EditorGUI.EndProperty();
		}
		void DrawLocalRotationGUI()
		{
			// Setup.
			GUILayout.Space( 2 );
			EditorGUI.BeginProperty( GUILayoutUtility.GetLastRect(), new GUIContent( string.Empty ), localRotation );
			EditorGUILayout.BeginHorizontal();

			// If Rotation has been modified outside, the euler hint will not be modified and the value displayed in the inspector will be wrong.
			// If in animation mode, this creates keys every time we move the playhead since the values are changed by the animation manager.

			if ( !serializedObject.isEditingMultipleObjects && !AnimationMode.InAnimationMode() )
			{
				if ( Quaternion.Angle( Quaternion.Euler( targetTransform.localEulerAngles ), Quaternion.Euler( localEulerHint.vector3Value ) ) > 0.06f )
				{
					localEulerHint.vector3Value = localRotation.quaternionValue.eulerAngles;
					GUI.changed = true;
				}
			}

			Vector3 eulerHintCache = localEulerHint.vector3Value;

			// Label.
			EditorGUILayout.LabelField( "Rotation", GUILayout.Width( 59 ) );

			#region Label Shortcuts
			EditorGUI.BeginChangeCheck();
			if ( EditorGUIExtensions.ScrollWheelClickOnRect( GUILayoutUtility.GetLastRect() ) )
			{
				GUIUtility.keyboardControl = 0;
				eulerHintCache = Vector3.zero;
				GUI.changed = true;
			}
			if ( EditorGUIExtensions.CtrlShiftRightClickOnRect( GUILayoutUtility.GetLastRect() ) )
			{
				ClipboardData.clipboardRotation = eulerHintCache;
			}
			if ( EditorGUIExtensions.CtrlRightClickOnRect( GUILayoutUtility.GetLastRect() ) )
			{
				eulerHintCache = ClipboardData.clipboardRotation;
				GUI.changed = true;
			}

			if ( EditorGUI.EndChangeCheck() )
			{
				foreach ( var obj in targets )
				{
					SerializedObject serial = new SerializedObject( obj );
					serial.Update();
					SerializedProperty hintProp = serial.FindProperty( "m_LocalEulerAnglesHint" );
					hintProp.vector3Value = eulerHintCache;
					serial.FindProperty( "m_LocalRotation" ).quaternionValue = Quaternion.Euler( hintProp.vector3Value );
					serial.ApplyModifiedProperties();
				}
			}
			#endregion

			#region X Value
			EditorGUI.BeginChangeCheck();
			EditorGUI.showMixedValue = IsEntireSelectionEqualInXRot() ? false : true;
			eulerHintCache.x = EditorGUILayout.FloatField( "X", eulerHintCache.x, GUILayout.MinWidth( 12 ) );

			if ( EditorGUIExtensions.ScrollWheelClickOnRect( GUILayoutUtility.GetLastRect() ) )
			{
				GUIUtility.keyboardControl = 0;
				foreach ( var obj in targets )
				{
					SerializedObject serial = new SerializedObject( obj );
					serial.Update();
					SerializedProperty hintProp = serial.FindProperty( "m_LocalEulerAnglesHint" );
					hintProp.vector3Value = hintProp.vector3Value.WithX( 0 );
					serial.FindProperty( "m_LocalRotation" ).quaternionValue = Quaternion.Euler( hintProp.vector3Value );
					serial.ApplyModifiedProperties();
				}
			}
			if ( EditorGUI.EndChangeCheck() )
			{
				foreach ( var obj in targets )
				{
					SerializedObject serial = new SerializedObject( obj );
					serial.Update();
					SerializedProperty hintProp = serial.FindProperty( "m_LocalEulerAnglesHint" );
					hintProp.vector3Value = hintProp.vector3Value.WithX( eulerHintCache.x );
					serial.FindProperty( "m_LocalRotation" ).quaternionValue = Quaternion.Euler( hintProp.vector3Value );
					serial.ApplyModifiedProperties();
				}
			}
			#endregion

			#region Y Value
			EditorGUI.BeginChangeCheck();
			EditorGUI.showMixedValue = IsEntireSelectionEqualInYRot() ? false : true;
			eulerHintCache.y = EditorGUILayout.FloatField( "Y", eulerHintCache.y, GUILayout.MinWidth( 12 ) );

			if ( EditorGUIExtensions.ScrollWheelClickOnRect( GUILayoutUtility.GetLastRect() ) )
			{
				GUIUtility.keyboardControl = 0;
				foreach ( var obj in targets )
				{
					SerializedObject serial = new SerializedObject( obj );
					serial.Update();
					SerializedProperty prop = serial.FindProperty( "m_LocalEulerAnglesHint" );
					prop.vector3Value = prop.vector3Value.WithY( 0 );
					serial.FindProperty( "m_LocalRotation" ).quaternionValue = Quaternion.Euler( prop.vector3Value );
					serial.ApplyModifiedProperties();
				}
			}
			if ( EditorGUI.EndChangeCheck() )
			{
				foreach ( var obj in targets )
				{
					SerializedObject serial = new SerializedObject( obj );
					serial.Update();
					SerializedProperty hintProp = serial.FindProperty( "m_LocalEulerAnglesHint" );
					hintProp.vector3Value = hintProp.vector3Value.WithY( eulerHintCache.y );
					serial.FindProperty( "m_LocalRotation" ).quaternionValue = Quaternion.Euler( hintProp.vector3Value );
					serial.ApplyModifiedProperties();
				}
			}

			#endregion

			#region Z Value
			EditorGUI.BeginChangeCheck();
			EditorGUI.showMixedValue = IsEntireSelectionEqualInZRot() ? false : true;
			eulerHintCache.z = EditorGUILayout.FloatField( "Z", eulerHintCache.z, GUILayout.MinWidth( 12 ) );

			if ( EditorGUIExtensions.ScrollWheelClickOnRect( GUILayoutUtility.GetLastRect() ) )
			{
				GUIUtility.keyboardControl = 0;
				foreach ( var obj in targets )
				{
					SerializedObject serial = new SerializedObject( obj );
					serial.Update();
					SerializedProperty prop = serial.FindProperty( "m_LocalEulerAnglesHint" );
					prop.vector3Value = prop.vector3Value.WithZ( 0 );
					serial.FindProperty( "m_LocalRotation" ).quaternionValue = Quaternion.Euler( prop.vector3Value );
					serial.ApplyModifiedProperties();
				}
			}
			if ( EditorGUI.EndChangeCheck() )
			{
				foreach ( var obj in targets )
				{
					SerializedObject serial = new SerializedObject( obj );
					serial.Update();
					SerializedProperty hintProp = serial.FindProperty( "m_LocalEulerAnglesHint" );
					hintProp.vector3Value = hintProp.vector3Value.WithZ( eulerHintCache.z );
					serial.FindProperty( "m_LocalRotation" ).quaternionValue = Quaternion.Euler( hintProp.vector3Value );
					serial.ApplyModifiedProperties();
				}
			}
			#endregion

			// End Block.
			EditorGUI.showMixedValue = false;
			EditorGUILayout.EndHorizontal();
			EditorGUI.EndProperty();
		}
		void DrawLocalScaleGUI()
		{
			// Setup.
			GUILayout.Space( 2 );
			EditorGUI.BeginProperty( GUILayoutUtility.GetLastRect(), new GUIContent( string.Empty ), localScale );
			EditorGUILayout.BeginHorizontal();

			Vector3 sclCache = localScale.vector3Value;

			// Label.
			EditorGUILayout.LabelField( "Scale", GUILayout.Width( 55 - 16 ) );

			#region Label Shortcuts
			EditorGUI.BeginChangeCheck();
			if ( EditorGUIExtensions.ScrollWheelClickOnRect( GUILayoutUtility.GetLastRect() ) )
			{
				sclCache = Vector3.one;
				GUI.changed = true;
			}
			if ( EditorGUIExtensions.CtrlShiftRightClickOnRect( GUILayoutUtility.GetLastRect() ) )
			{
				ClipboardData.clipboardRotation = targetTransform.localScale;
			}
			if ( EditorGUIExtensions.CtrlRightClickOnRect( GUILayoutUtility.GetLastRect() ) )
			{
				sclCache = ClipboardData.clipboardRotation;
				GUI.changed = true;
			}
			if ( EditorGUI.EndChangeCheck() )
			{
				foreach ( var obj in targets )
				{
					SerializedObject serial = new SerializedObject( obj );
					serial.Update();
					serial.FindProperty( "m_LocalScale" ).vector3Value = sclCache;
					serial.ApplyModifiedProperties();
				}
			}
			#endregion

			EditorGUI.showMixedValue = false;
			combinedScale = EditorGUILayout.Toggle( combinedScale, Ressources.bToolsSkin.customStyles[1], GUILayout.Width( 16 ), GUILayout.Height( 16 ) );
			EditorPrefs.SetBool( "bToolsCombinedScale", combinedScale );

			if ( !combinedScale )
			{ //Draw single fields
				#region X Value
				EditorGUI.BeginChangeCheck();
				EditorGUI.showMixedValue = IsEntireSelectionEqualInXScl() ? false : true;
				sclCache.x = EditorGUILayout.FloatField( "X", sclCache.x, GUILayout.MinWidth( 12 ) );

				if ( EditorGUIExtensions.ScrollWheelClickOnRect( GUILayoutUtility.GetLastRect() ) )
				{
					foreach ( var obj in targets )
					{
						SerializedObject serial = new SerializedObject( obj );
						serial.Update();
						serial.FindProperty( "m_LocalScale" ).vector3Value = serial.FindProperty( "m_LocalScale" ).vector3Value.WithX( 1 );
						serial.ApplyModifiedProperties();
					}
				}
				if ( EditorGUI.EndChangeCheck() )
				{
					foreach ( var obj in targets )
					{
						SerializedObject serial = new SerializedObject( obj );
						serial.Update();
						serial.FindProperty( "m_LocalScale" ).vector3Value = serial.FindProperty( "m_LocalScale" ).vector3Value.WithX( sclCache.x );
						serial.ApplyModifiedProperties();
					}
				}
				#endregion

				#region Y Value
				EditorGUI.BeginChangeCheck();
				EditorGUI.showMixedValue = IsEntireSelectionEqualInYScl() ? false : true;
				sclCache.y = EditorGUILayout.FloatField( "Y", sclCache.y, GUILayout.MinWidth( 12 ) );
				if ( EditorGUIExtensions.ScrollWheelClickOnRect( GUILayoutUtility.GetLastRect() ) )
				{
					foreach ( var obj in targets )
					{
						SerializedObject serial = new SerializedObject( obj );
						serial.Update();
						serial.FindProperty( "m_LocalScale" ).vector3Value = serial.FindProperty( "m_LocalScale" ).vector3Value.WithY( 1 );
						serial.ApplyModifiedProperties();
					}
				}
				if ( EditorGUI.EndChangeCheck() )
				{
					foreach ( var obj in targets )
					{
						SerializedObject serial = new SerializedObject( obj );
						serial.Update();
						serial.FindProperty( "m_LocalScale" ).vector3Value = serial.FindProperty( "m_LocalScale" ).vector3Value.WithY( sclCache.y );
						serial.ApplyModifiedProperties();
					}
				}
				#endregion

				#region Z Value
				EditorGUI.BeginChangeCheck();
				EditorGUI.showMixedValue = IsEntireSelectionEqualInZScl() ? false : true;
				sclCache.z = EditorGUILayout.FloatField( "Z", sclCache.z, GUILayout.MinWidth( 12 ) );

				if ( EditorGUIExtensions.ScrollWheelClickOnRect( GUILayoutUtility.GetLastRect() ) )
				{
					foreach ( var obj in targets )
					{
						SerializedObject serial = new SerializedObject( obj );
						serial.Update();
						serial.FindProperty( "m_LocalScale" ).vector3Value = serial.FindProperty( "m_LocalScale" ).vector3Value.WithZ( 1 );
						serial.ApplyModifiedProperties();
					}
				}
				if ( EditorGUI.EndChangeCheck() )
				{
					foreach ( var obj in targets )
					{
						SerializedObject serial = new SerializedObject( obj );
						serial.Update();
						serial.FindProperty( "m_LocalScale" ).vector3Value = serial.FindProperty( "m_LocalScale" ).vector3Value.WithZ( sclCache.z );
						serial.ApplyModifiedProperties();
					}
				}
				#endregion
			}
			else
			{ // Else, draw combined scale.
				if ( localScale.vector3Value.x != localScale.vector3Value.y || localScale.vector3Value.y != localScale.vector3Value.z || !IsEntireSelectionEqualInScale() )
				{
					EditorGUI.showMixedValue = true;
				}
				else
				{
					EditorGUI.showMixedValue = false;
				}

				float monoScale;
				monoScale = localScale.vector3Value.x;

				EditorGUI.BeginChangeCheck();
				monoScale = EditorGUILayout.FloatField( "S", monoScale );

				// SAVE
				if ( EditorGUI.EndChangeCheck() )
				{
					Vector3 newMonoScale = new Vector3( monoScale, monoScale, monoScale );
					localScale.vector3Value = newMonoScale;
				}
			}

			// End block.
			EditorGUI.showMixedValue = false;
			EditorGUILayout.EndHorizontal();
			EditorGUI.EndProperty();
		}

		void DrawWorldPositionGUI()
		{
			GUILayout.Space( 6 );
			EditorGUILayout.Vector3Field( "Position", targetTransform.position );
		}
		void DrawWorldRotationGUI()
		{
			GUILayout.Space( 2 );
			EditorGUILayout.Vector3Field( "Rotation", targetTransform.eulerAngles );
		}
		void DrawWorldScaleGUI()
		{
			GUILayout.Space( 2 );
			EditorGUILayout.Vector3Field( "Scale", targetTransform.lossyScale );
		}
		#endregion

		#region Check Methods
		bool IsEntireSelectionEqualInXPos()
		{
			if ( targets.Length <= 1 )
			{
				return true;
			}

			for ( int i = 1 ; i < targets.Length ; i++ )
			{
				UnityEngine.Transform transform2 = (UnityEngine.Transform)targets[i];
				if ( targetTransform.localPosition.x != transform2.localPosition.x )
				{
					return false;
				}
			}

			return true;
		}

		bool IsEntireSelectionEqualInYPos()
		{
			if ( targets.Length <= 1 )
			{
				return true;
			}

			for ( int i = 1 ; i < targets.Length ; i++ )
			{
				UnityEngine.Transform transform2 = (UnityEngine.Transform)targets[i];
				if ( targetTransform.localPosition.y != transform2.localPosition.y )
				{
					return false;
				}
			}

			return true;
		}

		bool IsEntireSelectionEqualInZPos()
		{
			if ( targets.Length <= 1 )
			{
				return true;
			}

			for ( int i = 1 ; i < targets.Length ; i++ )
			{
				UnityEngine.Transform transform2 = (UnityEngine.Transform)targets[i];
				if ( targetTransform.localPosition.z != transform2.localPosition.z )
				{
					return false;
				}
			}

			return true;
		}

		bool IsPositionTooBig()
		{
			if ( targetTransform.position.x >= 100001 || targetTransform.position.y >= 100001 || targetTransform.position.z >= 100001 )
			{
				return true;
			}

			return false;
		}

		bool IsEntireSelectionEqualInXScl()
		{
			if ( targets.Length <= 1 )
			{
				return true;
			}

			for ( int i = 1 ; i < targets.Length ; i++ )
			{
				UnityEngine.Transform transform2 = (UnityEngine.Transform)targets[i];
				if ( targetTransform.localScale.x != transform2.localScale.x )
				{
					return false;
				}
			}

			return true;
		}

		bool IsEntireSelectionEqualInYScl()
		{
			if ( targets.Length <= 1 )
			{
				return true;
			}

			for ( int i = 1 ; i < targets.Length ; i++ )
			{
				UnityEngine.Transform transform2 = (UnityEngine.Transform)targets[i];
				if ( targetTransform.localScale.y != transform2.localScale.y )
				{
					return false;
				}
			}

			return true;
		}

		bool IsEntireSelectionEqualInZScl()
		{
			if ( targets.Length <= 1 )
			{
				return true;
			}

			for ( int i = 1 ; i < targets.Length ; i++ )
			{
				UnityEngine.Transform transform2 = (UnityEngine.Transform)targets[i];
				if ( targetTransform.localScale.z != transform2.localScale.z )
				{
					return false;
				}
			}

			return true;
		}

		bool IsEntireSelectionEqualInScale()
		{
			return IsEntireSelectionEqualInXScl() && IsEntireSelectionEqualInYScl() && IsEntireSelectionEqualInZScl();

		}

		bool IsEntireSelectionEqualInRotation()
		{
			foreach ( UnityEngine.Transform t in Selection.GetTransforms( SelectionMode.Editable ) )
			{
				SerializedObject obj = new SerializedObject( t );
				SerializedProperty hint = obj.FindProperty( "m_LocalEulerAnglesHint" );

				if ( hint.vector3Value != localEulerHint.vector3Value )
				{
					return false;
				}
			}

			return true;
		}

		bool IsEntireSelectionEqualInXRot()
		{
			foreach ( UnityEngine.Transform t in Selection.GetTransforms( SelectionMode.Editable ) )
			{
				SerializedObject obj = new SerializedObject( t );
				SerializedProperty hint = obj.FindProperty( "m_LocalEulerAnglesHint" );

				if ( hint.vector3Value.x != localEulerHint.vector3Value.x )
				{
					return false;
				}
			}

			return true;

		}

		bool IsEntireSelectionEqualInYRot()
		{
			foreach ( UnityEngine.Transform t in Selection.GetTransforms( SelectionMode.Editable ) )
			{
				SerializedObject obj = new SerializedObject( t );
				SerializedProperty hint = obj.FindProperty( "m_LocalEulerAnglesHint" );

				if ( hint.vector3Value.y != localEulerHint.vector3Value.y )
				{
					return false;
				}
			}

			return true;
		}

		bool IsEntireSelectionEqualInZRot()
		{
			foreach ( UnityEngine.Transform t in Selection.GetTransforms( SelectionMode.Editable ) )
			{
				SerializedObject obj = new SerializedObject( t );
				SerializedProperty hint = obj.FindProperty( "m_LocalEulerAnglesHint" );

				if ( hint.vector3Value.z != localEulerHint.vector3Value.z )
				{
					return false;
				}
			}

			return true;
		}

		#endregion
	}
}