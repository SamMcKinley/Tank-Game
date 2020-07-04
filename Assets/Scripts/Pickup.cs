using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float lifeSpan;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifeSpan);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TankData>())
        {
            //On pickup
            OnPickup(other.GetComponent<TankData>());
        }
    }

    protected virtual void OnPickup(TankData Tank)
    {
        Destroy(this.gameObject);
    }
}
