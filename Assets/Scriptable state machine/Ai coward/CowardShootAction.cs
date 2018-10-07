
using UnityEngine;

public class CowardShootAction : Action {

    public override void DoAction(StateManager _stateManager) {
        _stateManager.navMeshAgent.destination = _stateManager.chaseTarget.position - _stateManager.chaseTarget.transform.forward;
        }

    }
