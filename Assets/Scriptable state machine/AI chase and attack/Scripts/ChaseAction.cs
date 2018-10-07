using UnityEngine;

/// <summary>
/// Sets the destination of the navmeshagent on statemanager to the chasetarget and makes the enemy look at target
/// </summary>
[CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : Action {

    public override void DoAction(StateManager _stateManager) {
        if (_stateManager.chaseTarget != null && _stateManager.navMeshAgent.isOnNavMesh) {
            _stateManager.navMeshAgent.SetDestination(_stateManager.chaseTarget.position);
            _stateManager.transform.LookAt(_stateManager.chaseTarget.position);
            }
        }

    }
