using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Model.MetadataClasses.Types.Members;
using Model.MetadataDefinitions;
using Model.MetadataExtensions;

namespace Model.MetadataClasses.Types
{
    [DataContract(IsReference = true)]
    public class TypeMetadata
    {
        #region fields
        [DataMember(EmitDefaultValue = false)]
        public TypeTypesEnum TypeEnum { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TypeName { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string NamespaceName { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<TypeMetadata> GenericArguments { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public Tuple<AccessLevelEnum, SealedEnum, AbstractEnum> Modifiers { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<AttributeMetadata> Attributes { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string FullTypeName { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public TypeMetadata DeclaringType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public TypeMetadata BaseType { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<TypeMetadata> ImplementedInterfaces { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<FieldMetadata> Fields { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<MethodMetadata> Methods { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<PropertyMetadata> Properties { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<IndexerMetadata> Indexers { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<EventMetadata> Events { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<ConstructorMetadata> Constructors { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<TypeMetadata> NestedTypes { get; set; }
        #endregion

        #region constructors
        public TypeMetadata(Type type)
        {
            TypeEnum = TypeEnumFactory.CreateTypeMetadataClass(type);

            FullTypeName = type.FullName;
            TypeName = type.Name;
            NamespaceName = type.Namespace;
            GenericArguments = !type.IsGenericTypeDefinition && !type.IsConstructedGenericType ? null : EmitGenericArguments(type.GetGenericArguments());
            Modifiers = EmitModifiers(type);
            Attributes = AttributeMetadata.EmitAttributes(type);
            
            DeclaringType = EmitDeclaringType(type.DeclaringType);

            BaseType = EmitExtends(type.BaseType);
            ImplementedInterfaces = EmitImplements(type.GetInterfaces());

            BindingFlags flagsToGetAll = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            Fields = FieldMetadata.EmitFields(type.GetFields(flagsToGetAll));
            Methods = MethodMetadata.EmitMethods(type.GetMethods(flagsToGetAll));
            Properties = PropertyMetadata.EmitProperties(type.GetProperties(flagsToGetAll));
            Indexers = IndexerMetadata.EmitIndexers(type.GetProperties(flagsToGetAll));
            Events = EventMetadata.EmitEvents(type.GetEvents(flagsToGetAll));
            Constructors = ConstructorMetadata.EmitConstructors(type.GetConstructors(flagsToGetAll));
            NestedTypes = EmitNestedTypes(type.GetNestedTypes(flagsToGetAll));
        }

        public TypeMetadata() { }
        #endregion

        #region methods
        private IEnumerable<TypeMetadata> EmitNestedTypes(IEnumerable<Type> nestedTypes)
        {
            return from _type in nestedTypes
                   where _type.GetVisible()
                   select new TypeMetadata(_type);
        }

        private TypeMetadata EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;

            return EmitReference(baseType);
        }

        private IEnumerable<TypeMetadata> EmitImplements(IEnumerable<Type> interfaces)
        {
            return from currentInterface in interfaces
                   select EmitReference(currentInterface);
        }

        public static TypeMetadata EmitReference(Type type)
        {
            long id = ReferenceMap.IdGenerator.GetId(type, out bool firstTime);
            if (firstTime)
            {
                TypeMetadata newTypeMetadata = new TypeMetadata(type);
                ReferenceMap.LoadedTypes.Add(id, newTypeMetadata);
                return newTypeMetadata;
            }
            else
            {
                ReferenceMap.LoadedTypes.TryGetValue(id, out TypeMetadata newTypeMetadata);
                return newTypeMetadata;
            }
        }

        public static IEnumerable<TypeMetadata> EmitGenericArguments(IEnumerable<Type> arguments)
        {
            return from Type _argument in arguments select EmitReference(_argument);
        }

        public static TypeMetadata EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;

            return EmitReference(declaringType);
        }

        private Tuple<AccessLevelEnum, SealedEnum, AbstractEnum> EmitModifiers(Type type)
        {
            //set defaults
            AccessLevelEnum _access = AccessLevelEnum.Private;
            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            SealedEnum _sealed = SealedEnum.NotSealed;

            // check if not default
            if (type.IsPublic || type.IsNestedPublic)
                _access = AccessLevelEnum.Public;
            else if (type.IsNestedPrivate)
                _access = AccessLevelEnum.Private;
            else if (type.IsNestedFamily)
                _access = AccessLevelEnum.Protected;
            else if (type.IsNotPublic || type.IsNestedAssembly)
                _access = AccessLevelEnum.Internal;
            else if (type.IsNestedFamORAssem || type.IsNestedFamORAssem)
                _access = AccessLevelEnum.ProtectedInternal;

            if (type.IsSealed)
                _sealed = SealedEnum.Sealed;

            if (type.IsAbstract)
                _abstract = AbstractEnum.Abstract;

            return new Tuple<AccessLevelEnum, SealedEnum, AbstractEnum>(_access, _sealed, _abstract);
        }
        #endregion

        protected bool Equals(TypeMetadata other)
        {
            return TypeEnum == other.TypeEnum && string.Equals(TypeName, other.TypeName) &&
                   string.Equals(NamespaceName, other.NamespaceName) && string.Equals(FullTypeName, other.FullTypeName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TypeMetadata) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) TypeEnum;
                hashCode = (hashCode * 397) ^ (TypeName != null ? TypeName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (NamespaceName != null ? NamespaceName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (FullTypeName != null ? FullTypeName.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
