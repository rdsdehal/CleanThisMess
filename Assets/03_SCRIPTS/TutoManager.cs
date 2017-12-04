using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
	public GameObject tutoPPRoot;
	int currentslide = 0;
	bool isOK;

	private void Awake()
	{
		Time.timeScale = 0;
	}

	private void Update()
	{
		if ( Input.GetMouseButtonDown( 0 ) )
		{
			for ( int i = 0 ; i < tutoPPRoot.transform.childCount ; i++ )
			{
				tutoPPRoot.transform.GetChild( i ).gameObject.SetActive( false );
			}
			currentslide++;


			if ( currentslide < tutoPPRoot.transform.childCount )
			{
				tutoPPRoot.transform.GetChild( currentslide ).gameObject.SetActive( true );
			}
			else if ( !isOK )
			{
				isOK = true;
				Time.timeScale = 1;
			}
		}
	}
}
