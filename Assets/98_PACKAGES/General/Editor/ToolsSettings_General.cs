using System;
using UnityEngine;
using UnityEngine.Internal;
using UnityEditor;
using bTools.CodeExtensions;

namespace bTools
{
	[Serializable, ExcludeFromDocs]
	public class ToolsSettings_General : ToolsSettingsBase
	{
		[SerializeField] public Color accentColor = new Color32( 238, 89, 89, 255 );
		[SerializeField] public Color lightAccentColor = new Color32( 176, 119, 119, 255 );
		[SerializeField] public Color shadedBackgroundColor = new Color32( 69, 69, 69, 255 );
		[SerializeField] public bool preventLockedFromSelection = false;

		public ToolsSettings_General()
		{
			moduleName = "General";
			subCategories = new Action[] { General, Optional };
			order = -1000;
		}

		public void General()
		{
			GUILayout.Space( 4 );
			EditorGUILayout.LabelField( "General Settings", EditorStyles.boldLabel );
			accentColor = EditorGUILayout.ColorField( "Accent Color", accentColor );
			lightAccentColor = EditorGUILayout.ColorField( "Light Accent Color", lightAccentColor );
			shadedBackgroundColor = EditorGUILayout.ColorField( "Background Color", shadedBackgroundColor );
			GUILayout.Label( "Make Locked Unselectable" );
			preventLockedFromSelection = EditorGUIExtensions.LayoutToggleSwitch( new string[] { "Enabled", "Disabled" }, preventLockedFromSelection, EditorStyles.miniButton );
		}

		public void Optional()
		{
			EditorGUILayout.LabelField( "Optional options opt into this optimum space" );
		}
	}
}
