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
                Debug.Log("In patrol state");
                if (CanSee == true)
                {
                    Debug.Log("Changing to advance state");
                    ChangeState(StateMachine.advance);
                }
                if (data.Health != data.MaxHealth)
                {
                    Debug.Log("Changing to flee state");
                    ChangeState(StateMachine.flee);
                }
                break;
            case StateMachine.advance:
                advance();
                if (Vector3.Distance(transform.position, GameManager.Instance.Player.position) < data.Range)
                {
                    Debug.Log("Changing to attack state");
                    ChangeState(StateMachine.attack);
                }
                if (CanSee == false)
                {
                    Debug.Log("Changing to patrol state");
                    ChangeState(StateMachine.patrol);
                }
                if (data.Health != data.MaxHealth)
                {
                    Debug.Log("Changing to flee state again");
                    ChangeState(StateMachine.flee);
                }
                break;
            case StateMachine.avoid:
                break;
            case StateMachine.attack:
                attack();
                if (Vector3.Distance(transform.position, GameManager.Instance.Player.position) > data.Range)
                {
                    Debug.Log("Changing to advance state again");
                    ChangeState(StateMachine.advance);
                }
                if (data.Health != data.MaxHealth)
                {
                    Debug.Log("Changing to flee state yet again");
                    ChangeState(StateMachine.flee);
                }
                break;
            case StateMachine.flee:
                flee();
                if (Vector3.Distance(transform.position, GameManager.Instance.Player.position) > SafeDistance)
                {
                    Debug.Log("Changing to search state");
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
            Debug.Log(HealthPickupPosition);
            motor.Move(data.moveSpeed);
            motor.RotateTowards(HealthPickupPosition);

        }
        //else
        //{
        //    search();
        //}


    }
}
