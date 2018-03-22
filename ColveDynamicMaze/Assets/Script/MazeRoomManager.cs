//By:Colve
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeRoomManager : MonoBehaviour
{
	public static MazeRoomManager instance;

	public static MazeRoomManager Instance
	{
		get { return instance; }
	}

	private void Awake()
	{
		instance = this;
	}

	private Vector2[] roomLattice = new Vector2[]{new Vector2(0, 1), new Vector2(0, -1), new Vector2(-1, 0), new Vector2(1, 0)};
	
	private List<MazeRoomItem> roomItemList;
	public List<MazeRoomItem> RoomItemList
	{
		get
		{
			return roomItemList; 
			
		}
	}

	private int roomCount;
	
	public int RoomCount
	{
		get
		{
			return roomItemList.Count;
		}
	}

	public void CreateRooms(Vector2 roomCreateRange)
	{
		if (roomItemList == null)
		{
			roomItemList = new List<MazeRoomItem>();
		}
		else
		{
			roomItemList.Clear();
		}
		int roomNumber = Random.Range((int)roomCreateRange.x, (int)roomCreateRange.y);
		for (int i = 0; i < roomNumber; i++)
		{
			roomItemList.Add(CreateRoom());
		}
	}

	private MazeRoomItem CreateRoom()
	{
		if (roomItemList.Count == 0)
		{
			return new MazeRoomItem() {LatticeList = new[] {Vector2.zero}};
		}
		else
		{
			MazeRoomItem item = new MazeRoomItem();
			while (true)
			{
				MazeRoomItem linkItem = roomItemList[Random.RandomRange(0, roomItemList.Count)];
				Vector2 linkPos = linkItem.LatticeList[Random.Range(0, linkItem.LatticeList.Length)];
				linkPos += roomLattice[Random.Range(0, roomLattice.Length)];
				bool canCreate = true;
				for (int i = 0; i < roomItemList.Count; i++)
				{
					if (roomItemList[i].LatticeList.Contains(linkPos))
					{
						canCreate = false;
						break;
					}
				}
				if (!canCreate)
				{
					continue;
				}
				item.LatticeList = new[] {linkPos};
				return item;
			}
		}
	}

}
