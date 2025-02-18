namespace NoxStudios.Core.EventBus
{
    /// <summary>
    /// Interface base para todos os eventos do sistema de Event Bus.
    /// </summary>
    /// <remarks>
    /// Qualquer evento que deseja ser publicado no Event Bus ou nos Event Channels, deve implementar esta interface.
    /// </remarks>
    public interface IEvent { }

    public struct ArgEvent : IEvent
    {
        public bool IsEventWorking { get; set; }
        public string EventName { get; set; }

        public ArgEvent(bool isEventWorking, string eventName)
        {
            IsEventWorking = isEventWorking;
            EventName = eventName;
        }
    }
    
    public struct NoArgEvent : IEvent { }
}