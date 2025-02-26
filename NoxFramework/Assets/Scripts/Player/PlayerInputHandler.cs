using System.Collections.Generic;
using NoxStudios.Core.StateMachine;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NoxStudios.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public PlayerInput playerInput;
        public StateMachine stateMachine;
        public List<BaseState> states = new();
        
        private readonly Dictionary<InputAction, BaseState> _inputToStateMapping = new();

        private void Awake() => InitializeStateMappings();

        private void InitializeStateMappings()
        {
            var inputActions = playerInput.actions;
            
            _inputToStateMapping[inputActions["Move"]] = FindStateOfType<MoveStateSO>();
            _inputToStateMapping[inputActions["Jump"]] = FindStateOfType<JumpStateSO>();
            _inputToStateMapping[inputActions["Attack"]] = FindStateOfType<AttackStateSO>();
        }
        
        private BaseState FindStateOfType<T>() where T : BaseState
        {
            foreach (var state in states)
            {
                if (state is not T typedState) continue;
                return typedState;
            }

            return null;
        }

        public void OnMoveInput(InputAction.CallbackContext context) 
            => HandleInput(context, playerInput.actions["Move"]);

        public void OnJumpInput(InputAction.CallbackContext context) 
            => HandleInput(context, playerInput.actions["Jump"]);

        public void OnAttackInput(InputAction.CallbackContext context) 
            => HandleInput(context, playerInput.actions["Attack"]);

        private void HandleInput(InputAction.CallbackContext context, InputAction inputAction)
        {
            if (context.performed 
                && _inputToStateMapping.TryGetValue(inputAction, out var state))
            {
                if (state == null) return;
                stateMachine.SetState(state);
            }
        }
    }
}