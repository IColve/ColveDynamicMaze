using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColveTest : MonoBehaviour
{
	public GameObject room;
	public int simpleRoomCount;
	public int complexRoomCount;
	private List<GameObject> rooms = new List<GameObject>();

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			TestCreateRooms(true);
		}

		if (Input.GetKeyDown(KeyCode.B))
		{
			MazeRoomManager.instance.complexRoomLattices = new List<Vector2[]>()
			{
				new []{Vector2.zero,new Vector2(0,1)},
				new []{Vector2.zero,new Vector2(1,0)},
				new []{Vector2.zero,new Vector2(1,0),new Vector2(0,1), new Vector2(1,1)},
				new []{Vector2.zero,new Vector2(0,-1)},
				new []{Vector2.zero,new Vector2(-1,0)},
				new []{Vector2.zero,new Vector2(-1,-1),new Vector2(-1,0),new Vector2(0,-1)}
			};
			TestCreateRooms(false);
		}
	}

	private void TestCreateRooms(bool isSimple)
	{
		for (int i = 0; i < rooms.Count; i++)
		{
			Destroy(rooms[i]);
		}
		rooms.Clear();
		if (isSimple)
		{
			MazeRoomManager.instance.CreateRooms(simpleRoomCount);
		}
		else
		{
			MazeRoomManager.instance.CreateRooms(simpleRoomCount, complexRoomCount);
		}
		List<MazeRoomItem> items = MazeRoomManager.instance.RoomItemList;
		for (int i = 0; i < items.Count; i++)
		{
			for (int j = 0; j < items[i].LatticeList.Count; j++)
			{
				Vector2 pos = items[i].LatticeList[j];
				GameObject obj = Instantiate(room) as GameObject;
				obj.transform.position = new Vector3(pos.x,0,pos.y);
				rooms.Add(obj);
			}
		}
	}

	public List<List<MyClass>> aaa;
	[System.Serializable]
	public class MyClass
	{
		public Vector2 vec;
	}
}
