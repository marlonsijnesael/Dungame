using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Statemanager framework made with unity official tutorial on plugable ai
/// changed to suit my dungeon generator
/// Behaviour of AI is written by me
/// </summary>
public class StateManager : MonoBehaviour {

    public State currentState;
    
    public Transform eyes;
    public GameObject[] body;
    public State remainState;
   

    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public List<Transform> wayPointList;
    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public float stateTimeElapsed;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        }

    private void Update() {
        //check if navmesh is working properly, otherwise use Warp() fucntion to reset the navmesh data on agent
        //this is done because of some issue created by baking the navmesh at runtime
        if (!navMeshAgent.isOnNavMesh) {
            navMeshAgent.Warp(this.transform.position);
            }
        currentState.UpdateState(this);
        
        }

    //gizmos for debugging and testing
    private void OnDrawGizmos() {
        if (currentState != null && eyes != null) {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(eyes.position, 10);
            }
        }

    //if next state is not the state it should be 
    public void TransitionToState(State nextState) {
        if (nextState != remainState) {
            currentState = nextState;
            OnExitState();
            }
        }

    private void OnExitState() {
        stateTimeElapsed = 0;
        }
    }