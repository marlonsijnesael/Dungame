using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/Coward/Caught")]
public class CowardCaught : Action {
    public Color Invicible;

    public override void DoAction(StateManager _stateManager) {
        foreach (GameObject bodyPart in _stateManager.body) {
            bodyPart.GetComponent<MeshRenderer>().material.color = Vector4.Lerp(bodyPart.GetComponent<MeshRenderer>().material.color, Invicible, Time.deltaTime);
            }
        }
    }

