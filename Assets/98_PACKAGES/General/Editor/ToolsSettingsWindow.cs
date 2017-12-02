using UnityEngine;
using UnityEngine.Internal;
using UnityEditor;
using System.Linq;
using System.Reflection;
using bTools.CodeExtensions;

namespace bTools
{
	[ExcludeFromDocs]
	public class ToolsSettingsWindow : EditorWindow
	{
		// GUI parameters.
		Vector2 modulesScroll;
		Vector2 subModulesScroll;
		int selectedModule = 0;
		int selectedSubModule = 0;

		[MenuItem( "bTools/Settings", false, 2000 )]
		static void Init()
		{
			var window = GetWindow<ToolsSettingsWindow>( "Settings", true );
			window.titleContent = new GUIContent( "Settings", Ressources.bToolsSkin.FindStyle( "settings" ).normal.background, "bTools Settings Window" );

			var settingClasses = Assembly.GetAssembly( typeof( ToolsSettingsBase ) ).GetTypes().Where( m => m.IsSubclassOf( typeof( ToolsSettingsBase ) ) );
			foreach ( var type in settingClasses )
			{
				Settings.Get( type );
			}
		}

		void OnGUI()
		{
			Rect toolsRect = new Rect( 0, 0, position.width / 3, position.height );
			Rect lineRect = new Rect( toolsRect.xMax - 2, 0, 2, position.height );

			GUILayout.BeginArea( toolsRect );
			EditorGUI.DrawRect( toolsRect, Settings.Get<ToolsSettings_General>().shadedBackgroundColor );

			// Modules list.
			using ( var scroll = new EditorGUILayout.ScrollViewScope( modulesScroll, GUIStyle.none, GUI.skin.verticalScrollbar ) )
			{
				modulesScroll = scroll.scrollPosition;

				for ( int i = 0 ; i < Settings.SettingsAssets.Count ; i++ )
				{
					EditorGUI.BeginChangeCheck();
					GUILayout.Toggle( i == selectedModule, Settings.SettingsAssets[i].moduleName, EditorStyles.toolbarButton );
					if ( EditorGUI.EndChangeCheck() )
					{
						selectedModule = i;
						selectedSubModule = 0;
					}

					if ( i == selectedModule )
					{
						for ( int j = 0 ; j < Settings.SettingsAssets[i].subCategories.Length ; j++ )
						{
							EditorGUI.BeginChangeCheck();
							string name = ObjectNames.NicifyVariableName( Settings.SettingsAssets[i].subCategories[j].Method.Name );
							GUILayout.Toggle( j == selectedSubModule, name, EditorStyles.helpBox );
							if ( EditorGUI.EndChangeCheck() )
							{
								selectedSubModule = j;
							}
						}
					}
				}
			}

			EditorGUI.DrawRect( lineRect, Colors.EerieBlack.WithAlpha( 0.8f ) );

			GUILayout.EndArea();

			// Module inner GUI.
			DrawSettingsSection( Settings.SettingsAssets[selectedModule] );
		}

		void DrawSettingsSection( ToolsSettingsBase module )
		{
			// Round values to ensure crisp text.
			Rect subModuleRect = new Rect( Mathf.Round( ( position.width / 3 ) + 2 ), 0, Mathf.Round( position.width - ( position.width / 3 ) - 2 ), position.height );

			GUI.BeginGroup( subModuleRect );
			GUILayout.BeginArea( new Rect( 0, 0, subModuleRect.width, subModuleRect.height ) );

			using ( var scroll = new EditorGUILayout.ScrollViewScope( subModulesScroll, GUIStyle.none, GUI.skin.verticalScrollbar ) )
			{
				subModulesScroll = scroll.scrollPosition;

				EditorGUI.BeginChangeCheck();

				EditorGUIUtility.labelWidth = subModuleRect.width / 2;

				try
				{
					module.subCategories[selectedSubModule].Invoke();
				}
				catch
				{//  System.Exception e
				 //Debug.LogError( "[bSettings] Exception caught while trying to draw the sub module: " + e.Message );
				 //Debug.LogError( e.StackTrace );
				 //Debug.LogError( e.Data );
				}

				EditorGUIUtility.labelWidth = 0;
				if ( EditorGUI.EndChangeCheck() )
				{
					EditorUtility.SetDirty( module );

					if ( module.OnSettingsChanged != null ) module.OnSettingsChanged();
				}
			}

			GUILayout.EndArea();
			GUI.EndGroup();
		}
	}
}
