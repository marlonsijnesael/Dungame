using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PluggableAI/Actions/Coward/Idle")]
public class CowardIDleAction : Action {

    public Color visableColor;

    public override void DoAction(StateManager _stateManager) {
        foreach (GameObject bodyPart in _stateManager.body) {
            bodyPart.GetComponent<MeshRenderer>().material.color = visableColor;
            }
        }
    }
