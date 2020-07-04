using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    public float Amount;

    protected override void OnPickup(TankData Tank)
    {
        Tank.HealthScript.AddHealth(Amount);
        GameManager.Instance.RemoveHealthFromList(this.gameObject);
        base.OnPickup(Tank);
    }

}
