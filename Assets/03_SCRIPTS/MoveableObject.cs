using UnityEngine;

[RequireComponent( typeof( Rigidbody ) )]
public class MoveableObject : MonoBehaviour
{
	private Rigidbody m_RigidBody;
	private CharacterJoint mouseJoint;

	private void Awake()
	{
		m_RigidBody = GetComponent<Rigidbody>();
	}

	public void PickupObject()
	{

	}

	public void ReleaseObject()
	{

	}
}
