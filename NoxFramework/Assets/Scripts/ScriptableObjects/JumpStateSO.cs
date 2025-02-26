using NoxStudios.Core.StateMachine;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "NoxStudios/StateMachine/JumpState", fileName = "JumpStateSO")]
    public class JumpStateSO : BaseState
    {
        public override void EnterState() => Debug.Log("Entrei no Jump State");
        public override void OnUpdate() => Debug.Log("Update do Jump State");
        public override void OnFixedUpdate() => Debug.Log("Fixed Update do Jump State");
        public override void ExitState() => Debug.Log("Saí do Jump State");
    }
}