using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Fish Fish, int quantity)
        {
            CartLine line = lineCollection
                .Where(g => g.Fish.FishId == Fish.FishId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Fish = Fish,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Fish Fish)
        {
            lineCollection.RemoveAll(l => l.Fish.FishId == Fish.FishId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Fish.Price * e.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public Fish Fish { get; set; }
        public int Quantity { get; set; }
    }

}
