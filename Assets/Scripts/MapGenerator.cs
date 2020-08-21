using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] gridPrefabs;
    public int rows;
    public int columns;
    private Room[,] grid;
    private float roomWidth = 50f;
    private float roomHeight = 50f;
    public bool LevelOfTheDay = false;
    public int SeedNumber;

    
    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[Random.Range(0, gridPrefabs.Length)];
    }

    public void GenerateGrid()
    {
        //Start with an empty grid
        grid = new Room[columns, rows];
        //For each grid row
        for (int currentRow=0;currentRow < rows; currentRow++)
        {
            for (int currentColumn=0;currentColumn < columns; currentColumn++)
            {
                //figure out the location
                float xPosition = roomWidth * currentColumn;
                float zPosition = roomHeight * currentRow;
                Vector3 newPosition = new Vector3(xPosition, 0, zPosition);

                //Create a new grid at the appropriate location
                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity) as GameObject;

                //Set the room's parent
                tempRoomObj.transform.parent = this.transform;

                //Give the room a meaningful name
                tempRoomObj.name = "Room_" + currentColumn + "," + currentRow;
                

                Room tempRoom = tempRoomObj.GetComponent<Room>();
                GameManager.Instance.SpawnEnemy(tempRoom);
                GameManager.Instance.Rooms.Add(tempRoom);

                grid[currentColumn, currentRow] = tempRoom;

                if(rows == 1)
                {
                    //Do nothing
                }
                else if(currentRow == 0)
                {
                    tempRoom.doorNorth.SetActive(false);
                }
                else if (currentRow == rows - 1)
                {
                    tempRoom.doorSouth.SetActive(false);
                }
                else
                {
                    tempRoom.doorNorth.SetActive(false);
                    tempRoom.doorSouth.SetActive(false);
                }

                if(columns == 1)
                {
                    //Do nothing
                }
                else if(currentColumn == 0)
                {
                    tempRoom.doorEast.SetActive(false);
                }
                else if(currentColumn == columns - 1)
                {
                    tempRoom.doorWest.SetActive(false);
                }
                else
                {
                    tempRoom.doorEast.SetActive(false);
                    tempRoom.doorWest.SetActive(false);
                }

            }
        }
        
    }
    
    public void generateGrid()
    {
        //Start at the top left and move left to right, top to bottom

        //Pick what room we are going to place
        //Place a room
        //Open the inside doors, once all the rooms are placed
    }
    // Start is called before the first frame update
    void Start()
    {
        LevelOfTheDay = GameManager.Instance.LevelOfTheDay;
        if (LevelOfTheDay == true)
        {
            int Date = System.DateTime.Now.Month + System.DateTime.Now.Day + System.DateTime.Now.Year;
            Random.InitState(Date);
        }
        else
        {
            Random.InitState(SeedNumber);
        }
        GenerateGrid();
        GameManager.Instance.GameSetup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
