using UnityEditor;
using UnityEngine;
using UnityEngine.Internal;

namespace bTools.CodeExtensions
{
	[CustomPropertyDrawer( typeof( RandomInt ) )]
	[CustomPropertyDrawer( typeof( RandomFloat ) )]
	[ExcludeFromDocs]
	public class RandomRangeDrawer : PropertyDrawer
	{
		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
		{
			EditorGUI.BeginProperty( position, label, property );

			var indent = EditorGUI.indentLevel;
			var oldWidth = EditorGUIUtility.labelWidth;
			EditorGUI.indentLevel = 0;

			if ( EditorGUIUtility.wideMode )
			{
				position = EditorGUI.PrefixLabel( position, GUIUtility.GetControlID( FocusType.Passive ), label );

				Rect minRect = new Rect( position );
				minRect.width /= 2;
				Rect maxRect = new Rect( minRect );
				maxRect.x = minRect.xMax;

				EditorGUIUtility.labelWidth = 29;
				EditorGUI.PropertyField( minRect, property.FindPropertyRelative( "min" ), new GUIContent( "Min:" ) );
				EditorGUI.PropertyField( maxRect, property.FindPropertyRelative( "max" ), new GUIContent( "Max:" ) );
			}
			else
			{
				position.height /= 2;
				EditorGUI.LabelField( position, label );

				Rect minRect = new Rect( position );
				minRect.y += position.height;
				minRect.x += 17;
				minRect.xMax -= 17;
				minRect.width = ( minRect.width / 2 );
				Rect maxRect = new Rect( minRect );
				maxRect.x = minRect.xMax;

				EditorGUIUtility.labelWidth = 29;
				EditorGUI.PropertyField( minRect, property.FindPropertyRelative( "min" ), new GUIContent( "Min:" ) );
				EditorGUI.PropertyField( maxRect, property.FindPropertyRelative( "max" ), new GUIContent( "Max:" ) );
			}

			EditorGUI.indentLevel = indent;
			EditorGUIUtility.labelWidth = oldWidth;

			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight( SerializedProperty property, GUIContent label )
		{
			if ( EditorGUIUtility.wideMode ) return base.GetPropertyHeight( property, label );
			else return base.GetPropertyHeight( property, label ) * 2;
		}
	}
}