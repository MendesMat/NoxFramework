using NoxStudios.Core.StateMachine;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "NoxStudios/StateMachine/AttackState", fileName = "AttackStateSO")]
    public class AttackStateSO : BaseState
    {
        public override void EnterState() => Debug.Log("Entrei no Attack State");
        public override void OnUpdate() => Debug.Log("Update do Attack State");
        public override void OnFixedUpdate() => Debug.Log("Fixed Update do Attack State");
        public override void ExitState() => Debug.Log("Saí do Attack State");
    }
}