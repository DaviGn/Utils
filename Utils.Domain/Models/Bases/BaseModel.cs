using Utils.Domain.Interfaces;

namespace Utils.Domain.Models.Bases
{
    public abstract class BaseModel : IBaseModel
    {
        public virtual int Id { get; set; }

        public object GetKey() => Id;
        public object GetId() => Id;
    }
}