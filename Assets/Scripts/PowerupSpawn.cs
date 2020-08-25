using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawn : MonoBehaviour
{
    public List<GameObject> PickupPrefabs;
    public GameObject CurrentPickup;
    public float SpawnDelay;
    public float NextSpawnTime;
    // Start is called before the first frame update
    void Start()
    {
        NextSpawnTime = Time.time + SpawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentPickup == null)
        {
            if(Time.time >= NextSpawnTime)
            {
                GameObject PickupPrefab = PickupPrefabs[Random.Range(0, PickupPrefabs.Count)];
                CurrentPickup = Instantiate(PickupPrefab, transform.position, Quaternion.identity);
                NextSpawnTime = Time.time + SpawnDelay;
            }
        }
        else
        {
            NextSpawnTime = Time.time + SpawnDelay;
        }
    }
}
