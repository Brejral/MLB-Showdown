using UnityEngine;
using System.Collections;
using Assets.Scripts.Classes;
using Assets.Resources.Scenes.CardViewer;
using Assets.Scripts.Core;

public class PlayerList : MonoBehaviour {
   public CardPrefab cardPrefab;
   public Transform contentPanel;

   // Use this for initialization
   void Start () {
      AddItems();
   }
   
   // Update is called once per frame
   void Update () {
   
   }

   private void AddItems()
   {
      contentPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 60 * Manager.allCards.Count);
      for(int i = 0; i < Manager.allCards.Count; i++)
      {
         PlayerListItem item = ObjectFactory.GetPlayerListItem(Manager.allCards[i], cardPrefab);
         item.transform.SetParent(contentPanel);
         
         item.transform.localPosition = new Vector2(150, -30 + (-60 * i));
      }
   }
}
