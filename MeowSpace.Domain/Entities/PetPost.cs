using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeowSpace.Domain.Entities
{
    public class PetPost
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostID { get; set; }

        [Required, NotNull, Url]
        public string PostImage { get; set; }

        [Required, NotNull]
        public string PostTitle { get; set; }

        [AllowNull]
        public string PostDescription { get; set; } = string.Empty;

        [Required, NotNull]
        public DateOnly PostCreationDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [AllowNull]
        public DateOnly PostUpdationDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [Required, NotNull]
        public string UserID { get; set; }

        public ICollection<PostComment> Comments { get; set; }
        public ICollection<PostLike> PostLikes { get; set; }
    }
}
