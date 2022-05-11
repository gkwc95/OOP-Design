using System;
using System.Collections.Generic;

namespace oop
{
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
            Console.WriteLine(totalPrice);
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
            if (discount.AmountToGetDiscount() != 0)
                price = (discount.DiscountPrice() * (amount / discount.AmountToGetDiscount())) + ((amount % discount.AmountToGetDiscount()) * price);
            return price;
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

    class Program
    {
        static void Main(string[] args)
        {
            IProduct[] cart = new IProduct[] { new A(), new B(), new C() };
            CheckoutSystem checkoutSystem = new CheckoutSystem();
            foreach (IProduct product in cart)
            {
                checkoutSystem.TotalPrice(product);
            }

        }
    }
}
