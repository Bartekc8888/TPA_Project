﻿namespace Model.MetadataClasses.Types.Members
{
    public class ParameterMetadata
    {
        private string m_Name;
        private TypeBasicInfo m_TypeMetadata;

        public ParameterMetadata(string name, TypeBasicInfo typeMetadata)
        {
            this.m_Name = name;
            this.m_TypeMetadata = typeMetadata;
        }
    }
}