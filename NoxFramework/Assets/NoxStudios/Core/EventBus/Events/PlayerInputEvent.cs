using static UnityEngine.InputSystem.InputAction;

namespace NoxStudios.Core.EventBus.Events
{
    public struct PlayerInputEvent : IEvent
    {
        public CallbackContext Context { get; }
        public bool IsInputReleased { get; }

        public PlayerInputEvent(CallbackContext context)
        {
            Context = context;
            IsInputReleased = context.canceled;
        }

        public PlayerInputEvent(bool isInputReleased)
        {
            Context = default;
            IsInputReleased = isInputReleased;
        }
    }
}