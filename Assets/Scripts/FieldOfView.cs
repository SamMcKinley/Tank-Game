using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public AIController controller;
    public float ViewRadius;
    public float ViewAngle;
    public Transform target;
    public LayerMask ObstacleLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //check the distance from the enemy to the player
        //if that distance is less than the view radius
        if (Vector3.Distance(transform.position, target.position) < ViewRadius)
        {
            //check to see if the player is in the view angle
            if(Vector3.Angle(transform.position, target.position) < ViewAngle / 2)
            {
                float Distance = Vector3.Distance(transform.position, target.position);
                Vector3 DirectionToTarget = target.position - transform.position;
                //if they are, raycast to see if there are any obstacles in the way
                if (!Physics.Raycast(transform.position, DirectionToTarget, Distance, ObstacleLayer))
                {
                    //if not, they can see the player
                    controller.CanSee = true;
                }
                else
                {
                    controller.CanSee = false;
                }

                //otherwise, we can't see the player
            }
            else
            {
                controller.CanSee = false;
            }

        }
        else
        {
            controller.CanSee = false;
        }


    }
    public Vector3 AngleToTarget(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}
