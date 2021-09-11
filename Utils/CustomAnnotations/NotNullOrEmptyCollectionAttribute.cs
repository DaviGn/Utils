using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Utils.CustomAnnotations
{
    public class NotNullOrEmptyCollectionAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var collection = value as ICollection;
            if (collection != null)
            {
                return collection.Count != 0;
            }
            var enumerable = value as IEnumerable;
            return enumerable != null && enumerable.GetEnumerator().MoveNext();
        }
    }
}
