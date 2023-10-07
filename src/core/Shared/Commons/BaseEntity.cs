

using System.ComponentModel.DataAnnotations;

namespace Shared.Commons
{
    public  class BaseEntity
    {

        [Key]
        public int Id { get; set; }
        public bool Status { get; set; }
    }
}
