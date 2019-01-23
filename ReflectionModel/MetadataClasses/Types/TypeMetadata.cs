using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Model.MetadataClasses.Types.Members;
using Model.MetadataDefinitions;
using Model.MetadataExtensions;
using ReflectionModel.MetadataExtensions;
using ReflectionModel.MetadataDefinitions;

namespace Model.MetadataClasses.Types
{
    public class TypeMetadata
    {
        #region fields
        public TypeTypesEnumMetadata TypeEnum { get; set; }

        public string TypeName { get; set; }
        public string NamespaceName { get; set; }
        public IEnumerable<TypeMetadata> GenericArguments { get; set; }
        public Tuple<AccessLevelEnumMetadata, SealedEnumMetadata, AbstractEnumMetadata> Modifiers { get; set; }
        public IEnumerable<AttributeMetadata> Attributes { get; set; }
        public string FullTypeName { get; set; }
        
        public TypeMetadata DeclaringType { get; set; }

        public TypeMetadata BaseType { get; set; }
        public IEnumerable<TypeMetadata> ImplementedInterfaces { get; set; }

        public IEnumerable<FieldMetadata> Fields { get; set; }
        public IEnumerable<MethodMetadata> Methods { get; set; }
        public IEnumerable<PropertyMetadata> Properties { get; set; }
        public IEnumerable<IndexerMetadata> Indexers { get; set; }
        public IEnumerable<EventMetadata> Events { get; set; }
        public IEnumerable<ConstructorMetadata> Constructors { get; set; }
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

        private Tuple<AccessLevelEnumMetadata, SealedEnumMetadata, AbstractEnumMetadata> EmitModifiers(Type type)
        {
            //set defaults
            AccessLevelEnumMetadata _access = AccessLevelEnumMetadata.Private;
            AbstractEnumMetadata _abstract = AbstractEnumMetadata.NotAbstract;
            SealedEnumMetadata _sealed = SealedEnumMetadata.NotSealed;

            // check if not default
            if (type.IsPublic || type.IsNestedPublic)
                _access = AccessLevelEnumMetadata.Public;
            else if (type.IsNestedPrivate)
                _access = AccessLevelEnumMetadata.Private;
            else if (type.IsNestedFamily)
                _access = AccessLevelEnumMetadata.Protected;
            else if (type.IsNotPublic || type.IsNestedAssembly)
                _access = AccessLevelEnumMetadata.Internal;
            else if (type.IsNestedFamORAssem || type.IsNestedFamORAssem)
                _access = AccessLevelEnumMetadata.ProtectedInternal;

            if (type.IsSealed)
                _sealed = SealedEnumMetadata.Sealed;

            if (type.IsAbstract)
                _abstract = AbstractEnumMetadata.Abstract;

            return new Tuple<AccessLevelEnumMetadata, SealedEnumMetadata, AbstractEnumMetadata>(_access, _sealed, _abstract);
        }
        #endregion

        public TypeMetadata(TypeModel model)
        {
            TypeEnum = EnumMapper.ConvertEnum<TypeTypesEnumMetadata, TypeTypesEnum>(model.TypeEnum);
            TypeName = model.TypeName;
            NamespaceName = model.NamespaceName;

            GenericArguments = model.GenericArguments == null ? null :
                model.GenericArguments.Select(EmitTypeMetadata);

            Modifiers = new Tuple<AccessLevelEnumMetadata, SealedEnumMetadata, AbstractEnumMetadata>(
                EnumMapper.ConvertEnum<AccessLevelEnumMetadata, AccessLevelEnum>(model.Modifiers.Item1),
                EnumMapper.ConvertEnum<SealedEnumMetadata, SealedEnum>(model.Modifiers.Item2),
                EnumMapper.ConvertEnum<AbstractEnumMetadata, AbstractEnum>(model.Modifiers.Item3));

            Attributes = model.Attributes.Select(AttributeMetadata.EmitUniqueType);
            FullTypeName = model.FullTypeName;

            DeclaringType = model.DeclaringType == null ? null : EmitTypeMetadata(model.DeclaringType);
            BaseType = model.BaseType == null ? null : EmitTypeMetadata(model.BaseType);

            ImplementedInterfaces =
                model.ImplementedInterfaces.Select(EmitTypeMetadata);
            Fields =
                model.Fields.Select(FieldMetadata.EmitUniqueType);
            Methods =
                model.Methods.Select(MethodMetadata.EmitUniqueType);
            Properties =
                model.Properties.Select(PropertyMetadata.EmitUniqueType);
            Indexers =
                model.Indexers.Select(IndexerMetadata.EmitUniqueType);
            Events =
                model.Events.Select(EventMetadata.EmitUniqueType);
            Constructors =
                model.Constructors.Select(ConstructorMetadata.EmitUniqueType);
            NestedTypes =
                model.NestedTypes.Select(EmitTypeMetadata);
        }

        public static TypeMetadata EmitTypeMetadata(TypeModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new TypeMetadata(propertyModel));
        }

        public static TypeModel EmitTypeModel(TypeMetadata type)
        {
            return UniqueEmitter.EmitType(type, propertyModel => propertyModel.ToModel());
        }

        public TypeModel ToModel()
        {
            TypeModel model = new TypeModel();
            model.TypeEnum = EnumMapper.ConvertEnum<TypeTypesEnum, TypeTypesEnumMetadata>(TypeEnum);
            model.TypeName = TypeName;
            model.NamespaceName = NamespaceName;
            model.GenericArguments = GenericArguments?.Select(EmitTypeModel);

            model.Modifiers = new Tuple<AccessLevelEnum, SealedEnum, AbstractEnum>(
                EnumMapper.ConvertEnum<AccessLevelEnum, AccessLevelEnumMetadata>(Modifiers.Item1),
                EnumMapper.ConvertEnum<SealedEnum, SealedEnumMetadata>(Modifiers.Item2),
                EnumMapper.ConvertEnum<AbstractEnum, AbstractEnumMetadata>(Modifiers.Item3));

            model.Attributes = Attributes?.Select(attributeMetadata => attributeMetadata.ToModel());
            model.FullTypeName = FullTypeName;
            model.DeclaringType = DeclaringType == null ? null : EmitTypeModel(DeclaringType);
            model.BaseType = DeclaringType == null ? null : EmitTypeModel(BaseType);
            model.ImplementedInterfaces = ImplementedInterfaces?.Select(EmitTypeModel);
            model.Fields = Fields?.Select(typeModel => typeModel.ToModel());
            model.Methods = Methods?.Select(typeModel => typeModel.ToModel());
            model.Properties = Properties?.Select(typeModel => typeModel.ToModel());
            model.Indexers = Indexers?.Select(typeModel => typeModel.ToModel());
            model.Events = Events?.Select(typeModel => typeModel.ToModel());
            model.Constructors = Constructors?.Select(typeModel => typeModel.ToModel());
            model.NestedTypes = NestedTypes?.Select(EmitTypeModel);

            return model;
        }

        protected bool Equals(TypeMetadata other)
        {
            return TypeEnum == other.TypeEnum && string.Equals(TypeName, other.TypeName) &&
                   string.Equals(NamespaceName, other.NamespaceName) &&
                   Equals(GenericArguments, other.GenericArguments) && Equals(Modifiers, other.Modifiers) &&
                   Equals(Attributes, other.Attributes) && string.Equals(FullTypeName, other.FullTypeName) &&
                   Equals(ImplementedInterfaces, other.ImplementedInterfaces) && Equals(Fields, other.Fields) &&
                   Equals(Methods, other.Methods) && Equals(Properties, other.Properties) &&
                   Equals(Indexers, other.Indexers) && Equals(Events, other.Events) &&
                   Equals(Constructors, other.Constructors) && Equals(NestedTypes, other.NestedTypes);
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
                hashCode = (hashCode * 397) ^ (GenericArguments != null ? GenericArguments.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Modifiers != null ? Modifiers.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Attributes != null ? Attributes.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (FullTypeName != null ? FullTypeName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ImplementedInterfaces != null ? ImplementedInterfaces.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Fields != null ? Fields.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Methods != null ? Methods.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Properties != null ? Properties.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Indexers != null ? Indexers.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Events != null ? Events.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Constructors != null ? Constructors.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (NestedTypes != null ? NestedTypes.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
