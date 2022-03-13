using RitechWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RitechWeb.Models.DataContext
{
    public class RitechWebDBContext:DbContext
    {
        public RitechWebDBContext():base("RitechWebDB")
        {

        }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Hakkimizda> Hakkimizda { get; set; }
        public DbSet<Hizmet> Hizmet { get; set; }
        public DbSet<Iletisim> Iletisim { get; set; }
        public DbSet<Kimlik> Kimlik { get; set; }
        public DbSet<Portfolyo> Portfolyo { get; set; }
        public DbSet<Slider> Slider  { get; set; }
    }
}