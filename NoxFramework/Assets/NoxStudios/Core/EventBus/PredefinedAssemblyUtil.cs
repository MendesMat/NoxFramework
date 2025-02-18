using System;
using System.Collections.Generic;
using UnityEngine;

namespace NoxStudios.Core.EventBus
{
    /// <summary>
    /// Utilitário para manipulação de assemblies predefinidos dentro do domínio da aplicação.
    /// Permite a recuperação de tipos que implementam uma interface específica.
    /// </summary>
    public static class PredefinedAssemblyUtil
    {
        /// <summary>
        /// Enumeração representando os assemblies predefinidos no Unity.
        /// </summary>
        private enum AssemblyType
        {
            AssemblyCSharpFirstPass,
            AssemblyCSharpEditorFirstPass,
            AssemblyCSharp,
            AssemblyCSharpEditor,
            NoxStudiosCoreEventBus
        }

        /// <summary>
        /// Obtém uma lista de tipos que implementam a interface fornecida, buscando nos assemblies predefinidos.
        /// </summary>
        /// <param name="interfaceType">O tipo da interface a ser buscada.</param>
        /// <returns>Uma lista de tipos que implementam a interface especificada.</returns>
        public static List<Type> GetTypes(Type interfaceType)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Dictionary<AssemblyType, Type[]> assemblyTypes = new();
            List<Type> types = new();

            // Itera sobre os assemblies carregados e armazena aqueles que pertencem aos tipos predefinidos.
            foreach (var assembly in assemblies)
            {
                var assemblyType = GetAssemblyType(assembly.GetName().Name);
                if (assemblyType == null) continue;
                
                assemblyTypes.Add((AssemblyType)assemblyType, assembly.GetTypes());
                Debug.Log($"Assembly Loaded: {assembly.GetName().Name}");
            }
            
            // Adiciona os tipos encontrados nos assemblies principais do Unity.
            AddTypesFromAssembly(assemblyTypes[AssemblyType.AssemblyCSharp], types, interfaceType);
            AddTypesFromAssembly(assemblyTypes[AssemblyType.AssemblyCSharpEditor], types, interfaceType);
            AddTypesFromAssembly(assemblyTypes[AssemblyType.NoxStudiosCoreEventBus], types, interfaceType);
            
            return types;
        }
        
        /// <summary>
        /// Obtém o tipo de assembly correspondente ao nome fornecido.
        /// </summary>
        /// <param name="assemblyName">Nome do assembly.</param>
        /// <returns>O tipo do assembly, se corresponder a um dos predefinidos; caso contrário, retorna null.</returns>
        private static AssemblyType? GetAssemblyType(string assemblyName)
        {
            return assemblyName switch
            {
                "Assembly-CSharp-firstpass" => AssemblyType.AssemblyCSharpFirstPass,
                "Assembly-CSharp-Editor-firstpass" => AssemblyType.AssemblyCSharpEditorFirstPass,
                "Assembly-CSharp" => AssemblyType.AssemblyCSharp,
                "Assembly-CSharp-Editor" => AssemblyType.AssemblyCSharpEditor,
                "NoxStudios.Core.EventBus" => AssemblyType.NoxStudiosCoreEventBus,
                _ => null
            };
        }
        
        /// <summary>
        /// Adiciona os tipos de um assembly específico à lista, caso implementem a interface desejada.
        /// </summary>
        /// <param name="assemblies">Array contendo os tipos do assembly.</param>
        /// <param name="types">Lista onde os tipos compatíveis serão adicionados.</param>
        /// <param name="interfaceType">Interface que os tipos devem implementar.</param>
        private static void AddTypesFromAssembly(Type[] assemblies, ICollection<Type> types, Type interfaceType)
        {
            if (assemblies == null) return;

            foreach (var assembly in assemblies)
            {
                // Verifica se o tipo implementa a interface desejada e adiciona à lista
                if (assembly != interfaceType && interfaceType.IsAssignableFrom(assembly))
                    types.Add(assembly);
            }
        }
    }
}