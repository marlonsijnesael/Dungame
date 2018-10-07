using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshMaker : MonoBehaviour {

    public static NavmeshMaker _Instance;
    public GridGenerator grid;
    public List<RoomNode> roomNodes = new List<RoomNode>();


    private void Awake() {
        if (_Instance == null) {
            _Instance = this;
            } else {
            Destroy(this);
            }
        }

    private void Start() {
        grid = GetComponent<GridGenerator>();
        }

    public void Bake() {

        this.gameObject.GetComponent<NavMeshSurface>().BuildNavMesh();
        }


    }