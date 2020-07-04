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
    public float Range;
    public float MaxHealth;
    public bool isFleeing = false;
    public float fireRate;
    public Health HealthScript;
    public float CowardlySearchRange;
    public float StoppingDistance;
    public float FleeLimit;
    public float NoiseDistance;
    public float HearingDistance;
    public bool CanHear;

}

