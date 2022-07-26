﻿using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using RulesEngine.Models;

namespace SRNetReportingRulesEngine.Models
{
    public class RulesEngineContext : DbContext
    {
        public DbSet<Workflow> Workflows { get; set; }

        public DbSet<Rule> Rules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=RulesEngineDemo;Trusted_Connection=true;");
            // optionsBuilder.UseSqlServer(@"Server=localhost;Database=RulesEngineDemo;User Id=sa;Password=Rampanttheater20!;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ScopedParam>()
                        .HasKey(k => k.Name);

            modelBuilder.Entity<Workflow>(entity =>
            {
                entity.HasKey(k => k.WorkflowName);
                entity.Ignore(b => b.WorkflowsToInject);
            });

            var serializationOptions = new JsonSerializerOptions();

            modelBuilder.Entity<Rule>(entity =>
            {
                entity.HasKey(k => k.RuleName);

                entity.Property(b => b.Properties)
                      .HasConversion(v => JsonSerializer.Serialize(v, serializationOptions),
                           v => JsonSerializer.Deserialize<Dictionary<string, object>>(v, serializationOptions));

                entity.Property(p => p.Actions)
                      .HasConversion(v => JsonSerializer.Serialize(v, serializationOptions),
                           v => JsonSerializer.Deserialize<RuleActions>(v, serializationOptions));

                entity.Ignore(b => b.WorkflowsToInject);
                entity.Ignore(b => b.WorkflowRulesToInject);
            });
        }
    }
}
