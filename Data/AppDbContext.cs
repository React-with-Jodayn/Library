using Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserBook> UserBooks { get; set; } // M-M
                                                       //    public DbSet<User> Users { get; set; }  ما منعمل هيك لانه احنا عرفناه فوق
        public DbSet<UserProfile> UserProfiles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // تعريف المفتاح المركب لجدول UserBook
            modelBuilder.Entity<UserBook>()
                .HasKey(ub => new { ub.UserId, ub.BookId });

            modelBuilder.Entity<User>() // نبدأ بتحديد علاقات خاصة ب User Entity
                .HasOne(u => u.UserProfile) // لديه علاقة 1-1 مع ...
                .WithOne(p => p.User) // كل يوزربروفايل مرتبط ب يوزر واحد
                .HasForeignKey<UserProfile>(p => p.UserId); // يوزر بروفايل عنده فورنكي اسمه يوزر اي دي

            List<IdentityRole<int>> roles = new List<IdentityRole<int>>
            {//Create IdentityRole
                       new IdentityRole<int> {
                                Id = -1,
                            Name ="Admin",
                            NormalizedName ="ADMIN"
                        },
                        new IdentityRole<int>{
                                    Id = -2,
                            Name="User",
                            NormalizedName ="USER"
                        }
            };
            //Add IdentityRole
            modelBuilder.Entity<IdentityRole<int>>().HasData(roles);
        }
    }
}