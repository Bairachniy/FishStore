using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishStore.Domain.Entities;
using System.Data.Entity;

namespace FishStore.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Fish> Fishes { get; set; }
    }
}
