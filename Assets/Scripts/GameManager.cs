using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject PlayerPrefab;
    public Transform Player;
    public Transform PlayerTwo;
    public List<GameObject> HealthPickups;
    public List<GameObject> EnemyTanks;
    public List<GameObject> EnemiesUnderAttack;
    public List<Room> Rooms;
    public MapGenerator generator;
    public bool LevelOfTheDay;
    public bool TwoPlayer;
    public float MusicVolume = 1;
    public float SoundEffectsVolume = 1;
    public int PlayerOneLives = 3;
    public int PlayerTwoLives = 3;
    public GameObject PlayerOneGameObject;
    public GameObject PlayerTwoGameObject;

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
            SpawnPlayer(PlayerOneGameObject, Player);
            SpawnPlayer(PlayerTwoGameObject, PlayerTwo);
            
            
            Debug.Log("Two Player Game");
        }
        else
        {
            SpawnPlayer(PlayerOneGameObject, Player);
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

    public void SpawnPlayer(GameObject PlayerToSpawn, Transform PlayerTransform)
    {
        int RandomRoom = Random.Range(0, Rooms.Count);
        PlayerToSpawn = Instantiate(PlayerPrefab);
        PlayerToSpawn.transform.position = Rooms[RandomRoom].PlayerSpawn.position;
        PlayerTransform = PlayerToSpawn.transform;
        if (TwoPlayer)
        {
            AdjustCameras();
        }
        
       
    }

    public void AdjustCameras()
    {
        if(Player != null)
        {
            GameObject camera = Player.GetChild(2).gameObject;
            camera.GetComponent<Camera>().rect = new Rect(0, .5f, 1, .5f);
            Player.GetComponent<InputManager>().input = InputManager.InputScheme.WASD;
        }
        
        if(PlayerTwo != null)
        {
            GameObject cameraTwo = PlayerTwo.GetChild(2).gameObject;
            cameraTwo.GetComponent<Camera>().rect = new Rect(0, 0, 1, .5f);
            PlayerTwo.GetComponent<InputManager>().input = InputManager.InputScheme.arrowKeys;
        }
       
    }
}
