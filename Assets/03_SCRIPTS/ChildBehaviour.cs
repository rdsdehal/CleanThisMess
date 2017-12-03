using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChildBehaviour : MoveableObject
{
    private ObjectsManager m_ChairManager = null;
    private EntryPoint m_EntryPoint = null;
    private Chair m_Chair = null;
    private Plate m_Plate = null;
    private GameObject m_Renderer = null;
    private GameObject m_Throwable = null;
    private NavMeshAgent m_NavMeshAgent = null;
    public MayhemMeter m_MayhemMeter = null;

    private void Start()
    {
        m_ChairManager = FindObjectOfType<ObjectsManager>();
        m_EntryPoint = FindObjectOfType<EntryPoint>();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_Renderer = GetComponentInChildren<Animator>().gameObject;
        m_MayhemMeter = m_EntryPoint.m_MayhemMeter;
        m_ExitPosition = m_EntryPoint.LeavePoint;
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
        InQueue,
        SittingIdle,
        MovingInQueue,
        MovingTowardChair,
        MovingTowardObject,
        ThrowSomething,
        Berserker,
        Sitting,
        Eating,
        Spitting,
        MovingTowardExit,
        Disapear,
        PickUp,
        Release,
    }

    public Vector3 m_ExitPosition = Vector3.zero;

    private float m_IdleTimer = 0f;
    private float m_Timer = 0f;

    public float m_ObjectMinDistance = 0.2f;
    public float m_SittingSmoothTime = 1f;
    public float m_PlateRayDistance = 1f;
    public float m_LeaveBonus = 2f;
    public float m_LeaveMalus = 2f;

    private bool m_HaveEat = false;

    public int m_CurrentWaitPoint = 0;

    public LayerMask m_RaycastLayer;

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
            case CurrentState.InQueue:

                break;
            case CurrentState.MovingInQueue:
                if (m_CurrentWaitPoint > 0)
                {
                    m_NavMeshAgent.SetDestination(m_EntryPoint.GetNewSpawnPoint(m_CurrentWaitPoint));
                    m_CurrentWaitPoint--;
                }
                break;
            case CurrentState.Idle:
                m_IdleTimer = Random.Range(8f, 12f);
                break;
            case CurrentState.MovingTowardChair:
                m_Chair = m_ChairManager.FindChair(this.gameObject);
                if (m_Chair == null)
                {
                    SwitchState(CurrentState.MovingTowardExit);
                }
                else
                {
                    NavMeshHit closePointChair;
                    NavMesh.SamplePosition(m_Chair.transform.position, out closePointChair, 1.5f, NavMesh.AllAreas);
                    m_NavMeshAgent.SetDestination(closePointChair.position);
                }
                break;
            case CurrentState.MovingTowardObject:
                m_Throwable = m_ChairManager.FindNearestThrowable(this.gameObject);
                NavMeshHit closePointThrowable;
                NavMesh.SamplePosition(m_Throwable.transform.position, out closePointThrowable, 1.5f, NavMesh.AllAreas);
                m_NavMeshAgent.SetDestination(closePointThrowable.position);
                break;
            case CurrentState.ThrowSomething:

                break;
            case CurrentState.Sitting:
                canBePickedUp = false;
                m_Timer = 0f;
                m_IdleTimer = Random.Range(3f, 6f);
                m_Chair.EnterChair();
                m_NavMeshAgent.enabled = false;
                transform.position = m_Chair.transform.position + Vector3.up * 0.25f;
                transform.forward = m_Chair.transform.forward;
                break;
            case CurrentState.Eating:
                m_Timer = 0f;
                break;
            case CurrentState.Spitting:

                break;
            case CurrentState.SittingIdle:
                m_Timer = 0f;
                m_IdleTimer = Random.Range(4f, 6f);
                break;
            case CurrentState.MovingTowardExit:
                m_NavMeshAgent.SetDestination(m_ExitPosition);
                break;
            case CurrentState.Disapear:
                if (m_HaveEat)
                {
                    m_MayhemMeter.ChangeMeter(m_LeaveBonus);
                }
                else
                {
                    m_MayhemMeter.ChangeMeter(-m_LeaveMalus);
                }
                Destroy(gameObject);
                break;
            case CurrentState.PickUp:
                m_NavMeshAgent.enabled = false;
                break;
            case CurrentState.Release:

                break;
            case CurrentState.Berserker:
                m_Renderer.transform.position = transform.position;
                m_NavMeshAgent.enabled = true;
                break;

        }
    }

    void OnStateUpdate()
    {
        switch (m_CurrentState)
        {
            case CurrentState.Spawn:
                if (m_CurrentWaitPoint == 0)
                {
                    SwitchState(CurrentState.Idle);
                }
                else
                {
                    SwitchState(CurrentState.InQueue);
                }
                break;

            case CurrentState.InQueue:
                if (m_EntryPoint.m_MustReload)
                {
                    SwitchState(CurrentState.MovingInQueue);
                }
                break;
            case CurrentState.MovingInQueue:
                if (m_EntryPoint.m_MustReload)
                {
                    SwitchState(CurrentState.MovingInQueue);
                }
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
                if (Vector3.Distance(transform.position, m_NavMeshAgent.destination) < 0.6f)
                {
                    SwitchState(CurrentState.Sitting);
                }
                break;

            case CurrentState.MovingTowardObject:
                if (Vector3.Distance(transform.position, m_NavMeshAgent.destination) < 0.6f)
                {
                    SwitchState(CurrentState.ThrowSomething);
                }
                break;

            case CurrentState.ThrowSomething:
                m_Throwable.GetComponent<Rigidbody>().AddForceAtPosition((transform.forward * 30f) + Vector3.up * 30f, transform.position, ForceMode.Impulse);
                SwitchState(CurrentState.MovingTowardChair);
                break;

            case CurrentState.Sitting:
                m_Timer += Time.deltaTime;

                RaycastHit plateHit;
                Ray forwardRay = new Ray(transform.position + Vector3.up * 0.3f, transform.forward);
                if (Physics.Raycast(forwardRay, out plateHit, m_PlateRayDistance, m_RaycastLayer, QueryTriggerInteraction.Collide))
                {
                    Plate m_Plate = plateHit.collider.GetComponentInParent<Plate>();
                    if (m_Plate != null)
                    {
                        if (m_Plate.plateState == Plate.PlateState.Full)
                        {
                            SwitchState(CurrentState.Eating);
                        }
                        else if (m_Plate.plateState == Plate.PlateState.Garbage)
                        {
                            SwitchState(CurrentState.Spitting);
                        }
                        if (m_Timer >= m_IdleTimer)
                        {
                            SwitchState(CurrentState.Berserker);
                        }
                    }
                    else
                    {
                        if (m_Timer >= m_IdleTimer)
                        {
                            SwitchState(CurrentState.Berserker);
                        }
                    }
                }
                break;
            case CurrentState.Eating:
                m_Timer += Time.deltaTime;
                RaycastHit plateHit2;
                Ray forwardRay2 = new Ray(transform.position + Vector3.up * 0.3f, transform.forward);
                if (Physics.Raycast(forwardRay2, out plateHit2, m_PlateRayDistance, m_RaycastLayer, QueryTriggerInteraction.Collide))
                {
                    m_Plate = plateHit2.collider.GetComponentInParent<Plate>();
                    if (m_Plate == null)
                    {
                        SwitchState(CurrentState.Berserker);
                    }
                }
                if (m_Timer > 4.0f && m_Plate != null)
                {
                    m_Plate.Consume();
                    if (m_HaveEat)
                    {
                        if (Random.Range(0f, 1f) > 0.8f)
                        {
                            SwitchState(CurrentState.SittingIdle);
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
                            SwitchState(CurrentState.SittingIdle);
                        }
                        else
                        {
                            SwitchState(CurrentState.Spitting);
                        }
                    }
                }
                break;

            case CurrentState.SittingIdle:
                m_Timer += Time.deltaTime;
                if (m_Timer >= m_IdleTimer)
                {
                    SwitchState(CurrentState.MovingTowardExit);
                }
                break;

            case CurrentState.Spitting:

                SwitchState(CurrentState.MovingTowardExit);
                break;

            case CurrentState.MovingTowardExit:
                if (Vector3.Distance(transform.position, m_NavMeshAgent.destination) < 0.3f)
                {
                    SwitchState(CurrentState.Disapear);
                }
                break;

            case CurrentState.Disapear:

                break;
            case CurrentState.PickUp:

                break;
            case CurrentState.Release:
                m_NavMeshAgent.enabled = true;
                RaycastHit releaseHit;
                Ray downRay = new Ray(transform.position, Vector3.down);
                if (Physics.Raycast(downRay, out releaseHit, 100.0f, m_RaycastLayer))
                {
                    m_Chair = releaseHit.collider.GetComponentInParent<Chair>();
                    if (m_Chair != null)
                    {
                        SwitchState(CurrentState.Sitting);
                    }
                }
                else
                {
                    if (m_HaveEat)
                    {
                        SwitchState(CurrentState.MovingTowardExit);
                    }
                    else
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
                }
                break;
            case CurrentState.Berserker:
                if (Random.Range(0f, 1f) > 0.5f)
                {
                    SwitchState(CurrentState.MovingTowardObject);
                }
                else
                {
                    SwitchState(CurrentState.MovingTowardExit);
                }
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
                m_EntryPoint.m_SpawnIndex--;
                m_EntryPoint.ReloadChild();
                break;
            case CurrentState.MovingInQueue:

                break;
            case CurrentState.InQueue:

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
                m_HaveEat = true;
                canBePickedUp = true;
                m_HaveEat = true;
                break;
            case CurrentState.Spitting:

                break;

            case CurrentState.SittingIdle:
                m_Renderer.transform.position = transform.position;
                m_Chair.ExitChair();
                m_Chair.m_Rigidbody.isKinematic = false;
                m_NavMeshAgent.enabled = true;
                break;
            case CurrentState.MovingTowardExit:

                break;
            case CurrentState.Disapear:

                break;
            case CurrentState.PickUp:

                break;
            case CurrentState.Release:

                break;
            case CurrentState.Berserker:
                m_Chair.ExitChair();
                break;
        }
    }

    public override void PickupObject(Rigidbody joint)
    {
        base.PickupObject(joint);
        SwitchState(CurrentState.PickUp);
    }

    public override void ReleaseObject(Vector3 mouseVelocity)
    {
        base.ReleaseObject(mouseVelocity);
        m_RigidBody.isKinematic = true;
        Vector3 newPosition = new Vector3(transform.position.x, 0.8f, transform.position.z);
        transform.position = newPosition;
        SwitchState(CurrentState.Release);
    }
}