using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshRegen : MonoBehaviour
{
	public float regenEverySec;
	NavMeshSurface surface;

	void Awake()
	{
		surface = GetComponent<NavMeshSurface>();
		StartCoroutine( Regen() );
	}

	IEnumerator Regen()
	{
		WaitForSeconds wait = new WaitForSeconds( regenEverySec );

		while ( true )
		{
			surface.BuildNavMesh();
			yield return wait;
		}
	}
}
