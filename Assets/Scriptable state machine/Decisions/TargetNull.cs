using UnityEngine;

/// <summary>
/// returns true if statemanager has no target, so the AI will know it should quit a target dependend state
/// </summary>
[CreateAssetMenu(menuName = "PluggableAI/Decisions/TargetNull")]
public class TargetNull : Decision {

    public override bool Decide(StateManager _stateManager) {
        if (_stateManager.chaseTarget == null) {
            return true;
            }
        return false;
        }

    }
