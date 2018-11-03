using Model.MetadataDefinitions;
using Model.MetadataExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Model.MetadataClasses.Types.Members
{
    internal class MethodMetadata
    {
        #region vars
        private string m_Name;
        private IEnumerable<TypeBasicInfo> m_GenericArguments;
        private Tuple<AccessLevelEnum, AbstractEnum, StaticEnum, VirtualEnum, OverrideEnum> m_Modifiers;
        private TypeBasicInfo m_ReturnType;
        private bool m_Extension;
        private IEnumerable<ParameterMetadata> m_Parameters;
        #endregion

        internal static IEnumerable<MethodMetadata> EmitMethods(IEnumerable<MethodBase> methods)
        {
            return from MethodBase _currentMethod in methods
                   where _currentMethod.GetVisible()
                   select new MethodMetadata(_currentMethod);
        }

        //constructor
        private MethodMetadata(MethodBase method)
        {
            m_Name = method.Name;
            m_GenericArguments = !method.IsGenericMethodDefinition ? null : TypeBasicInfo.EmitGenericArguments(method.GetGenericArguments());
            m_ReturnType = EmitReturnType(method);
            m_Parameters = EmitParameters(method.GetParameters());
            m_Modifiers = EmitModifiers(method);
            m_Extension = EmitExtension(method);
        }

        #region methods
        private static IEnumerable<ParameterMetadata> EmitParameters(IEnumerable<ParameterInfo> parms)
        {
            return from parm in parms
                   select new ParameterMetadata(parm.Name, TypeBasicInfo.EmitReference(parm.ParameterType));
        }

        private static TypeBasicInfo EmitReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;

            return TypeBasicInfo.EmitReference(methodInfo.ReturnType);
        }

        private static bool EmitExtension(MethodBase method)
        {
            return method.IsDefined(typeof(ExtensionAttribute), true);
        }

        private static Tuple<AccessLevelEnum, AbstractEnum, StaticEnum, VirtualEnum, OverrideEnum> EmitModifiers(MethodBase method)
        {
            AccessLevelEnum _access = AccessLevelEnum.Private;
            if (method.IsPublic)
                _access = AccessLevelEnum.Public;
            else if (method.IsFamily)
                _access = AccessLevelEnum.Protected;
            else if (method.IsFamilyAndAssembly)
                _access = AccessLevelEnum.ProtectedInternal;

            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            if (method.IsAbstract)
                _abstract = AbstractEnum.Abstract;

            StaticEnum _static = StaticEnum.NotStatic;
            if (method.IsStatic)
                _static = StaticEnum.Static;

            VirtualEnum _virtual = VirtualEnum.NotVirtual;
            if (method.IsVirtual)
                _virtual = VirtualEnum.Virtual;

            OverrideEnum _override = OverrideEnum.NotOverride;
            Type baseType = method.DeclaringType.BaseType;
            if (baseType != null && baseType.GetMethod(method.Name).DeclaringType != method.DeclaringType)
            {
                _override = OverrideEnum.Override;
            }

            return new Tuple<AccessLevelEnum, AbstractEnum, StaticEnum, VirtualEnum, OverrideEnum>(_access, _abstract, _static, _virtual, _override);
        }
        #endregion
    }
}