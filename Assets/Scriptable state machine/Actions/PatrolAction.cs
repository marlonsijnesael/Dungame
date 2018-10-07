
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action {

    public GridGenerator grid;

    public override void DoAction(StateManager controller) {
        Patrol(controller);
        }

    private void Patrol(StateManager controller) {
        if (grid == null) {
            return;
            }
        controller.navMeshAgent.destination = NewRoomPos();
        controller.navMeshAgent.isStopped = false;

        }

    public Vector3 NewRoomPos() {
        List<RoomNode> rooms = new List<RoomNode>();
        foreach(RoomNode room in rooms) {
            if (room.type == 1) {
                rooms.Add(room);
                }
            }

        RoomNode roomToCheck = rooms[Random.Range(0, rooms.Count)];
        return roomToCheck.worldPos;
        }

    }