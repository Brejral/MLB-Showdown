using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Core
{
   public static class Util
   {
      public static ESpeedClass GetSpeedClass(int speed)
      {
         return (speed >= 18 ? ESpeedClass.A : (speed >= 13 ? ESpeedClass.B : ESpeedClass.C));
      }

      public static string GetResultText(int? previous, int result, bool isHR)
      {
         return isHR ? result + "+" : (previous == null && result > 1 ? "1\u2013" + result : (previous == result || result == 0 ? "\u2013" : (previous + 1 == result || result == 1 ? result.ToString() : (previous + 1) + "\u2013" + result)));
      }

      public static string GetPlayerImageFileString(string playerName, bool isBatter)
      {
         playerName = playerName.Replace(".", string.Empty).Replace("'", string.Empty);
         string[] splitStr = playerName.Split(' ');
         string fileName = string.Empty;
         for (int i = 0; i < splitStr.Length; i++)
         {
            fileName += (i != 0 ? "_" : string.Empty) + splitStr[i].ToLower();
         }
         return (isBatter ? "Batters/" : "Pitchers/") + fileName;
      }

      public static string GetPositionForNum(string nums)
      {
         if (nums.Length == 4)
         {
            return "IF";
         }
         if (nums.Length == 3)
         {
            return "OF";
         }
         if (nums.Length == 2)
         {
            return (ParseEnum<EPosition>(nums[0].ToString())).GetDescription() + "/" + (ParseEnum<EPosition>(nums[1].ToString())).GetDescription();
         }
         if (nums.Length == 0 || nums[0] == 'D')
         {
            return "\u2013";
         }
         return (ParseEnum<EPosition>(nums[0].ToString())).GetDescription();
      }

      public static T ParseEnum<T>(string str)
      {
         return (T)Enum.Parse(typeof(T), str);
      }

      public static double GetIpNumeric(double ip)
      {
         return Math.Floor(ip) + (ip - Math.Floor(ip)) / 3;
      }

      public static double GetIpDisplay(double ip)
      {
         return Math.Floor(ip) + Math.Round((ip - Math.Floor(ip)) * 3) / 10;
      }

      public static Sprite GetTeamLogo(string team)
      {
         return UnityEngine.Resources.Load<Sprite>("TeamLogos/" + team);
      }

      
   }
}
