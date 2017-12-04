using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopTrigger : MonoBehaviour
{
	public ParticleSystem fx;
	public AudioSource source;
	public AudioClip clip;

	void OnTriggerEnter( Collider other )
	{
		var move = other.GetComponentInParent<MoveableObject>();
		if ( move && move.objectType == "basketball" )
		{
			source.PlayOneShot( clip );
			fx.Play();
		}
	}
}