using System;
using UnityEngine;

namespace NoxStudios.Core.EventBus
{
    public class Hero : MonoBehaviour
    {
        private HealthComponent _health;
        private ManaComponent _mana;
        
        private EventBinding<TestEvent> _eventBinding;
        private EventBinding<PlayerEvent> _playerEventBinding;

        private void Awake()
        {
            _health = gameObject.GetComponent<HealthComponent>();
            _mana = gameObject.GetComponent<ManaComponent>();
        }

        private void OnEnable()
        {
            _eventBinding = new EventBinding<TestEvent>(HandleTestEvent);
            _playerEventBinding = new EventBinding<PlayerEvent>(HandlePlayerEvent);
            
            EventBus<TestEvent>.Register(_eventBinding);
            EventBus<PlayerEvent>.Register(_playerEventBinding);
        }
        
        private void OnDisable()
        {
            EventBus<TestEvent>.Unregister(_eventBinding);
            EventBus<PlayerEvent>.Unregister(_playerEventBinding);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) EventBus<TestEvent>.Raise(new TestEvent());
            
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                EventBus<PlayerEvent>.Raise(new PlayerEvent
                {
                    Health = _health.Health,
                    Mana = _mana.Mana
                });
            }
        }

        private static void HandleTestEvent()
        {
            Debug.Log("Test event received.");
        }

        private static void HandlePlayerEvent(PlayerEvent playerEvent)
        {
            Debug.Log($"Player event received! Health: {playerEvent.Health}, Mana: {playerEvent.Mana}");
        }
    }
}