using UnityEngine;
using UnityEditor;

namespace bTools.TransformComponent
{
	public class TransformToolsWindow : EditorWindow
	{
		#region Properties
		Vector2 scroll;

		//Align section
		bool alignFoldout;
		string alignFoldoutText;
		static bool[] alignOptions = new bool[9] { true, true, true, false, false, false, false, false, false };
		public static UnityEngine.Transform targetAlignTransform;

		//Parent section
		bool parentFoldout;
		string parentFoldoutText;
		public static UnityEngine.Transform targetParentTransform;

		//Distribute section
		bool distributeFoldout;
		string distributeFoldoutText;
		static UnityEngine.Transform distributeStartBounds;
		static UnityEngine.Transform distributeEndBounds;

		//Noise section
		bool noiseFoldout;
		string noiseFoldoutText;
		Vector2 noiseMinMax;
		TransformTools.NoiseMode noiseMode;

		//Nudge section
		bool nudgeFoldout;
		string nudgeFoldoutText;
		float nudgeAmount = 0.5f;
		#endregion

		[MenuItem( "bTools/Transform/Transform Tools Window", false, 1400 )]
		public static void OpenWindow()
		{
			var window = GetWindow<TransformToolsWindow>( string.Empty, true );
			window.titleContent = new GUIContent( "Transform", Ressources.bToolsSkin.FindStyle( "tools" ).normal.background, "General tools for Transforms" );
			window.minSize = new Vector2( 230, 100 );
		}

		public void OnGUI()
		{
			//Window setup
			GUILayout.Space( 2 );

			scroll = EditorGUILayout.BeginScrollView( scroll );

			EditorGUIUtility.labelWidth = 9999;
			alignFoldoutText = alignFoldout ? "▼Align and Look At" : "►Align and Look At";
			alignFoldout = EditorGUILayout.Foldout( alignFoldout, alignFoldoutText, true, EditorStyles.boldLabel );
			EditorGUIUtility.labelWidth = 0;
			if ( alignFoldout )
			{
				DrawAlignSection();
			}

			GUILayout.Space( 2 );
			EditorGUIUtility.labelWidth = 9999;
			parentFoldoutText = parentFoldout ? "▼Parenting" : "►Parenting";
			parentFoldout = EditorGUILayout.Foldout( parentFoldout, parentFoldoutText, true, EditorStyles.boldLabel );
			EditorGUIUtility.labelWidth = 0;
			if ( parentFoldout )
			{
				DrawParentSection();
			}
			//GUI.backgroundColor = GUI.de

			GUILayout.Space( 2 );
			EditorGUIUtility.labelWidth = 9999;
			distributeFoldoutText = distributeFoldout ? "▼Distribute" : "►Distribute";
			distributeFoldout = EditorGUILayout.Foldout( distributeFoldout, distributeFoldoutText, true, EditorStyles.boldLabel );
			EditorGUIUtility.labelWidth = 0;
			if ( distributeFoldout )
			{
				DrawDistributeSection();
			}

			GUILayout.Space( 2 );
			EditorGUIUtility.labelWidth = 9999;
			noiseFoldoutText = noiseFoldout ? "▼Noise" : "►Noise";
			noiseFoldout = EditorGUILayout.Foldout( noiseFoldout, noiseFoldoutText, true, EditorStyles.boldLabel );
			EditorGUIUtility.labelWidth = 0;
			if ( noiseFoldout )
			{
				DrawNoiseSection();
			}

			GUILayout.Space( 2 );
			EditorGUIUtility.labelWidth = 9999;
			nudgeFoldoutText = nudgeFoldout ? "▼Nudge" : "►Nudge";
			nudgeFoldout = EditorGUILayout.Foldout( nudgeFoldout, nudgeFoldoutText, true, EditorStyles.boldLabel );
			EditorGUIUtility.labelWidth = 0;
			if ( nudgeFoldout )
			{
				DrawNudgeSection();
			}

			EditorGUILayout.EndScrollView();
		}

		//Align 
		void DrawAlignSection()
		{

			EditorGUILayout.LabelField( "Transform to align to", EditorStyles.boldLabel, GUILayout.Width( 180 ) );
			bTools.TransformComponent.TransformToolsWindow.targetAlignTransform = EditorGUILayout.ObjectField( (Object)bTools.TransformComponent.TransformToolsWindow.targetAlignTransform, typeof( UnityEngine.Transform ), true, GUILayout.Width( 189 ) ) as UnityEngine.Transform;
			EditorGUILayout.LabelField( "Position", EditorStyles.boldLabel, GUILayout.Width( 180 ) );
			EditorGUILayout.BeginHorizontal();
			alignOptions[0] = GUILayout.Toggle( alignOptions[0], "X", "Button", GUILayout.Width( 40 ), GUILayout.Height( 16 ) );
			alignOptions[1] = GUILayout.Toggle( alignOptions[1], "Y", "Button", GUILayout.Width( 40 ), GUILayout.Height( 16 ) );
			alignOptions[2] = GUILayout.Toggle( alignOptions[2], "Z", "Button", GUILayout.Width( 40 ), GUILayout.Height( 16 ) );

			if ( alignOptions[0] && alignOptions[1] && alignOptions[2] )
			{

				if ( GUILayout.Button( "None", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
				{

					alignOptions[0] = false;
					alignOptions[1] = false;
					alignOptions[2] = false;
				}
			}

			else
			{
				if ( GUILayout.Button( "All", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
				{

					alignOptions[0] = true;
					alignOptions[1] = true;
					alignOptions[2] = true;
				}
			}

			EditorGUILayout.EndHorizontal();
			EditorGUILayout.LabelField( "Rotation", EditorStyles.boldLabel, GUILayout.Width( 180 ) );
			EditorGUILayout.BeginHorizontal();
			alignOptions[3] = GUILayout.Toggle( alignOptions[3], "X", "Button", GUILayout.Width( 40 ), GUILayout.Height( 16 ) );
			alignOptions[4] = GUILayout.Toggle( alignOptions[4], "Y", "Button", GUILayout.Width( 40 ), GUILayout.Height( 16 ) );
			alignOptions[5] = GUILayout.Toggle( alignOptions[5], "Z", "Button", GUILayout.Width( 40 ), GUILayout.Height( 16 ) );

			if ( alignOptions[3] && alignOptions[4] && alignOptions[5] )
			{
				if ( GUILayout.Button( "None", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
				{
					alignOptions[3] = false;
					alignOptions[4] = false;
					alignOptions[5] = false;
				}
			}

			else
			{
				if ( GUILayout.Button( "All", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
				{

					alignOptions[3] = true;
					alignOptions[4] = true;
					alignOptions[5] = true;
				}
			}

			EditorGUILayout.EndHorizontal();

			EditorGUILayout.LabelField( "Scale", EditorStyles.boldLabel, GUILayout.Width( 180 ) );
			EditorGUILayout.BeginHorizontal();
			alignOptions[6] = GUILayout.Toggle( alignOptions[6], "X", "Button", GUILayout.Width( 40 ), GUILayout.Height( 16 ) );
			alignOptions[7] = GUILayout.Toggle( alignOptions[7], "Y", "Button", GUILayout.Width( 40 ), GUILayout.Height( 16 ) );
			alignOptions[8] = GUILayout.Toggle( alignOptions[8], "Z", "Button", GUILayout.Width( 40 ), GUILayout.Height( 16 ) );

			if ( alignOptions[6] && alignOptions[7] && alignOptions[8] )
			{
				if ( GUILayout.Button( "None", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
				{
					alignOptions[6] = false;
					alignOptions[7] = false;
					alignOptions[8] = false;
				}
			}

			else
			{
				if ( GUILayout.Button( "All", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
				{
					alignOptions[6] = true;
					alignOptions[7] = true;
					alignOptions[8] = true;
				}
			}
			EditorGUILayout.EndHorizontal();
			GUILayout.Space( 2 );
			EditorGUILayout.BeginHorizontal();
			EditorGUI.BeginDisabledGroup( targetAlignTransform == null ? true : false );

			if ( GUILayout.Button( "Apply align", GUILayout.Width( 84 ), GUILayout.Height( 18 ) ) )
			{
				Undo.RecordObjects( Selection.transforms, "Transform align" );
				TransformTools.AlignTransforms( targetAlignTransform, Selection.transforms, alignOptions );
			}

			if ( GUILayout.Button( "Look At", GUILayout.Width( 84 ), GUILayout.Height( 18 ) ) )
			{
				Undo.RecordObjects( Selection.transforms, "Transform LookAt" );
				for ( int i = 0 ; i < Selection.transforms.Length ; i++ )
				{
					Selection.transforms[i].LookAt( targetAlignTransform );
				}
			}

			EditorGUI.EndDisabledGroup();

			EditorGUILayout.EndHorizontal();
		}

		//Parenting 
		void DrawParentSection()
		{
			EditorGUILayout.LabelField( "Target Transform", EditorStyles.boldLabel, GUILayout.Width( 180 ) );
			bTools.TransformComponent.TransformToolsWindow.targetParentTransform = EditorGUILayout.ObjectField( (Object)bTools.TransformComponent.TransformToolsWindow.targetParentTransform, typeof( UnityEngine.Transform ), true, GUILayout.Width( 189 ) ) as UnityEngine.Transform;
			EditorGUILayout.LabelField( "Parent to:", EditorStyles.boldLabel, GUILayout.Width( 180 ) );

			EditorGUILayout.BeginHorizontal();

			EditorGUI.BeginDisabledGroup( targetParentTransform == null ? true : false );
			if ( GUILayout.Button( "Target", GUILayout.Width( 84 ), GUILayout.Height( 18 ) ) )
			{
				foreach ( UnityEngine.Transform t in Selection.transforms )
				{
					Undo.SetTransformParent( t, (UnityEngine.Transform)bTools.TransformComponent.TransformToolsWindow.targetParentTransform, "Set new parent using transform tools" );
				}
			}

			if ( GUILayout.Button( "Deep", GUILayout.Width( 84 ), GUILayout.Height( 18 ) ) )
			{
				foreach ( UnityEngine.Transform t in Selection.GetTransforms( SelectionMode.Deep | SelectionMode.ExcludePrefab | SelectionMode.Editable ) )
				{
					Undo.SetTransformParent( t, (UnityEngine.Transform)bTools.TransformComponent.TransformToolsWindow.targetParentTransform, "Set new parent using transform tools" );
				}
			}
			EditorGUI.EndDisabledGroup();

			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();

			if ( GUILayout.Button( "New", GUILayout.Width( 84 ), GUILayout.Height( 18 ) ) )
			{
				GameObject newParent = new GameObject();
				newParent.name = "Folder GameObject";
				Undo.RegisterCreatedObjectUndo( newParent, "Created folder using transform tools" );

				foreach ( UnityEngine.Transform t in Selection.transforms )
				{
					Undo.SetTransformParent( t, newParent.transform, "Created folder using transform tools" );
				}
			}

			if ( GUILayout.Button( "Deep", GUILayout.Width( 84 ), GUILayout.Height( 18 ) ) )
			{
				GameObject newParent = new GameObject();
				newParent.name = "Folder GameObject";
				Undo.RegisterCreatedObjectUndo( newParent, "Created folder using transform tools" );

				foreach ( UnityEngine.Transform t in Selection.GetTransforms( SelectionMode.Deep | SelectionMode.ExcludePrefab | SelectionMode.Editable ) )
				{
					Undo.SetTransformParent( t, newParent.transform, "Created folder using transform tools" );
				}
			}

			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();

			if ( GUILayout.Button( "World", GUILayout.Width( 84 ), GUILayout.Height( 18 ) ) )
			{
				foreach ( UnityEngine.Transform t in Selection.transforms )
				{
					Undo.SetTransformParent( t, null, "Parented to world using transform tools" );
				}
			}

			if ( GUILayout.Button( "Deep", GUILayout.Width( 84 ), GUILayout.Height( 18 ) ) )
			{
				foreach ( UnityEngine.Transform t in Selection.GetTransforms( SelectionMode.Deep | SelectionMode.ExcludePrefab | SelectionMode.Editable ) )
				{
					Undo.SetTransformParent( t, null, "Parented to world using transform tools" );
				}
			}

			EditorGUILayout.EndHorizontal();
		}

		//Distribute 
		void DrawDistributeSection()
		{
			EditorGUILayout.LabelField( "Bounds: Start/End", EditorStyles.boldLabel, GUILayout.Width( 180 ) );

			EditorGUILayout.BeginHorizontal();
			bTools.TransformComponent.TransformToolsWindow.distributeStartBounds = EditorGUILayout.ObjectField( string.Empty, (Object)bTools.TransformComponent.TransformToolsWindow.distributeStartBounds, typeof( UnityEngine.Transform ), true, GUILayout.Width( 92 ) ) as UnityEngine.Transform;
			bTools.TransformComponent.TransformToolsWindow.distributeEndBounds = EditorGUILayout.ObjectField( string.Empty, (Object)bTools.TransformComponent.TransformToolsWindow.distributeEndBounds, typeof( UnityEngine.Transform ), true, GUILayout.Width( 92 ) ) as UnityEngine.Transform;
			EditorGUILayout.EndHorizontal();
			GUILayout.Space( 2 );

			EditorGUI.BeginDisabledGroup( distributeStartBounds == null || distributeEndBounds == null ? true : false );

			EditorGUILayout.BeginHorizontal();
			if ( GUILayout.Button( "Pos", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				if ( !ValidateDistributeSelection() )
					return;

				TransformTools.DistributePosition( distributeStartBounds.position, distributeEndBounds.position, Selection.transforms );
			}

			if ( GUILayout.Button( "X", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				if ( !ValidateDistributeSelection() )
					return;

				Undo.RecordObjects( Selection.transforms, "Transform Distribute" );
				TransformTools.DistributeXPosition( distributeStartBounds.position, distributeEndBounds.position, Selection.transforms );
			}
			if ( GUILayout.Button( "Y", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				if ( !ValidateDistributeSelection() )
					return;

				TransformTools.DistributeYPosition( distributeStartBounds.position, distributeEndBounds.position, Selection.transforms );
			}
			if ( GUILayout.Button( "Z", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				if ( !ValidateDistributeSelection() )
					return;

				TransformTools.DistributeZPosition( distributeStartBounds.position, distributeEndBounds.position, Selection.transforms );
			}

			EditorGUILayout.EndHorizontal();

			if ( GUILayout.Button( "Rotation (Quaternion)", GUILayout.Width( 172 ), GUILayout.Height( 16 ) ) )
			{
				if ( !ValidateDistributeSelection() )
					return;
				TransformTools.DistributeRotation( distributeStartBounds.rotation, distributeEndBounds.rotation, Selection.transforms );
			}

			EditorGUILayout.BeginHorizontal();
			if ( GUILayout.Button( "Scl", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				if ( !ValidateDistributeSelection() )
					return;

				Undo.RecordObjects( Selection.transforms, "Transform Distribute" );
				TransformTools.DistributeScale( distributeStartBounds.localScale, distributeEndBounds.localScale, Selection.transforms );
			}

			if ( GUILayout.Button( "X", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				if ( !ValidateDistributeSelection() )
					return;

				Undo.RecordObjects( Selection.transforms, "Transform Distribute" );
				TransformTools.DistributeXScale( distributeStartBounds.localScale.x, distributeEndBounds.localScale.x, Selection.transforms );
			}
			if ( GUILayout.Button( "Y", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				if ( !ValidateDistributeSelection() )
					return;

				Undo.RecordObjects( Selection.transforms, "Transform Distribute" );
				TransformTools.DistributeYScale( distributeStartBounds.localScale.y, distributeEndBounds.localScale.y, Selection.transforms );
			}
			if ( GUILayout.Button( "Z", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				if ( !ValidateDistributeSelection() )
					return;

				Undo.RecordObjects( Selection.transforms, "Transform Distribute" );
				TransformTools.DistributeZScale( distributeStartBounds.localScale.z, distributeEndBounds.localScale.z, Selection.transforms );
			}

			EditorGUILayout.EndHorizontal();

			EditorGUI.EndDisabledGroup();
		}

		bool ValidateDistributeSelection()
		{
			if ( Selection.transforms.Length == 0 )
			{
				Debug.LogWarning( "[Distribute Tool] Selection is empty" );
				return false;
			}

			foreach ( UnityEngine.Transform t in Selection.transforms )
			{
				if ( t == bTools.TransformComponent.TransformToolsWindow.distributeStartBounds || t == bTools.TransformComponent.TransformToolsWindow.distributeEndBounds )
				{
					Debug.LogWarning( "[Distribute Tool] Selection can't contain bounding objects" );
					return false;
				}
			}

			return true;
		}

		//Noise 
		void DrawNoiseSection()
		{
			GUILayout.Label( "Noise range", EditorStyles.boldLabel, GUILayout.Width( 180 ) );

			EditorGUILayout.BeginHorizontal();
			EditorGUIUtility.labelWidth = 26;
			noiseMinMax.x = EditorGUILayout.FloatField( "Min", noiseMinMax.x, GUILayout.Width( 84 ) );
			GUILayout.Space( -2 );
			noiseMinMax.y = EditorGUILayout.FloatField( "Max", noiseMinMax.y, GUILayout.Width( 84 ) );
			EditorGUILayout.EndHorizontal();

			GUILayout.Space( 1 );
			EditorGUIUtility.labelWidth = 50;
			noiseMode = (TransformTools.NoiseMode)EditorGUILayout.EnumPopup( "Mode", noiseMode, GUILayout.Width( 170 ) );
			EditorGUIUtility.labelWidth = 0;

			EditorGUILayout.BeginHorizontal();
			GUILayout.Space( 8 );
			if ( GUILayout.Button( "Pos", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				Undo.RecordObjects( Selection.transforms, "Transform Noise" );
				TransformTools.AddPosNoise( noiseMinMax.x, noiseMinMax.y, Selection.transforms, noiseMode );
			}
			if ( GUILayout.Button( "X", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				Undo.RecordObjects( Selection.transforms, "Transform Noise" );
				TransformTools.AddXPosNoise( noiseMinMax.x, noiseMinMax.y, Selection.transforms, noiseMode );
			}
			if ( GUILayout.Button( "Y", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				Undo.RecordObjects( Selection.transforms, "Transform Noise" );
				TransformTools.AddYPosNoise( noiseMinMax.x, noiseMinMax.y, Selection.transforms, noiseMode );
			}
			if ( GUILayout.Button( "Z", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				Undo.RecordObjects( Selection.transforms, "Transform Noise" );
				TransformTools.AddZPosNoise( noiseMinMax.x, noiseMinMax.y, Selection.transforms, noiseMode );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Space( 8 );
			if ( GUILayout.Button( "Rot", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				Undo.RecordObjects( Selection.transforms, "Transform Noise" );
				TransformTools.AddRotNoise( noiseMinMax.x, noiseMinMax.y, Selection.transforms, noiseMode );
			}
			if ( GUILayout.Button( "X", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				Undo.RecordObjects( Selection.transforms, "Transform Noise" );
				TransformTools.AddXRotNoise( noiseMinMax.x, noiseMinMax.y, Selection.transforms, noiseMode );
			}
			if ( GUILayout.Button( "Y", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				Undo.RecordObjects( Selection.transforms, "Transform Noise" );
				TransformTools.AddYRotNoise( noiseMinMax.x, noiseMinMax.y, Selection.transforms, noiseMode );
			}
			if ( GUILayout.Button( "Z", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				Undo.RecordObjects( Selection.transforms, "Transform Noise" );
				TransformTools.AddZRotNoise( noiseMinMax.x, noiseMinMax.y, Selection.transforms, noiseMode );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Space( 8 );
			if ( GUILayout.Button( "Scl", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				Undo.RecordObjects( Selection.transforms, "Transform Noise" );
				TransformTools.AddSclNoise( noiseMinMax.x, noiseMinMax.y, Selection.transforms, noiseMode );
			}
			if ( GUILayout.Button( "X", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				Undo.RecordObjects( Selection.transforms, "Transform Noise" );
				TransformTools.AddXSclNoise( noiseMinMax.x, noiseMinMax.y, Selection.transforms, noiseMode );
			}
			if ( GUILayout.Button( "Y", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				Undo.RecordObjects( Selection.transforms, "Transform Noise" );
				TransformTools.AddYSclNoise( noiseMinMax.x, noiseMinMax.y, Selection.transforms, noiseMode );
			}
			if ( GUILayout.Button( "Z", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				Undo.RecordObjects( Selection.transforms, "Transform Noise" );
				TransformTools.AddZSclNoise( noiseMinMax.x, noiseMinMax.y, Selection.transforms, noiseMode );
			}

			EditorGUILayout.EndHorizontal();
		}

		//Nudge
		void DrawNudgeSection()
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField( "Amount", GUILayout.Width( 50 ) );
			nudgeAmount = EditorGUILayout.FloatField( string.Empty, nudgeAmount, GUILayout.Width( 45 ) );
			if ( GUILayout.Button( "-", GUILayout.Width( 28 ), GUILayout.Height( 16 ) ) )
			{
				nudgeAmount = -nudgeAmount;
				GUIUtility.keyboardControl = 0;
			}
			EditorGUILayout.EndHorizontal();

			GUILayout.Space( 4 );

			EditorGUILayout.BeginHorizontal();
			if ( GUILayout.Button( ".25", GUILayout.Width( 28 ), GUILayout.Height( 16 ) ) )
			{
				nudgeAmount = 0.25f;
				GUIUtility.keyboardControl = 0;
			}
			if ( GUILayout.Button( ".5", GUILayout.Width( 28 ), GUILayout.Height( 16 ) ) )
			{
				nudgeAmount = 0.5f;
				GUIUtility.keyboardControl = 0;
			}
			if ( GUILayout.Button( "1", GUILayout.Width( 28 ), GUILayout.Height( 16 ) ) )
			{
				nudgeAmount = 1f;
				GUIUtility.keyboardControl = 0;
			}
			if ( GUILayout.Button( "10", GUILayout.Width( 28 ), GUILayout.Height( 16 ) ) )
			{
				nudgeAmount = 10f;
				GUIUtility.keyboardControl = 0;
			}
			if ( GUILayout.Button( "45", GUILayout.Width( 28 ), GUILayout.Height( 16 ) ) )
			{
				nudgeAmount = 45f;
				GUIUtility.keyboardControl = 0;
			}
			if ( GUILayout.Button( "90", GUILayout.Width( 28 ), GUILayout.Height( 16 ) ) )
			{
				nudgeAmount = 90f;
				GUIUtility.keyboardControl = 0;
			}

			EditorGUILayout.EndHorizontal();

			GUILayout.Space( 4 );
			EditorGUILayout.BeginHorizontal();
			if ( GUILayout.Button( "Pos", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				Vector3 nudge = new Vector3( nudgeAmount, nudgeAmount, nudgeAmount );
				foreach ( UnityEngine.Transform transform in Selection.GetTransforms( SelectionMode.Editable ) )
				{
					transform.localPosition += nudge;
				}
			}
			if ( GUILayout.Button( "X", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				Vector3 nudge = new Vector3( nudgeAmount, 0, 0 );
				foreach ( UnityEngine.Transform transform in Selection.GetTransforms( SelectionMode.Editable ) )
				{
					transform.localPosition += nudge;
				}
			}
			if ( GUILayout.Button( "Y", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				Vector3 nudge = new Vector3( 0, nudgeAmount, 0 );
				foreach ( UnityEngine.Transform transform in Selection.GetTransforms( SelectionMode.Editable ) )
				{
					transform.localPosition += nudge;
				}
			}
			if ( GUILayout.Button( "Z", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				Vector3 nudge = new Vector3( 0, 0, nudgeAmount );
				foreach ( UnityEngine.Transform transform in Selection.GetTransforms( SelectionMode.Editable ) )
				{
					transform.localPosition += nudge;
				}
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			if ( GUILayout.Button( "Rot", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				Quaternion nudge = Quaternion.Euler( nudgeAmount, nudgeAmount, nudgeAmount );
				foreach ( UnityEngine.Transform transform in Selection.GetTransforms( SelectionMode.Editable ) )
				{
					transform.localRotation *= nudge;
				}
			}
			if ( GUILayout.Button( "X", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				Quaternion nudge = Quaternion.Euler( nudgeAmount, 0, 0 );
				foreach ( UnityEngine.Transform transform in Selection.GetTransforms( SelectionMode.Editable ) )
				{
					transform.localRotation *= nudge;
				}
			}
			if ( GUILayout.Button( "Y", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				Quaternion nudge = Quaternion.Euler( 0, nudgeAmount, 0 );
				foreach ( UnityEngine.Transform transform in Selection.GetTransforms( SelectionMode.Editable ) )
				{
					transform.localRotation *= nudge;
				}
			}
			if ( GUILayout.Button( "Z", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				Quaternion nudge = Quaternion.Euler( 0, 0, nudgeAmount );
				foreach ( UnityEngine.Transform transform in Selection.GetTransforms( SelectionMode.Editable ) )
				{
					transform.localRotation *= nudge;
				}
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			if ( GUILayout.Button( "Scl", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				Vector3 nudge = new Vector3( nudgeAmount, nudgeAmount, nudgeAmount );
				foreach ( UnityEngine.Transform transform in Selection.GetTransforms( SelectionMode.Editable ) )
				{
					transform.localScale += nudge;
				}
			}
			if ( GUILayout.Button( "X", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				Vector3 nudge = new Vector3( nudgeAmount, nudgeAmount, nudgeAmount );
				foreach ( UnityEngine.Transform transform in Selection.GetTransforms( SelectionMode.Editable ) )
				{
					transform.localScale += nudge;
				}
			}
			if ( GUILayout.Button( "Y", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				Vector3 nudge = new Vector3( nudgeAmount, nudgeAmount, nudgeAmount );
				foreach ( UnityEngine.Transform transform in Selection.GetTransforms( SelectionMode.Editable ) )
				{
					transform.localScale += nudge;
				}
			}
			if ( GUILayout.Button( "Z", GUILayout.Width( 40 ), GUILayout.Height( 16 ) ) )
			{
				Vector3 nudge = new Vector3( nudgeAmount, nudgeAmount, nudgeAmount );
				foreach ( UnityEngine.Transform transform in Selection.GetTransforms( SelectionMode.Editable ) )
				{
					transform.localScale += nudge;
				}
			}

			EditorGUILayout.EndHorizontal();
		}

		//Shortcut calls
		public static void SetTargetTransform( UnityEngine.Transform target )
		{
			targetAlignTransform = target;
			targetParentTransform = target;
		}
	}
}
