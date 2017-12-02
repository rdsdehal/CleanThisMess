using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Internal;

namespace bTools
{
	// Adds functions to the EditorApplication delegates
	[InitializeOnLoad, ExcludeFromDocs]
	public static class EditorHooks
	{
		static EditorHooks()
		{
			EditorApplication.update += EditorUpdate;
		}

		static void EditorUpdate()
		{
			if ( Settings.Get<ToolsSettings_General>().preventLockedFromSelection )
			{
				RemoveLockedFromSelection();
			}
		}

		static void RemoveLockedFromSelection()
		{
			Object[] sel = Selection.objects;
			List<Object> newSel = new List<Object>( sel.Length );

			for ( int i = 0 ; i < sel.Length ; i++ )
			{// If it is locked, skip.
				if ( ( (int)sel[i].hideFlags & 8 ) != 0 && sel[i] is GameObject && PrefabUtility.GetPrefabType( sel[i] ) != PrefabType.ModelPrefab )
				{
					continue;
				}

				// If any of the above conditions are false, keep it selected.
				newSel.Add( sel[i] );
			}

			Selection.objects = newSel.ToArray();
		}
	}
}