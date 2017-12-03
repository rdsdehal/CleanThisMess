using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinnedMeshRandomMat : MonoBehaviour
{
	public Material[] mats;

	private void Awake()
	{
		GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial = mats[Random.Range( 0, mats.Length )];
	}
}
