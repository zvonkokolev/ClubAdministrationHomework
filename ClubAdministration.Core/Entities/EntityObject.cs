using GitStat.Core.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitStat.Core.Entities
{
    public class EntityObject : IEntityObject
    {
        [Key]
        public int Id { get; set; }

        [Timestamp]
        public byte[] RowVersion
        {
            get;
            set;
        }
    }
}
