using UnityEngine;

[RequireComponent( typeof( Rigidbody ) )]
public class MoveableObject : MonoBehaviour
{
	public string objectType;
	[Range( 0, 1 )]
	public float mouseVelocityFactor = 1;
	public Vector3 localAnchor;
	public bool canBePickedUp;
	public bool benneable;
	public float initialMalus;
	public float dotPerSecondMalus;


	private bool isTipped;
	protected Rigidbody m_RigidBody;
	private MayhemMeter mayhemMeter;

	private void Awake()
	{
		m_RigidBody = GetComponent<Rigidbody>();
		mayhemMeter = FindObjectOfType<MayhemMeter>();
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
		transform.parent = joint.transform;
		isTipped = false;

		transform.localPosition = Vector3.zero + localAnchor;
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
		transform.SetParent( null, true );

		m_RigidBody.AddForce( mouseVelocity * mouseVelocityFactor, ForceMode.Impulse );
	}

	public virtual void ReleaseObject( Transform snapPos )
	{
		m_RigidBody.velocity = Vector3.zero;
		m_RigidBody.angularVelocity = Vector3.zero;
		m_RigidBody.isKinematic = false;
		m_RigidBody.useGravity = true;
		transform.SetParent( null, true );

		transform.position = snapPos.position;
		transform.rotation = snapPos.rotation;
	}
}
