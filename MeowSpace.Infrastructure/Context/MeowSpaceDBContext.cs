using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeowSpace.Domain.Entities;
using MeowSpace.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MeowSpace.Infrastructure.Context
{
    public class MeowSpaceDBContext : DbContext
    {
        public MeowSpaceDBContext(DbContextOptions<MeowSpaceDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PetPostConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<PetPost> PetPosts { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<PostCommentLikes> PostCommentLikes { get; set; }
    }
}
