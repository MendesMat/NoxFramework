using System;
using System.Collections.Generic;

namespace NoxStudios.Core.EventBus
{
    public static class PredefinedAssemblyUtil
    {
        private enum AssemblyType
        {
            AssemblyCSharp,
            AssemblyCSharpEditor,
            AssemblyCSharpEditorFirstPass,
            AssemblyCSharpFirstPass,
        }

        private static AssemblyType? GetAssemblyType(string assemblyName)
        {
            return assemblyName switch
            {
                "AssemblyCSharp" => AssemblyType.AssemblyCSharp,
                "AssemblyCSharpEditor" => AssemblyType.AssemblyCSharpEditor,
                "AssemblyCSharpEditorFirstPass" => AssemblyType.AssemblyCSharpEditorFirstPass,
                "AssemblyCSharpFirstPass" => AssemblyType.AssemblyCSharpFirstPass,
                _ => null
            };
        }

        public static List<Type> GetTypes(Type interfaceType)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Dictionary<AssemblyType, Type[]> assemblyTypes = new ();
            List<Type> types = new ();

            foreach (var assembly in assemblies)
            {
                var assemblyType = GetAssemblyType(assembly.GetName().Name);

                if (assemblyType != null) 
                    assemblyTypes.Add((AssemblyType)assemblyType, assembly.GetTypes());
            }
            
            AddTypesFromAssembly(assemblyTypes[AssemblyType.AssemblyCSharp], types, interfaceType);
            AddTypesFromAssembly(assemblyTypes[AssemblyType.AssemblyCSharpEditor], types, interfaceType);
            
            return types;
        }

        private static void AddTypesFromAssembly(Type[] assembly, ICollection<Type> types, Type interfaceType)
        {
            if(assembly == null) return;

            foreach (var type in assembly)
            {
                if(type != interfaceType && interfaceType.IsAssignableFrom(type)) 
                    types.Add(type);
            }
        }
    }
}