using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePickup : MonoBehaviour
{
	MoveableObject pickedObject;
	Rigidbody m_Rigidbody;
	GlowingOutlineRenderer glowRenderer;
	Camera cam;


	private void Awake()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
		glowRenderer = FindObjectOfType<GlowingOutlineRenderer>();
		cam = Camera.main;
	}


	private void FixedUpdate()
	{
		Cursor.visible = false;
		glowRenderer.glowingObjects.Clear();

		Ray screenRay = cam.ScreenPointToRay( Input.mousePosition );
		RaycastHit screenHit;
		Physics.Raycast( screenRay, out screenHit );
		m_Rigidbody.MovePosition( screenHit.point );


		RaycastHit pickupHit;
		Ray worldRay = new Ray( cam.transform.position, transform.position - cam.transform.position );
		if ( Physics.Raycast( worldRay, out pickupHit ) )
		{
			if ( pickupHit.collider != null && pickupHit.collider.CompareTag( "MoveableObject" ) )
			{
				if ( Input.GetMouseButton( 0 ) && pickedObject == null )
				{
					pickedObject = pickupHit.collider.GetComponentInParent<MoveableObject>();
					pickedObject.PickupObject();
				}

				var glow = pickupHit.collider.GetComponentInParent<GlowingObject>();
				if ( !glowRenderer.glowingObjects.Contains( glow ) ) glowRenderer.glowingObjects.Add( glow );
			}
		}
	}
}