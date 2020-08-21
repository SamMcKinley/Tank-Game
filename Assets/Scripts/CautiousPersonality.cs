using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CautiousPersonality : AIController
{
    // Start is called before the first frame update
    void Start()
    {
        CurrentState = StateMachine.patrol;
    }

    // Update is called once per frame
    void Update()
    {
        switch (CurrentState)
        {
            case StateMachine.patrol:
                patrol();
                if (CanSee == true)
                {
                    ChangeState(StateMachine.advance);
                }
                if (data.Health != data.MaxHealth)
                {
                    ChangeState(StateMachine.flee);
                }
                break;
            case StateMachine.advance:
                advance();
                if (Vector3.Distance(transform.position, GameManager.Instance.Player.position) < data.Range)
                {
                    ChangeState(StateMachine.attack);
                }
                if (CanSee == false)
                {
                    ChangeState(StateMachine.patrol);
                }
                if (data.Health != data.MaxHealth)
                {
                    ChangeState(StateMachine.flee);
                }
                break;
            case StateMachine.avoid:
                break;
            case StateMachine.attack:
                attack();
                if (Vector3.Distance(transform.position, GameManager.Instance.Player.position) > data.Range)
                {
                    ChangeState(StateMachine.advance);
                }
                if (data.Health != data.MaxHealth)
                {
                    ChangeState(StateMachine.flee);
                }
                break;
            case StateMachine.flee:
                flee();
                if (Vector3.Distance(transform.position, GameManager.Instance.Player.position) > SafeDistance)
                {
                    ChangeState(StateMachine.search);
                }
                break;
            case StateMachine.search:
                search();
                if (data.Health >= data.MaxHealth)
                {
                    ChangeState(StateMachine.patrol);
                }
                break;


        }
    }
    protected override void search()
    {
        data.isFleeing = false;
        float Distance = Vector3.Distance(transform.position, GameManager.Instance.HealthPickups[0].transform.position);
        Vector3 HealthPickupPosition = Vector3.zero;
        int Index = 0;
        for (int I = 0; I < GameManager.Instance.HealthPickups.Count; I++)
        {
            if (GameManager.Instance.HealthPickups[I] != null && Vector3.Distance(transform.position, GameManager.Instance.HealthPickups[I].transform.position) < Distance)
            {
                Distance = Vector3.Distance(transform.position, GameManager.Instance.HealthPickups[I].transform.position);
                HealthPickupPosition = GameManager.Instance.HealthPickups[I].transform.position;
                Index = I;
            }
        }
        if (HealthPickupPosition != null && GameManager.Instance.HealthPickups[Index] != null)
        {
            motor.Move(data.moveSpeed);
            motor.RotateTowards(HealthPickupPosition);

        }
        //else
        //{
        //    search();
        //}


    }
}
