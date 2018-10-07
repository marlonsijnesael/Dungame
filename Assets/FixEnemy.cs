using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FixEnemy : MonoBehaviour {

    public NavMeshAgent navAgent;

    public void Start() {
        navAgent = this.gameObject.GetComponent<NavMeshAgent>();
        }

    private void Update() {
        if (!navAgent.isOnNavMesh) {
            navAgent.Warp(transform.position);
            }
        }
    }
