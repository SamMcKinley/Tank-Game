using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public TankMotor motor;
    public TankData data;
    private float Timer;
    private bool CanShoot;
    public float ShootDelay;
    public enum InputScheme { WASD, arrowKeys};
    public InputScheme input = InputScheme.WASD;
    // Start is called before the first frame update
    void Start()
    {
        motor = gameObject.GetComponent<TankMotor>();
        data = gameObject.GetComponent<TankData>();
    }

    // Update is called once per frame
    void Update()
    {
        //Checking to see which input scheme we are using
       switch (input)
        {
            //If we are using the arrow keys
            case InputScheme.arrowKeys:
                //checking to see if the player pressed the up arrow
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    //moving the tank forward
                    motor.Move(data.moveSpeed);
                }
                //Checking to see if the player pressed the down arrow
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    //Moving the player backward
                    motor.Move(-data.moveSpeed);
                }
                //Checking to see if the user pressed the left arrow
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    //Rotating the player to the left
                    motor.Rotate(-data.moveSpeed);
                }
                //Checking to see if the player pressed the right arrow
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    //Rotating the player to the right
                    motor.Rotate(data.moveSpeed);
                }
                //If the player did not press anything
                else
                {
                    //Do not move the player at all
                    motor.Move(0);
                }
                break;
            case InputScheme.WASD:
                //Checking to see if the user pressed the W key
                if (Input.GetKey(KeyCode.W))
                {
                    //Move the player forward
                    motor.Move(data.moveSpeed);
                }
                //Checking to see if the user pressed the S Key
                else if (Input.GetKey(KeyCode.S))
                {
                    //Move the player backward
                    motor.Move(-data.moveSpeed);
                }
                //Checking to see if the user pressed the A key
                else if (Input.GetKey(KeyCode.A))
                {
                    //Rotate the player to the left
                    motor.Rotate(-data.moveSpeed);
                }
                //Checking to see if the user pressed the D Key
                else if (Input.GetKey(KeyCode.D))
                {
                    //Rotate the user to the right
                    motor.Rotate(data.moveSpeed);
                }
                //If the user did not press anything
                else
                {
                    //Do not move the player at all
                    motor.Move(0);
                }
                break;
       }
        //Subtracting real time from our timer value
        Timer -= Time.deltaTime;
        //If timer is less than one
        if (Timer < 1)
        {
            //Then the tank is able to shoot
            CanShoot = true;
            //Reset the timer for the delay between shots
            Timer = ShootDelay;
        }
        //If the player presses the spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if the player can shoot
            if (CanShoot)
            {
                //Shoot the bullet
                motor.Shoot(data.Bullet, data.FirePoint);
                //So we can shoot again once the timer hits zero
                CanShoot = false;
            }
        }
    }
}
