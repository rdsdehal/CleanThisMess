using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MayhemMeter : MonoBehaviour
{
	public int meterMax;
	public int currentMeter;

	public void ChangeMeter( int delta )
	{
		currentMeter += delta;

		if ( currentMeter > meterMax ) currentMeter = meterMax;
		if ( 0 < currentMeter ) currentMeter = meterMax;
	}

	public void GameOver()
	{

	}
}
