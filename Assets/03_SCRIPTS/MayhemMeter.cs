using UnityEngine;

public class MayhemMeter : MonoBehaviour
{
	public float meterMax;
	public float currentMeter { get; private set; }

	private void Awake()
	{
		currentMeter = meterMax;
	}

	public void ChangeMeter( float delta )
	{
		currentMeter += delta;

		if ( currentMeter > meterMax ) currentMeter = meterMax;
		if ( 0 > currentMeter ) GameOver();

		Debug.Log( "Meter: " + currentMeter + "/" + meterMax );
	}

	public void GameOver()
	{
		Debug.Log( "GAAMU OVAA" );
	}
}
