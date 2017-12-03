using UnityEngine;

public class Benne : MonoBehaviour
{
	AmazonDelivery delivery;

	private void Awake()
	{
		delivery = FindObjectOfType<AmazonDelivery>();
	}

	private void OnTriggerEnter( Collider other )
	{
		var obj = other.GetComponentInParent<MoveableObject>();
		if ( obj && obj.benneable )
		{
			if ( obj.benneOmozons )
			{
				delivery.ScheduleDelivery( obj.gameObject );
				obj.gameObject.SetActive( false );
			}
			else
			{
				Destroy( obj.gameObject );
			}
		}
	}
}