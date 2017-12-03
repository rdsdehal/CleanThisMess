using UnityEngine;

public class Plate : MonoBehaviour
{
	public enum PlateState
	{
		Clean,
		Full,
		Dirty,
		Garbage
	}

	public PlateState plateState = PlateState.Clean;

	public GameObject dirtyPlate;
	public GameObject[] food;
	public GameObject[] garbage;

	public void Clean()
	{
		plateState = PlateState.Clean;

		dirtyPlate.SetActive( false );
		for ( int i = 0 ; i < food.Length ; i++ )
		{
			food[i].SetActive( false );
		}
		for ( int i = 0 ; i < garbage.Length ; i++ )
		{
			garbage[i].SetActive( false );
		}
	}

	public void Cook()
	{
		if ( plateState == PlateState.Clean )
		{
			plateState = PlateState.Full;

			food[Random.Range( 0, food.Length )].SetActive( true );
		}
	}

	public void Consume()
	{
		plateState = PlateState.Dirty;

		dirtyPlate.SetActive( true );
		for ( int i = 0 ; i < food.Length ; i++ )
		{
			food[i].SetActive( false );
		}
		for ( int i = 0 ; i < garbage.Length ; i++ )
		{
			garbage[i].SetActive( false );
		}
	}
}
