namespace LoansComparer.CrossCutting.Utils
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EntityPropertyNameAttribute : Attribute
    {
        private readonly string _propertyName;

        public EntityPropertyNameAttribute(string propertyName)
        {
            _propertyName = propertyName;
        }

        public string PropertyName { get => _propertyName; }
    }
}
