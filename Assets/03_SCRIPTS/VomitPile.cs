using System;
using UnityEngine;

public class VomitPile : MonoBehaviour
{
	public float cleanBonus;
	public float dotPerSecond;

	MayhemMeter meter;

	private void Awake()
	{
		meter = FindObjectOfType<MayhemMeter>();
	}

	void Update()
	{
		meter.ChangeMeter( -dotPerSecond * Time.deltaTime );
	}

	public void Clean()
	{
		meter.ChangeMeter( cleanBonus );
		Destroy( gameObject );
	}
}