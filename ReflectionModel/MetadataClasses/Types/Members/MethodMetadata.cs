using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Model.MetadataDefinitions;
using ReflectionModel.MetadataDefinitions;
using ReflectionModel.MetadataExtensions;

namespace Model.MetadataClasses.Types.Members
{
    public class MethodMetadata
    {
        #region vars
        public string Name { get; set; }
        public IEnumerable<TypeMetadata> GenericArguments { get; set; }
        public Tuple<AccessLevelEnumMetadata, AbstractEnumMetadata, StaticEnumMetadata, VirtualEnumMetadata, OverrideEnumMetadata> Modifiers { get; set; }
        public string ReturnType { get; set; }
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

        public MethodMetadata(string name, IEnumerable<TypeMetadata> genericArguments, Tuple<AccessLevelEnumMetadata, AbstractEnumMetadata, StaticEnumMetadata, VirtualEnumMetadata, OverrideEnumMetadata> modifiers, string returnType, bool extension, IEnumerable<ParameterMetadata> parameters)
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

        private static string EmitReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;

            return methodInfo.ReturnType.Name;
        }

        private static bool EmitExtension(MethodBase method)
        {
            return method.CustomAttributes
                         .Where(x => x.AttributeType == typeof(ExtensionAttribute))
                         .Count() == 1;
        }

        private static Tuple<AccessLevelEnumMetadata, AbstractEnumMetadata, StaticEnumMetadata, VirtualEnumMetadata, OverrideEnumMetadata> EmitModifiers(MethodBase method)
        {
            AccessLevelEnumMetadata _access = AccessLevelEnumMetadata.Private;
            if (method.IsPublic)
                _access = AccessLevelEnumMetadata.Public;
            else if (method.IsFamily)
                _access = AccessLevelEnumMetadata.Protected;
            else if (method.IsFamilyAndAssembly)
                _access = AccessLevelEnumMetadata.ProtectedInternal;

            AbstractEnumMetadata _abstract = AbstractEnumMetadata.NotAbstract;
            if (method.IsAbstract)
                _abstract = AbstractEnumMetadata.Abstract;

            StaticEnumMetadata _static = StaticEnumMetadata.NotStatic;
            if (method.IsStatic)
                _static = StaticEnumMetadata.Static;

            VirtualEnumMetadata _virtual = VirtualEnumMetadata.NotVirtual;
            if (method.IsVirtual)
                _virtual = VirtualEnumMetadata.Virtual;

            OverrideEnumMetadata _override = OverrideEnumMetadata.NotOverride;

            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo != null && methodInfo.GetBaseDefinition().DeclaringType != methodInfo.DeclaringType)
            {
                _override = OverrideEnumMetadata.Override;
            }

            return new Tuple<AccessLevelEnumMetadata, AbstractEnumMetadata, StaticEnumMetadata, VirtualEnumMetadata, OverrideEnumMetadata>(_access, _abstract, _static, _virtual, _override);
        }
        #endregion

        public MethodMetadata(MethodModel model)
        {
            Name = model.Name;
            GenericArguments = model.GenericArguments?.Select(TypeMetadata.EmitTypeMetadata);

            Modifiers = new Tuple<AccessLevelEnumMetadata, AbstractEnumMetadata, StaticEnumMetadata,
                VirtualEnumMetadata, OverrideEnumMetadata>(
                EnumMapper.ConvertEnum<AccessLevelEnumMetadata, AccessLevelEnum>(model.Modifiers.Item1),
                EnumMapper.ConvertEnum<AbstractEnumMetadata, AbstractEnum>(model.Modifiers.Item2),
                EnumMapper.ConvertEnum<StaticEnumMetadata, StaticEnum>(model.Modifiers.Item3),
                EnumMapper.ConvertEnum<VirtualEnumMetadata, VirtualEnum>(model.Modifiers.Item4),
                EnumMapper.ConvertEnum<OverrideEnumMetadata, OverrideEnum>(model.Modifiers.Item5)
                );

            ReturnType = model.ReturnType;
            Extension = model.Extension;
            Parameters =
                model.Parameters.Select(ParameterMetadata.EmitUniqueType);
        }

        public MethodModel ToModel()
        {
            MethodModel methodModel = new MethodModel
            {
                Name = Name,

                GenericArguments =
                GenericArguments?.Select(typeModel => typeModel.ToModel()),

                Modifiers = new Tuple<AccessLevelEnum, AbstractEnum, StaticEnum,
                VirtualEnum, OverrideEnum>(
                EnumMapper.ConvertEnum<AccessLevelEnum, AccessLevelEnumMetadata>(Modifiers.Item1),
                EnumMapper.ConvertEnum<AbstractEnum, AbstractEnumMetadata>(Modifiers.Item2),
                EnumMapper.ConvertEnum<StaticEnum, StaticEnumMetadata>(Modifiers.Item3),
                EnumMapper.ConvertEnum<VirtualEnum, VirtualEnumMetadata>(Modifiers.Item4),
                EnumMapper.ConvertEnum<OverrideEnum, OverrideEnumMetadata>(Modifiers.Item5)
            ),

                ReturnType = ReturnType,
                Extension = Extension,
                Parameters =
                Parameters?.Select(parameterModel => parameterModel.ToModel())
            };

            return methodModel;
        }

        public static MethodMetadata EmitUniqueType(MethodModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new MethodMetadata(propertyModel));
        }

        protected bool Equals(MethodMetadata other)
        {
            return string.Equals(Name, other.Name) && Equals(GenericArguments, other.GenericArguments) &&
                   Equals(Modifiers, other.Modifiers) && Equals(ReturnType, other.ReturnType) &&
                   Extension == other.Extension && Equals(Parameters, other.Parameters);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
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
                hashCode = (hashCode * 397) ^ Extension.GetHashCode();
                hashCode = (hashCode * 397) ^ (Parameters != null ? Parameters.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}