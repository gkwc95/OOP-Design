using System;
using System.Collections.Generic;

namespace oop
{
    class Program
    {
        static void Main(string[] args)
        {
            IProduct[] cart = new IProduct[] { new A(), new B(), new C(), new C(), new B(), new B(), new A() }; //Items in the cart.
            CheckoutSystem checkoutSystem = new CheckoutSystem();
            foreach (IProduct product in cart)
            {
                checkoutSystem.TotalPrice(product);
            }
        }
    }
    
    public class CheckoutSystem
    {
        private int totalPrice;
        private Dictionary<IProduct, int> cart;

        public CheckoutSystem()
        {
            cart = new Dictionary<IProduct, int>();
        }

        public void TotalPrice(IProduct product)
        {
            if (!cart.ContainsKey(product))
                cart.Add(product, 1);
            else cart[product]++;

            totalPrice = 0;
            foreach (var item in cart)
                totalPrice += item.Key.getPrice(item.Value);
            Console.WriteLine($"Total Price: {totalPrice}"); //Printing out the price
        }
    }

    public interface IProduct
    {
        int getPrice(int amount);
    }

    public class Product : IProduct
    {
        private int price;
        private IDiscount discount;

        public Product(int price, IDiscount discount)
        {
            this.price = price;
            this.discount = discount;
        }
        
        public int getPrice(int amount)
        {
             int priceInTotal;
            if (discount.AmountToGetDiscount() != 0)
                priceInTotal = (discount.DiscountPrice() * (amount / discount.AmountToGetDiscount()))+ price * (amount % discount.AmountToGetDiscount());
            else 
                priceInTotal = amount *price;
            return priceInTotal;
        }
        
        public override bool Equals(Object obj)
        {
            return this.GetType().Equals(obj.GetType());
        }

        public override int GetHashCode()
        {
            String name = this.GetType().Name.ToString();
            int value = 0;
            foreach (char letter in name) value += letter;
            return price + value; 
        }
    }

    public class A : Product
    {
        public A() : base(5, new DiscountA()) { }
    }

    public class B : Product
    {
        public B() : base(6, new DiscountB()) { }
    }

    public class C : Product
    {
        public C() : base(7, new DiscountC()) { }
    }


    public interface IDiscount
    {
        int AmountToGetDiscount();
        int DiscountPrice();
    }

    public class Discount : IDiscount
    {
        private int amountToGetDiscount;
        private int discountPrice;
        public Discount(int amountToGetDiscount, int discountPrice)
        {
            this.amountToGetDiscount = amountToGetDiscount;
            this.discountPrice = discountPrice;
        }
        public int AmountToGetDiscount()
        {
            return amountToGetDiscount;
        }

        public int DiscountPrice()
        {
            return discountPrice;
        }
    }

    public class DiscountA : Discount
    {
        public DiscountA() : base(2, 8) { }
    }

    public class DiscountB : Discount
    {
        public DiscountB() : base(3, 12) { }
    }

    public class DiscountC : Discount
    {
        public DiscountC() : base(0, 0) { }
    }
}
