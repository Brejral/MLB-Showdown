using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Core
{
   public static class Extensions
   {
      public static string GetDescription<T>(this T e) where T : IConvertible
      {
         string description = null;

         if (e is Enum)
         {
            Type type = e.GetType();
            FieldInfo[] fields = type.GetFields();

            foreach (FieldInfo field in fields)
            {
               if (field.Name == e.ToString())
               {
                  var memInfo = type.GetMember(field.Name);
                  var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                  if (descriptionAttributes.Length > 0)
                  {
                     // we're only getting the first description we find
                     // others will be ignored
                     description = ((DescriptionAttribute)descriptionAttributes[0]).Description;
                  }

                  break;
               }
            }
         }

         return description;
      }

      public static Transform GetChild(this MonoBehaviour monoBahavior, string childName)
      {
         Transform transform = null;
         foreach (Transform trans in monoBahavior.transform)
         {
            if (trans.name == childName)
            {
               transform = trans;
               break;
            }
         }
         return transform;
      }

      public static Sprite RoundEdges(this Sprite sprite, float r, float cx, float cy)
      {
         Texture2D tex = sprite.texture; 
         Color[] c = tex.GetPixels(0, 0, tex.width, tex.height);
         Texture2D b = new Texture2D(tex.width, tex.height);
         for (int i = 0; i < (tex.width * tex.height); i++)
         {
            int y = Mathf.FloorToInt(((float)i) / ((float)tex.height));
            int x = Mathf.FloorToInt(((float)i - ((float)(y * tex.height))));
            if (r * r >= (x - cx) * (x - cx) + (y - cy) * (y - cy))
            {
               b.SetPixel(x, y, c[i]);
            }
            else
            {
               b.SetPixel(x, y, Color.clear);
            }
         }
         b.Apply();
         return Sprite.Create(b, new Rect(0, 0, b.width, b.height), new Vector2(0.5f, 0.5f));
      }
   }
}
