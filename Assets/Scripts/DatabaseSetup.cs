using UnityEngine;
using System.Collections;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System.Collections.Generic;
using System;
using Assets.Scripts.Core;

public static class DatabaseSetup
{

   public static void SetupDatabase()
   {
      DatabaseAccessor.dbConnection.Open();
      //SetupAllBatterStats();
      //SetupBatterInfo();

      //SetupAllPitcherStats();
      //SetupPitcherInfo();

      DatabaseAccessor.dbConnection.Close();
      Debug.Log("SetupDatabase() Complete");
   }

   public static void SetupAllBatterStats()
   {
      ClearBatterStats();
      Debug.Log("ClearBatterStats() Complete");
      SetupBatterStats();
      Debug.Log("SetupBatterStats() Complete");
      SetupBatterStats2();
      Debug.Log("SetupBatterStats2() Complete");
      SetupBatterFielding();
      Debug.Log("SetupBatterFielding() Complete");
      SetupBatterCatching();
      Debug.Log("SetupBatterCatching() Complete");
   }

   public static void SetupBatterInfo()
   {
      ClearBatterInfo();
      List<List<object>> list = GetBatterStats();
      foreach (List<object> row in list)
      {
         InsertBatterInfo(row);
      }
   }

   public static void SetupPitcherInfo()
   {
      ClearPitcherInfo();
      List<List<object>> list = GetPitcherStats();
      foreach (List<object> row in list)
      {
         InsertPitcherInfo(row);
      }
   }

   public static void SetupAllPitcherStats()
   {
      ClearPitcherStats();
      Debug.Log("ClearPitcherStats() Complete");
      SetupPitcherStats();
      Debug.Log("SetupPitcherStats() Complete");
      SetupPitcherStats2();
      Debug.Log("SetupPitcherStats2() Complete");

   }

   public static List<List<object>> GetBatterStats()
   {
      List<List<object>> list = new List<List<object>>();
      SqliteCommand cmd = DatabaseAccessor.dbConnection.CreateCommand();
      cmd.CommandText = "SELECT Name, Age, Team, OBP, AVG, PA, H, BS.'2B', BS.'3B', HR, SB, CS, (BB + IBB + HBP) as Walks, BS.'SO%', BS.'GO/AO', PosSummary, DRS, BS.'CS%', Bats, G FROM BatterStats BS WHERE Team <> '';";
      SqliteDataReader reader = cmd.ExecuteReader();
      while (reader.Read())
      {
         List<object> entryList = new List<object>();
         entryList.Add(reader.GetString(0)); //Name
         entryList.Add(reader.GetInt32(1)); //Age
         entryList.Add(reader.GetString(2)); //Team
         entryList.Add(reader.IsDBNull(3) ? (double?)null : reader.GetDouble(3)); //OBP
         entryList.Add(reader.IsDBNull(4) ? (double?)null : reader.GetDouble(4)); //AVG
         entryList.Add(reader.GetInt32(5)); //PA
         entryList.Add(reader.GetInt32(6)); //H
         entryList.Add(reader.GetInt32(7)); //2B
         entryList.Add(reader.GetInt32(8)); //3B
         entryList.Add(reader.GetInt32(9)); //HR
         entryList.Add(reader.GetInt32(10)); //SB
         entryList.Add(reader.GetInt32(11)); //CS
         entryList.Add(reader.GetInt32(12)); //Walks
         entryList.Add(reader.IsDBNull(13) ? (double?)null : reader.GetDouble(13)); //SO%
         entryList.Add(reader.IsDBNull(14) ? (double?)null : reader.GetDouble(14)); //GO/AO
         entryList.Add(reader.GetString(15)); //PosSummary
         entryList.Add(reader.IsDBNull(16) ? 0 : reader.GetInt32(16)); //DRS
         entryList.Add(reader.IsDBNull(17) ? 0.00 : reader.GetDouble(17)); //CS%
         entryList.Add(reader.GetString(18)); //Bats
         entryList.Add(reader.GetInt32(19)); //G
         list.Add(entryList);
      }
      return list;
   }

   public static List<List<object>> GetPitcherStats()
   {
      List<List<object>> list = new List<List<object>>();
      SqliteCommand cmd = DatabaseAccessor.dbConnection.CreateCommand();
      cmd.CommandText = "SELECT Name, Age, Team, Throws, W, L, ERA, G, GS, GF, SV, IP, H, R, ER, HR, BB, SO, BF, WHIP, PS.'HR9', PS.'SO%', PS.'BB%', PS.'X/H%', PS.'GO/AO', PS.'IF/FB' FROM PitcherStats PS WHERE Team <> '';";
      SqliteDataReader reader = cmd.ExecuteReader();
      while (reader.Read())
      {
         List<object> entryList = new List<object>();
         entryList.Add(reader.GetString(0)); //Name
         entryList.Add(reader.GetInt32(1)); //Age
         entryList.Add(reader.GetString(2)); //Team
         entryList.Add(reader.GetString(3)); //Throws
         entryList.Add(reader.GetInt32(4)); //W
         entryList.Add(reader.GetInt32(5)); //L
         entryList.Add(reader.IsDBNull(6) ? (double?)null : reader.GetDouble(6)); //ERA
         entryList.Add(reader.GetInt32(7)); //G
         entryList.Add(reader.GetInt32(8)); //GS
         entryList.Add(reader.GetInt32(9)); //GF
         entryList.Add(reader.GetInt32(10)); //SV
         entryList.Add(reader.IsDBNull(11) ? (double?)null : reader.GetDouble(11)); //IP
         entryList.Add(reader.GetInt32(12)); //H
         entryList.Add(reader.GetInt32(13)); //R
         entryList.Add(reader.GetInt32(14)); //ER
         entryList.Add(reader.GetInt32(15)); //HR
         entryList.Add(reader.GetInt32(16)); //BB
         entryList.Add(reader.GetInt32(17)); //SO
         entryList.Add(reader.GetInt32(18)); //BF
         entryList.Add(reader.IsDBNull(19) ? (double?)null : reader.GetDouble(19)); //WHIP
         entryList.Add(reader.IsDBNull(20) ? (double?)null : reader.GetDouble(20)); //HR9
         entryList.Add(reader.IsDBNull(21) ? (double?)null : reader.GetDouble(21)); //SO%
         entryList.Add(reader.IsDBNull(22) ? (double?)null : reader.GetDouble(22)); //BB%
         entryList.Add(reader.IsDBNull(23) ? (double?)null : reader.GetDouble(23)); //X/H%
         entryList.Add(reader.IsDBNull(24) ? (double?)null : reader.GetDouble(24)); //GO/AO
         entryList.Add(reader.IsDBNull(25) ? (double?)null : reader.GetDouble(25)); //IF/FB
         list.Add(entryList);
      }
      return list;
   }

   public static void ClearBatterStats()
   {
      ExecuteNonQuery("DELETE FROM BatterStats;");
   }

   public static void ClearBatterInfo()
   {
      ExecuteNonQuery("DELETE FROM BatterInfo;");
   }

   public static void ClearPitcherStats()
   {
      ExecuteNonQuery("DELETE FROM PitcherStats;");
   }

   public static void ClearPitcherInfo()
   {
      ExecuteNonQuery("DELETE FROM PitcherInfo;");
   }

   public static void SetupBatterStats()
   {
      List<string> csvList = GetBatterStatsCsv();
      csvList.RemoveAt(0);
      foreach (string csv in csvList)
      {
         InsertBatterStat(csv);
      }
   }

   public static void SetupBatterStats2()
   {
      List<string> csvList = GetBatterStats2Csv();
      csvList.RemoveAt(0);
      foreach (string csv in csvList)
      {
         UpdateBatterStat(csv);
      }
   }

   public static void SetupBatterFielding()
   {
      List<string> csvList = GetBatterFieldingCsv();
      csvList.RemoveAt(0);
      foreach (string csv in csvList)
      {
         UpdateBatterStatFielding(csv);
      }
   }

   public static void SetupBatterCatching()
   {
      List<string> csvList = GetBatterCatchingCsv();
      csvList.RemoveAt(0);
      foreach (string csv in csvList)
      {
         UpdateBatterStatCatching(csv);
      }
   }

   public static void SetupPitcherStats()
   {
      List<string> csvList = GetPitcherStatsCsv();
      csvList.RemoveAt(0);
      foreach (string csv in csvList)
      {
         InsertPitcherStat(csv);
      }
   }

   public static void SetupPitcherStats2()
   {
      List<string> csvList = GetPitcherStats2Csv();
      csvList.RemoveAt(0);
      foreach (string csv in csvList)
      {
         UpdatePitcherStat(csv);
      }
   }

   public static void InsertBatterInfo(List<object> row)
   {
      string commandText = String.Format("INSERT INTO BatterInfo ('Name','Age','Team','Bats','OnBase','Speed','Positions','Points','SO','GB','FB','BB','1B','1B+','2B','3B','HR') VALUES (\"{0}\",{1},'{2}',", row[0] as string, (int)row[1], row[2] as string);
      double obp = (double)row[3];
      double avg = (double)row[4];
      int pa = (int)row[5];
      int h = (int)row[6];
      int h2 = (int)row[7];
      int h3 = (int)row[8];
      int hr = (int)row[9];
      int sb = (int)row[10];
      int walks = (int)row[12];
      double sop = (double)row[13];
      double goao = (double)row[14];
      string posSummary = (string)row[15];
      int drs = (int)row[16];
      double csp = (double)row[17];
      string bats = (string)row[18];
      int g = (int)row[19];
      double sbpg = (double)sb / g;

      int onBase = obp >= .425 ? 14 : (obp >= .4 ? 13 : (obp >= .375 ? 12 : (obp >= .35 ? 11 : (obp >= .325 ? 10 : 9))));
      int speed = sbpg >= .4 ? 24 : (sbpg >= .35 ? 23 : (sbpg >= .3 ? 22 : (sbpg >= .25 ? 21 : (sbpg >= .2 ? 20 : (sbpg >= .15 ? 19 : (sbpg >= .1 ? 18 : 8 + (int)Math.Round(sbpg * 100)))))));
      int outs = (avg >= .325 ? 4 : (avg >= .275 ? 5 : (avg >= .225 ? 6 : 7)));
      List<int> results = new List<int>();
      results.Add((int)Math.Round(sop * outs)); // SO
      int outsLeft = outs - results[0];
      results.Add(results[0] + (int)Math.Round(goao / (goao + 1) * outsLeft)); //GB
      results.Add(outs); //FB
      double pahr = (double)pa / hr;
      int hrs = pahr <= 15 ? 17 : (pahr <= 25 ? 18 : (pahr <= 40 ? 19 : (pahr <= 60 ? 20 : (pahr <= 85 ? 21 : 22))));
      int onBaseCount = hrs - outs - 1;
      List<int> onBaseInts = new List<int>();
      int bbh = walks + h;
      onBaseInts.Add((int)Math.Round(onBaseCount * (double)walks / bbh)); //BB
      onBaseInts.Add((int)Math.Round(onBaseCount * (double)(h - h2 - h3 - hr) / bbh)); //1B
      onBaseInts.Add(0); //1B+
      onBaseInts.Add((int)Math.Round(onBaseCount * (double)h2 / bbh)); //2B
      onBaseInts.Add((int)Math.Round(onBaseCount * (double)h3 / bbh)); //3B
      int diff = onBaseCount - onBaseInts[0] - onBaseInts[1] - onBaseInts[3] - onBaseInts[4];
      if (speed > 15)
      {
         onBaseInts[2] = diff;
      }
      else
      {
         bool flag = false;
         while (diff > 0)
         {
            onBaseInts[flag ? 1 : 0]++;
            flag = !flag;
            diff--;
         }
      }
      results.Add(results[2] + onBaseInts[0]); //BB
      results.Add(results[3] + onBaseInts[1]); //1B
      results.Add(results[4] + onBaseInts[2]); //1B+
      results.Add(results[5] + onBaseInts[3]); //2B
      results.Add(results[6] + onBaseInts[4]); //3B
      results.Add(hrs); //HR

      string positionString = GetPositions(posSummary, drs, csp);

      //points
      int points = 20;
      points += (onBase - 10) * 6;
      points += (speed - 15) * 1;
      points += (6 - outs) * 6;
      points += bats == "S" ? 3 : 0;
      points += (20 - hrs) * 7;
      points += (10 - results[3]) * 1;
      points += (16 - results[4]) * 1;
      points += GetFieldingFactorForPoints(positionString) * 1;
      points *= 10;
      points = points <= 0 ? 10 : points;

      commandText += "'" + bats + "'," + onBase + "," + speed + ",'" + positionString + "'," + points;
      foreach (int result in results)
      {
         commandText += "," + result.ToString();
      }
      commandText += ")";
      ExecuteNonQuery(commandText);
   }

   public static void InsertBatterStat(string csv)
   {
      string[] csvSplit = csv.Split(',');
      if (csvSplit[csvSplit.Length - 1] == "1" || csvSplit[csvSplit.Length - 1] == string.Empty) { return; }
      string commandText = "INSERT INTO BatterStats (Name, Bats, Age, Team, League, G, PA, AB, R, H, '2B', '3B', HR, RBI, SB, CS, BB, SO, AVG, OBP, SLG, OPS, OPSP, TB, GDP, HBP, SH, SF, IBB, PosSummary)VALUES (";
      for (int i = 0; i < csvSplit.Length; i++)
      {
         string str = csvSplit[i];
         List<int> strIndices = new List<int> { 0, 2, 3, 28 };
         bool isStr = strIndices.Contains(i);
         str = (str == string.Empty ? null : str);
         commandText += (i == 0 ? string.Empty : ",") + (isStr ? "\"" : string.Empty) + str.Split('*')[0].Split('#')[0] + (isStr ? "\"" : string.Empty)
            + (i == 0 ? ",\"" + (str.Contains("#") ? "S" : str.Contains("*") ? "L" : "R") + "\"" : string.Empty);

      }
      commandText += ");";
      ExecuteNonQuery(commandText);
   }

   public static void InsertPitcherInfo(List<object> row)
   {
      //Name, Age, Team, Throws, W, L, ERA, G, GS, GF, SV, IP, H, R, ER, HR, BB, SO, BF, WHIP, PS.'HR9', PS.'SO%', PS.'BB%', PS.'X/H%', PS.'GO/AO', PS.'IF/FB'
      string commandText = String.Format("INSERT INTO PitcherInfo (Name, Age, Team, Control, IP, Throws, Positions, PU, SO, GB, FB, BB, '1B', '2B', HR, Points) VALUES (\"{0}\",{1},'{2}',", row[0] as string, (int)row[1], row[2] as string);
      string throws = (string)row[3];
      //int w = (int)row[4];
      //int l = (int)row[5];
      double era = (double)row[6];
      int g = (int)row[7];
      int gs = (int)row[8];
      int gf = (int)row[9];
      //int sv = (int)row[10];
      double ip = Util.GetIpNumeric((double)row[11]);
      //int bf = (int)row[18];
      double whip = (double)row[19];
      double hr9 = (double)row[20];
      double sop = (double)row[21];
      double bbp = (double)row[22];
      double xhp = (double)row[23];
      double goao = (double)row[24];
      double iffb = (double)row[25];
      string position = gs * 2 > g ? "Starter" : gf * 2 > g ? "Closer" : "Reliever";

      int control = (era <= 2.8 ? 6 : era <= 3.2 ? 5 : era <= 3.6 ? 4 : era <= 4 ? 3 : era <= 4.4 ? 2 : 1);
      int ipCalc = (int)Math.Round(position == "Starter" ? (ip - (g - gs)) / gs : ip / g);
      ipCalc = ipCalc < 1 ? 1 : ipCalc > 2 && position != "Starter" ? 2 : ipCalc;

      int outs = whip <= .9 ? 18 : whip <= 1.2 ? 17 : whip <= 1.4 ? 16 : whip <= 2 ? 15 : 14;
      int hr = hr9 <= .5 ? 28 : hr9 <= .7 ? 27 : hr9 <= .9 ? 26 : hr9 <= 1.1 ? 25 : hr9 <= 1.3 ? 24 : hr9 <= 1.5 ? 23 : hr9 <= 1.7 ? 22 : hr9 <= 2 ? 21 : 20;

      List<int> results = new List<int>();
      int soResults = (int)Math.Round(sop * outs);
      int outsLeft = outs - soResults;
      int gbResults = (int)Math.Round(goao / (goao + 1) * outsLeft);
      outsLeft -= gbResults;
      int fbResults = (int)Math.Round(outsLeft * (1 - 2 * iffb));
      outsLeft -= fbResults;
      results.Add(outsLeft);
      results.Add(results[0] + soResults);
      results.Add(results[1] + gbResults);
      results.Add(outs);
      int hits = hr - outs - 1;
      int bbs = (int)Math.Round(hits * 1.5 * bbp);
      int singles = (int)Math.Ceiling(((hr == 20 ? 19 : 20) - (results[3] + bbs) - 1) * (1 - xhp));
      hits -= bbs;
      results.Add(results[3] + bbs);
      results.Add(hr != 20 && xhp < .25 ? 20 : results[4] + singles);
      results.Add(hr - 1);
      results.Add(hr);

      int points = position == "Starter" ? 65 : 35;
      int factor = position == "Starter" ? 2 : 1;
      points += (control - 6) * 3 * factor;
      points += (position == "Starter" ? ipCalc - 7 : ipCalc - 2) * 4 * factor;
      points += (results[3] - 18) * 3 * factor;
      points += (results[5] - 19) * 2 * factor;
      points += (results[7] - 28) * 1 * factor;
      points = points * 10;
      points = points < 10 ? 10 : points;

      commandText += control + "," + ipCalc + ",'" + throws + "','" + position + "'";
      foreach (int result in results)
      {
         commandText += "," + result.ToString();
      }
      commandText += "," + points + ")";
      ExecuteNonQuery(commandText);
   }

   public static void InsertPitcherStat(string csv)
   {
      string[] csvSplit = csv.Split(',');
      if (double.Parse(csvSplit[10]) < 20.0) { return; }
      string commandText = "INSERT INTO PitcherStats (Name, Throws, Age, Team, W, L, ERA, G, GS, GF, SV, IP, H, R, ER, HR, BB, SO, BF, WHIP, 'HR9') VALUES (";
      for (int i = 0; i < csvSplit.Length; i++)
      {
         string str = csvSplit[i];
         List<int> strIndices = new List<int> { 0, 2 };
         bool isStr = strIndices.Contains(i);
         str = (string.IsNullOrEmpty(str) ? string.Empty : str);
         commandText += (i == 0 ? string.Empty : ",") + (isStr ? "\"" : string.Empty) + str.Replace("*", string.Empty) + (isStr ? "\"" : string.Empty)
            + (i == 0 ? ",\"" + (str.Contains("*") ? "LHP" : "RHP") + "\"" : string.Empty);

      }
      commandText += ");";
      ExecuteNonQuery(commandText);
   }

   public static void UpdateBatterStat(string csv)
   {
      string[] csvSplit = csv.Split(',');
      ExecuteNonQuery(String.Format("UPDATE BatterStats SET 'SO%' = {3}, 'GO/AO' = {4} WHERE Name = \"{0}\" AND Age = {1} AND Team = '{2}';", csvSplit[0], csvSplit[1], csvSplit[2], csvSplit[3], csvSplit[4] == string.Empty ? "NULL" : csvSplit[4]));
   }

   public static void UpdateBatterStatFielding(string csv)
   {
      string[] csvSplit = csv.Split(',');
      ExecuteNonQuery(String.Format("UPDATE BatterStats SET 'DRS' = {3} WHERE Name = \"{0}\" AND Age = {1} AND Team = '{2}';", csvSplit[0], csvSplit[1], csvSplit[2], csvSplit[3]));
   }

   public static void UpdateBatterStatCatching(string csv)
   {
      string[] csvSplit = csv.Split(',');
      ExecuteNonQuery(String.Format("UPDATE BatterStats SET 'CS%' = {3} WHERE Name = \"{0}\" AND Age = {1} AND Team = '{2}';", csvSplit[0], csvSplit[1], csvSplit[2], csvSplit[3] == string.Empty ? "0.00" : csvSplit[3]));
   }

   public static void UpdatePitcherStat(string csv)
   {
      string[] csvSplit = csv.Split(',');
      ExecuteNonQuery(String.Format("UPDATE PitcherStats SET 'SO%' = {3}, 'BB%' = {4}, 'X/H%' = {5}, 'GO/AO' = {6}, 'IF/FB' = {7} WHERE Name = \"{0}\" AND Age = {1} AND Team = '{2}';", csvSplit[0], csvSplit[1], csvSplit[2], csvSplit[3], csvSplit[4] == string.Empty ? "NULL" : csvSplit[4], csvSplit[5], csvSplit[6], csvSplit[7]));
   }

   public static List<string> GetBatterStatsCsv()
   {
      return GetCsvFile("Assets/Database/mlb-stats-2016-batters.csv");
   }

   public static List<string> GetBatterStats2Csv()
   {
      return GetCsvFile("Assets/Database/mlb-stats-2016-batters2.csv");
   }

   public static List<string> GetBatterFieldingCsv()
   {
      return GetCsvFile("Assets/Database/mlb-fielding-2016-batters.csv");
   }

   public static List<string> GetBatterCatchingCsv()
   {
      return GetCsvFile("Assets/Database/mlb-catching-2016-batters.csv");
   }

   public static List<string> GetPitcherStatsCsv()
   {
      return GetCsvFile("Assets/Database/mlb-stats-2016-pitchers.csv");
   }

   public static List<string> GetPitcherStats2Csv()
   {
      return GetCsvFile("Assets/Database/mlb-stats-2016-pitchers2.csv");
   }

   public static void ExecuteNonQuery(string commandText)
   {
      try
      {
         SqliteCommand command = DatabaseAccessor.dbConnection.CreateCommand();
         command.CommandText = commandText;
         command.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
         Debug.Log("Exception in ExecuteNonQuery() commandText: " + commandText + " error: " + ex.Message);
      }
   }

   public static List<string> GetCsvFile(string csvFile)
   {
      List<string> list = new List<string>();
      using (var fs = File.OpenRead(csvFile))
      using (var reader = new StreamReader(fs))
      {
         while (!reader.EndOfStream)
         {
            list.Add(reader.ReadLine());
         }
      }
      return list;
   }

   public static string GetPositions(string posSummary, int drs, double csp)
   {
      Dictionary<char, int> positionRatings = new Dictionary<char, int>();
      posSummary = posSummary.Replace("D", string.Empty);

      foreach (char c in posSummary)
      {
         positionRatings.Add(c, GetFieldingForPosition(c, drs, csp));
      }

      if (posSummary.Length == 1)
      {
         return GetPositionString(posSummary[0], positionRatings[posSummary[0]]);
      }
      if (posSummary.Length == 2)
      {
         if (posSummary.Contains("7") && posSummary.Contains("9"))
         {
            return "79-" + positionRatings[posSummary[0]];
         }
         if (posSummary.Contains("8") && (posSummary.Contains("7") || posSummary.Contains("9")))
         {
            if (positionRatings[posSummary.Contains("7") ? '7' : '9'] == positionRatings['8'])
            {
               return "789-" + positionRatings['8'];
            }
            string posStr8 = GetPositionString('8', positionRatings['8']);
            string posStr79 = GetPositionString(posSummary.Contains("7") ? '7' : '9', positionRatings[posSummary.Contains("7") ? '7' : '9']);
            return posSummary[0] == '8' ? posStr8 + "," + posStr79 : posStr79 + "," + posStr8;
         }
         if (positionRatings[posSummary[0]] == positionRatings[posSummary[1]] && !posSummary.Contains("7") && !posSummary.Contains("9"))
         {
            return posSummary + "-" + positionRatings[posSummary[0]];
         }
         return GetPositionString(posSummary[0], positionRatings[posSummary[0]]) + ","
               + GetPositionString(posSummary[1], positionRatings[posSummary[1]]);
      }
      if (posSummary.Length == 3)
      {
         return Length3PosSummary(posSummary, positionRatings);
      }
      if (posSummary.Length == 4)
      {
         return Length4PosSummary(posSummary, positionRatings);
      }
      if (posSummary.Length > 4)
      {
         if (posSummary.Contains("3") && posSummary.Contains("4") && posSummary.Contains("5") && posSummary.Contains("6"))
         {
            if (posSummary.Contains("8") && (posSummary.Contains("7") || posSummary.Contains("9")))
            {
               return new List<int> { 7, 8, 9 }.Contains(posSummary[0]) ? "789-" + positionRatings[posSummary.Contains("7") ? '7' : '9'] + ",3456-" + positionRatings['3'] :
                  "3456-" + positionRatings['3'] + ",789-" + positionRatings[posSummary.Contains("7") ? '7' : '9'];
            }
            if (posSummary.Contains("8"))
            {
               return posSummary[0] == '8' ? "8-" + positionRatings['8'] + ",3456-" + positionRatings['3'] :
                  "3456-" + positionRatings['3'] + ",8-" + positionRatings['8'];
            }
            if (posSummary.Contains("7") || posSummary.Contains("9"))
            {
               return new List<int> { 7, 9 }.Contains(posSummary[0]) ? "79-" + positionRatings[posSummary.Contains("7") ? '7' : '9'] + ",3456-" + positionRatings['3'] :
                  "3456-" + positionRatings['3'] + ",79-" + positionRatings[posSummary.Contains("7") ? '7' : '9'];
            }
         }
         return Length4PosSummary(posSummary.Substring(0, 4), positionRatings);
      }

      return string.Empty;
   }

   public static string Length3PosSummary(string posSummary, Dictionary<char, int> positionRatings)
   {
      if (posSummary.Contains("7") && posSummary.Contains("8") && posSummary.Contains("9"))
      {
         if (positionRatings['7'] == positionRatings['8'])
         {
            return "789-" + positionRatings['7'];
         }
         string posStr8 = GetPositionString('8', positionRatings['8']);
         string posStr79 = GetPositionString('7', positionRatings['7']);
         return posSummary[0] == '8' ? posStr8 + "," + posStr79 : posStr79 + "," + posStr8;
      }
      if (posSummary.Contains("7") && posSummary.Contains("9"))
      {
         int i = 0;
         char non79 = posSummary[i];
         while (non79 == '7' || non79 == '9')
         {
            non79 = posSummary[++i];
         }
         string posStr79 = GetPositionString('7', positionRatings['7']);
         string posStrNon = GetPositionString(non79, positionRatings[non79]);
         return posSummary[0] == non79 ? posStrNon + "," + posStr79 : posStr79 + "," + posStrNon;
      }
      if (posSummary.Contains("7") || posSummary.Contains("9"))
      {
         if (posSummary.Contains("8"))
         {
            char non789 = ' ';
            int iNon789 = -1;
            int ofRating = 3;
            List<char> charlist = new List<char>() { '7', '8', '9' };
            for (int i = 0; i < 3; i++)
            {
               if (!charlist.Contains(posSummary[i]))
               {
                  non789 = posSummary[i];
                  iNon789 = i;
               }
               else if (ofRating > positionRatings[posSummary[i]])
               {
                  ofRating = positionRatings[posSummary[i]];
               }
            }
            return iNon789 > 0 ? "789-" + ofRating + "," + non789 + "-" + positionRatings[non789] :
               non789 + "-" + positionRatings[non789] + "," + "789-" + ofRating;
         }
         else
         {
            int i79 = posSummary.Contains("7") ? posSummary.IndexOf('7') : posSummary.IndexOf('9');
            char non1 = ' ', non2 = ' ';
            int ratingNon = 5;
            for (int i = 0; i < 3; i++)
            {
               if (i != i79)
               {
                  if (non1 == ' ')
                  {
                     non1 = posSummary[i];
                  }
                  else
                  {
                     non2 = posSummary[i];
                  }
                  if (ratingNon > positionRatings[posSummary[i]]) { ratingNon = positionRatings[posSummary[i]]; }
               }
            }
            return i79 > 0 ? OrderString(non1, non2) + "-" + ratingNon + "," + GetPositionString('7', positionRatings[posSummary[i79]]) :
               GetPositionString('7', positionRatings[posSummary[i79]]) + "," + OrderString(non1, non2) + "-" + ratingNon;
         }
      }
      if (posSummary.Contains("4") && posSummary.Contains("6"))
      {
         char non46 = ' ';
         int iNon46 = -1;
         for (int i = 0; i < 3; i++)
         {
            if (posSummary[i] != '4' && posSummary[i] != '6')
            {
               non46 = posSummary[i];
               iNon46 = i;
               break;
            }
         }
         return iNon46 > 0 ? "46-" + positionRatings['4'] + "," + non46 + "-" + positionRatings[non46] :
            non46 + "-" + positionRatings[non46] + "," + "46-" + positionRatings['4'];
      }
      if (positionRatings[posSummary[0]] == positionRatings[posSummary[1]] || positionRatings[posSummary[2]] == positionRatings[posSummary[1]] || positionRatings[posSummary[0]] == positionRatings[posSummary[2]])
      {
         if (positionRatings[posSummary[0]] == positionRatings[posSummary[1]])
         {
            return OrderString(posSummary[0], posSummary[1]) + "-" + positionRatings[posSummary[0]] + "," + posSummary[2] + "-" + positionRatings[posSummary[2]];
         }
         if (positionRatings[posSummary[0]] == positionRatings[posSummary[2]])
         {
            return OrderString(posSummary[0], posSummary[2]) + "-" + positionRatings[posSummary[0]] + "," + posSummary[1] + "-" + positionRatings[posSummary[1]];
         }
         if (positionRatings[posSummary[1]] == positionRatings[posSummary[2]])
         {
            return OrderString(posSummary[1], posSummary[2]) + "-" + positionRatings[posSummary[1]] + "," + posSummary[0] + "-" + positionRatings[posSummary[0]];
         }
      }
      if (posSummary.Contains("3"))
      {
         int i3 = posSummary.IndexOf('3');
         char non1 = ' ', non2 = ' ';
         int ratingNon = 5;
         for (int i = 0; i < 3; i++)
         {
            if (i != i3)
            {
               if (non1 == ' ')
               {
                  non1 = posSummary[i];
               }
               else
               {
                  non2 = posSummary[i];
               }
               if (ratingNon > positionRatings[posSummary[i]]) { ratingNon = positionRatings[posSummary[i]]; }
            }
         }
         return i3 > 0 ? OrderString(non1, non2) + "-" + ratingNon + "," + GetPositionString('3', positionRatings[posSummary[i3]]) :
            GetPositionString('3', positionRatings[posSummary[i3]]) + "," + OrderString(non1, non2) + "-" + ratingNon;
      }
      return string.Empty;
   }

   public static string Length4PosSummary(string posSummary, Dictionary<char, int> positionRatings)
   {
      if (posSummary.Contains("3") && posSummary.Contains("4") && posSummary.Contains("5") && posSummary.Contains("6"))
      {
         if (positionRatings['3'] == positionRatings['4'] && positionRatings['4'] == positionRatings['5'])
         {
            return "3456-" + positionRatings['3'];
         }
         return posSummary[0] == '3' || posSummary[0] == '5' ? "35-" + positionRatings['3'] + ",46-" + positionRatings['4'] :
            "46-" + positionRatings['4'] + ",35-" + positionRatings['3'];
      }
      if (posSummary.Contains("7") && posSummary.Contains("8") && posSummary.Contains("9"))
      {
         char non789 = ' ';
         int iNon789 = -1;
         int ofRating = 3;
         List<char> charlist = new List<char>() { '7', '8', '9' };
         for (int i = 0; i < 4; i++)
         {
            if (!charlist.Contains(posSummary[i]))
            {
               non789 = posSummary[i];
               iNon789 = i;
            }
            else if (ofRating > positionRatings[posSummary[i]])
            {
               ofRating = positionRatings[posSummary[i]];
            }
         }
         return iNon789 > 0 ? "789-" + ofRating + "," + non789 + "-" + positionRatings[non789] :
            non789 + "-" + positionRatings[non789] + "," + "789-" + ofRating;
      }
      if (posSummary.Contains("8") && (posSummary.Contains("7") || posSummary.Contains("9")))
      {
         List<char> charlist = new List<char>() { '7', '8', '9' };
         char non1 = ' ', non2 = ' ';
         int ratingNon = 5;
         int ofRating = 3;
         for (int i = 0; i < 4; i++)
         {
            if (!charlist.Contains(posSummary[i]))
            {
               if (non1 == ' ')
               {
                  non1 = posSummary[i];
               }
               else
               {
                  non2 = posSummary[i];
               }
               if (ratingNon > positionRatings[posSummary[i]]) { ratingNon = positionRatings[posSummary[i]]; }
            }
            else if (ofRating > positionRatings[posSummary[i]])
            {
               ofRating = positionRatings[posSummary[i]];
            }
         }
         return !charlist.Contains(posSummary[0]) ? OrderString(non1, non2) + "-" + ratingNon + ",789-" + ofRating :
            "789- " + ofRating + "," + OrderString(non1, non2) + " -" + ratingNon;
      }
      if (posSummary.Contains("7") && posSummary.Contains("9"))
      {
         char non1 = ' ', non2 = ' ';
         int ratingNon = 5;
         for (int i = 0; i < 4; i++)
         {
            if (posSummary[i] != '7' && posSummary[i] != '9')
            {
               if (non1 == ' ')
               {
                  non1 = posSummary[i];
               }
               else
               {
                  non2 = posSummary[i];
               }
               if (ratingNon > positionRatings[posSummary[i]]) { ratingNon = positionRatings[posSummary[i]]; }
            }
         }
         return posSummary[0] != '7' && posSummary[0] != '9' ? OrderString(non1, non2) + "-" + ratingNon + "," + GetPositionString('7', positionRatings['7']) :
            GetPositionString('7', positionRatings['7']) + "," + OrderString(non1, non2) + "-" + ratingNon;
      }
      if (posSummary.Contains("7") || posSummary.Contains("9"))
      {
         return Length3PosSummary(posSummary.Substring(0, 3), positionRatings);
      }
      return string.Empty;
   }

   public static string OrderString(char c1, char c2)
   {
      int i1 = int.Parse(c1.ToString());
      int i2 = int.Parse(c2.ToString());
      if ((i1 == 5 && i2 == 6) || (i1 == 6 && i2 == 5)) { return "65"; }
      if (i1 > i2) { return c2.ToString() + c1.ToString(); }
      return c1.ToString() + c2.ToString();
   }

   public static string GetPositionString(char position, int rating)
   {
      return GetPositionString(position.ToString(), rating);
   }

   public static string GetPositionString(string positions, int rating)
   {
      return (positions.Contains("7") || positions.Contains("9") ? "79-" : positions + "-") + rating;
   }

   public static int GetFieldingForPosition(char position, int drs, double csp)
   {
      switch (position)
      {
         case '2':
            return csp / 0.05 > 10 ? 10 : (int)Math.Floor(csp / 0.05);
         case '3':
            return drs >= 5 ? 1 : 0;
         case '4':
         case '6':
            return drs >= 15 ? 5 : (drs >= 7 ? 4 : (drs >= 2 ? 3 : (drs >= 0 ? 2 : (drs >= -1 ? 1 : 0))));
         case '5':
            return drs >= 10 ? 3 : (drs >= 5 ? 2 : (drs >= 0 ? 1 : 0));
         case '7':
         case '9':
            return drs >= 7 ? 2 : (drs >= 0 ? 1 : 0);
         case '8':
            return drs >= 10 ? 3 : (drs >= 5 ? 2 : (drs >= 0 ? 1 : 0));
      }
      return 0;
   }

   public static int GetFieldingFactorForPoints(string positionString)
   {
      if (string.IsNullOrEmpty(positionString)) { return 0; }
      int factor = 0;
      string[] splitPositions = positionString.Split(',');
      factor += 2 * splitPositions.Length;
      foreach (string str in splitPositions)
      {
         string[] split = str.Split('-');
         int val = int.Parse(split[1]);
         foreach (char c in split[0])
         {
            switch (c)
            {
               case '2':
                  factor += val - 5;
                  break;
               case '3':
                  factor += val * 5;
                  break;
               case '4':
               case '6':
                  factor += val - 3;
                  break;
               case '5':
                  factor += val - 2;
                  break;
               case '7':
               case '9':
                  factor += val - 1;
                  break;
               case '8':
                  factor += val - 2;
                  break;
            }
         }
      }
      return factor;
   }

   public static SqliteConnection GetDatabaseConnection()
   {
      SqliteConnection connection = DatabaseAccessor.dbConnection;
      connection.Open();
      return connection;
   }
}