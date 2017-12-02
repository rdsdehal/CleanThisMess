using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChildBehaviour : MonoBehaviour
{
    private ObjectsManager m_ChairManager = null;
    private Chair m_Chair = null;
    private GameObject m_Renderer = null;
    private GameObject m_Throwable = null;
    private NavMeshAgent m_NavMeshAgent = null;

    private void Start()
    {
        m_ChairManager = FindObjectOfType<ObjectsManager>();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_Renderer = GetComponentInChildren<MeshRenderer>().gameObject;
    }

    private void Update()
    {
        OnStateUpdate();
    }

    public CurrentState m_CurrentState;
    public enum CurrentState
    {
        Spawn,
        Idle,
        MovingTowardChair,
        MovingTowardObject,
        ThrowSomething,
        Sitting,
        Eating,
        Spitting,
        MovingTowardExit,
        Disapear,
        PickUp,
        Release,
    }

    private Vector3 m_CurrentVelocity = Vector3.zero;
    private Vector3 m_LastPositionBeforeSitting = Vector3.zero;
    public Vector3 m_ExitPosition = Vector3.zero;

    private float m_IdleTimer = 0f;
    private float m_Timer = 0f;

    public float m_ObjectMinDistance = 0.2f;
    public float m_SittingSmoothTime = 1f;

    private bool m_HaveEat = false;

    public LayerMask objectLayer;

    void SwitchState(CurrentState m_NewState)
    {
        OnStateExit(m_CurrentState);
        m_CurrentState = m_NewState;
        OnStateEnter(m_CurrentState);
    }

    void OnStateEnter(CurrentState m_NewState)
    {
        switch (m_NewState)
        {
            case CurrentState.Spawn:

                break;
            case CurrentState.Idle:
                m_IdleTimer = Random.Range(2f, 5f);
                break;
            case CurrentState.MovingTowardChair:
                m_Chair = m_ChairManager.FindChair(this.gameObject);
                m_NavMeshAgent.SetDestination(m_Chair.transform.position);
                break;
            case CurrentState.MovingTowardObject:
                m_Throwable = m_ChairManager.FindNearestThrowable(this.gameObject);
                m_NavMeshAgent.SetDestination(m_Throwable.transform.position);
                break;
            case CurrentState.ThrowSomething:

                break;
            case CurrentState.Sitting:
                m_Chair.EnterChair();
                m_LastPositionBeforeSitting = m_Renderer.transform.position;
                break;
            case CurrentState.Eating:
                m_Timer = 0f;
                break;
            case CurrentState.Spitting:

                break;
            case CurrentState.MovingTowardExit:
                m_NavMeshAgent.SetDestination(m_ExitPosition);
                break;
            case CurrentState.Disapear:

                break;
            case CurrentState.PickUp:
                m_NavMeshAgent.enabled = false;
                break;
        }
    }

    void OnStateUpdate()
    {
        switch (m_CurrentState)
        {
            case CurrentState.Spawn:
                SwitchState(CurrentState.Idle);
                break;

            case CurrentState.Idle:
                m_Timer += Time.deltaTime;
                if (m_Timer >= m_IdleTimer)
                {
                    if (Random.Range(0f, 1f) > 0.8f)
                    {
                        SwitchState(CurrentState.MovingTowardChair);
                    }
                    else
                    {
                        SwitchState(CurrentState.MovingTowardObject);
                    }
                }
                break;

            case CurrentState.MovingTowardChair:
                if (m_NavMeshAgent.remainingDistance <= 0.6f)
                {
                    SwitchState(CurrentState.Sitting);
                }
                break;

            case CurrentState.MovingTowardObject:
                if (m_NavMeshAgent.remainingDistance <= m_ObjectMinDistance)
                {
                    SwitchState(CurrentState.ThrowSomething);
                }
                break;

            case CurrentState.ThrowSomething:
                SwitchState(CurrentState.MovingTowardChair);
                break;

            case CurrentState.Sitting:
                m_Renderer.transform.position = Vector3.SmoothDamp(m_Renderer.transform.position, m_Chair.transform.position + Vector3.up * 0.5f, ref m_CurrentVelocity, m_SittingSmoothTime);
                m_Timer += Time.deltaTime;
                if (Vector3.Distance(m_Renderer.transform.position, m_Chair.transform.position + Vector3.up * 0.5f) < 0.05f)
                {
                    SwitchState(CurrentState.Eating);
                }
                break;

            case CurrentState.Eating:
                m_Timer += Time.deltaTime;
                if (m_Timer > 4.0f)
                {
                    if (m_HaveEat)
                    {
                        if (Random.Range(0f, 1f) > 0.8f)
                        {
                            SwitchState(CurrentState.MovingTowardExit);
                        }
                        else
                        {
                            SwitchState(CurrentState.Spitting);
                        }
                    }
                    else
                    {
                        if (Random.Range(0f, 1f) > 0.2f)
                        {
                            SwitchState(CurrentState.MovingTowardExit);
                        }
                        else
                        {
                            SwitchState(CurrentState.Spitting);
                        }
                    }
                }
                break;

            case CurrentState.Spitting:

                SwitchState(CurrentState.MovingTowardExit);
                break;

            case CurrentState.MovingTowardExit:

                SwitchState(CurrentState.Disapear);
                break;

            case CurrentState.Disapear:

                break;
            case CurrentState.PickUp:

                break;
        }
    }

    void OnStateExit(CurrentState m_LastState)
    {
        switch (m_LastState)
        {
            case CurrentState.Spawn:

                break;
            case CurrentState.Idle:

                break;
            case CurrentState.MovingTowardChair:

                break;
            case CurrentState.MovingTowardObject:

                break;
            case CurrentState.ThrowSomething:

                break;
            case CurrentState.Sitting:

                break;
            case CurrentState.Eating:
                m_Renderer.transform.position = m_LastPositionBeforeSitting;
                m_Chair.ExitChair();
                m_HaveEat = true;
                break;
            case CurrentState.Spitting:

                break;
            case CurrentState.MovingTowardExit:

                break;
            case CurrentState.Disapear:

                break;
            case CurrentState.PickUp:
                m_NavMeshAgent.enabled = true;
                RaycastHit releaseHit;
                Ray downRay = new Ray(transform.position, transform.position + Vector3.down);
                if (Physics.Raycast(downRay, out releaseHit, 100.0f, objectLayer))
                {
                    m_Chair = releaseHit.collider.GetComponentInParent<Chair>();
                    if (m_Chair != null)
                    {
                        SwitchState(CurrentState.Eating);
                    }
                    else
                    {
                        if (m_HaveEat)
                        {
                            SwitchState(CurrentState.MovingTowardExit);
                        }
                        else
                        {
                            if (Random.Range(0f, 1f) > 0.2f)
                            {
                                SwitchState(CurrentState.MovingTowardChair);
                            }
                            else
                            {
                                SwitchState(CurrentState.MovingTowardObject);
                            }
                        }
                    }
                }
                break;
        }
    }

    public void PickupCharacter()
    {
        SwitchState(CurrentState.PickUp);
    }

    public void ReleaseCharacter()
    {
        SwitchState(CurrentState.Release);
    }
}