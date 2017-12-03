﻿using UnityEngine;
using UnityEngine.UI;

public class MayhemMeter : MonoBehaviour
{
	public float meterMax;
	public float currentMeter { get; private set; }
	public TextMesh timerText;

	public Transform meterVisual;
	private float initialMaxScale;

	float timer;

	private void Update()
	{
		timer += Time.deltaTime;

		Vector3 scale = meterVisual.localScale;
		scale.x = Mathf.Lerp( 0, initialMaxScale, currentMeter / meterMax );
		meterVisual.localScale = scale;

		float minute = timer / 60;
		float secs = timer % 60;

		timerText.text = System.String.Format( "{0:F1}:{1:F1}", minute.ToString( "00" ), secs.ToString( "00" ) );
	}

	private void Awake()
	{
		currentMeter = meterMax;
		initialMaxScale = meterVisual.localScale.x;
	}

	public void ChangeMeter( float delta )
	{
		currentMeter += delta;

		if ( currentMeter > meterMax ) currentMeter = meterMax;
		if ( 0 > currentMeter ) GameOver();
	}

	public void GameOver()
	{
		Debug.Log( "GAAMU OVAA" );
	}
}
