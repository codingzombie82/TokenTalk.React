using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace TokenTalk.Models
{
    public class TalkAppDbContext : DbContext
    {
        //EntityFrameworkCore
        //Microsoft.EntityFrameworkCore.SqlServer
        //Microsoft.EntityFrameworkCore.Tools
        //Microsoft.EntityFrameworkCore.InMemory
        //SqlClient
        //ConfigurationManager

        public TalkAppDbContext()
        {
            
        }

        public TalkAppDbContext(DbContextOptions<TalkAppDbContext> options) : base(options) { 
        
        
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured) {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            //Talk 테이블의 Created 열은 자동을 GetDate() 제약 조건을 부여하기
            modelBuilder.Entity<Talk>().Property(m => m.Created).HasDefaultValueSql("GetDate()");
        }

        public DbSet<Talk> Talks { get; set; }
    }
}
