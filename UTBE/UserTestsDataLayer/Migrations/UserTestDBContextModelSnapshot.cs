// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserTestsDL;

namespace UserTestsDataLayer.Migrations
{
    [DbContext(typeof(UserTestDBContext))]
    partial class UserTestDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UserTestsModels.Category", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("UserTestsModels.Goal", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Checked")
                        .HasColumnType("bit");

                    b.Property<DateTime>("GoalDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("WPM")
                        .HasColumnType("float");

                    b.HasKey("CategoryId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("UserTestsModels.TypeTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumberOfErrors")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfWords")
                        .HasColumnType("int");

                    b.Property<int>("TimeTaken")
                        .HasColumnType("int");

                    b.Property<int>("UserStatId")
                        .HasColumnType("int");

                    b.Property<double>("WPM")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("UserStatId");

                    b.ToTable("TypeTests");
                });

            modelBuilder.Entity("UserTestsModels.User", b =>
                {
                    b.Property<string>("Auth0Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Revapoints")
                        .HasColumnType("int");

                    b.HasKey("Auth0Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UserTestsModels.UserStat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AverageAccuracy")
                        .HasColumnType("float");

                    b.Property<double>("AverageWPM")
                        .HasColumnType("float");

                    b.Property<int>("Losses")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfTests")
                        .HasColumnType("int");

                    b.Property<int>("TotalTestTime")
                        .HasColumnType("int");

                    b.Property<double>("WLRatio")
                        .HasColumnType("float");

                    b.Property<int>("WinStreak")
                        .HasColumnType("int");

                    b.Property<int>("Wins")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UserStats");
                });

            modelBuilder.Entity("UserTestsModels.UserStatCatJoin", b =>
                {
                    b.Property<int>("UserStatId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("UserStatId", "UserId", "CategoryId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserStatId")
                        .IsUnique();

                    b.ToTable("UserStatCatJoins");
                });

            modelBuilder.Entity("UserTestsModels.Goal", b =>
                {
                    b.HasOne("UserTestsModels.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserTestsModels.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UserTestsModels.TypeTest", b =>
                {
                    b.HasOne("UserTestsModels.UserStat", "UserStat")
                        .WithMany("TypeTests")
                        .HasForeignKey("UserStatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserStat");
                });

            modelBuilder.Entity("UserTestsModels.UserStatCatJoin", b =>
                {
                    b.HasOne("UserTestsModels.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserTestsModels.UserStat", "UserStat")
                        .WithOne("UserStatCatJoin")
                        .HasForeignKey("UserTestsModels.UserStatCatJoin", "UserStatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("UserStat");
                });

            modelBuilder.Entity("UserTestsModels.UserStat", b =>
                {
                    b.Navigation("TypeTests");

                    b.Navigation("UserStatCatJoin");
                });
#pragma warning restore 612, 618
        }
    }
}
