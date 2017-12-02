using UnityEngine;

[RequireComponent( typeof( Rigidbody ) )]
public class MoveableObject : MonoBehaviour
{
	public string objectType;
	[Range( 0, 1 )]
	public float mouseVelocityFactor = 1;

	public bool canBePickedUp { get; private set; }
	private Rigidbody m_RigidBody;
	private CharacterJointDisabler mouseJoint;

	private void Awake()
	{
		m_RigidBody = GetComponent<Rigidbody>();
		mouseJoint = new CharacterJointDisabler();

		mouseJoint.CopyValuesAndDestroyJoint( GetComponent<CharacterJoint>() );
	}

	public void PickupObject( Rigidbody joint )
	{
		Vector3 connectdAnchor = Vector3.zero;
		connectdAnchor.y = joint.transform.position.y;

		transform.position = joint.transform.position - mouseJoint.Anchor;
		mouseJoint.CreateJoint( gameObject, joint, connectdAnchor );
		m_RigidBody.isKinematic = false;
		m_RigidBody.velocity = Vector3.zero;
		m_RigidBody.angularVelocity = Vector3.zero;
	}

	public void ReleaseObject( Vector3 mouseVelocity )
	{
		mouseJoint.DestroyJoint();
		m_RigidBody.velocity = Vector3.zero;
		m_RigidBody.angularVelocity = Vector3.zero;
		m_RigidBody.AddForce( mouseVelocity * mouseVelocityFactor, ForceMode.Impulse );

		//RaycastHit hit;
		//if ( Physics.Raycast( transform.position, Vector3.down, out hit, 100 ) )
		//{
		//	transform.rotation = Quaternion.identity;
		//	transform.position = hit.point;


		//}
	}

	public void ReleaseObject( Transform snapPos )
	{
		mouseJoint.DestroyJoint();
		m_RigidBody.velocity = Vector3.zero;
		m_RigidBody.angularVelocity = Vector3.zero;

		transform.position = snapPos.position;
		transform.rotation = snapPos.rotation;
	}
}
