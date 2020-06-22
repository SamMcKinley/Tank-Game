using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    private TankData data;
    // Start is called before the first frame update
    void Start()
    {
        data = gameObject.GetComponent<TankData>();
    }

    
    public void TakeDamage(float DamageAmount)
    {
        //Subtracts damage amount from total health
        data.Health -= DamageAmount;
        //If enemy or player has less than one health
        if (data.Health <= 0)
        {
            //Character death
            Die();
        }
    }
    private void Die()
    {
        Destroy(this.gameObject);
    }
    
}
