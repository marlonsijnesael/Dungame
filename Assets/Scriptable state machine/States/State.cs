using UnityEngine;

/// <summary>
/// used to create state assets
/// The state then loops through each action and each transition
/// </summary>
[CreateAssetMenu(menuName = "PluggableAI/State")]
public class State : ScriptableObject {

    public Action[] actions;
    public State[] possibleStates;
    public Transition[] transitions;
    public Color sceneGizmoColor = Color.grey;

    public void UpdateState(StateManager controller) {
        DoActions(controller);
        CheckTransitions(controller);
        }

    private void DoActions(StateManager controller) {
        for (int i = 0; i < actions.Length; i++) {
            actions[i].DoAction(controller);
            }
        }

    private void CheckTransitions(StateManager controller) {
        for (int i = 0; i < transitions.Length; i++) {
            bool decisionSucceeded = transitions[i].decision.Decide(controller);

            if (decisionSucceeded) {
                controller.TransitionToState(transitions[i].trueState);
                } else {
                controller.TransitionToState(transitions[i].falseState);
                }
            }
        }


    }
