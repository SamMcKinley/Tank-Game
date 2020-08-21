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
        // NOTE_FROM_HUE:  YOU CALL THIS TWICE, BUT DON'T NEED TO CALL IT HERE: 
        // GameSetup();
    }

    public void GameSetup()
    {
        if (TwoPlayer)
        {
            // NOTE_FROM_HUE:  Your old version tried to pass in a GameObject parameter, change that parameter
            //                 in the function, and then keep those changes.  
            //                 This is not the correct way to think about parameters.
            //                 Think of Parameters as INPUT, and return values as OUTPUT -- so if you want
            //                     to get that instantiated object back, you need to "return" it 
            //                     as shown in this change
            //                 C# does allow this "multiple return" concept with a concept called an "out"
            //                    parameter, however, not every language does. So it is always better to 
            //                    come up with logic for having one return value
            PlayerOneGameObject = SpawnPlayer();
            PlayerTwoGameObject = SpawnPlayer();
            if (TwoPlayer)
            {
                AdjustCameras();
            }

            // NOTE_FROM_HUE:  Setting these values, but see note below about how you shouldn't need both
            Player = PlayerOneGameObject.transform;
            PlayerTwo = PlayerTwoGameObject.transform;

        }
        else
        {
            // NOTE_FROM_HUE:  Used return value here, as well
            PlayerOneGameObject = SpawnPlayer();
           
            // NOTE_FROM_HUE:  Setting these values, but see note below about how you shouldn't need both
            Player = PlayerOneGameObject.transform;

            Debug.Log("One Player Game");
        }

        // NOTE_FROM_HUE: You use a second set of variables (Player and PlayerTwo) quite often.
        //                These are transforms, and not the game objects we got here. 
        //                For now, I am setting them when I spawn, but you want to only store once
        //                I recommend storing just the transform (since it is smaller and you can
        //                  get the .gameObject from the transform when needed.)
        //
        // ALSO: You have 4 variables that all refer to one concept/idea of players
        //       (PlayerOneGameObject, PlayerTwoGameObject, Player, PlayerTwo) and with this, you have
        //       a hard-coded max players of 2. 
        //       Look at using just a List<Transform> of players and using an index for each player.
        //       This way, player[0] is player,  player[1] is playerTwo, etc. 
        //       You could have an unlimited number of players this way, just player.Add() more players
        //           as you spawn them, and use the .Count to know how many you have. 
        //       You can also iterate through them, so you can do things like have your AI iterate through
        //           them and find the closest, or the one with the least health, or the one that does
        //           the most damage. Making helper functions that iterate through all players and return
        //           the one you are looking for can really help you make more advanced features.

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

    // scope=public    returnType=GameObject     name=SpawnPlayer  (  parameters )
    public GameObject SpawnPlayer()
    {
        int RandomRoom = Random.Range(0, Rooms.Count);
        GameObject PlayerToSpawn; // NOTE_FROM_HUE: Created local variable
        PlayerToSpawn = Instantiate(PlayerPrefab) as GameObject; // NOTE_FROM_HUE: Instantiated into local var
        PlayerToSpawn.transform.position = Rooms[RandomRoom].PlayerSpawn.position;

        

        return PlayerToSpawn; // NOTE_FROM_HUE: returned the spawned player
    }

    public void AdjustCameras()
    {
        Debug.Log("Adjust Cameras method has been called");
        if (PlayerOneGameObject != null)
        {
            Camera camera = PlayerOneGameObject.GetComponentInChildren<Camera>();
            camera.rect = new Rect(0, .5f, 1, .5f);
            PlayerOneGameObject.GetComponent<InputManager>().input = InputManager.InputScheme.WASD;
        }

        if (PlayerTwoGameObject != null)
        {
            Camera cameraTwo = PlayerTwoGameObject.GetComponentInChildren<Camera>();
            cameraTwo.rect = new Rect(0, 0, 1, .5f);
            PlayerTwoGameObject.GetComponent<InputManager>().input = InputManager.InputScheme.arrowKeys;
        }

    }
}
