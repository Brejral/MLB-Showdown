using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Core
{
   public enum EBats
   {
      S,
      L,
      R
   }

   public enum EThrows
   {
      LHP,
      RHP
   }

   public enum EPosition
   {
      [Description("Starter")]
      Starter = 1,
      [Description("C")]
      Catcher = 2,
      [Description("1B")]
      FirstBase = 3,
      [Description("2B")]
      SecondBase = 4,
      [Description("3B")]
      ThirdBase = 5,
      [Description("SS")]
      ShortStop = 6,
      [Description("LF")]
      LeftField = 7,
      [Description("CF")]
      CenterField = 8,
      [Description("RF")]
      RightField = 9,
      [Description("-")]
      DesignatedHitter = 10,
      [Description("Reliever")]
      Reliever = 11,
      [Description("Closer")]
      Closer = 12
   }

   public enum ESpeedClass
   {
      A,
      B,
      C
   }
}
