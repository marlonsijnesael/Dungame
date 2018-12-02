
using UnityEngine;
using UnityEngine.AI;

//because the room is spawned in run-time, it is not possible to pre-build the navmesh, so
//Once the room is set up, a navmesh will be baked to make the scene traversable for agents
public class NavmeshMaker : MonoBehaviour {

    public static NavmeshMaker _Instance;

    private void Awake() {
        if (_Instance == null) {
            _Instance = this;
            } else {
            Destroy(this);
            }
        }

    public void Bake() {
        this.gameObject.GetComponent<NavMeshSurface>().BuildNavMesh();
        }
    }