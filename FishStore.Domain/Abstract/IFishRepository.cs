using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishStore.Domain.Entities;

namespace FishStore.Domain.Abstract
{
    public interface IFishRepository
    {
        IEnumerable<Fish> Fishes { get; }
        void SaveFish(Fish fish);
        Fish DeleteFish(int fishId);
    }
}
