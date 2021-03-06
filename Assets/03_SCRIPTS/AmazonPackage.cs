﻿using UnityEngine;

public class AmazonPackage : MonoBehaviour
{
	public GameObject packageContents;

	private void OnCollisionEnter( Collision collision )
	{
		packageContents.transform.position = transform.position + ( Vector3.up * 0.25f );
		packageContents.transform.rotation = Quaternion.identity;
		var rb = packageContents.GetComponent<Rigidbody>();
		if ( rb )
		{
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;

		}
		packageContents.SetActive( true );

		FindObjectOfType<AmazonDelivery>().PlayDelivery( transform.position );

		Invoke( "DoSpawn", 0.05f );
	}

	private void DoSpawn()
	{
		packageContents.GetComponent<Rigidbody>().AddForce( Vector3.up * 5, ForceMode.Impulse );
		Destroy( gameObject );
	}
}
