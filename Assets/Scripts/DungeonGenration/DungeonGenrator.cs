using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungenGenrator : MonoBehaviour
{
    public DungenGenrationData dungenGenrationData;
    private List<Vector2Int> dungeonRooms;

    private void Start()
    {
        dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungenGenrationData);
        SpawnRoome(dungeonRooms);
    }

    private void SpawnRoome(IEnumerable<Vector2Int> rooms)
    {
        RoomController.instance.LoadRoom("Start", 0, 0);
        foreach(Vector2Int roomLoaction in rooms)
        {
          
                RoomController.instance.LoadRoom(RoomController.instance.GetRandomRoomName(), roomLoaction.x, roomLoaction.y);
         
            
        }
    }
}
