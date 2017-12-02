using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MousePickup : MonoBehaviour
{
	public LayerMask floorLayer;
	public LayerMask objectLayer;
	public LayerMask snapObjectLayer;

	private MoveableObject pickedObject;
	private Rigidbody m_Rigidbody;
	private GlowingOutlineRenderer glowRenderer;
	private Camera cam;
	private bool mouseInput;
	private List<SnapPosition> snapPos = new List<SnapPosition>();

	private void Awake()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
		glowRenderer = FindObjectOfType<GlowingOutlineRenderer>();
		cam = Camera.main;
		snapPos = FindObjectsOfType<SnapPosition>().ToList();
	}

	private void Update()
	{
		Cursor.visible = false;
		glowRenderer.glowingObjects.Clear();

		Ray screenRay = cam.ScreenPointToRay( Input.mousePosition );
		RaycastHit screenHit;
		if ( Physics.Raycast( screenRay, out screenHit, 100, floorLayer ) )
		{
			m_Rigidbody.MovePosition( screenHit.point );
		}

		if ( pickedObject == null )
		{
			DoEmptyHand();
		}
		else
		{
			DoFullHand();
		}
	}

	private void DoEmptyHand()
	{
		RaycastHit pickupHit;
		Ray worldRay = new Ray( cam.transform.position, transform.position - cam.transform.position );
		if ( Physics.Raycast( worldRay, out pickupHit, 100, objectLayer ) )
		{
			// PICKUP OBJECT
			if ( Input.GetMouseButtonDown( 0 ) && pickedObject == null )
			{
				mouseInput = false;

				pickedObject = pickupHit.collider.GetComponentInParent<MoveableObject>();
				pickedObject.PickupObject( m_Rigidbody );

				foreach ( var item in snapPos )
				{
					if ( item.objectType == pickedObject.objectType ) item.ShowSnap();
				}
			}

			var glow = pickupHit.collider.GetComponentInParent<GlowingObject>();
			if ( !glowRenderer.glowingObjects.Contains( glow ) ) glowRenderer.glowingObjects.Add( glow );
		}
	}

	private void DoFullHand()
	{
		RaycastHit snapHit;
		Ray snapRay = new Ray( cam.transform.position, transform.position - cam.transform.position );

		if ( Physics.Raycast( snapRay, out snapHit, 100, snapObjectLayer, QueryTriggerInteraction.Collide ) )
		{
			// SHOW DAT BOI
			Debug.Log( "ggg" );
		}


		// DROP OBJECT
		if ( !Input.GetMouseButton( 0 ) )
		{
			Vector3 vel = m_Rigidbody.velocity;
			vel.y = 0;

			// NO SNAP
			if ( snapHit.collider == null )
			{
				pickedObject.ReleaseObject( vel );
			}
			// SNAP
			else
			{
				pickedObject.ReleaseObject( snapHit.collider.transform );
			}
			pickedObject = null;

			foreach ( var item in snapPos )
			{
				item.HideSnap();
			}

			mouseInput = false;
		}
	}
}