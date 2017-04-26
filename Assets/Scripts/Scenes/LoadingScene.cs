using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.Core;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Assets.Scripts.Classes;

public class LoadingScene : MonoBehaviour
{
   private Text loadingText;
   private ProgressBar progressBar;
   private int updateCount = 0;
   private int dots = 0;

   // Use this for initialization
   void Start()
   {
      progressBar = GameObject.Find("ProgressBar").GetComponent<ProgressBar>();
      loadingText = GameObject.Find("LoadingText").GetComponent<Text>();
      StartCoroutine("LoadAllCards");
   }

   // Update is called once per frame
   void Update()
   {
      updateCount++;
      if (updateCount == 50)
      {
         updateCount = 0;
         dots++;
         if (dots > 3) { dots = 0; }
         string t = "Loading";
         for (int i = 0; i < dots; i++)
         {
            t += ".";
         }
         loadingText.text = t;
      }
   }

   public void Initialize()
   {

   }

   private void LoadAllCards()
   {
      Manager.allCards = DatabaseAccessor.GetAllCards(progressBar);
      SceneManager.LoadScene("CardViewerScene");
   }
}
