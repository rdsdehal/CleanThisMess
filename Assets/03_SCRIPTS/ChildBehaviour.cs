using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildBehaviour : MonoBehaviour
{

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
    }

    private void Update()
    {
        OnStateUpdate();
    }

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
                if (Random.Range(0f, 1f) > 0.5f)
                {
                    SwitchState(CurrentState.MovingTowardChair);
                }
                else
                {
                    SwitchState(CurrentState.MovingTowardObject);
                }
                break;

            case CurrentState.MovingTowardChair:
                SwitchState(CurrentState.Sitting);
                break;

            case CurrentState.MovingTowardObject:
                SwitchState(CurrentState.ThrowSomething);
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
        }
    }
}