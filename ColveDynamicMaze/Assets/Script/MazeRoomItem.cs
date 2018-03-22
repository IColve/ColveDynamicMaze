//By:Colve

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRoomItem
 {
     private List<Vector2> latticeList;
     public List<Vector2> LatticeList
     {
         get { return latticeList; }
         set { latticeList = value; }
     }

     public MazeRoomItem()
     {
         latticeList = new List<Vector2>();
     }
 }
