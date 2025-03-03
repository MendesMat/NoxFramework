using static UnityEngine.InputSystem.InputAction;

namespace NoxStudios.Core.EventBus.Events
{
    public struct PlayerInputEvent : IEvent
    {
        public CallbackContext Context { get; }
        public bool IsReleased { get; }

        public PlayerInputEvent(CallbackContext context)
        {
            Context = context;
            IsReleased = context.canceled;
        }
    }
}