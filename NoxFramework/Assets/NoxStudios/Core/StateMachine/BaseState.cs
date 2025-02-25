using UnityEngine;

namespace NoxStudios.Core.StateMachine
{
    public abstract class BaseState : ScriptableObject
    {
        public StateMachine MyStateMachine { get; set; }
        public bool IsComplete { get; set; }

        public void SetStateMachine(StateMachine stateMachine) => MyStateMachine = stateMachine;

        public virtual void EnterState() { }
        public virtual void OnUpdate() { }
        public virtual void OnFixedUpdate() { }
        public virtual void ExitState() { }
    }
}