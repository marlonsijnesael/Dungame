using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Coward/Orbit")]
public class ActionOrbitTarget : Action {

    public override void DoAction(StateManager _stateManager) {
        _stateManager.navMeshAgent.destination = _stateManager.chaseTarget.position - (_stateManager.chaseTarget.transform.forward * 3);
        _stateManager.transform.LookAt(_stateManager.chaseTarget);

        }
    }
