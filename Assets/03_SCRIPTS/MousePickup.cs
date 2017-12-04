using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MousePickup : MonoBehaviour
{
	public float maxMagnitudeForLaunch;
	public LayerMask floorLayer;
	public LayerMask objectLayer;
	public LayerMask snapObjectLayer;
	public Mesh emptyHand;
	public Mesh grabHand;
	public AudioClip pickupSound;

	public Light spotTarget;
	public float spotTargetHeight;

	private MoveableObject pickedObject;
	private Rigidbody m_Rigidbody;
	private GlowingOutlineRenderer glowRenderer;
	private Camera cam;
	private List<SnapPosition> snapPos = new List<SnapPosition>();
	private MeshFilter handRenderer;
	private float mouseHeight;
	private AudioSource source;

	private void Awake()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
		glowRenderer = FindObjectOfType<GlowingOutlineRenderer>();
		cam = Camera.main;
		snapPos = FindObjectsOfType<SnapPosition>().ToList();
		handRenderer = GetComponentInChildren<MeshFilter>();
		mouseHeight = transform.position.y;
		source = GetComponent<AudioSource>();
		Cursor.visible = false;
	}

	private void Update()
	{
		glowRenderer.glowingObjects.Clear();

		Ray screenRay = cam.ScreenPointToRay( Input.mousePosition );
		RaycastHit screenHit;
		if ( Physics.Raycast( screenRay, out screenHit, 100, floorLayer ) )
		{
			m_Rigidbody.MovePosition( screenHit.point );
		}

		if ( Input.GetMouseButton( 0 ) )
		{
			handRenderer.sharedMesh = grabHand;
		}
		else
		{
			handRenderer.sharedMesh = emptyHand;
		}

		if ( pickedObject == null )
		{
			DoEmptyHand();
			spotTarget.enabled = false;
		}
		else
		{
			DoFullHand();

			spotTarget.enabled = true;
			Vector3 pos = transform.position;
			pos.y = spotTargetHeight;
			spotTarget.transform.position = pos;
		}
	}

	private void DoEmptyHand()
	{
		RaycastHit pickupHit;
		Ray worldRay = new Ray( cam.transform.position, transform.position - cam.transform.position );

		if ( Physics.Raycast( worldRay, out pickupHit, 100, objectLayer, QueryTriggerInteraction.Collide ) )
		{
			// PICKUP OBJECT
			var moveObject = pickupHit.collider.GetComponentInParent<MoveableObject>();
			if ( Input.GetMouseButtonDown( 0 ) && moveObject.canBePickedUp )
			{
				pickedObject = moveObject;
				pickedObject.PickupObject( m_Rigidbody );

				Vector3 pos = transform.position;
				pos.y = mouseHeight + pickedObject.mouseOffset;
				transform.position = pos;
				source.PlayOneShot( pickupSound );

				// SHOW SNAPS
				foreach ( var item in snapPos )
				{
					if ( item.objectType == pickedObject.objectType ) item.ShowSnap();
				}
			}

			// SHOW GLOW
			var glow = pickupHit.collider.GetComponentInParent<GlowingObject>();
			if ( moveObject.canBePickedUp && !glowRenderer.glowingObjects.Contains( glow ) ) glowRenderer.glowingObjects.Add( glow );
		}
	}

	private void DoFullHand()
	{
		RaycastHit snapHit;
		Ray snapRay = new Ray( cam.transform.position, transform.position - cam.transform.position );

		if ( Physics.Raycast( snapRay, out snapHit, 100, snapObjectLayer, QueryTriggerInteraction.Collide ) )
		{
			var snap = snapHit.collider.GetComponent<SnapPosition>();
			snap.Select();
		}

		// ROTATE OBJECT
		if ( Input.GetMouseButtonDown( 1 ) )
		{
			pickedObject.transform.Rotate( new Vector3( 0, 90, 0 ) );
		}

		// DROP OBJECT
		if ( !Input.GetMouseButton( 0 ) )
		{
			Vector3 vel = m_Rigidbody.velocity;
			vel.y = 0;

			// DROP NO SNAP
			if ( snapHit.collider == null )
			{
				pickedObject.ReleaseObject( Vector3.ClampMagnitude( vel, maxMagnitudeForLaunch ) );
			}
			// DROP SNAP
			else
			{
				pickedObject.ReleaseObject( snapHit.collider.transform );
			}

			pickedObject = null;

			Vector3 pos = transform.position;
			pos.y = mouseHeight;
			transform.position = pos;
			source.PlayOneShot( pickupSound );

			foreach ( var item in snapPos )
			{
				item.HideSnap();
			}
		}
	}

	private void OnTriggerEnter( Collider other )
	{
		if ( other.CompareTag( "DropItem" ) && pickedObject != null )
		{
			pickedObject.ReleaseObject( Vector3.zero );
			pickedObject = null;

			foreach ( var item in snapPos )
			{
				item.HideSnap();
			}
		}
	}
}