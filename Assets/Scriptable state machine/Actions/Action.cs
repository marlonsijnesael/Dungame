using UnityEngine;

//abstract script to implement in action
public abstract class Action : ScriptableObject {

    public abstract void DoAction(StateManager _stateManager);
}
