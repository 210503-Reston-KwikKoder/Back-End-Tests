using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTestsModels;


namespace UserTestsDL
{
    public class UserTestDBContext:DbContext
    {
        public UserTestDBContext(DbContextOptions options): base(options) { }
        protected UserTestDBContext() { }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
       public DbSet<UserStat> UserStats { get; set; }
        public DbSet<UserStatCatJoin> UserStatCatJoins { get; set; }
        public DbSet<TypeTest> TypeTests { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(user => user.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<User>()
                .HasIndex(user => user.Auth0Id)
                .IsUnique();
            modelBuilder.Entity<Category>()
                .Property(cat => cat.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<UserStat>()
                .Property(userStat => userStat.Id)
                .ValueGeneratedOnAdd();          
            modelBuilder.Entity<UserStatCatJoin>()
                .HasKey(uscj => new { uscj.UserStatId, uscj.UserId, uscj.CategoryId });          
            modelBuilder.Entity<TypeTest>()
                .Property(tT => tT.Id)
                .ValueGeneratedOnAdd();
           
        }
    }
}
