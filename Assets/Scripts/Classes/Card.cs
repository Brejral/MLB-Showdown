using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Classes
{
   public class Card
   {
      private List<int> resultChart = new List<int>();

      public string Name { get; set; }
      public string Team { get; set; }
      public int OnBase { get; set; }
      public int Control { get; set; }
      public int Speed { get; set; }
      public int IP { get; set; }
      public int Points { get; set; }
      public List<int> ResultChart { get { return resultChart; } }
      public ESpeedClass SpeedClass { get { return Util.GetSpeedClass(Speed); } }
      public EBats Bats { get; set; }
      public EThrows Throws { get; set; }
      public string Positions { get; set; }
      public bool IsBatter { get; set; }

      public string InfoText
      {
         get
         {
            return Points + " PT.   " + (IsBatter ? "SPEED " + SpeedClass.ToString() + " (" + Speed + ")   BATS " + Bats.ToString() + "   " + PositionText
            : PositionText + "   " + Throws.ToString() + "   IP " + IP);
         }
      }

      public string PositionText
      {
         get
         {
            if (!IsBatter) { return Util.ParseEnum<EPosition>(Positions).ToString().ToUpper(); }
            if (string.IsNullOrEmpty(Positions)) { return "\u2013"; }
            string str = string.Empty;
            foreach (string s in Positions.Split(','))
            {
               string[] split = s.Split('-');
               string position = Util.GetPositionForNum(split[0]);
               str += (string.IsNullOrEmpty(str) ? string.Empty : "   ") + position + "+" + split[1];
            }
            return str;
         }
      }

      public Card(List<object> dbValues)
      {
         PopulateCard(dbValues);
      }

      public void PopulateCard(List<object> dbValues)
      {
         IsBatter = (bool)dbValues[0];
         Name = (string)dbValues[1];
         Team = (string)dbValues[2];
         Positions = (string)dbValues[4];
         Points = (int)dbValues[7];
         if (IsBatter)
         {
            Bats = Util.ParseEnum<EBats>((string)dbValues[3]);
            OnBase = (int)dbValues[5];
            Speed = (int)dbValues[6];
            for (int i = 8; i < 17; i++)
            {
               ResultChart.Add((int)dbValues[i]);
            }
         }
         else
         {
            Throws = Util.ParseEnum<EThrows>((string)dbValues[3]);
            Control = (int)dbValues[5];
            IP = (int)dbValues[6];
            for (int i = 8; i < 16; i++)
            {
               ResultChart.Add((int)dbValues[i]);
            }
         }
      }
   }
}
