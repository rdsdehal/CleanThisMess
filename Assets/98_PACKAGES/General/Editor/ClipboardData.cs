using UnityEditor;
using UnityEngine;
using UnityEngine.Internal;

namespace bTools
{
	// Holds clipboard data
	[InitializeOnLoad, ExcludeFromDocs]
	public static class ClipboardData
	{
		public static Vector3 clipboardPosition;
		public static Vector3 clipboardRotation;
		public static Vector3 clipboardScale;
		public static GameObject clipboardGameObject;

		static ClipboardData()
		{
		}
	}
}
