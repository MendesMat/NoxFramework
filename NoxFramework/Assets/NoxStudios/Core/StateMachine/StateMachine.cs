using System.Collections.Generic;
using UnityEngine;

namespace NoxStudios.Core.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        public List<BaseState> states = new();
        private BaseState _currentState;

        private void Awake()
        {
            foreach (var state in states) state.SetStateMachine(this);
        }

        private void Update() => _currentState.OnUpdate();
        private void FixedUpdate() => _currentState.OnFixedUpdate();

        public void SetState(BaseState newState, bool reset = false)
        {
            if(newState == null || !states.Contains(newState)) return;
            
            if(_currentState != newState || reset)
            {
                _currentState.ExitState();
                _currentState = newState;
                newState.EnterState();
            }
        }
    }
}