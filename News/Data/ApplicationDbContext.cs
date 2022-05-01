﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using News.Configurations;
using News.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using News.Models;

namespace News.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //// Bỏ tiền tố AspNet của các bảng: mặc định các bảng trong IdentityDbContext có
            //// tên với tiền tố AspNet như: AspNetUserRoles, AspNetUser ...
            //// Đoạn mã sau chạy khi khởi tạo DbContext, tạo database sẽ loại bỏ tiền tố đó
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }



            builder.ApplyConfiguration(new SubmissionConfigurations());
            builder.ApplyConfiguration(new AppRoleConfigurations());
            builder.ApplyConfiguration(new AppUserConfigurations());
            builder.ApplyConfiguration(new CategoriesConfigurations());
            builder.ApplyConfiguration(new DepartmentConfigurations());
            builder.ApplyConfiguration(new IdeaConfigurations());
            builder.ApplyConfiguration(new UserInDepartmentConfigurations());
            builder.ApplyConfiguration(new LikeInIdeaConfigurations());
            builder.ApplyConfiguration(new CommentsConfigurations());
            builder.ApplyConfiguration(new ContactEmailConfigurations());

            builder.Seed();


        }

        public DbSet<Submission> Submission { set; get; }
        public DbSet<AppRole> AppRole { set; get; }
        public DbSet<AppUser> AppUser { set; get; }
        public DbSet<Categories> Categories { set; get; }
        public DbSet<Department> Department { set; get; }
        public DbSet<Idea> Idea { set; get; }
        public DbSet<UserInDepartment> UserInDepartment { set; get; }
        public DbSet<LikeInIdea> LikeInIdea { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<ContactEmail> ContactEmail { get; set; }
        public DbSet<News.Models.DetailIdeaModels> DetailIdeaModels { get; set; }
        public DbSet<News.Models.AssignToRoleModels> AssignToRoleModels { get; set; }
        public DbSet<News.Models.AssignToDepartment> AssignToDepartment { get; set; }
    }
}
