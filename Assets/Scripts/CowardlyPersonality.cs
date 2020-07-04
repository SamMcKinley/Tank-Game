using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowardlyPersonality : AIController
{
    public GameObject Test;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (CurrentState)
        {
            case StateMachine.flee:
                flee();
                if(CanSee == false)
                {
                    ChangeState(StateMachine.patrol);
                }
                if(CanSee == true && IsInRange(false) == true)
                {
                    ChangeState(StateMachine.search);
                }
                break;
            case StateMachine.patrol:
                patrol();
                if(CanSee == true)
                {
                    ChangeState(StateMachine.flee);
                }
                break;
            case StateMachine.search:
                search();
                if(Vector3.Distance(transform.position, Test.transform.position)< data.StoppingDistance)
                {
                    ChangeState(StateMachine.idle);
                }
                break;
            case StateMachine.idle:
                idle();
                if(CanSee == false && IsInRange(false) == false)
                {
                    ChangeState(StateMachine.patrol);
                }
                break;
                
        }
    }
    protected override void search()
    {
        data.isFleeing = false;
        if(GameManager.Instance.EnemyTanks != null)
        {
            float Distance = Vector3.Distance(transform.position, GameManager.Instance.EnemyTanks[0].transform.position);
            int Index = 0;
            for(int I = 0; I < GameManager.Instance.EnemyTanks.Count; I++)
            {
                float NextDistance = Vector3.Distance(transform.position, GameManager.Instance.EnemyTanks[I].transform.position);

                if(NextDistance < Distance)
                {
                    if (GameManager.Instance.EnemyTanks[I] != this.gameObject)
                    {
                        Distance = NextDistance;
                        Index = I;
                    }
                    
                }
            }
            if(GameManager.Instance.EnemyTanks[Index]!= null)
            {
                Test = GameManager.Instance.EnemyTanks[Index];
                motor.Move(data.moveSpeed);
                motor.RotateTowards(GameManager.Instance.EnemyTanks[Index].transform.position);
            }

        }
    } 
}
