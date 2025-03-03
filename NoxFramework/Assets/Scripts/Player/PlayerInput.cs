using NoxStudios.Core.EventBus;
using NoxStudios.Core.EventBus.Events;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace NoxStudios.Player
{
    public class PlayerInput : MonoBehaviour
    {
        public void OnMove(CallbackContext context) => PublishInput(context);
        public void OnJump(CallbackContext context) => PublishInput(context);
        public void OnAttack(CallbackContext context) => PublishInput(context);

        private static void PublishInput(CallbackContext context)
        {
            EventBus<PlayerInputEvent>.Publish(new PlayerInputEvent(context));
            if (context.canceled) PublishInputReleased();
        }

        private static void PublishInputReleased()
            => EventBus<PlayerInputEvent>.Publish(new PlayerInputEvent(true));
    }
}