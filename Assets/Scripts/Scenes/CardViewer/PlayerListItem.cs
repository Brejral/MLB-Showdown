using Assets.Scripts.Classes;
using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Resources.Scenes.CardViewer
{
   public class PlayerListItem : MonoBehaviour
   {
      public Button Button;
      public Text Name;
      public Text Team;
      public Image TeamLogo;
      public CardPrefab Prefab;
      public Card Card;
      public bool isActive = false;

      void Start()
      {
         Button.onClick.AddListener(HandleClick);
      }

      void Update()
      {

      }

      public void OnPointerClick(PointerEventData eventData)
      {
         Button.onClick.Invoke();
      }

      public void Initialize(Card card, CardPrefab prefab)
      {
         Card = card;
         Prefab = prefab;
         Name.text = card.Name;
         Team.text = card.Team;
         TeamLogo.sprite = Util.GetTeamLogo(card.Team);
         isActive = true;
      }

      public void Deactivate()
      {
         transform.SetParent(null);
         isActive = false;
      }

      public void HandleClick()
      {
         Prefab.SetCard(Card);
      }
   }
}
