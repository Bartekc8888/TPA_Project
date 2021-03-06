﻿
using System.Runtime.Serialization;

namespace Serialization.MetadataDefinitions
{
    [DataContract]
    public enum AccessLevelSerializationModelEnum
    {
        [EnumMember]
        Public,
        [EnumMember]
        Protected,
        [EnumMember]
        Internal,
        [EnumMember]
        ProtectedInternal,
        [EnumMember]
        Private
    }
}