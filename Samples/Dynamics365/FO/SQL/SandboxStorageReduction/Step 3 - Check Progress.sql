/*
Disclaimer:
This Sample Code is provided for the purpose of illustration only and is not intended to be used in a production environment.
THIS SAMPLE CODE AND ANY RELATED INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
We grant You a nonexclusive, royalty-free right to use and modify the Sample Code and to reproduce and distribute the object code form of the Sample Code, provided that You agree: (i) to not use Our name, logo, or trademarks to market Your software product in which the Sample Code is embedded; (ii) to include a valid copyright notice on Your software product in which the Sample Code is embedded; and (iii) to indemnify, hold harmless, and defend Us and Our suppliers from and against any claims or lawsuits, including attorneys fees, that arise or result from the use or distribution of the Sample Code.
*/
/*
    "Version": "1.0",
    "Author": "Alessandro Colombini",
    "LastUpdate": "7/26/2024",
    "Description": "Clean Up Space in Sandbox",
*/

-------------------------------------------------------------------------------------------------------------------------------
--- CHECK Progress Cleansing Procedure
-------------------------------------------------------------------------------------------------------------------------------
SELECT * FROM [dbo].[D365_CLEAN_SPACEUSED_SUMMARY] AS A
ORDER BY A.TIMESTAMP_ ASC;

SELECT A.OBJECTTYPE, A.STAGE, count(A.TABLENAME) as OBJECT_COUNT, sum(A.CONSUMPTION_MB) as SAVING_MB
FROM [dbo].[D365_CLEAN_SPACEUSED_DETAIL] AS A
GROUP BY A.OBJECTTYPE, A.STAGE

----to use for detailed analysis by stage
--SELECT A.* FROM [dbo].[D365_CLEAN_SPACEUSED_DETAIL] AS A
--WHERE A.CONSUMPTION_MB > 0 
----AND A.STAGE = 'LOCAL'
--ORDER BY A.OBJECTTYPE, A.STAGE
-------------------------------------------------------------------------------------------------------------------------------
--- CHECK Progress END
--------------------------------------------------------------------------------------------------------------------------------