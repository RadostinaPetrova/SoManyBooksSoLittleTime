namespace SoManyBooksSoLittleTime.Data.Models
{
    using System;

    using SoManyBooksSoLittleTime.Data.Common.Models;

    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int BookId { get; set; }

        public virtual Book Book { get; set; }

        public string Extension { get; set; }

        // The content of the image is in the file system
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
