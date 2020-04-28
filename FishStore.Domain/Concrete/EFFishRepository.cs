using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishStore.Domain.Entities;
using FishStore.Domain.Abstract;

namespace FishStore.Domain.Concrete
{
    public class EFFishRepository : IFishRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Fish> Fishes
        {
            get { return context.Fishes; }
        }
        public void SaveFish(Fish fish)
        {
            if (fish.FishId == 0)
                context.Fishes.Add(fish);
            else
            {
                Fish dbEntry = context.Fishes.Find(fish.FishId);
                if (dbEntry != null)
                {
                    dbEntry.Name = fish.Name;
                    dbEntry.Description = fish.Description;
                    dbEntry.Price = fish.Price;
                    dbEntry.Category = fish.Category;
                    //dbEntry.ImageData = fish.ImageData;
                    //dbEntry.ImageMimeType = fish.ImageMimeType;
                }
            }
            context.SaveChanges();
        }
        public Fish DeleteFish(int fishId)
        {
            Fish dbEntry = context.Fishes.Find(fishId);
            if (dbEntry != null)
            {
                context.Fishes.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
