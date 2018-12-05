using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Model.MetadataDefinitions;

namespace Model.MetadataClasses.Types.Members
{
    [DataContract(IsReference = true)]
    public class MethodMetadata
    {
        #region vars
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public IEnumerable<TypeBasicInfo> GenericArguments { get; set; }
        [DataMember]
        public Tuple<AccessLevelEnum, AbstractEnum, StaticEnum, VirtualEnum, OverrideEnum> Modifiers { get; set; }
        [DataMember]
        public TypeBasicInfo ReturnType { get; set; }
        [DataMember]
        public bool Extension { get; set; }
        [DataMember]
        public IEnumerable<ParameterMetadata> Parameters { get; set; }
        #endregion

        internal static IEnumerable<MethodMetadata> EmitMethods(IEnumerable<MethodBase> methods)
        {
            return from MethodBase _currentMethod in methods where !(_currentMethod.IsSpecialName)
                   select new MethodMetadata(_currentMethod);
        }

        public MethodMetadata(MethodBase method)
        {
            Name = method.Name;
            GenericArguments = !method.IsGenericMethodDefinition ? null : TypeBasicInfo.EmitGenericArguments(method.GetGenericArguments());
            ReturnType = EmitReturnType(method);
            Parameters = EmitParameters(method.GetParameters());
            Modifiers = EmitModifiers(method);
            Extension = EmitExtension(method);
        }

        public MethodMetadata() { }


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

            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo != null && methodInfo.GetBaseDefinition().DeclaringType != methodInfo.DeclaringType)
            {
                _override = OverrideEnum.Override;
            }

            return new Tuple<AccessLevelEnum, AbstractEnum, StaticEnum, VirtualEnum, OverrideEnum>(_access, _abstract, _static, _virtual, _override);
        }
        #endregion
    }
}