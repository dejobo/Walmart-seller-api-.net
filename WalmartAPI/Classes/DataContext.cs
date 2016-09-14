using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalmartAPI.Classes
{
    public class DataContext :DbContext
    {
        public DbSet<WMSystemOrder> systemOrderSet { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<SystemOrder>()
            //    .Property(t => t.orderNumber)
            //    .HasColumnAnnotation("Index", new IndexAnnotation(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute()))
            base.OnModelCreating(modelBuilder);
        }
    }
}
