using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Experimental.XR;

public class ObstacleAvoidance : MonoBehaviour
{
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

    // Update is called once per frame
    void Update()
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
                if(CanMove == false)
                {
                    ChangeState(Avoidance.moveToAvoid);
                    Timer = TimeToMove;
                }
                break;
            case Avoidance.moveToAvoid:
                moveToAvoid();
                Timer -= Time.deltaTime;
                if(Timer <= 0)
                {
                    ChangeState(Avoidance.none);
                }
                if(CanMove == false)
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
        if(transform.rotation.y != transform.rotation.y + AvoidAngle)
        {
            motor.Rotate(data.rotateSpeed);
        }
    }
    public void moveToAvoid()
    {
        motor.Move(data.moveSpeed);
    }
    public void ChangeState(Avoidance newState)
    {
        CurrentAvoidanceState = newState;
    }

}
