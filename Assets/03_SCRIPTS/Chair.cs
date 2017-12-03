using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
	private MoveableObject m_MovableObject = null;
	[HideInInspector] public Rigidbody m_Rigidbody = null;
	private ObjectsManager m_ObjectManager = null;
	public bool isOccupied = false;

	private void Awake()
	{
		m_MovableObject = GetComponent<MoveableObject>();
		m_Rigidbody = GetComponent<Rigidbody>();
		m_ObjectManager = FindObjectOfType<ObjectsManager>();
		if ( m_ObjectManager ) m_ObjectManager.m_ChairList.Add( this );
	}

	public void EnterChair()
	{
		isOccupied = true;
		m_MovableObject.canBePickedUp = false;
		m_Rigidbody.isKinematic = true;
		m_ObjectManager.m_ChairList.Remove( this );
		m_ObjectManager.m_ThrowableList.Remove( this.gameObject );
	}

	public void ExitChair()
	{
		isOccupied = false;
		m_MovableObject.canBePickedUp = true;
		m_Rigidbody.isKinematic = false;
		m_ObjectManager.m_ChairList.Add( this );
		m_ObjectManager.m_ThrowableList.Add( this.gameObject );
	}
}
