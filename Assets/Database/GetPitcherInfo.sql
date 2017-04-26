select 
Name, 
Team, 
Positions,
Control, 
IP, 
Case when PU = 0 Then '-' WHEN PU = 1 THEN PU Else '1-'|| PU END AS PU,
Case when SO = PU Then '-' WHEN (PU + 1) = SO THEN SO Else (PU + 1) || '-' || SO END AS SO,
Case when GB = SO THEN '-' WHEN (SO + 1) = GB THEN GB ELSE (SO + 1) || '-' || GB END AS GB,
Case when FB = GB THEN '-' WHEN (GB + 1) = FB THEN FB ELSE (GB + 1) || '-' || FB END AS FB,
Case when BB = FB THEN '-' WHEN (FB + 1) = BB THEN BB ELSE (FB + 1) || '-' || BB END AS BB,
Case when BI.'1B' = BB THEN '-' WHEN (BB + 1) = BI.'1B' THEN BI.'1B' ELSE (BB + 1) || '-' || BI.'1B' END AS '1B',
Case when BI.'2B' = BI.'1B' THEN '-' WHEN (BI.'1B' + 1) = BI.'2B' THEN BI.'2B' ELSE (BI.'1B' + 1) || '-' || BI.'2B' END AS '2B',
HR || '+' AS HR

from PitcherInfo BI
Order by Team, Name