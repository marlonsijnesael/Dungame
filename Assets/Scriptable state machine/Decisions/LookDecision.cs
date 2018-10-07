using UnityEngine;

/// returns true if enemy casts ray and returns another living entity
/// if true , stateManager target is set to the spotted entity

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision {

    public int radius;
    public int rayLength;

    public override bool Decide(StateManager _stateManager) {
        bool targetVisible = Look(_stateManager);
        return targetVisible;
        }

    private bool Look(StateManager _stateManager) {
        Vector3 fwd = _stateManager.eyes.transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        Debug.DrawRay(_stateManager.eyes.transform.position, fwd * 50, Color.green);
        //spherecast is used here to simulate eyesight as it creates a bigger collision point
        if (Physics.SphereCast(_stateManager.eyes.transform.position,radius, fwd, out hit, rayLength)) {
            if (hit.transform.tag == "Player" ) {
                Debug.Log(hit.transform.name);
                _stateManager.chaseTarget = hit.transform;
                return true;
                }
            //when agent encounters another agent, it copies the agent's target and "helps "the other target
            else if (hit.transform.tag == "Agent" && hit.transform.GetComponent<StateManager>().chaseTarget != null) {
                _stateManager.chaseTarget = hit.transform.GetComponent<StateManager>().chaseTarget;
                }
            } else {
            return false;
            }
        return false;

        }
    }
