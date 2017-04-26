using Assets.Scripts.Classes;
using Assets.Scripts.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPrefab : MonoBehaviour
{
   private Card card = null;
   public bool isActive = false;

   public void Initialize(Card card)
   {
      SetCard(card);
      isActive = true;
   }

   // Use this for initialization
   void Start()
   {
   }

   // Update is called once per frame
   void Update()
   {

   }

   public void Deactivate()
   {
      transform.SetParent(null);
      isActive = false;
   }

   public void SetCard(Card card)
   {
      this.card = card;
      UpdateImage();
   }

   private void UpdateImage()
   {
      Vector3 scale = GetComponent<RectTransform>().localScale;
      GetComponent<RectTransform>().localScale = Vector3.one;
      this.GetChild("CardImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(Util.GetPlayerImageFileString(card.Name, card.IsBatter));
      this.GetChild("TeamLogo").GetComponent<Image>().sprite = Resources.Load<Sprite>("TeamLogos/" + card.Team);
      this.GetChild("OnBaseControlIcon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Cards/" + (card.IsBatter ? "OnBase" : "Control"));
      this.GetChild("OnBaseControlText").GetComponent<Text>().text = card.IsBatter ? card.OnBase.ToString() : "+" + card.Control.ToString();
      this.GetChild("OnBaseControlText").GetComponent<RectTransform>().localPosition = card.IsBatter ? new Vector2(-171, -172) : new Vector2(-173, -174.5f);
      this.GetChild("ResultLabels").GetComponent<Text>().text = card.IsBatter ?
         "SO       GB       FB       BB       1B       1B+       2B       3B       HR" :
         "PU        SO        GB        FB        BB        1B        2B        HR";
      this.GetChild("NameText").GetComponent<Text>().text = card.Name.ToUpper();
      this.GetChild("InfoText").GetComponent<Text>().text = card.InfoText;
      this.GetChild("PUResult").GetComponent<Text>().text = !card.IsBatter ? Util.GetResultText(null, card.ResultChart[0], false) : string.Empty;
      this.GetChild("SOResult").GetComponent<RectTransform>().localPosition = card.IsBatter ? new Vector2(-177, -221.5f) : new Vector2(-118.5f, -221.5f);
      this.GetChild("SOResult").GetComponent<Text>().text = Util.GetResultText(card.IsBatter ? (int?)null : card.ResultChart[0], card.ResultChart[card.IsBatter ? 0 : 1], false);
      this.GetChild("GBResult").GetComponent<RectTransform>().localPosition = card.IsBatter ? new Vector2(-133.5f, -221.5f) : new Vector2(-70.5f, -221.5f);
      this.GetChild("GBResult").GetComponent<Text>().text = Util.GetResultText(card.ResultChart[card.IsBatter ? 0 : 1], card.ResultChart[card.IsBatter ? 1 : 2], false);
      this.GetChild("FBResult").GetComponent<RectTransform>().localPosition = card.IsBatter ? new Vector2(-90.5f, -221.5f) : new Vector2(-24, -221.5f);
      this.GetChild("FBResult").GetComponent<Text>().text = Util.GetResultText(card.ResultChart[card.IsBatter ? 1 : 2], card.ResultChart[card.IsBatter ? 2 : 3], false);
      this.GetChild("BBResult").GetComponent<RectTransform>().localPosition = card.IsBatter ? new Vector2(-48, -221.5f) : new Vector2(23, -221.5f);
      this.GetChild("BBResult").GetComponent<Text>().text = Util.GetResultText(card.ResultChart[card.IsBatter ? 2 : 3], card.ResultChart[card.IsBatter ? 3 : 4], false);
      this.GetChild("1BResult").GetComponent<RectTransform>().localPosition = card.IsBatter ? new Vector2(-4, -221.5f) : new Vector2(71, -221.5f);
      this.GetChild("1BResult").GetComponent<Text>().text = Util.GetResultText(card.ResultChart[card.IsBatter ? 3 : 4], card.ResultChart[card.IsBatter ? 4 : 5], false);
      this.GetChild("1B+Result").GetComponent<Text>().text = card.IsBatter ? Util.GetResultText(card.ResultChart[4], card.ResultChart[5], false) : string.Empty;
      this.GetChild("2BResult").GetComponent<RectTransform>().localPosition = card.IsBatter ? new Vector2(89, -221.5f) : new Vector2(118, -221.5f);
      this.GetChild("2BResult").GetComponent<Text>().text = Util.GetResultText(card.ResultChart[5], card.ResultChart[6], false);
      this.GetChild("3BResult").GetComponent<Text>().text = card.IsBatter ? Util.GetResultText(card.ResultChart[6], card.ResultChart[7], false) : string.Empty;
      this.GetChild("HRResult").GetComponent<RectTransform>().localPosition = card.IsBatter ? new Vector2(176.5f, -221.5f) : new Vector2(166, -221.5f);
      this.GetChild("HRResult").GetComponent<Text>().text = Util.GetResultText(null, card.ResultChart[card.IsBatter ? 8 : 7], true);
      GetComponent<RectTransform>().localScale = scale;
   }
}
