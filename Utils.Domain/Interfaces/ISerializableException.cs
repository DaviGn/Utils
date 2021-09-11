namespace Utils.Domain.Interfaces
{
    public interface ISerializableException
    {
        object Serialize(string environment = null);
    }
}
