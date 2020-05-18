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
    }
    //Handle moving the tank
    public void Move(float speed)
    {
        // Create a vector to hold our speed data
        Vector3 speedVector = tf.forward * speed;
    }

    //Handle rotating the tank
    public void Rotate(float speed)
    {
        //create a vector to hold our rotation data
        Vector3 rotateVector;
        //Start by rotating by one degree per frame draw
        rotateVector = Vector3.up;
        //adjust rotation based off speed
        rotateVector *= Time.deltaTime;
        //pass our rotation vecctor into transform.rotate
        transform.Rotate(rotateVector, Space.Self);
    }
}
