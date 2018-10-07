using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Overlap")]
public class DecisionOverlapSphere : Decision {

    public float radius;

    public override bool Decide(StateManager controller) {
        bool targetVisible = Look(controller);
        return targetVisible;
        }

    private bool Look(StateManager controller) {
        Collider[] hitColliders = Physics.OverlapSphere(controller.eyes.position, radius);
        Collider closest = new Collider();

        float clostestDist = 1000f;
        foreach (Collider col in hitColliders) {
            if (col.tag == "Player" || col.tag == "Agent") {
                float distance = Vector3.Distance(controller.transform.position, col.gameObject.transform.position);
                if (distance < clostestDist) {
                    closest = col;
                    }
                }
            }
        
        if (closest.gameObject.transform != null) {
            controller.chaseTarget = closest.gameObject.transform;
            return true;
            }
        return false;
        }
    }
