using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalusDot : MonoBehaviour
{
	public ParticleSystem fx;
	public float dotPerSecondMalus;
	private MayhemMeter mayhemMeter;

	private void Awake()
	{
		mayhemMeter = FindObjectOfType<MayhemMeter>();
		fx.Play( true );
	}

	void Update()
	{
		mayhemMeter.ChangeMeter( -dotPerSecondMalus * Time.deltaTime );
	}
}