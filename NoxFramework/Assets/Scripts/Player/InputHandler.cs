using NoxStudios.Core.EventBus;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InputHandler : MonoBehaviour
    {
        public void OnMoveInput(InputAction.CallbackContext context)
        {
            if(context.performed) EventBus<OnMoveEvent>.Publish(new OnMoveEvent());
        }
        
        public void OnJumpInput(InputAction.CallbackContext context)
        {
            if(context.performed) EventBus<OnJumpEvent>.Publish(new OnJumpEvent());
        }
        
        public void OnAttackInput(InputAction.CallbackContext context)
        {
            if(context.performed) EventBus<OnAttackEvent>.Publish(new OnAttackEvent());
        }
    }

    public struct OnMoveEvent : IEvent { }
    public struct OnJumpEvent : IEvent { }
    public struct OnAttackEvent : IEvent { }
}