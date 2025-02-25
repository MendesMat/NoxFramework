using NoxStudios.Core.StateMachine;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "NoxStudios/StateMachine", fileName = "ExampleStateSO")]
    public class TestStateSO : BaseState
    {
        public override void EnterState() => Debug.Log("Entrei no TestState");
        public override void OnUpdate() => Debug.Log("Update do OtherState");
        public override void OnFixedUpdate() => Debug.Log("Fixed Update do TestState");
        public override void ExitState() => Debug.Log("Saí do TestState");
    }
}