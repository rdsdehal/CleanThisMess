using UnityEngine;
using UnityEngine.SceneManagement;

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

		float minute = timer / 1000;
		float secs = timer % 60;
		System.TimeSpan span = new System.TimeSpan( 0, 0, 0, (int)timer, 0 );

		timerText.text = System.String.Format( "{0:F1}:{1:F1}", span.Minutes.ToString( "00" ), span.Seconds.ToString( "00" ) );
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
		SceneManager.LoadSceneAsync( 2 );
	}
}
