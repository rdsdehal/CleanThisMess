﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
	public void LoadScene( int scene )
	{
		SceneManager.LoadScene( scene );
	}

	public void Quit()
	{
		Application.Quit();
	}
}
