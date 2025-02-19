// using System;
// using System.Collections.Generic;
// using UnityEditor;
// using UnityEngine;
//
// namespace NoxStudios.Core.EventBus
// {
//     /// <summary>
//     /// Utilitário para gerenciamento do Event Bus no runtime e no editor do Unity.
//     /// Responsável por registrar automaticamente todos os tipos de eventos e inicializar os Event Buses correspondentes.
//     /// </summary>
//     public static class EventBusUtil
//     {
//         /// <summary>
//         /// Lista somente leitura contendo todos os tipos de eventos registrados.
//         /// Esses eventos são encontrados automaticamente nas assemblies registradas.
//         /// </summary>
//         private static IReadOnlyList<Type> EventTypes { get; set; }
//
//         /// <summary>
//         /// Lista somente leitura contendo todos os tipos de Event Buses inicializados.
//         /// </summary>
//         private static IReadOnlyList<Type> EventBusTypes { get; set; }
//
//     #if UNITY_EDITOR
//         /// <summary>
//         /// Estado atual do Play Mode no editor do Unity.
//         /// </summary>
//         public static PlayModeStateChange PlayModeState { get; set; }
//
//         /// <summary>
//         /// Método chamado automaticamente na inicialização do editor.
//         /// Registra um callback para limpar os Event Buses ao sair do modo de edição.
//         /// </summary>
//         [InitializeOnLoadMethod]
//         public static void InitializeEditor()
//         {
//             EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
//             EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
//         }
//
//         /// <summary>
//         /// Callback acionado quando o estado do Play Mode é alterado.
//         /// Se o Unity estiver saindo do modo de edição, limpa todos os Event Buses.
//         /// </summary>
//         /// <param name="state">Novo estado do Play Mode.</param>
//         private static void OnPlayModeStateChanged(PlayModeStateChange state)
//         {
//             PlayModeState = state;
//             if (state == PlayModeStateChange.ExitingEditMode) ClearAllBuses();
//         }
//     #endif
//
//         /// <summary>
//         /// Método chamado automaticamente antes de carregar a primeira cena do jogo.
//         /// Responsável por registrar todos os eventos e inicializar seus respectivos Event Buses.
//         /// </summary>
//         [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
//         public static void Initialize()
//         {
//             EventTypes = PredefinedAssemblyUtil.GetTypes(typeof(IEvent));
//             EventBusTypes = InitializeAllBuses();
//         }
//
//         /// <summary>
//         /// Inicializa todas as instâncias de Event Buses para os eventos encontrados.
//         /// </summary>
//         /// <returns>Lista contendo os tipos de Event Buses inicializados.</returns>
//         private static List<Type> InitializeAllBuses()
//         {
//             var eventBusTypes = new List<Type>();
//             var typeDef = typeof(EventBus<>);
//             
//             foreach (var eventType in EventTypes)
//             {
//                 var busType = typeDef.MakeGenericType(eventType);
//                 eventBusTypes.Add(busType);
//                 Debug.Log($"Initialized EventBus<{eventType.Name}>");
//             }
//             
//             return eventBusTypes;
//         }
//         
//         /// <summary>
//         /// Limpa todas as instâncias de Event Buses.
//         /// Esse método é chamado quando o Unity sai do modo de edição para garantir que
//         /// os eventos não persistam entre execuções.
//         /// </summary>
//         private static void ClearAllBuses()
//         {
//             Debug.Log("Clearing All Buses");
//
//             foreach (var busType in EventBusTypes)
//             {
//                 var clearMethod = busType.GetMethod(
//                     "Clear",
//                     System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
//
//                 if (clearMethod != null)
//                     clearMethod.Invoke(null, null);
//                 
//                 if(clearMethod == null) Debug.Log("The method must be called 'Clear'.");
//             }
//         }
//     }
// }