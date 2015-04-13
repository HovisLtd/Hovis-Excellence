using System;
using System.ComponentModel.DataAnnotations;

namespace Hovis.Excellence.Web.Models
{
    public abstract class PersistedEntity
    {
        public PersistedEntity()
        {
            DateCreated = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }
    }
}