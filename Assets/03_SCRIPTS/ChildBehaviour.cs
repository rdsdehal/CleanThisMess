using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChildBehaviour : MonoBehaviour
{
    private ObjectsManager m_ChairManager = null;
    private GameObject m_Chair = null;
    private GameObject m_Throwable = null;
    private NavMeshAgent m_NavMeshAgent = null;
    private void Start()
    {
        m_ChairManager = FindObjectOfType<ObjectsManager>();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
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
    }

    private float m_IdleTimer = 0f;
    private float m_Timer = 0f;

    public float m_RemainDistance = 0.2f;

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
                m_IdleTimer = Random.Range(2f, 10f);
                break;
            case CurrentState.MovingTowardChair:
                m_Chair = m_ChairManager.FindNearestChair(this.gameObject);
                m_NavMeshAgent.SetDestination(m_Chair.transform.position);
                break;
            case CurrentState.MovingTowardObject:
                m_Throwable = m_ChairManager.FindNearestThrowable(this.gameObject);
                m_NavMeshAgent.SetDestination(m_Throwable.transform.position);
                break;
            case CurrentState.ThrowSomething:

                break;
            case CurrentState.Sitting:

                break;
            case CurrentState.Eating:

                break;
            case CurrentState.Spitting:

                break;
            case CurrentState.MovingTowardExit:

                break;
            case CurrentState.Disapear:

                break;
            case CurrentState.PickUp:

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
                    if (Random.Range(0f, 1f) > 0.5f)
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
                if (m_NavMeshAgent.remainingDistance <= m_RemainDistance)
                {
                    SwitchState(CurrentState.Sitting);
                }
                break;

            case CurrentState.MovingTowardObject:
                if (m_NavMeshAgent.remainingDistance <= m_RemainDistance)
                {
                    SwitchState(CurrentState.ThrowSomething);
                }
                break;

            case CurrentState.ThrowSomething:
                SwitchState(CurrentState.MovingTowardChair);
                break;

            case CurrentState.Sitting:
                SwitchState(CurrentState.Eating);
                break;

            case CurrentState.Eating:
                if (Random.Range(0f, 1f) > 0.2f)
                {
                    SwitchState(CurrentState.MovingTowardExit);
                }
                else
                {
                    SwitchState(CurrentState.Spitting);
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

                break;
            case CurrentState.Spitting:

                break;
            case CurrentState.MovingTowardExit:

                break;
            case CurrentState.Disapear:

                break;
            case CurrentState.PickUp:

                break;
        }
    }

    public void PickupCharacter()
    {
        SwitchState(CurrentState.PickUp);
    }

    public void ReleaseCharacter()
    {

    }
}