using System.Linq;

namespace Utils.Extensions
{
    public static class PropertyExtensions
    {
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            return obj.GetType().GetProperties().Single(pi => pi.Name == propertyName).GetValue(obj, null);
        }
    }
}