using Assets.Resources.Scenes.CardViewer;
using Assets.Scripts.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Core
{
   public class ObjectFactory : MonoBehaviour
   {
      protected static ObjectFactory instance;
      public GameObject cardPrefab;
      public static List<CardPrefab> cardPrefabs = new List<CardPrefab>();
      public GameObject progressBar;
      public static List<ProgressBar> progressBars = new List<ProgressBar>();
      public GameObject playerListItem;
      public static List<PlayerListItem> playerListItems = new List<PlayerListItem>();

      void Awake()
      {
         if (instance == null)
         {
            DontDestroyOnLoad(gameObject);
            instance = this;
         }
         else if (instance != this)
         {
            Destroy(gameObject);
         }
      }

      void Start()
      {

      }

      public static CardPrefab GetCardPrefab(Card card)
      {
         CardPrefab item = cardPrefabs.FirstOrDefault(cp => !cp.isActive);
         if (item == null)
         {
            item = CreateCardPrefab(card);
            cardPrefabs.Add(item);
         }
         else
         {
            item.Initialize(card);
         }
         return item;
      }

      private static CardPrefab CreateCardPrefab(Card card)
      {
         CardPrefab prefab = (Instantiate(instance.cardPrefab, Vector2.zero, Quaternion.identity) as GameObject).GetComponent<CardPrefab>();
         prefab.Initialize(card);
         return prefab;
      }

      private static ProgressBar CreateProgressBar(int count = 0)
      {
         ProgressBar prefab = (Instantiate(instance.progressBar, Vector2.zero, Quaternion.identity) as GameObject).GetComponent<ProgressBar>();
         prefab.Initialize(count);
         return prefab;
      }

      public static PlayerListItem GetPlayerListItem(Card card, CardPrefab cardPrefab)
      {
         PlayerListItem item = playerListItems.FirstOrDefault(pli => !pli.isActive);
         if (item == null)
         {
            item = CreatePlayerListItem(card, cardPrefab);
            playerListItems.Add(item);
         }
         else
         {
            item.Initialize(card, cardPrefab);
         }
         return item;
      }

      private static PlayerListItem CreatePlayerListItem(Card card, CardPrefab cardPrefab)
      {
         PlayerListItem prefab = (Instantiate(instance.playerListItem, Vector2.zero, Quaternion.identity) as GameObject).GetComponent<PlayerListItem>();
         prefab.Initialize(card, cardPrefab);
         return prefab;
      }
   }
}
