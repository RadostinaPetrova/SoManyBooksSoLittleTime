namespace SoManyBooksSoLittleTime.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SoManyBooksSoLittleTime.Data.Common.Models;

    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int BookId { get; set; }

        public virtual Book Book { get; set; }

        [MaxLength(3)]
        public string Extension { get; set; }

        [MaxLength(150)]
        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
