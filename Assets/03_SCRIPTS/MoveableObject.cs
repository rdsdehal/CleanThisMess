using UnityEngine;

[RequireComponent( typeof( Rigidbody ) )]
public class MoveableObject : MonoBehaviour
{
	public string objectType;
	[Range( 0, 1 )]
	public float mouseVelocityFactor = 1;
	public float mouseOffset;
	public bool canBePickedUp;
	public Vector3 localOffset;
	public bool benneable;
	public bool benneOmozons;
	public bool canBurn;
	public float initialMalus;
	public float dotPerSecondMalus;

	private bool isTipped;
	protected Rigidbody m_RigidBody;
	private MayhemMeter mayhemMeter;
	private AmazonDelivery amazon;
	private Transform cachedPickupParent;


	private void Awake()
	{
		m_RigidBody = GetComponent<Rigidbody>();
		mayhemMeter = FindObjectOfType<MayhemMeter>();
		amazon = FindObjectOfType<AmazonDelivery>();
	}

	private void Update()
	{
		if ( Vector3.Dot( transform.up, Vector3.up ) < 0.8f )
		{
			if ( !isTipped )
			{
				isTipped = true;
				mayhemMeter.ChangeMeter( -initialMalus );
			}

			mayhemMeter.ChangeMeter( -dotPerSecondMalus * Time.deltaTime );
		}
	}

	public virtual void PickupObject( Rigidbody joint )
	{
		m_RigidBody.isKinematic = true;
		m_RigidBody.useGravity = false;
		m_RigidBody.velocity = Vector3.zero;
		m_RigidBody.angularVelocity = Vector3.zero;
		cachedPickupParent = transform.parent;
		transform.parent = joint.transform;
		isTipped = false;

		transform.localPosition = Vector3.zero + localOffset;
		float yRot = transform.localEulerAngles.y;
		transform.localRotation = Quaternion.identity;
		transform.Rotate( new Vector3( 0, yRot, 0 ) );
	}
	public virtual void ReleaseObject( Vector3 mouseVelocity )
	{
		m_RigidBody.velocity = Vector3.zero;
		m_RigidBody.angularVelocity = Vector3.zero;
		m_RigidBody.isKinematic = false;
		m_RigidBody.useGravity = true;
		transform.SetParent( cachedPickupParent, true );

		m_RigidBody.AddForce( mouseVelocity * mouseVelocityFactor, ForceMode.Impulse );
	}

	public virtual void ReleaseObject( Transform snapPos )
	{
		m_RigidBody.velocity = Vector3.zero;
		m_RigidBody.angularVelocity = Vector3.zero;
		m_RigidBody.isKinematic = false;
		m_RigidBody.useGravity = true;
		transform.SetParent( cachedPickupParent, true );

		transform.position = snapPos.position;
		transform.rotation = snapPos.rotation;
	}

	public virtual void Burn()
	{
		amazon.ScheduleDelivery( gameObject );
		gameObject.SetActive( false );
		var obj = Instantiate( amazon.burningMessPrefab, transform.position, Quaternion.identity );
		obj.GetComponent<Rigidbody>().AddForce( Vector3.up * 5, ForceMode.Impulse );
	}
}
