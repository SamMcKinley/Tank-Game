using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateOfFirePickup : Pickup
{
    private TankData data;
    public float fireRate;
    public float Lifespan;
    public float originalFirerate;
    private float Timer;

    private void Update()
    {
        if(Timer > 0)
        {
            Timer -= Time.deltaTime;
        }

        if(Timer < 0)
        {
            data.ShootDelay = originalFirerate;
            Destroy(this.gameObject);
        }

    }

    protected override void OnPickup(TankData Tank)
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.enabled = false;
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;

        originalFirerate = Tank.ShootDelay;
        Tank.ShootDelay = fireRate;
        Timer = Lifespan;
        data = Tank;
    }

    



    
}
