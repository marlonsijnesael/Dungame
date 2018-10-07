using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Target null")]
public class DecisionTargetNull : Decision {

    public override bool Decide(StateManager controller) {
        if (controller.chaseTarget == null) {
            return true;
            }
        return false;
        }

    }
