using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NoxStudios.Core.EventBus
{
    public static class EventBusUtil
    {
        private static IReadOnlyList<Type> EventTypes { get; set; }
        private static IReadOnlyList<Type> EventBusTypes { get; set; }
        
    #if UNITY_EDITOR
        public static PlayModeStateChange PlayModeState { get; set; }

        [InitializeOnLoadMethod]
        public static void InitializeEditor()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            PlayModeState = state;
            if(state == PlayModeStateChange.ExitingEditMode) ClearAllBuses();
        }
    #endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            EventTypes = PredefinedAssemblyUtil.GetTypes(typeof(IEvent));
            EventBusTypes = InitializeAllBuses();
        }

        private static List<Type> InitializeAllBuses()
        {
            List<Type> eventBusTypes = new ();

            var typeDef = typeof(EventBus<>);
            
            foreach (var eventType in EventTypes)
            {
                var busType = typeDef.MakeGenericType(eventType);
                eventBusTypes.Add(busType);
                Debug.Log($"Initialized EventBus<{eventType.Name}>");
            }
            
            return eventBusTypes;
        }
        
        private static void ClearAllBuses()
        {
            Debug.Log("Clearing All Buses");

            for (int i = 0; i < EventBusTypes.Count; i++)
            {
                var busType = EventBusTypes[i];
                
                var clearMethod = busType.GetMethod(
                    "Clear",
                    System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
                
                if(clearMethod != null)
                    clearMethod.Invoke(null, null);
            }
        }
    }
}