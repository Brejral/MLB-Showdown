using UnityEngine;
using System.Collections;
using Assets.Scripts.Core;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
   private int count;
   private static readonly int width = 800;

   // Use this for initialization
   void Start()
   {
   }

   // Update is called once per frame
   void Update()
   {
      
   }

   public void Initialize(int count = 0)
   {
      this.count = count;
   }

   public void SetProgress(int value)
   {
      SetProgressByPercent(count > 0 ? (double)value / count : value);
   }

   public void SetProgressByPercent(double percent)
   {
      if (percent > 1) { percent = percent / 100; }
      this.GetChild("Progress").GetComponent<RectTransform>().sizeDelta = new Vector2((float)(width * percent), 20);
      this.GetChild("PercentText").GetComponent<Text>().text = percent.ToString("P1");
   }
}
