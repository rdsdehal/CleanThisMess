using UnityEditor;

namespace bTools.TransformComponent
{
	public class TransformMenuItems : Editor
	{
		[MenuItem( "bTools/Transform/Set as Align Destination %&a", false, 1400 )]
		public static void SetAsAlignDestination()
		{
			TransformToolsWindow.SetTargetTransform( Selection.activeTransform );
			EditorWindow.GetWindow<TransformToolsWindow>();
		}
	}
}
