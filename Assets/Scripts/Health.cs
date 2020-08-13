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
    public void AddHealth(float Amount)
    {
        data.Health += Amount;
        if(data.Health > data.MaxHealth)
        {
            data.Health = data.MaxHealth;
        }
        GameManager.Instance.EnemiesUnderAttack.Remove(this.gameObject);
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
        if (gameObject.GetComponent<AIController>())
        {
            GameManager.Instance.EnemiesUnderAttack.Add(this.gameObject);
        }
        
    }
    private void Die()
    {
        GameManager.Instance.EnemiesUnderAttack.Remove(this.gameObject);

        Destroy(this.gameObject);
    }
    
}
