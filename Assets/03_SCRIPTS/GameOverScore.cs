using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScore : MonoBehaviour
{
	public Text text;

	private void Start()
	{
		float besttime = PlayerPrefs.GetFloat( "CleanThisMess.BestTime", 0 );
		float time = PlayerPrefs.GetFloat( "CleanThisMess.CurrentTime", 0 );

		TimeSpan bestspan = new TimeSpan( 0, 0, 0, (int)besttime, 0 );
		TimeSpan span = new TimeSpan( 0, 0, 0, (int)time, 0 );

		text.text = String.Format( "Best Time {0:F1}:{1:F1} \nTime {2:F1}:{3:F1}", bestspan.Minutes.ToString( "00" ), bestspan.Seconds.ToString( "00" ), span.Minutes.ToString( "00" ), span.Seconds.ToString( "00" ) );
	}
}
