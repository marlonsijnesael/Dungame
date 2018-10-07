using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Behind")]
public class DecisionBehindTarget : Decision {

    public override bool Decide(StateManager _stateManager) {

        return BehindTarget(_stateManager);
        }

    public bool BehindTarget(StateManager _stateManager) {

        if (Vector3.Dot(_stateManager.chaseTarget.transform.position, _stateManager.transform.forward) > 0) {
            return true;
            }
        return false;
        }
    }
