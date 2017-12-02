using UnityEngine;

public class CharacterJointDisabler
{
	CharacterJoint joint;

	public Vector3 Anchor;
	public Vector3 Axis;
	public Vector3 SwingAxis;
	public SoftJointLimitSpring Twist;
	public SoftJointLimitSpring Swing;
	public SoftJointLimit LowTwistLimit;
	public SoftJointLimit HighTwistLimit;
	public SoftJointLimit Swing1Limit;
	public SoftJointLimit Swing2Limit;
	public float BreakForce;
	public float BreakTorque;
	public bool EnableCollision;
	public bool EnableProjection;
	public float ProjectionDistance;
	public float ProjectionAngle;

	public void CopyValuesAndDestroyJoint( CharacterJoint characterJoint )
	{
		Anchor = characterJoint.anchor;
		Axis = characterJoint.axis;
		SwingAxis = characterJoint.swingAxis;
		LowTwistLimit = characterJoint.lowTwistLimit;
		Twist = characterJoint.twistLimitSpring;
		Swing = characterJoint.swingLimitSpring;
		HighTwistLimit = characterJoint.highTwistLimit;
		Swing1Limit = characterJoint.swing1Limit;
		Swing2Limit = characterJoint.swing2Limit;
		BreakForce = characterJoint.breakForce;
		BreakTorque = characterJoint.breakTorque;
		EnableCollision = characterJoint.enableCollision;
		EnableProjection = characterJoint.enableProjection;
		ProjectionDistance = characterJoint.projectionAngle;
		ProjectionAngle = characterJoint.projectionDistance;

		MonoBehaviour.Destroy( characterJoint );
	}

	public void CreateJoint( GameObject obj, Rigidbody connectedBody, Vector3 connectedAnchor )
	{
		joint = obj.AddComponent<CharacterJoint>();

		joint.autoConfigureConnectedAnchor = false;
		joint.connectedBody = connectedBody;
		joint.anchor = Anchor;
		joint.axis = Axis;
		joint.connectedAnchor = connectedAnchor;
		joint.swingAxis = SwingAxis;
		joint.lowTwistLimit = LowTwistLimit;
		joint.highTwistLimit = HighTwistLimit;
		joint.swing1Limit = Swing1Limit;
		joint.swing2Limit = Swing2Limit;
		joint.breakForce = BreakForce;
		joint.breakTorque = BreakTorque;
		joint.enableCollision = EnableCollision;
		joint.enableProjection = EnableProjection;
		joint.twistLimitSpring = Twist;
		joint.swingLimitSpring = Swing;
		joint.projectionAngle = ProjectionAngle;
		joint.projectionDistance = ProjectionDistance;
	}

	public void DestroyJoint()
	{
		MonoBehaviour.Destroy( joint );
	}
}