using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotateSpeed;
    public GameObject Bullet;
    public Transform FirePoint;
    public float ShootDelay;
    public float Health;
    private void Start()
    {
        //The firepoint for the characters
        FirePoint = GameObject.Find("FirePoint").GetComponent<Transform>();
    }
}

