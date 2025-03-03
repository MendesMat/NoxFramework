using NoxStudios.Core.StateMachine;
using UnityEngine;

namespace NoxStudios.ScriptableObjects
{
    [CreateAssetMenu(menuName = "NoxStudios/StateMachine/IdleState", fileName = "IdleStateSO")]
    public class IdleStateSO : BaseState
    {
        public override void EnterState() => Debug.Log("Entrei no Idle State");
        public override void OnUpdate() => Debug.Log("Update do Idle State");
        public override void OnFixedUpdate() => Debug.Log("Fixed Update do Idle State");
        public override void ExitState() => Debug.Log("Saí do Idle State");
    }
}