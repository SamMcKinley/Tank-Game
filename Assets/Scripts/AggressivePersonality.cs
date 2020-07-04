using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressivePersonality : AIController
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
                if(CanSee == false)
                {
                    ChangeState(StateMachine.idle);
                }
                if(CanSee == true && IsInRange(true) == false)
                {
                    ChangeState(StateMachine.advance);
                }
                break;
            case StateMachine.idle:
                idle();
                if (CanSee == true && IsInRange(true) == true)
                {
                    ChangeState(StateMachine.attack);
                }
                if(CanSee == true)
                {
                    ChangeState(StateMachine.advance);
                }
                break;
            case StateMachine.advance:
                advance();
                if(CanSee == true && IsInRange(true) == true)
                {
                    ChangeState(StateMachine.attack);
                }
                if(CanSee == false)
                {
                    ChangeState(StateMachine.idle);
                }
                break;
        }
    }
}
