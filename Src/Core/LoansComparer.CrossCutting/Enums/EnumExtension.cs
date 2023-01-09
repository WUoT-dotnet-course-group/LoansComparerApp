using System.ComponentModel;

namespace LoansComparer.CrossCutting.Enums
{
    public static class EnumExtension
    {
        public static string GetEnumDescription<T>(this T value) where T : Enum
        {
            var enumType = value.GetType();
            var fieldName = Enum.GetName(enumType, value);

            if (string.IsNullOrEmpty(fieldName))
            {
                return string.Empty;
            }

            return enumType.GetField(fieldName)?.GetCustomAttributes(false).OfType<DescriptionAttribute>().SingleOrDefault()?.Description ?? string.Empty;
        }

        public static bool TryGetEnumValue<T>(string? description, out T enumValue) where T : Enum
        {
            enumValue = default!;
            
            if (description == null)
            {
                return false;
            }

            var enumField = typeof(T).GetFields().SingleOrDefault(x => Attribute.GetCustomAttributes(x).OfType<DescriptionAttribute>().SingleOrDefault()?.Description == description);
            var value = enumField?.GetValue(null);
            if (value is not null) 
            { 
                enumValue = (T)value;
            }
            
            return value is not null;
        }
    }
}
