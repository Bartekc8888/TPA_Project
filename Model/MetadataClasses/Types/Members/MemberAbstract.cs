namespace Model.MetadataClasses.Types.Members
{
    public abstract class MemberAbstract
    {
        public string Name { get; }
        public TypeBasicInfo TypeMetadata { get; }

        public MemberAbstract(string name, TypeBasicInfo typeInfo)
        {
            this.Name = name;
            this.TypeMetadata = typeInfo;
        }
    }
}
