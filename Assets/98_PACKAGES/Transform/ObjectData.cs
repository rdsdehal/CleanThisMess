using System.Collections.Generic;
using UnityEngine;
using bTools.CodeExtensions;

namespace bTools.TransformComponent
{
	[System.Serializable]
	[DisallowMultipleComponent]
	public class ObjectData : MonoBehaviour
	{
		[SerializeField, HideInInspector] public List<TransformData> m_savedTransformValues = new List<TransformData>();
		[SerializeField, HideInInspector] public List<string> m_savedTransformKeys = new List<string>();
	}
}
