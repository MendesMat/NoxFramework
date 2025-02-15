namespace NoxStudios.Core.EventBus
{
    public interface IEvent { }
    
    public struct TestEvent : IEvent { }

    public struct PlayerEvent : IEvent
    {
        public int Health { get; set; }
        public int Mana { get; set; }
    }
}