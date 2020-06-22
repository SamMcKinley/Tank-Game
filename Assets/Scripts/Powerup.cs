using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Powerup
{
    public float speedModifier;
    public float healthModifier;
    public float maxHealthModifier;
    public float fireRateModifier;
    public float fireRate;

    public float duration; //In seconds
    public bool isPermanent;

    public void onActivate(TankData target)
    {
        target.moveSpeed += speedModifier;
        target.Health += healthModifier;
        target.MaxHealth += maxHealthModifier;
        target.fireRate += fireRateModifier;
    }

    public void onDeactivate(TankData target)
    {

    }
}
