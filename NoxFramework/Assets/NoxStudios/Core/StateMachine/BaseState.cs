using UnityEngine;

namespace NoxStudios.Core.StateMachine
{
    public abstract class BaseState : ScriptableObject
    {
        public bool IsComplete { get; set; }

        public virtual void EnterState() { }
        public virtual void OnUpdate() { }
        public virtual void OnFixedUpdate() { }
        public virtual void ExitState() { }
    }
}