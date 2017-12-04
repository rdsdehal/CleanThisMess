using UnityEngine;

public class Benne : MonoBehaviour
{
	public ParticleSystem fx;
	public ParticleSystem fxSoul;
	AmazonDelivery delivery;

	public AudioSource audiosource;
	public AudioClip clip;

	private void Awake()
	{
		delivery = FindObjectOfType<AmazonDelivery>();
	}

	private void OnTriggerEnter( Collider other )
	{
		var obj = other.GetComponentInParent<MoveableObject>();
		if ( obj && obj.benneable && !other.isTrigger )
		{
			fx.transform.position = obj.transform.position;
			fx.Play();
			audiosource.PlayOneShot( clip );

			if ( obj.benneOmozons )
			{
				var plate = other.GetComponentInParent<Plate>();
				if ( plate != null ) plate.Clean();
				delivery.ScheduleDelivery( obj.gameObject );
				obj.gameObject.SetActive( false );

				var child = other.GetComponentInParent<ChildBehaviour>();
				if ( child )
				{
					fxSoul.transform.position = obj.transform.position;
					fxSoul.Play();
				}
			}
			else
			{
				Destroy( obj.gameObject );
			}
		}
	}
}