using UnityEditor;

namespace bTools.TransformComponent
{
	[CustomEditor( typeof( ObjectData ) )]
	public class ObjectDataEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			EditorGUILayout.HelpBox( "Used by bTools to store data about this GameObject", MessageType.Info );
		}
	}
}