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

    private void Update()
    {
        //If enemy or player has less than one health
        if(data.Health < 1)
        {
            //Character death
            Die();
        }
    }
    public void TakeDamage(float DamageAmount)
    {
        //Subtracts damage amount from total health
        data.Health -= DamageAmount;
    }
    private void Die()
    {
        Destroy(this.gameObject);
    }
    
}
