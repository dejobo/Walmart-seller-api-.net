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
        public DbSet<WMSystemShipment> systemShipmentSet { get; set; }
        public DbSet<WMSystemInventory> systemInventorySet { get; set; }
        public DbSet<WMSystemCancellation> systemCancellationSet { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WMSystemShipment>()
                .ToTable("vw_WMShipments")
                .MapToStoredProcedures();

            modelBuilder.Entity<WMSystemInventory>()
                .ToTable("vw_WMSystemInventory");
            base.OnModelCreating(modelBuilder);
        }
    }
}
