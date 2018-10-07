using UnityEngine;

/// <summary>
/// checks if enemy is allowed to attack again
/// attacks target and sets new attack cooldown
/// then updates the target data (in this case updates the health)
/// </summary>
[CreateAssetMenu(menuName = "PluggableAI/Actions/Attack")]
public class AttackMelee : Action {
   
    public override void DoAction(StateManager _stateManager) {
       
        if (Time.time > _stateManager.GetComponent<EntityData>().nextAttack && _stateManager.chaseTarget != null) {
            _stateManager.GetComponent<EntityData>().nextAttack = Time.time + _stateManager.GetComponent<EntityData>().attackRate;
            _stateManager.chaseTarget.GetComponent<EntityData>().health -= _stateManager.GetComponent<EntityData>().attackDamage;
            _stateManager.GetComponent<EntityData>().UpdateData();
            }
        }
    }
    
