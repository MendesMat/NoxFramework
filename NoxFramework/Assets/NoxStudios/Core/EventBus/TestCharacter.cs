using UnityEngine;

namespace NoxStudios.Core.EventBus
{
    public class TestCharacter : MonoBehaviour
    {
        private EventBinding<ArgEvent> _argEvent;
        private EventBinding<NoArgEvent> _noArgEvent;

        private void OnEnable()
        {
            _argEvent = new EventBinding<ArgEvent>(TestArgEvent);
            _noArgEvent = new EventBinding<NoArgEvent>(TestNoArgEvent);
            
            EventBus<ArgEvent>.Register(_argEvent);
            EventBus<NoArgEvent>.Register(_noArgEvent);
        }

        private void OnDisable()
        {
            EventBus<ArgEvent>.Unregister(_argEvent);
            EventBus<NoArgEvent>.Unregister(_noArgEvent);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                EventBus<ArgEvent>.Publish(new ArgEvent
                {
                    IsEventWorking = true,
                    EventName = "Evento com Argumento"
                });
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                EventBus<NoArgEvent>.Publish(new NoArgEvent());
            }
        }

        public void TestArgEvent(ArgEvent argEvent) => Debug.Log($"Evento com argumentos executado.");
        public void TestNoArgEvent() => Debug.Log($"Evento sem argumentos executado.");
    }
}