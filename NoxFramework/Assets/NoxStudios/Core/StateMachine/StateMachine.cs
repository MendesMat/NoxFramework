using UnityEngine;

namespace NoxStudios.Core.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        private BaseState _currentState;

        private void Update() => _currentState?.OnUpdate();
        private void FixedUpdate() => _currentState?.OnFixedUpdate();

        public void SetState(BaseState newState, bool reset = false)
        {
            if(newState == null) return;
            
            if(_currentState != newState || reset)
            {
                _currentState?.ExitState();
                _currentState = newState;
                newState.EnterState();
            }
        }
    }
}