using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(TankData))]
public class TankMotor : MonoBehaviour
{
    //Need a reference to the character controller component
    private CharacterController characterController;
    private TankData data;
    private Transform tf;

    private void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        data = gameObject.GetComponent<TankData>();
        tf = gameObject.GetComponent<Transform>();
    }
    public void Shoot(GameObject Bullet, Transform FirePoint)
    {
        //Instantiate the bullet
        Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
    }
    //Handle moving the tank
    public void Move(float speed)
    {
        // Create a vector to hold our speed data
        Vector3 speedVector = tf.forward * speed *Time.deltaTime;
        characterController.SimpleMove(speedVector);
    }

    //Handle rotating the tank
    public void Rotate(float speed)
    {
        //create a vector to hold our rotation data
        Vector3 rotateVector;
        //Start by rotating by one degree per frame draw
        rotateVector = Vector3.up;
        

        if (speed < 0)
        {
            //adjust rotation based off speed
            rotateVector -= new Vector3(0, Time.deltaTime * Mathf.Abs(speed),0);
            //pass our rotation vector into transform.rotate
            transform.Rotate(-rotateVector, Space.Self);

        }
        else
        {
            //adjust rotation based off speed
            rotateVector += new Vector3(0, Time.deltaTime * Mathf.Abs(speed), 0);
            //pass our rotation vector into transform.rotate
            transform.Rotate(rotateVector, Space.Self);
        }
        
        
    }
    public bool RotateTowards(Vector3 target, float rotationSpeed)
    {
        
        Vector3 VectorToTarget;
        //A line that goes from our current position to the target's position
        if(data.isFleeing == true)
        {
            VectorToTarget = -1*(target - transform.position);
        }
        else
        {
            VectorToTarget = target - transform.position;
        }
        Quaternion targetRotation = Quaternion.LookRotation(VectorToTarget);
        if(targetRotation == transform.rotation)
        {
            return false;
        }
        tf.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation,
            data.rotateSpeed * Time.deltaTime);
        return true;
    } 
}
