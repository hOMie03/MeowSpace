using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeowSpace.Domain.Entities
{
    public class PostComment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentID { get; set; }

        [Required, NotNull]
        public string UserID { get; set; }

        [Required, NotNull]
        public string Comment { get; set; }

        public ICollection<PostCommentLikes> CommentLikes { get; set; }
    }
}
