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

	private Vector2[] simpleRoomLattice = new Vector2[]{new Vector2(0, 1), new Vector2(0, -1), new Vector2(-1, 0), new Vector2(1, 0)};
	public List<Vector2[]> complexRoomLattices;
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

	public void CreateRooms(int simpleRoomCount, int ComplexRoomCount = 0)
	{
		if (roomItemList == null)
		{
			roomItemList = new List<MazeRoomItem>();
		}
		else
		{
			roomItemList.Clear();
		}
		for (int i = 0; i < simpleRoomCount; i++)
		{
			roomItemList.Add(CreateRoom());
		}
		for (int i = 0; i < ComplexRoomCount; i++)
		{
			roomItemList.Add(CreateRoom(false));
		}
	}

	private MazeRoomItem CreateRoom(bool isSimple = true)
	{
		if (roomItemList.Count == 0)
		{
			return new MazeRoomItem() {LatticeList = new List<Vector2>(){Vector2.zero}};
		}
		else
		{
			MazeRoomItem item = new MazeRoomItem();
			while (true)
			{
				MazeRoomItem linkItem = roomItemList[Random.RandomRange(0, roomItemList.Count)];
				Vector2 linkPos = linkItem.LatticeList[Random.Range(0, linkItem.LatticeList.Count)];
				linkPos += simpleRoomLattice[Random.Range(0, simpleRoomLattice.Length)];
				bool canCreate = true;
				if (isSimple)
				{
					for (int i = 0; i < roomItemList.Count; i++)
					{
						if (roomItemList[i].LatticeList.Contains(linkPos))
						{
							canCreate = false;
							break;
						}
					}
					item.LatticeList.Add(linkPos);
				}
				else
				{
					if (complexRoomLattices == null || complexRoomLattices.Count == 0)
					{
						Debug.LogError("complexRoomLattices has nothing");
						return CreateRoom();
					}
					item.LatticeList.Add(linkPos);
					Vector2[] roomLattices = complexRoomLattices[Random.Range(0, complexRoomLattices.Count)];
					for (int i = 0; i < roomLattices.Length; i++)
					{
						item.LatticeList.Add(linkPos + roomLattices[i]);
					}
					for (int i = 0; i < roomItemList.Count; i++)
					{
						for (int j = 0; j < item.LatticeList.Count; j++)
						{
							if (roomItemList[i].LatticeList.Contains(item.LatticeList[j]))
							{
								canCreate = false;
								break;
							}
						}
						if (!canCreate)
						{
							item.LatticeList.Clear();
							break;
						}
					}
				}
				if (!canCreate)
				{
					continue;
				}
				return item;
			}
		}
	}
}
