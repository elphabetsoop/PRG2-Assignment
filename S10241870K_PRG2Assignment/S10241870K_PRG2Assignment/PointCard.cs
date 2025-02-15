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
    internal class PointCard
    {
        public int Points { get; set; }
        public int PunchCard { get; set; }
        public string Tier { get; set; }

        //ctor
        public PointCard()
        {
            
        }

        public PointCard(int points, int pc)
        {
            Points = points;
            PunchCard = pc;

            if (Points >= 100)
            {
                Tier = "Gold";
            }
            else if (Points >= 50)
            {
                Tier = "Silver";
            }
            else // Points < 50
            {
                Tier = "Ordinary";
            }
        }

        //meth
        public void AddPoints(int addPoints)
        {
            Points += addPoints;
        }
        public void RedeemPoints(int redeemPoints)
        {
            Points -= redeemPoints;
        }
        public void Punch() 
        { 
            if (PunchCard == 10)
            {
                PunchCard = 0; //reset back to 0; method is called to check if this is 11th ice cream
            }
        }

        public override string ToString()
        {
            return $"Points: {Points} \t Punchcard: {PunchCard} \t Tier: {Tier}";
        }
    }
}
