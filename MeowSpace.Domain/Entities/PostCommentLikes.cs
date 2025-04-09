using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeowSpace.Domain.Entities
{
    public class PostCommentLikes
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentLikeID { get; set; }
        public int CommentLikesCounter { get; set; } = 0;
    }
}
