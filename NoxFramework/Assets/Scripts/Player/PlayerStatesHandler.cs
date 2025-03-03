using System.Collections.Generic;
using NoxStudios.Core.EventBus;
using NoxStudios.Core.EventBus.Events;
using NoxStudios.Core.StateMachine;
using NoxStudios.ScriptableObjects;
using ScriptableObjects;
using UnityEngine;

namespace NoxStudios.Player
{
    public class PlayerStatesHandler : MonoBehaviour
    {
        public StateMachine stateMachine;
        public List<BaseState> states = new();

        private EventBinding<PlayerInputEvent> _inputEventBinding;
        private readonly Dictionary<string, BaseState> _inputToState = new();

        private void Awake()
        {
            _inputEventBinding = new EventBinding<PlayerInputEvent>(HandleInput);
            
            _inputToState["Idle"] = GetType<IdleStateSO>();
            _inputToState["Move"] = GetType<MoveStateSO>();
            _inputToState["Jump"] = GetType<JumpStateSO>();
            _inputToState["Attack"] = GetType<AttackStateSO>();
        }

        private void OnEnable() => EventBus<PlayerInputEvent>.Register(_inputEventBinding);

        private void OnDisable() => EventBus<PlayerInputEvent>.Unregister(_inputEventBinding);

        private T GetType<T>() where T : BaseState
        {
            foreach (var state in states)
            {
                if (state is T stateType) return stateType;
            }
            
            Debug.Log($"Estado do tipo {typeof(T).Name} não encontrado na lista de estados. Retorno nulo.");
            return null;
        }

        private void HandleInput(PlayerInputEvent @event)
        {
            if (@event.IsInputReleased || !_inputToState.TryGetValue(@event.Context.action.name, out var newState))
            {
                stateMachine.SetState(_inputToState["Idle"]);
                return;
            }
            
            stateMachine.SetState(newState);
        }
    }
}