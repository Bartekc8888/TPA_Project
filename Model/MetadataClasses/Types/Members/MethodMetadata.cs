using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Model.MetadataDefinitions;

namespace Model.MetadataClasses.Types.Members
{
    public class MethodMetadata
    {
        #region vars
        public string Name { get; set; }
        public IEnumerable<TypeMetadata> GenericArguments { get; set; }
        public Tuple<AccessLevelEnum, AbstractEnum, StaticEnum, VirtualEnum, OverrideEnum> Modifiers { get; set; }
        public TypeMetadata ReturnType { get; set; }
        public bool Extension { get; set; }
        public IEnumerable<ParameterMetadata> Parameters { get; set; }
        #endregion

        internal static IEnumerable<MethodMetadata> EmitMethods(IEnumerable<MethodBase> methods)
        {
            return from MethodBase currentMethod in methods where !(currentMethod.IsSpecialName)
                   select new MethodMetadata(currentMethod);
        }

        public MethodMetadata(MethodBase method)
        {
            Name = method.Name;
            GenericArguments = !method.IsGenericMethodDefinition ? null : TypeMetadata.EmitGenericArguments(method.GetGenericArguments());
            ReturnType = EmitReturnType(method);
            Parameters = EmitParameters(method.GetParameters());
            Modifiers = EmitModifiers(method);
            Extension = EmitExtension(method);
        }

        public MethodMetadata(string name, IEnumerable<TypeMetadata> genericArguments, Tuple<AccessLevelEnum, AbstractEnum, StaticEnum, VirtualEnum, OverrideEnum> modifiers, TypeMetadata returnType, bool extension, IEnumerable<ParameterMetadata> parameters)
        {
            Name = name;
            GenericArguments = genericArguments;
            Modifiers = modifiers;
            ReturnType = returnType;
            Extension = extension;
            Parameters = parameters;
        }
        
        public MethodMetadata(MethodMetadata model) : this(
            model.Name,
            model.GenericArguments,
            model.Modifiers,
            model.ReturnType,
            model.Extension,
            model.Parameters)
        {
        }

        public MethodMetadata() { }


        #region methods
        private static IEnumerable<ParameterMetadata> EmitParameters(IEnumerable<ParameterInfo> parms)
        {
            return from parm in parms
                   select new ParameterMetadata(parm.Name, TypeMetadata.EmitReference(parm.ParameterType));
        }

        private static TypeMetadata EmitReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;

            return TypeMetadata.EmitReference(methodInfo.ReturnType);
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

        protected bool Equals(MethodMetadata other)
        {
            return string.Equals(Name, other.Name) && Equals(GenericArguments, other.GenericArguments) &&
                   Equals(Modifiers, other.Modifiers) && Equals(ReturnType, other.ReturnType) &&
                   Equals(Parameters, other.Parameters);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MethodMetadata) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (GenericArguments != null ? GenericArguments.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Modifiers != null ? Modifiers.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ReturnType != null ? ReturnType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Parameters != null ? Parameters.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}