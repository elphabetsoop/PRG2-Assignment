﻿//==========================================================
// Student Number : S10257905F
// Student Name : Tan Syn Kit
// Partner Name : Tan Yi Jing Valery
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10241870K_PRG2Assignment //Syn Kit
{
    internal class Cup : IceCream
    {
        //ctor
        public Cup()
        {
            
        }
        public Cup(int scoops, List<Flavour> fL, List<Topping> tL):base("Cup", scoops, fL, tL)
        {
            
        }

        //meth
        public override double CalculatePrice()
        {
            double totalPrice = 0;
            double scoopPrice = 0;

            if (Scoops == 1)
            {
                scoopPrice = 4.0;
            }
            else if (Scoops == 2)
            {
                scoopPrice = 5.5;
            }
            else if (Scoops == 3)
            {
                scoopPrice = 6.5;
            }

            totalPrice += scoopPrice;

            
            foreach (Flavour f in Flavours) //iterate through flavours, if premium add $2
            {
                if (f.Premium == true)
                {
                    totalPrice += 2;
                }
            }

            int numOfToppings = Toppings.Count;
            totalPrice += (numOfToppings * 1); //toppings at $1 each

            return totalPrice;
        } //CalculatePrice()

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
