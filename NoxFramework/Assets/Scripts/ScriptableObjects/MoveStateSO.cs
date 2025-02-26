using NoxStudios.Core.StateMachine;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "NoxStudios/StateMachine/MoveState", fileName = "MoveStateSO")]
    public class MoveStateSO : BaseState
    {
        public override void EnterState() => Debug.Log("Entrei no Move State");
        public override void OnUpdate() => Debug.Log("Update do Move State");
        public override void OnFixedUpdate() => Debug.Log("Fixed Update do Move State");
        public override void ExitState() => Debug.Log("Saí do Move State");
    }
}