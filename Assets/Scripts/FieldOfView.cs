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
    public ObstacleAvoidance obstacleAvoidance;

    // Start is called before the first frame update
    void Start()
    {
        obstacleAvoidance = gameObject.GetComponent<ObstacleAvoidance>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit Hit = default;
        Debug.DrawRay(transform.position, transform.forward);
        if (Physics.Raycast(transform.position, transform.forward, out Hit, ViewRadius, ObstacleLayer))
        {
            Debug.Log(Hit.collider.name);
            
                Debug.Log("Can Move");
                obstacleAvoidance.CanMove = true;
            
            
        }
        else
        {
            obstacleAvoidance.CanMove = false;
        }
        if (Physics.Raycast(transform.position, transform.forward, out Hit, ViewRadius))
        { 
            if (Hit.collider.GetComponent<Playercontroller>())
            {
                controller.CanSee = true;
            }
            else
            {
                controller.CanSee = false;
            }
            //if not, they can see the player
            controller.CanSee = true;
        }
    
        //check the distance from the enemy to the player
        //if that distance is less than the view radius
        if (Vector3.Distance(transform.position, target.position) < ViewRadius)
        {
            //check to see if the player is in the view angle
            if(Vector3.Angle(transform.position, target.position) < ViewAngle / 2)
            {
                controller.CanSee = true;
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
