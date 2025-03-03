using NoxStudios.Core.EventBus;
using NoxStudios.Core.EventBus.Events;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace NoxStudios.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public void OnMove(CallbackContext context) => HandleInput(context);
        public void OnJump(CallbackContext context) => HandleInput(context);
        public void OnAttack(CallbackContext context) => HandleInput(context);

        private static void HandleInput(CallbackContext context)
        {
            if (context.canceled)
            {
                PublishInputReleased();
                return;
            }

            EventBus<PlayerInputEvent>.Publish(new PlayerInputEvent(context));
        }

        private static void PublishInputReleased()
            => EventBus<PlayerInputEvent>.Publish(new PlayerInputEvent(true));
    }
}