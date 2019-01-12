using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.IO;

/// <summary>
/// this class is used to fill the created 2d node grid with rooms and walls
/// all code except for the pathfinding algorithm is written by me
/// </summary>
public class FillMap : MonoBehaviour {

    private GridGenerator _gridReference;
    private List<RoomNode> path = new List<RoomNode>();

    [Header("Iterations")]
    public int amountOfIterations;
    public int enemyCount;

    [Header("Prefabs")]

    public GameObject wallPrefab;
    public GameObject[] roomPrefab;
    public GameObject playerPrefab;
    public GameObject enemeyPrefab;

    private Vector3 PlayerSpawnPoint;

    private Vector3 EnemySpawnPoint;

    //public GameObject[] interiors;   --uncomment this to use multiple different interiors

    private void Start() {

        if (SpawnSettings._instance.switchInput) {
            CreateRoomsFromJSON();
            } else {
            _gridReference = GetComponent<GridGenerator>();
            CreateRoomsProcedurally();
            }
        }

    public void CreateRoomsFromJSON() {
        SaveDataClass loadedSave;
        GameObject spawnObj = SpawnSettings._instance.transform.gameObject;
        using (StreamReader stream = new StreamReader(SpawnSettings._instance.loadingPath)) {
            string json = stream.ReadToEnd();
            loadedSave = JsonUtility.FromJson<SaveDataClass>(json);
            Debug.Log(loadedSave.amountOfRooms);
            }

        foreach (CompressedRoomNode cRoomNode in loadedSave.cRoomNodes) {
            GameObject room = Instantiate(roomPrefab[cRoomNode.interriorType]);
            room.transform.position = new Vector3(cRoomNode.position.x, 0, cRoomNode.position.y);
            room.transform.SetParent(spawnObj.transform);
            room.GetComponent<Doors>().SetBools(cRoomNode.doorDirections);
            room.GetComponent<Doors>().SetDoors();
            }
        GameObject player = Instantiate(playerPrefab);
        PlayerSpawnPoint = spawnObj.transform.GetChild(Random.Range(0, SpawnSettings._instance.transform.childCount - 1)).position;
        player.transform.position = new Vector3(PlayerSpawnPoint.x, PlayerSpawnPoint.y + 10, PlayerSpawnPoint.z);
        }


    //Creates a path between two random nodes and repeats this proces for the amount of iterations wanted. 
    //when This is done, it determines if the node is a Room or a wall and Instantiates the right prefab.
    public void CreateRoomsProcedurally() {

        //start at one and end at -1 to make sure that the level is always closed at the borders
        Vector2Int room1 = new Vector2Int(Random.Range(1, _gridReference.arraySizeX - 1), Random.Range(1, _gridReference.arraySizeY - 1));
        Vector2Int room2 = new Vector2Int(Random.Range(1, _gridReference.arraySizeX - 1), Random.Range(1, _gridReference.arraySizeY - 1));
        RoomNode node1 = _gridReference.roomNodeArray[room1.x, room1.y];
        RoomNode node2 = _gridReference.roomNodeArray[room2.x, room2.y];

        //set first node as player spawnpoint
        PlayerSpawnPoint = node1.worldPos;

        PlayerSpawnPoint.y += 5;

        //first create and connect rooms
        for (int i = 0; i < amountOfIterations; i++) {
            FindPath(node1, node2);
            node1 = node2;
            room1 = new Vector2Int(Random.Range(1, _gridReference.arraySizeX - 1), Random.Range(1, _gridReference.arraySizeY - 1));
            node2 = _gridReference.roomNodeArray[room1.x, room1.y];
            }


        //fill in empty nodes as walls
        foreach (RoomNode room in _gridReference.roomNodeArray) {
            if (room.type != 1) {
                room.type = 0;

                }
            if (room.type == 1) {
                int roomType = Random.Range(0, roomPrefab.Length);
                InstantiateNode(room, roomPrefab[roomType], true, roomType);
                if (Random.value > 0.7f) {
                    EnemySpawnPoint = new Vector3(room.worldPos.x, room.worldPos.y + 5, room.worldPos.z);
                    GameObject enemy = Instantiate(enemeyPrefab);

                    //enemy.GetComponent<NavMeshAgent>().Warp(EnemySpawnPoint);
                    }
                SetDoors(room);
                } else if (room.type == 0) {
                InstantiateNode(room, wallPrefab, false, -1);
                }
            }
        for (int c = 0; c < this.transform.childCount; c++) {
            if (this.transform.GetChild(c).childCount == 0) {
                Destroy(this.transform.GetChild(c).gameObject);
                }
            }
        //instantiate player when all rooms are created
        GameObject player = Instantiate(playerPrefab);
        player.transform.position = PlayerSpawnPoint;

        NavmeshMaker._Instance.Bake();
        }

    public void SpawnEnemy(float _range, Vector3 _spawnPoint) {
        Vector3 randomPoint = _spawnPoint + Random.insideUnitSphere * _range;
        NavMeshHit hit;
        for (int i = 0; i < 30; i++) {
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) {
                // _spawnPoint = hit.position;
                }
            }
        }

    //function to instantiate the nodes without code repitition
    public void InstantiateNode(RoomNode _room, GameObject _prefab, bool _filled, int _roomType) {
        _room.self = Instantiate(_prefab);
        _room.self.transform.SetParent(this.transform);
        _room.isFilled = _filled;
        _room.interriorType = _roomType;
        _room.InitSelf();
        }

    //reverses the path produced by the a* pathfinding and marks all nodes along the path with the type 1 (room)
    public void Trace(RoomNode _startNode, RoomNode _endNode) {
        path = new List<RoomNode>();
        RoomNode currentNode = _endNode;

        while (currentNode != _startNode) {
            path.Add(currentNode);
            currentNode = currentNode.parent;
            currentNode.type = 1;
            }
        path.Reverse();
        }

    //checks if room needs doors/walls
    public void SetDoors(RoomNode _node) {
        Doors doorScript = _node.self.GetComponent<Doors>();
        doorScript.nodeData = _node;
        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                //check all nodes left, right, up and down from the current node
                if (x == 1 && y == 0 || x == 0 && y == 1 || x == -1 && y == 0 || x == 0 && y == -1) {

                    int checkX = _node.gridX + x;
                    int checkY = _node.gridY + y;

                    //check if coordinates are actually on grid
                    if (checkX >= 0 && checkX < _gridReference.arraySizeX && checkY >= 0 && checkY < _gridReference.arraySizeY) {
                        RoomNode NeighbournNode = _gridReference.roomNodeArray[checkX, checkY];

                        //check types of surrounding nodes and determine if there should be a wall
                        if (NeighbournNode.type != 0) {
                            if (x == 1 && y == 0) {
                                //doorScript.hasDoorEast = true;
                                doorScript.doorDirections[1] = 1;
                                }
                            if (x == -1 && y == 0) {
                                //doorScript.hasDoorWest = true;
                                doorScript.doorDirections[3] = 1;
                                }
                            if (x == 0 && y == 1) {
                                //doorScript.hasDoorNorth = true;
                                doorScript.doorDirections[0] = 1;
                                }
                            if (x == 0 && y == -1) {
                                //doorScript.hasDoorSouth = true;
                                doorScript.doorDirections[2] = 1;
                                }
                            }

                        }
                    }
                }
            }
        doorScript.SetDoors();
        // doorScript.SetInterior(interiors[Random.Range(0, interiors.Length)]);
        }

    //A* pathfinding algorithm
    public void FindPath(RoomNode startNode, RoomNode targetNode) {
        List<RoomNode> openSet = new List<RoomNode>();
        HashSet<RoomNode> closedSet = new HashSet<RoomNode>();
        openSet.Add(startNode);

        while (openSet.Count > 0) {
            RoomNode node = openSet[0];
            for (int i = 1; i < openSet.Count; i++) {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost) {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                    }
                }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode) {
                Trace(startNode, targetNode);
                Debug.Log("found");
                return;
                }

            foreach (RoomNode neighbour in _gridReference.LoopThrough(node)) {
                if (closedSet.Contains(neighbour)) {
                    continue;
                    }

                int newCostToNeighbour = node.gCost + _gridReference.GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = _gridReference.GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))

                        openSet.Add(neighbour);
                    }
                }
            }
        }
    }
