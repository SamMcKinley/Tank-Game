using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {

	public GameObject doorNorth;
	public GameObject doorSouth;
	public GameObject doorEast;
	public GameObject doorWest;
	public Transform EnemySpawn;
	public GameObject AssignedEnemy;
    public List<Transform> Waypoints;

    private void Update()
    {
        if(AssignedEnemy == null)
        {
            GameManager.Instance.SpawnEnemy(this);
        }
    }




}
