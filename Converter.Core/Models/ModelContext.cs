using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Converter.Core.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Conversionhistory> Conversionhistories { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Userlogin> Userlogins { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521) (CONNECT_DATA=(SID=xe))));User Id=C##Salad; Password=Test321;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("C##SALAD")
                .UseCollation("USING_NLS_COMP");

            modelBuilder.Entity<Conversionhistory>(entity =>
            {
                entity.HasKey(e => e.Conversionid)
                    .HasName("SYS_C008516");

                entity.ToTable("CONVERSIONHISTORY");

                entity.Property(e => e.Conversionid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CONVERSIONID");

                entity.Property(e => e.Filesize)
                    .HasColumnType("FLOAT")
                    .HasColumnName("FILESIZE");

                entity.Property(e => e.Outputfile)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("OUTPUTFILE");

                entity.Property(e => e.Sourcefile)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("SOURCEFILE");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USERID");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Rolename)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ROLENAME");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("USERID");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("FULLNAME");

                entity.Property(e => e.Phonenumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.Userloginid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USERLOGINID");

                entity.HasOne(d => d.Userlogin)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Userloginid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_U123");
            });

            modelBuilder.Entity<Userlogin>(entity =>
            {
                entity.ToTable("USERLOGIN");

                entity.HasIndex(e => e.Email, "SYS_C008522")
                    .IsUnique();

                entity.Property(e => e.Userloginid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("USERLOGINID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Password).HasColumnName("PASSWORD");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ROLEID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Userlogins)
                    .HasForeignKey(d => d.Roleid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ROLE1223");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
