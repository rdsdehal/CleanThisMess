using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    private MoveableObject m_MovableObject = null;
    [HideInInspector] public Rigidbody m_Rigidbody = null;
    public bool isOccupied = false;

    private void Awake()
    {
        m_MovableObject = GetComponent<MoveableObject>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    public void EnterChair()
    {
        isOccupied = true;
        m_MovableObject.canBePickedUp = false;
        m_Rigidbody.isKinematic = true;
    }

    public void ExitChair()
    {
        isOccupied = false;
        m_MovableObject.canBePickedUp = true;
        m_Rigidbody.isKinematic = false;
    }
}
