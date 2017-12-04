using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutoManager : MonoBehaviour
{
	public GameObject tutoPPRoot;
	public GameObject tutoEnd;
	int currentslide = 0;
	bool isOK;
	bool endTuto;

	private void Awake()
	{
		Time.timeScale = 0;
	}

	private void Start()
	{
		Cursor.visible = true;
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
				Cursor.visible = false;
			}

			if ( endTuto )
			{
				SceneManager.LoadSceneAsync( 0 );
			}
		}
	}

	public void EndTuto()
	{
		Time.timeScale = 0;
		endTuto = true;
		tutoEnd.SetActive( true );
	}
}