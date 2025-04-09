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
    public class PostLike
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostLikeID { get; set; }
        public int LikesCounter { get; set; } = 0;
    }
}
