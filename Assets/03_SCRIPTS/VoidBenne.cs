using UnityEngine;

public class VoidBenne : MonoBehaviour
{
	AmazonDelivery delivery;

	private void Awake()
	{
		delivery = FindObjectOfType<AmazonDelivery>();
	}

	private void OnTriggerEnter( Collider other )
	{
		var obj = other.GetComponentInParent<MoveableObject>();
		if ( obj )
		{
			delivery.ScheduleDelivery( obj.gameObject );
			obj.gameObject.SetActive( false );
		}
	}
}