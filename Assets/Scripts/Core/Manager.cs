using UnityEngine;
using System.Collections;
using Assets.Scripts.Core;
using Assets.Scripts.Classes;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {
   public Canvas canvas;
   public static List<Card> allCards = new List<Card>();
   public static Manager instance;

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

   // Use this for initialization
   void Start () {
      DatabaseSetup.SetupDatabase();
      CardPrefab cardPrefab = ObjectFactory.GetCardPrefab(DatabaseAccessor.GetCard("Andrew McCutchen"));
      RectTransform canvasTransform = canvas.GetComponent<RectTransform>();
      cardPrefab.transform.SetParent(canvasTransform.transform, false);
      SceneManager.LoadScene("LoadingScene");
   }
   
   // Update is called once per frame
   void Update () {
   
   }
}
