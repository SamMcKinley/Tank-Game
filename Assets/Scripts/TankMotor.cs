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
    public Transform tf;
    private AudioSource TankAudio;

    private void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        data = gameObject.GetComponent<TankData>();
        tf = gameObject.transform;
        TankAudio = gameObject.GetComponent<AudioSource>();
    }
    public void Shoot(GameObject Bullet, Transform FirePoint)
    {
        //Instantiate the bullet
        Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
        TankAudio.volume = GameManager.Instance.SoundEffectsVolume;
        TankAudio.Play();
    }
    //Handle moving the tank
    public void Move(float speed)
    {
        // Create a vector to hold our speed data
        Debug.Log(tf);
        Vector3 speedVector = tf.forward * speed *Time.deltaTime;
        characterController.SimpleMove(speedVector);
    }

    //Handle rotating the tank
    public void Rotate(float speed)
    {
        Vector3 rotateVector = Vector3.up * speed * Time.deltaTime;
        tf.Rotate(rotateVector, Space.Self);



    }
    public void RotateTowards(Vector3 target)
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
        Quaternion targetRotation = Quaternion.LookRotation(VectorToTarget, Vector3.up);
        targetRotation.x = 0;
        targetRotation.z = 0;
        data.transform.rotation = Quaternion.RotateTowards(data.transform.rotation, targetRotation,
            data.rotateSpeed * Time.deltaTime);

    } 
}
