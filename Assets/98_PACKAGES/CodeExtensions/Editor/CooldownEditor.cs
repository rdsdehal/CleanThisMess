using UnityEngine;
using UnityEngine.Internal;
using UnityEditor;

namespace bTools.CodeExtensions
{
	[CustomPropertyDrawer( typeof( Cooldown ) )]
	[CustomPropertyDrawer( typeof( UnscaledCooldown ) )]
	[ExcludeFromDocs]
	public class CooldownEditor : PropertyDrawer
	{
		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
		{
			EditorGUI.BeginProperty( position, label, property );
			EditorGUI.PropertyField( position, property.FindPropertyRelative( "m_duration" ), label );
			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight( SerializedProperty property, GUIContent label )
		{
			return base.GetPropertyHeight( property, label );
		}
	}
}