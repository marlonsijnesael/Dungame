using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class contains all data for a room/node on the grid.
/// this class is used to store data for the procedural generation of our map
/// </summary>
[System.Serializable]
public class RoomNode {

    //position on node grid
    public int gridX, gridY;

    //check if node will be filled with a room or a wall
    public bool isFilled;

    //to find room in inspector
    public string roomName;

    //worldposition calculated with grid position in grid generator
    public Vector3 worldPos;

    //reference to gameObject attached to this node
    public GameObject self;

    //cost for A* pathfinding to traverse from/to this node on grid g= cost from starting point to target node and H = heuristic
    public int gCost, hCost;

    //combined cost of gCost and hCost
    public int fCost {
        get { return hCost + gCost; }
        }

    //parent node for path tracing
    public RoomNode parent;

    //type for room instantiation
    public int type = 0;    //type 0 == empty node, type 1 = room
    public int interriorType = 0;

    public List<RoomNode> Neighbours = new List<RoomNode>();

    //Roomnode constructor
    public RoomNode(int _gridX, int _gridY, string _roomName, Vector3 _worldPos, bool _isRoom) {
        gridX = _gridX;
        gridY = _gridY;
        roomName = _roomName;
        worldPos = _worldPos;
        isFilled = _isRoom;
        }

    //used when populating the map with either rooms or maps
    public void InitSelf() {
        if (isFilled) {
            type = 1;
            } else {
            type = 0;
            }
        self.transform.position = worldPos;
        self.name = roomName;
        }
    }