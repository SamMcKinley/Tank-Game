using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody Rigidbody;
    [SerializeField] private float ShootingForce;
    [SerializeField] private float BulletDelay;
    [SerializeField] private float BulletDamage;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();

        //Creating the vector that the bullet will travel on
        Vector3 BulletVector = transform.forward;
        //Adding force to the bullet
        Rigidbody.AddForce(BulletVector * ShootingForce);
        //Destroy the bullet after a set amount of time
        Destroy(this.gameObject, BulletDelay);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If the bullet collides with the ground
        if (collision.collider.CompareTag("ground"))
        {
            //Destroy the bullet
            Destroy(this.gameObject);
        }
        //If the bullet collides with the enemy
        if (collision.collider.CompareTag("Enemy"))
        {//The enemy takes damage
            Health Health = collision.collider.GetComponent<Health>();
            Health.TakeDamage(BulletDamage);
            Destroy(this.gameObject);
        }
    }
}
