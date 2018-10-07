using UnityEngine;


/// <summary>
/// Checks if distance to target is either bigger or smaller than target, depending on the value of biggerthantarget bool
/// this is used to check if target is in range for actions like attacking 
/// </summary>
[CreateAssetMenu(menuName = "PluggableAI/Decisions/Distance")]
public class DistanceDecision : Decision {

    public bool BiggerThanTarget;

    public int target;

    public override bool Decide(StateManager _stateManager) {
        bool targetVisible = IsEnemyclose(_stateManager);
        return targetVisible;
        }

    private bool IsEnemyclose(StateManager _stateManager) {

        if (_stateManager.chaseTarget == null) {
            return false;
            }
        float distToTarget = Vector3.Distance(_stateManager.transform.position, _stateManager.chaseTarget.transform.position);

        if (BiggerThanTarget) {
            if (distToTarget > target) {
                return true;
                }
            } else {
            if (distToTarget < target) {
                return true;
                }
            }
        return false;
        }


    }
