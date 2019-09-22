﻿using DownTimeAlerter.Data.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DownTimeAlerter.Data.Domain.Context {
    public class DownTimeAlerterDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid> {
        public DownTimeAlerterDbContext(DbContextOptions<DownTimeAlerterDbContext> options)
            : base(options) { }

        public DbSet<Monitor> Monitors { get; set; }
        public DbSet<MonitorRequest> MonitorRequests { get; set; }
    }
}
