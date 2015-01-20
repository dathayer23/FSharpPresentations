using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    /// <summary>
    /// Requirements - You can only pay for a cart once.
    ///              - Once a cart is paid for, you cannot change the items in it.
    ///              - Empty carts cannot be paid for.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class NaiveShoppingCart<TItem>
    {
        private List<TItem> items;
        private decimal paidAmount;

        public NaiveShoppingCart()
        {
            this.items = new List<TItem>();
            this.paidAmount = 0;
        }

        /// Is cart paid for?
        public bool IsPaidFor { get { return this.paidAmount > 0; } }

        /// Readonly list of items
        public IEnumerable<TItem> Items { get { return this.items; } }

        /// add item only if not paid for
        public void AddItem(TItem item)
        {
            if (!this.IsPaidFor)
            {
                this.items.Add(item);
            }
        }

        /// remove item only if not paid for
        public void RemoveItem(TItem item)
        {
            if (!this.IsPaidFor)
            {
                this.items.Remove(item);
            }
        }

        /// pay for the cart
        public void Pay(decimal amount)
        {
            if (!this.IsPaidFor)
            {
                this.paidAmount = amount;
            }
        }
    }

}
