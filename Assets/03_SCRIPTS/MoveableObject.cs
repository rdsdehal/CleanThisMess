using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveableObject : MonoBehaviour
{
    public string objectType;
    [Range(0, 1)]
    public float mouseVelocityFactor = 1;
    public Vector3 localAnchor;
    public bool canBePickedUp;

    protected Rigidbody m_RigidBody;

    private void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

    public virtual void PickupObject(Rigidbody joint)
    {
        m_RigidBody.isKinematic = true;
        m_RigidBody.useGravity = false;
        m_RigidBody.velocity = Vector3.zero;
        m_RigidBody.angularVelocity = Vector3.zero;
        transform.parent = joint.transform;

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    public virtual void ReleaseObject(Vector3 mouseVelocity)
    {
        m_RigidBody.velocity = Vector3.zero;
        m_RigidBody.angularVelocity = Vector3.zero;
        m_RigidBody.isKinematic = false;
        m_RigidBody.useGravity = true;
        transform.SetParent(null, true);

        m_RigidBody.AddForce(mouseVelocity * mouseVelocityFactor, ForceMode.Impulse);
    }

    public virtual void ReleaseObject(Transform snapPos)
    {
        m_RigidBody.velocity = Vector3.zero;
        m_RigidBody.angularVelocity = Vector3.zero;
        m_RigidBody.isKinematic = false;
        m_RigidBody.useGravity = true;
        transform.SetParent(null, true);

        transform.position = snapPos.position;
        transform.rotation = snapPos.rotation;
    }
}
