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

--------------------------------------------------------------------------------------------------------------------------------
--- EXECUTE CLEANSING PROCEDURE
--------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------
--- PARAMETERS Settings --------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------
-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- 
-- --> @run_command 			-- --> 1 = Run SQL Command; 0 = Simulation
-- --> @run_verbose 			-- --> 1 = Print verbose details; 0 = Print only stage's execution status
-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- 
-- --> @cleanup_staging 	 	-- --> 1 = TRUNCATE All Staging Tables; 0 = cleansing skipped
-- --> @cleanup_regular			-- --> 1 = TRUNCATE All Regular Tables (by the predefined input list); 0 = cleansing skipped
-- --> @cleanup_regular_list	-- --> comma separted list of Regular Tables to truncate (if empty cleansing skipped)
-- --> @cleanup_index 			-- --> 1 = DROP all 'DAMS' indexes; 0 = cleansing skipped
-- --> @cleanup_idx_local	    -- --> 1 = DISABLE all 'Country Specific' indexes; 0 = cleansing skipped
-- --> @cleanup_idx_local_list	-- --> comma separted list of 'Country Specific' code to disable (if empty cleansing skipped)
-- --> @cleanup_dmf				-- --> 1 = TRUNCATE all 'DMF_*' tables (framework's working tables); 0 = cleansing skipped
--------------------------------------------------------------------------------------------------------------------------------
DECLARE @RC int
DECLARE @run_command int
DECLARE @run_verbose int
DECLARE @cleanup_staging int
DECLARE @cleanup_regular int
DECLARE @cleanup_regular_list nvarchar(max)
DECLARE @cleanup_index int
DECLARE @cleanup_idx_local int
DECLARE @cleanup_idx_local_list nvarchar(max)
DECLARE @cleanup_dmf int

EXECUTE @RC = [dbo].[SP_D365_CLEAN_MAINPROCEDURE]
	@run_command = 0
	,@run_verbose = 1
	,@cleanup_staging = 1
	,@cleanup_regular = 1
	--- !!! NOTE !!! --> add other tables at the end of this string comma separated !!! ----------------------------------------
	,@cleanup_regular_list = 'EVENTCUD,DOCUHISTORY,SYSEXCEPTIONTABLE,EVENTCUDLINES,DMFDEFINITIONGROUPEXECUTION,INTEGRATIONACTIVITYRUNTIMEEXECUTIONTABLE,INTEGRATIONACTIVITYMESSAGETABLE,TRANSACTIONLOG,JOURNALERROR,DMFSTAGINGLOGDETAILS,DMFDEFINITIONGROUPEXECUTIONPROGRESS,DMFSTAGINGLOG,DIMENSIONDATAINTEGRITYLOG,SYSOUTGOINGEMAILTABLE,DMFEXECUTION,SYSUSERLOG,DMFDEFINITIONGROUPEXECUTIONHISTORY,INVENTCLOSINGLOG,INTEGRATIONACTIVITYRUNTIMEMONITORINGTABLE,DMFDEFINITIONGROUPEXECUTIONBATCHLINK,PROCESSEXECUTIONSTATUSLOG,AIDCOUPONSDASHBOARD_CAP,DMFSTAGINGVALIDATIONLOG,DMFSTAGINGEXECUTIONERRORS,DMFSTAGINGHISTORYCLEANUPTABLE,DMFSTAGINGHISTORYBACKUPMAPPING,DMFSTAGINGERRORCODE,SYSOUTGOINGEMAILDATA,DMFDEFINITIONGROUPEXECUTIONTRACE'
	----------------------------------------------------------------------------------------------------------------------------
	,@cleanup_index = 1
	,@cleanup_idx_local = 1
	,@cleanup_idx_local_list = '_RU,_VE,_EE,_MX,_TA,_CU,_JP,_AK,_BR,_LE,_IX'
	,@cleanup_dmf = 0
GO
--------------------------------------------------------------------------------------------------------------------------------
--- EXECUTE CLEANSING PROCEDURE --- END !!!
--------------------------------------------------------------------------------------------------------------------------------