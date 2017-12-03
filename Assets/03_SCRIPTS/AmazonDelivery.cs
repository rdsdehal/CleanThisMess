﻿using System.Collections;
using UnityEngine;

public class AmazonDelivery : MonoBehaviour
{
	public Vector2 randomDeliveryTime;
	public Transform deliveryPoint;
	public GameObject packageBase;
	public GameObject burningMessPrefab;
	public ParticleSystem fx;

	public void ScheduleDelivery( GameObject packageContents )
	{
		StartCoroutine( DoDelivery( packageContents ) );
	}

	IEnumerator DoDelivery( GameObject packageContents )
	{
		yield return new WaitForSeconds( Random.Range( randomDeliveryTime.x, randomDeliveryTime.y ) );
		var package = Instantiate( packageBase, deliveryPoint );
		package.GetComponent<AmazonPackage>().packageContents = packageContents;

		yield break;
	}

	public void PlayDelivery( Vector3 pos )
	{
		fx.transform.position = pos;
		fx.Play();
	}
}
