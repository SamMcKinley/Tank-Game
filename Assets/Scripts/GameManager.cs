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
    public List<Room> Rooms;
    public MapGenerator generator;
    public bool LevelOfTheDay;
    public bool TwoPlayer;
    public float MusicVolume;
    public float SoundEffectsVolume;

    private void Start()
    {
        generator = GameObject.FindObjectOfType<MapGenerator>();
        generator.LevelOfTheDay = LevelOfTheDay;
        GameSetup();
    }

    public void GameSetup()
    {
        if (TwoPlayer)
        {
            Debug.Log("Two Player Game");
        }
        else
        {
            Debug.Log("One Player Game");
        }
    }
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

    public void Respawn()
    {
        int RandomRoom = Random.Range(0, Rooms.Count);

        GameObject player = Instantiate(PlayerPrefab);
        player.transform.position = Rooms[RandomRoom].PlayerSpawn.position;
        //Camera.main.GetComponent<CameraController>().target = player.transform;
        Player = player.transform;
    }
}
