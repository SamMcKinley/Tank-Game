using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public List<Transform> WayPoint;
    public int CurrentWaypoint;
    public TankData data;
    public TankMotor motor;
    public bool CanSee = false;
    public float Timer;
    public bool CanShoot;
    public float SafeDistance;
    public ObstacleAvoidance avoidance;

    public enum Personality
    {
        Cautious, 
    }
    public enum StateMachine
    {
        patrol, advance, avoid, attack, flee, search, idle
    }
    public StateMachine CurrentState;

    // Start is called before the first frame update
    void Start()
    {

    }
    public void ChangeState(StateMachine newState)
    {
        CurrentState = newState;
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    protected void idle()
    {

        
    }

    protected bool IsInRange(bool IsShooting)
    {
        float DistanceBetween = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);
        if(IsShooting == true)
        {
            if (DistanceBetween <= data.Range)
            {
                return true;
            }
        }
        else
        {
            if(DistanceBetween <= data.CowardlySearchRange)
            {
                return true;
            }
        }
        return false;
    }

    protected void patrol()
    {
        if (avoidance.CurrentAvoidanceState == ObstacleAvoidance.Avoidance.none)
        {
            motor.Move(data.moveSpeed);
            motor.RotateTowards(WayPoint[CurrentWaypoint].position);

        }        //Check if we are close enough to the current waypoint
        if (Vector3.Distance(transform.position, WayPoint[CurrentWaypoint].position) < 1)
        {
            //If we are, switch to the next waypoint
            //if our current waypoint is greater than or equal to waypoint.size
            if (CurrentWaypoint >= WayPoint.Count)
            {
                //then set current waypoint = 0
                CurrentWaypoint = 0;
            }
            //if not, increment current waypoint by 1
            else
            {
                CurrentWaypoint += 1;
            }
        }


    }
    protected void advance()
    {
        //Move function from TankMotor
        motor.Move(data.moveSpeed);
        //RotateTowards from TankMotor
        motor.RotateTowards(GameManager.Instance.Player.position);
    }
    protected void avoid()
    {

    }
    protected void attack()
    {
        //if we are able to see the player and are close enough to shoot 
        //Subtracting real time from our timer value
        Timer -= Time.deltaTime;
        //If timer is less than one
        if (Timer <= 0)
        {
            //Then the tank is able to shoot
            CanShoot = true;
            //Reset the timer for the delay between shots
            Timer = data.ShootDelay;
        }
        //If the player presses the spacebar
        //if the player can shoot
        if (CanShoot)
        {
            //Shoot the bullet
            motor.Shoot(data.Bullet, data.FirePoint);
            //So we can shoot again once the timer hits zero
            CanShoot = false;
        }

        
    }
    protected void flee()
    {
        data.isFleeing = true;
        Vector3 PlayerPosition = GameManager.Instance.Player.position;
        motor.Move(data.moveSpeed);
        motor.RotateTowards(PlayerPosition);
    }
    protected virtual void search()
    {
       

    }

}
