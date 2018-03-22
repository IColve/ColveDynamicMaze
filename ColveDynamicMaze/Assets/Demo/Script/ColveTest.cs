using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColveTest : MonoBehaviour
{
	public GameObject room;
	public Vector2 roomCreateRange;
	private List<GameObject> rooms = new List<GameObject>();

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			TestCreateRooms();
		}
	}

	private void TestCreateRooms()
	{
		for (int i = 0; i < rooms.Count; i++)
		{
			Destroy(rooms[i]);
		}
		rooms.Clear();
		MazeRoomManager.instance.CreateRooms(roomCreateRange);
		List<MazeRoomItem> items = MazeRoomManager.instance.RoomItemList;
		for (int i = 0; i < items.Count; i++)
		{
			for (int j = 0; j < items[i].LatticeList.Length; j++)
			{
				Vector2 pos = items[i].LatticeList[j];
				GameObject obj = Instantiate(room) as GameObject;
				obj.transform.position = new Vector3(pos.x,0,pos.y);
				rooms.Add(obj);
			}
		}
	}
}
