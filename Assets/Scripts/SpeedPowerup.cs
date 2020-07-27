using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : Pickup
{
    public float Amount;

    protected override void OnPickup(TankData Tank)
    {
        Tank.moveSpeed = Amount;
        base.OnPickup(Tank);
    }

}
