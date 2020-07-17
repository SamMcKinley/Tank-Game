using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Experimental.XR;

public class ObstacleAvoidance : MonoBehaviour
{
    float Angle = 0;
    public Avoidance CurrentAvoidanceState;
    public float TimeToMove;
    private float Timer;
    public bool CanMove;
    public float AvoidAngle;
    private TankMotor motor;
    private TankData data;
    public enum Avoidance
    {
        none, turnToAvoid, moveToAvoid
    }

    // Start is called before the first frame update
    void Start()
    {
        Timer = TimeToMove;
        data = gameObject.GetComponent<TankData>();
        motor = gameObject.GetComponent<TankMotor>();
    }

    private void Update()
    {
        Avoid();
    }
    public void Avoid()
    {
        switch (CurrentAvoidanceState)
        {
            case Avoidance.none:
                none();
                if(CanMove == true)
                {
                    ChangeState(Avoidance.turnToAvoid);
                }
                break;
            case Avoidance.turnToAvoid:
                turnToAvoid();
                Debug.Log(Angle);
                if(CanMove == false)
                {
                    Timer = TimeToMove;
                    ChangeState(Avoidance.moveToAvoid);
                }
                break;
            case Avoidance.moveToAvoid:
                moveToAvoid();
                
                if(Timer <= 0)
                {
                    ChangeState(Avoidance.none);
                }
                if(CanMove == true)
                {
                   ChangeState(Avoidance.turnToAvoid);
                }
                break;
        }
    }
    public void none()
    {

    }
    public void turnToAvoid()
    {
        
        
            motor.Rotate(data.rotateSpeed);
        
        
    }
    public void moveToAvoid()
    {
        Timer -= Time.deltaTime;
        motor.Move(data.moveSpeed);
    }
    public void ChangeState(Avoidance newState)
    {
        CurrentAvoidanceState = newState;
        Debug.Log("Changing my state to " + newState.ToString());
    }

}
