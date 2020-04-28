using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FishStore.Domain.Entities;

namespace FishStore.WebUI.Models
{
    public class FishesListViewModel
    {
        public IEnumerable<Fish> Fishes { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}