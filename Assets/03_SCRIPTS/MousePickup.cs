using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePickup : MonoBehaviour
{
	public LayerMask floorLayer;
	public LayerMask objectLayer;

	private MoveableObject pickedObject;
	private Rigidbody m_Rigidbody;
	private GlowingOutlineRenderer glowRenderer;
	private Camera cam;
	private bool mouseInput;

	private void Awake()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
		glowRenderer = FindObjectOfType<GlowingOutlineRenderer>();
		cam = Camera.main;
	}

	private void Update()
	{
		Cursor.visible = false;
		glowRenderer.glowingObjects.Clear();
		mouseInput = Input.GetMouseButtonDown( 0 );

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
			if ( mouseInput && pickedObject == null )
			{
				mouseInput = false;

				pickedObject = pickupHit.collider.GetComponentInParent<MoveableObject>();
				pickedObject.PickupObject( m_Rigidbody );
			}

			var glow = pickupHit.collider.GetComponentInParent<GlowingObject>();
			if ( !glowRenderer.glowingObjects.Contains( glow ) ) glowRenderer.glowingObjects.Add( glow );
		}
	}

	private void DoFullHand()
	{
		if ( mouseInput )
		{
			Vector3 vel = m_Rigidbody.velocity;
			vel.y = 0;
			pickedObject.ReleaseObject( vel );
			pickedObject = null;

			mouseInput = false;
		}
	}
}