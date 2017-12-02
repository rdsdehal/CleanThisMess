using UnityEngine;

public class Plate : MonoBehaviour
{
	public enum PlateState
	{
		Clean,
		Full,
		Dirty
	}

	public PlateState plateState = PlateState.Clean;




}
