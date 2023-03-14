﻿using InFatec.API.Model;
using Microsoft.EntityFrameworkCore;

namespace InFatec.API.Context
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options) {}


        public DbSet<ApiLogin> Login { get; set; }
        public DbSet<CodeModel> Code { get; set; }  
    }
}
