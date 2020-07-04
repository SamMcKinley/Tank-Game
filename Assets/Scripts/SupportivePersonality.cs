using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportivePersonality : AIController
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (CurrentState)
        {
            case StateMachine.attack:
                attack();
                if (CanSee == true && IsInRange(true) == false)
                {
                    ChangeState(StateMachine.advance);
                }
                if (data.Health <= data.FleeLimit)
                {
                    ChangeState(StateMachine.flee);
                }
                break;
            case StateMachine.patrol:
                patrol();
                if (CanSee == true && IsInRange(true) == true)
                {
                    ChangeState(StateMachine.attack);
                }
                if (CanSee == true && IsInRange(true) == false)
                {
                    ChangeState(StateMachine.advance);
                }
                if (GameManager.Instance.EnemiesUnderAttack.Count > 0)
                {
                    ChangeState(StateMachine.search);
                }
                break;
            case StateMachine.advance:
                attack();
                if (CanSee == true && IsInRange(true) == true)
                {
                    ChangeState(StateMachine.attack);
                }
                if (CanSee == false)
                {
                    ChangeState(StateMachine.patrol);
                }
                if (data.Health <= data.FleeLimit)
                {
                    ChangeState(StateMachine.flee);
                }
                break;
            case StateMachine.search:
                search();
                if (CanSee == true && IsInRange(true) == true)
                {
                    ChangeState(StateMachine.attack);
                }
                if(CanSee == true && IsInRange(true) == false)
                {
                    ChangeState(StateMachine.advance);
                }
                break;
            case StateMachine.flee:
                flee();
                if (CanSee == false && GameManager.Instance.EnemiesUnderAttack.Count > 0)
                {
                    ChangeState(StateMachine.search);
                }
                if (CanSee == false && GameManager.Instance.EnemiesUnderAttack.Count == 0)
                {
                    ChangeState(StateMachine.patrol);
                }
                break;
        }
    }

    protected override void search()
    {
        data.isFleeing = false;
        if (GameManager.Instance.EnemiesUnderAttack != null)
        {
            float Distance = Vector3.Distance(transform.position, GameManager.Instance.EnemiesUnderAttack[0].transform.position);
            int Index = 0;
            for (int I = 0; I < GameManager.Instance.EnemiesUnderAttack.Count; I++)
            {
                float NextDistance = Vector3.Distance(transform.position, GameManager.Instance.EnemiesUnderAttack[I].transform.position);

                if (NextDistance < Distance)
                {
                    if (GameManager.Instance.EnemiesUnderAttack[I] != this.gameObject)
                    {
                        Distance = NextDistance;
                        Index = I;
                    }

                }
            }
            if (GameManager.Instance.EnemiesUnderAttack[Index] != null)
            {
                motor.Move(data.moveSpeed);
                motor.RotateTowards(GameManager.Instance.EnemiesUnderAttack[Index].transform.position);
            }
        }
    }
}
