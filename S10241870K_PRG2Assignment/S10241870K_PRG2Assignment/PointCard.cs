using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10241870K_PRG2Assignment //syn
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
                //### 11th ice cream free logic
                PunchCard = 0;
            }
        }

        public override string ToString()
        {
            return $"Points: {Points} \t Punchcard: {PunchCard} \t Tier: {Tier}";
        }
    }
}
