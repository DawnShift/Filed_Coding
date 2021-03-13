using System;

namespace Filed_Coding.ShearedModel.Models
{
    public class DtoBase<T>
    {
        public T Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}