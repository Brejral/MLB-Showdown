using Assets.Scripts.Classes;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Core
{
   public static class DatabaseAccessor
   {
      public static SqliteConnection dbConnection = new SqliteConnection("URI=file:" + Application.dataPath + "/Database/mlb-showdown.db");

      public static Card GetCard(string playerName)
      {
         dbConnection.Open();
         List<object> dbValues = GetCardFromDatabase(playerName);
         Card card = new Card(dbValues);

         dbConnection.Close();
         return card;
      }

      public static List<Card> GetAllCards(ProgressBar progressBar = null)
      {
         dbConnection.Open();
         List<List<object>> dbList = GetAllCardsFromDatabase();
         if (progressBar != null) { progressBar.SetProgressByPercent(dbList.Count > 0 ? .1 : 1); }
         List<Card> cards = new List<Card>();
         for (int i = 0; i < dbList.Count; i++)
         {
            cards.Add(new Card(dbList[i]));
            if (progressBar != null) { progressBar.SetProgressByPercent(((double)i / dbList.Count) * .9 + .1); }
         }

         dbConnection.Close();
         return cards;
      }

      public static List<List<object>> GetAllCardsFromDatabase()
      {
         SqliteCommand command = dbConnection.CreateCommand();
         command.CommandText = @"
            SELECT 
               1 AS IsBatter, 
               BI.Name, 
               BI.Team, 
               BI.Bats,
               BI.Positions,
               BI.OnBase,
               BI.Speed,
               BI.Points,
               BI.SO,
               BI.GB,
               BI.FB,
               BI.BB,
               BI.'1B',
               BI.'1B+',
               BI.'2B',
               BI.'3B',
               BI.HR
               FROM BatterInfo BI
         UNION
            SELECT 
               0 AS IsBatter, 
               PI.Name, 
               PI.Team, 
               PI.Throws,
               PI.Positions,
               PI.Control,
               PI.IP,
               PI.Points,
               PI.PU,
               PI.SO,
               PI.GB,
               PI.FB,
               PI.BB,
               PI.'1B',
               PI.'2B',
               PI.HR,
               0
               FROM PitcherInfo PI
         ORDER BY Team, Name;";
         SqliteDataReader reader = command.ExecuteReader();
         return GetCardDataFromReader(reader);
      }

      public static List<object> GetCardFromDatabase(string playerName)
      {
         SqliteCommand command = dbConnection.CreateCommand();
         command.CommandText = String.Format(@"
            SELECT 
               1 AS IsBatter, 
               BI.Name, 
               BI.Team, 
               BI.Bats,
               BI.Positions,
               BI.OnBase,
               BI.Speed,
               BI.Points,
               BI.SO,
               BI.GB,
               BI.FB,
               BI.BB,
               BI.'1B',
               BI.'1B+',
               BI.'2B',
               BI.'3B',
               BI.HR
               FROM BatterInfo BI
               WHERE BI.Name = '{0}'
         UNION
            SELECT 
               0 AS IsBatter, 
               PI.Name, 
               PI.Team, 
               PI.Throws,
               PI.Positions,
               PI.Control,
               PI.IP,
               PI.Points,
               PI.PU,
               PI.SO,
               PI.GB,
               PI.FB,
               PI.BB,
               PI.'1B',
               PI.'2B',
               PI.HR,
               0
               FROM PitcherInfo PI
               WHERE PI.Name = '{0}';", playerName);
         SqliteDataReader reader = command.ExecuteReader();
         return GetCardDataFromReader(reader).First();
      }

      public static List<List<object>> GetCardDataFromReader(SqliteDataReader reader)
      {
         List<List<object>> list = new List<List<object>>();
         while (reader.Read())
         {
            List<object> values = new List<object>();
            bool isBatter = reader.GetBoolean(0);
            values.Add(isBatter);
            values.Add(reader.GetString(1));
            values.Add(reader.GetString(2));
            values.Add(reader.GetString(3));
            values.Add(reader.GetString(4));
            for (int i = 5; i < (isBatter ? 17 : 16); i++)
            {
               values.Add(reader.GetInt32(i));
            }
            list.Add(values);
         }

         return list;
      }
   }
}
