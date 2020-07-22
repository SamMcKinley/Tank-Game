using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject PlayerPrefab;
    public Transform Player;
    public List<GameObject> HealthPickups;
    public List<GameObject> EnemyTanks;
    public List<GameObject> EnemiesUnderAttack;
    public void RemoveHealthFromList(GameObject Index)
    {
        HealthPickups.Remove(Index);
    }
    public void SpawnEnemy(Room room)
    {
        GameObject Enemy = Instantiate(EnemyTanks[Random.Range(0, EnemyTanks.Count)], room.EnemySpawn.position, Quaternion.identity);
        room.AssignedEnemy = Enemy;
        Enemy.GetComponent<AIController>().WayPoint = room.Waypoints;
        Enemy.GetComponent<FieldOfView>().target = Player;
    }
}
