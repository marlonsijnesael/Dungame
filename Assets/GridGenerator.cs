using System.Collections.Generic;
using UnityEngine;

//clas creates grid of nodes on start and translates each node to worldposition
public class GridGenerator : MonoBehaviour {

    public RoomNode[,] roomNodeArray;               // 2d array to create grid of room nodes

    [Header("grid Sizes")]
    public int arraySizeX;
    public int arraySizeY;              // Size of grid
    public int gridWorldSizeX;
    public int gridWorldSizeY;

    [Header("node")]
    public int nodeDiameter;
    public int nodeRadius;

    private const int horizontalCost = 10;
    private const int verticalCost = 14;

    public Vector3 worldBottomLeft;

    private void Awake() {
        //calculate  the size of the array relative to the worldSize
        arraySizeX = Mathf.Abs(gridWorldSizeX / (nodeDiameter * 2));
        arraySizeY = Mathf.Abs(gridWorldSizeY / (nodeDiameter * 2));
        nodeRadius = 2 * nodeDiameter;
        CreateGrid();
        FindNeighbours();
        }

    //calculate the bottom left part of the world as a starting point
    //create "grid"and loop through it with a nested for loop
    //calculate worldposition and instantiate new node
    public void CreateGrid() {
        //iteration count for easy tracking
        int iterationCount = 0;
        roomNodeArray = new RoomNode[arraySizeX, arraySizeY];
        worldBottomLeft = transform.position - Vector3.right * gridWorldSizeX / 2 - Vector3.forward * gridWorldSizeY / 2;

        for (int x = 0; x < arraySizeX; x++) {
            for (int z = 0; z < arraySizeY; z++) {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (z * nodeDiameter + nodeRadius);
                iterationCount++;
                RoomNode NewRoom = new RoomNode(x, z, "roomNode" + iterationCount.ToString(), worldPoint, FillRoom());
                roomNodeArray[x, z] = NewRoom;
                }
            }
        }

    //check and set all neigbours for each node in array
    public void FindNeighbours() {
        foreach (RoomNode _r in roomNodeArray) {
            _r.Neighbours = LoopThrough(_r);
            }
        }

    //list loops through the neighbours of the selected node, returns neighbours;
    public List<RoomNode> LoopThrough(RoomNode _inTarray) {
        List<RoomNode> neighbours = new List<RoomNode>();

        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                if (x == 1 && y == 0 || x == 0 && y == 1 || x == -1 && y == 0 || x == 0 && y == -1) {

                    int checkX = _inTarray.gridX + x;
                    int checkY = _inTarray.gridY + y;

                    if (checkX >= 0 && checkX < arraySizeX && checkY >= 0 && checkY < arraySizeY) {
                        neighbours.Add(roomNodeArray[checkX, checkY]);
                        }
                    }
                }
            }
        return neighbours;
        }

    //Translate world position to grid position
    public RoomNode NodeFromWorldPoint(Vector3 worldPosition) {
        float percentX = (worldPosition.x + gridWorldSizeX / 2) / gridWorldSizeX;
        float percentY = (worldPosition.z + gridWorldSizeY / 2) / gridWorldSizeY;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((arraySizeX - 1) * percentX);
        int y = Mathf.RoundToInt((arraySizeY - 1) * percentY);
        return roomNodeArray[x, y];
        }

    //selects which node is actually a room or not
    public bool FillRoom() {
        if (Random.Range(0, 2) == 0) {
            return true;
            } else {
            return false;
            }
        }

    //calculate distance between two nodes
    public int GetDistance(RoomNode _roomnodeA, RoomNode _roomnodeB) {
        int distX = Mathf.Abs(_roomnodeA.gridX - _roomnodeB.gridX);
        int distY = Mathf.Abs(_roomnodeA.gridY - _roomnodeB.gridY);

        if (distX > distY) {
            return verticalCost * distY + horizontalCost * (distX - distY);
            }
        Debug.Log(verticalCost * distX + 10 * (distY - distX));
        return verticalCost * distX + 10 * (distY - distX);
        }
    }