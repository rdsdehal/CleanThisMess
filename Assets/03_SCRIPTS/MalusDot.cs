using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalusDot : MonoBehaviour
{
	public float dotPerSecondMalus;
	private MayhemMeter mayhemMeter;

	private void Awake()
	{
		mayhemMeter = FindObjectOfType<MayhemMeter>();
	}

	void Update()
	{
		mayhemMeter.ChangeMeter( -dotPerSecondMalus * Time.deltaTime );
	}
}