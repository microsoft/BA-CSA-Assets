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
--- D365_CLEAN_SPACEUSED_SUMMARY --- Summary DB size saving progress generated for each step
--------------------------------------------------------------------------------------------------------------------------------
DROP TABLE IF EXISTS [dbo].[D365_CLEAN_SPACEUSED_SUMMARY];
GO

CREATE TABLE [dbo].[D365_CLEAN_SPACEUSED_SUMMARY]
(
	DBNAME NVARCHAR(255),
	DBSIZE_MB NUMERIC(32,6),
	UNALLOCATED_MB NUMERIC(32,6),
	RESERVED_MB NUMERIC(32,6),
	DATA_MB NUMERIC(32,6),
	INDEX_MB NUMERIC(32,6),
	UNUSED_MB NUMERIC(32,6),
	STAGE NVARCHAR(10),
	TIMESTAMP_ DATETIME
	CONSTRAINT [SPACEUSED_SUMMARY_TABLENAMEIDX] PRIMARY KEY CLUSTERED
	(
		[DBNAME] ASC, [STAGE] ASC, [TIMESTAMP_] ASC
	)
); 
ALTER TABLE [dbo].[D365_CLEAN_SPACEUSED_SUMMARY] ADD  DEFAULT (GETDATE()) FOR [TIMESTAMP_];
GO
--------------------------------------------------------------------------------------------------------------------------------
--- D365_CLEAN_SPACEUSED_SUMMARY --- END !!!
--------------------------------------------------------------------------------------------------------------------------------

--------------------------------------------------------------------------------------------------------------------------------
--- D365_CLEAN_SPACEUSED_DETAIL --- Detailed DB size saving progress generated for each step
--------------------------------------------------------------------------------------------------------------------------------
DROP TABLE IF EXISTS [dbo].[D365_CLEAN_SPACEUSED_DETAIL];
GO

CREATE TABLE [dbo].[D365_CLEAN_SPACEUSED_DETAIL]
(
	OBJECTTYPE NVARCHAR(5),
	TABLENAME NVARCHAR(155),	
	TABLEROWCOUNT INT,
	INDEXNAME NVARCHAR(155),	
	CONSUMPTION_MB NUMERIC(32,6),
	STAGE NVARCHAR(10),
	TIMESTAMP_ DATETIME
	CONSTRAINT [SPACEUSED_DETAIL_TABLENAMEIDX] PRIMARY KEY CLUSTERED
	(
		[TABLENAME] ASC, [INDEXNAME] ASC, [STAGE] ASC, [TIMESTAMP_] ASC
	)
); 
ALTER TABLE [dbo].[D365_CLEAN_SPACEUSED_DETAIL] ADD  DEFAULT (0) FOR [TABLEROWCOUNT];
GO
ALTER TABLE [dbo].[D365_CLEAN_SPACEUSED_DETAIL] ADD  DEFAULT ('') FOR [INDEXNAME];
GO
ALTER TABLE [dbo].[D365_CLEAN_SPACEUSED_DETAIL] ADD  DEFAULT (0) FOR [CONSUMPTION_MB];
GO
ALTER TABLE [dbo].[D365_CLEAN_SPACEUSED_DETAIL] ADD  DEFAULT (GETDATE()) FOR [TIMESTAMP_];
GO
--------------------------------------------------------------------------------------------------------------------------------
--- D365_CLEAN_SPACEUSED_DETAIL --- END !!!
--------------------------------------------------------------------------------------------------------------------------------

--------------------------------------------------------------------------------------------------------------------------------
--- SP_D365_CLEAN_SPACEUSED --- Store Procedure to collect all DB size saving progress generated from the main procedure
--------------------------------------------------------------------------------------------------------------------------------
DROP PROCEDURE IF EXISTS [dbo].[SP_D365_CLEAN_SPACEUSED];
GO

DROP TABLE IF EXISTS [dbo].[D365_CLEAN_SPACEUSED];
GO

CREATE PROCEDURE [dbo].[SP_D365_CLEAN_SPACEUSED]
	(@stage NVARCHAR(10), 
	@objectType NVARCHAR(5) = '',
	@tablename NVARCHAR(155) = '',
	@indexname NVARCHAR(155) = '')
AS
BEGIN
	DROP TABLE IF EXISTS [dbo].[D365_CLEAN_SPACEUSED];

	IF(@objectType = '' AND @tablename = '' AND @indexname	= '')
	BEGIN
		CREATE TABLE [dbo].[D365_CLEAN_SPACEUSED]
		(
			DBNAME NVARCHAR(255),
			DBSIZE_MB NVARCHAR(50),
			UNALLOCATED_MB NVARCHAR(50),
			RESERVED_MB NVARCHAR(50),
			DATA_MB NVARCHAR(50),
			INDEX_MB NVARCHAR(50),
			UNUSED_MB NVARCHAR(50),
			CONSTRAINT [SPACEUSED_TABLENAMEIDX] PRIMARY KEY CLUSTERED
			(
				[DBNAME] ASC
			)
		); 

		INSERT INTO [dbo].[D365_CLEAN_SPACEUSED] EXECUTE sp_spaceused @oneresultset='true';

		INSERT INTO [D365_CLEAN_SPACEUSED_SUMMARY] (DBNAME, DBSIZE_MB, UNALLOCATED_MB, RESERVED_MB, DATA_MB, INDEX_MB, UNUSED_MB, STAGE)
		SELECT TS.DBNAME, 
		CAST(SUBSTRING(TS.DBSIZE_MB, 1, LEN(TS.DBSIZE_MB)-3) AS NUMERIC) AS DBSIZE_MB,
		CAST(SUBSTRING(TS.UNALLOCATED_MB, 1, LEN(TS.UNALLOCATED_MB)-3) AS NUMERIC) AS UNALLOCATED_MB,
		CAST(SUBSTRING(TS.RESERVED_MB, 1, LEN(TS.RESERVED_MB)-3) AS NUMERIC)/1024 AS RESERVED_MB, 
		CAST(SUBSTRING(TS.DATA_MB, 1, LEN(TS.DATA_MB)-3) AS NUMERIC)/1024 AS DATA_MB,
		CAST(SUBSTRING(TS.INDEX_MB, 1, LEN(TS.INDEX_MB)-3) AS NUMERIC)/1024 AS INDEX_MB,
		CAST(SUBSTRING(TS.UNUSED_MB, 1, LEN(TS.UNUSED_MB)-3) AS NUMERIC)/1024 AS UNUSED_MB,
		@stage as STAGE
		FROM [D365_CLEAN_SPACEUSED] as TS

		DROP TABLE IF EXISTS [dbo].[D365_CLEAN_SPACEUSED];
	END
	ELSE
	BEGIN
		IF(@objectType <> '' AND @tablename <> '' AND @indexname = '')
		BEGIN
			CREATE TABLE [dbo].[D365_CLEAN_SPACEUSED]
			(
				TABLENAME NVARCHAR(155),
				TABLEROWCOUNT int,
				RESERVED_MB NVARCHAR(50),
				DATA_MB NVARCHAR(50),
				INDEX_MB NVARCHAR(50),
				UNUSED_MB NVARCHAR(50),
				CONSTRAINT [SPACEUSED_TABLENAMEIDX] PRIMARY KEY CLUSTERED
				(
					[TABLENAME] ASC
				)
			); 

			INSERT INTO [dbo].[D365_CLEAN_SPACEUSED] EXECUTE sp_spaceused @tablename;

			INSERT INTO [D365_CLEAN_SPACEUSED_DETAIL] (OBJECTTYPE, TABLENAME, TABLEROWCOUNT, CONSUMPTION_MB, STAGE)
			SELECT @objectType AS OBJECTTYPE,
			TS.TABLENAME AS TABLENAME, 
			TS.TABLEROWCOUNT,
			CASE @objectType 
				WHEN 'TABLE' THEN CAST(SUBSTRING(TS.RESERVED_MB, 1, LEN(TS.RESERVED_MB)-3) AS NUMERIC)/1024  
				WHEN 'INDEX' THEN CAST(SUBSTRING(TS.INDEX_MB, 1, LEN(TS.INDEX_MB)-3) AS NUMERIC)/1024 
				ELSE 0
			END AS CONSUMPTION_MB,
			@stage AS STAGE
			FROM [D365_CLEAN_SPACEUSED] AS TS

			DROP TABLE IF EXISTS [dbo].[D365_CLEAN_SPACEUSED];
		END
		ELSE
		BEGIN
			INSERT INTO [D365_CLEAN_SPACEUSED_DETAIL] (OBJECTTYPE, TABLENAME, INDEXNAME, CONSUMPTION_MB, STAGE)
			SELECT @objectType AS OBJECTTYPE,
			@tablename AS TABLENAME,
			@indexname AS INDEXNAME,
			CAST(8 * SUM(ISNULL(C.used_pages,0)) / 1024.0 AS NUMERIC) AS CONSUMPTION_MB,
			@stage AS STAGE
			FROM SYS.indexes AS A 
			LEFT OUTER JOIN SYS.partitions AS B
			ON B.object_id = A.object_id
			AND B.index_id = A.index_id
			LEFT OUTER JOIN sys.allocation_units AS C
			ON C.container_id = B.partition_id
			WHERE A.OBJECT_ID = OBJECT_ID(@tablename)
			AND A.name = @indexname
		END
	END
END;
GO
--------------------------------------------------------------------------------------------------------------------------------
--- SP_D365_CLEAN_SPACEUSED --- END !!!
--------------------------------------------------------------------------------------------------------------------------------


--------------------------------------------------------------------------------------------------------------------------------
--- GET_D365_CLEAN_SAVING_EST --- FUNCTION to get estimated saving from each stage executed by the main procedure
--------------------------------------------------------------------------------------------------------------------------------
DROP FUNCTION IF EXISTS [dbo].[GET_D365_CLEAN_SAVING_EST]
GO

CREATE FUNCTION [dbo].[GET_D365_CLEAN_SAVING_EST] (@stage NVARCHAR(10))
RETURNS NUMERIC(32,6)
AS
BEGIN
    DECLARE @result NUMERIC(32,6) = 0;
	DECLARE @saving NUMERIC(32,6) = 0;

	-- Declare the cursor for the list of partitions to be processed.
	DECLARE tablecursor CURSOR READ_ONLY FAST_FORWARD FOR 
	SELECT sum(A.CONSUMPTION_MB) as SAVING_MB
	FROM [dbo].[D365_CLEAN_SPACEUSED_DETAIL] AS A WITH (NOLOCK)
	WHERE A.STAGE = @stage
	GROUP BY A.OBJECTTYPE, A.STAGE;
 
 	-- Open the cursor.
	OPEN tablecursor;
 
	-- Loop through the cursor.
	FETCH NEXT FROM tablecursor
		INTO @saving;

	WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @saving = ISNULL(@saving, 0);

		SET @result = @result + @saving;

		-- Loop through the cursor.
		FETCH NEXT FROM tablecursor
			INTO @saving;
	END;
	
	-- Close and deallocate the cursor.
	CLOSE tablecursor;
	DEALLOCATE tablecursor;

    RETURN @result;
END;
GO
--------------------------------------------------------------------------------------------------------------------------------
--- GET_D365_CLEAN_SAVING_EST --- END !!!
--------------------------------------------------------------------------------------------------------------------------------

--------------------------------------------------------------------------------------------------------------------------------
--- SP_D365_CLEAN_MAINPROCEDURE --- MAIN Procedure to clean tables based the chosen parameters
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
DROP PROCEDURE IF EXISTS [dbo].[SP_D365_CLEAN_MAINPROCEDURE];
GO

CREATE PROCEDURE [dbo].[SP_D365_CLEAN_MAINPROCEDURE]
(
	@run_command INT = 0, 
	@run_verbose INT = 0,
	@cleanup_staging INT = 0,
	@cleanup_regular INT = 0,
	@cleanup_regular_list NVARCHAR(MAX) = '',
	@cleanup_index INT = 0,
	@cleanup_idx_local INT = 0,
	@cleanup_idx_local_list NVARCHAR(MAX) = '',
	@cleanup_dmf INT = 0
)
AS
BEGIN
    SET NOCOUNT ON;

	--- Variables Declarations ---
	DECLARE @schemaid	INT; 
	DECLARE @tableid 	INT;
	DECLARE @indexid	INT;

	DECLARE @schemaname	NVARCHAR(6); 
	DECLARE @tablename	NVARCHAR(155); 
	DECLARE @indexname	NVARCHAR(155);
	DECLARE @objectname	NVARCHAR(155);
	DECLARE @objecttype	NVARCHAR(5);

	DECLARE @localization NVARCHAR(3);

	DECLARE @command	NVARCHAR(MAX);

	DECLARE @timestart	DATETIME;
	DECLARE @stage 		NVARCHAR(10);
	DECLARE @saving 	NUMERIC = 0;
	------------------------------

	IF(OBJECT_ID('[dbo].[D365_CLEAN_SPACEUSED_SUMMARY]') Is Not Null) BEGIN TRUNCATE TABLE [dbo].[D365_CLEAN_SPACEUSED_SUMMARY] END
	IF(OBJECT_ID('[dbo].[D365_CLEAN_SPACEUSED_DETAIL]') Is Not Null) BEGIN TRUNCATE TABLE [dbo].[D365_CLEAN_SPACEUSED_DETAIL] END

	IF (@run_command = 0) PRINT '!!! SIMULATION ONLY, @RunCommand was set = 0 !!!'

	IF (@cleanup_staging = 1)
	BEGIN
		-- Drop the temporary tables.
		DROP TABLE IF EXISTS #staging_work_to_do;

		-- Variables setting 
		SET @timestart = GETDATE();
		SET @stage = 'STAGING'
		SET @objecttype = 'TABLE'

		-- Summary DB Space Used BEFORE executing stage
		EXECUTE SP_D365_CLEAN_SPACEUSED @stage

		SELECT
			a.[object_id] AS tableid,
			a.[name] AS tablename,
			a.[schema_id] as schemaid,
			b.[name] as schemaname
		INTO #staging_work_to_do
		FROM sys.tables as a WITH (NOLOCK)
		join sys.schemas as b WITH (NOLOCK)
		on b.schema_id = a.schema_id
		WHERE a.type = 'U'			-- Include USER_TABLE tables only 
		and b.name = 'dbo'			-- Include tables on DBO schema only 
		and (a.name LIKE '%STAGING' or a.name LIKE '%STAGING_CAP' or a.name LIKE '%STAGE_CAP')
		and a.name not in ('CUSTAGING','CUSTAGINGLEGALENTITY','CUSTAGINGLINE','RETAILASSORTMENTLOOKUPSTAGING');

		-- Declare the cursor for the list of partitions to be processed.
		DECLARE tablecursor CURSOR READ_ONLY FAST_FORWARD FOR SELECT * FROM #staging_work_to_do;
	
		-- Open the cursor.
		OPEN tablecursor;
	
		-- Loop through the cursor.
		FETCH NEXT
			FROM tablecursor
			INTO @tableid, @tablename, @schemaid, @schemaname;

		WHILE @@FETCH_STATUS = 0
		BEGIN
			SET @objectname = CONCAT(@schemaname,'.',@tablename );
			SET @command = N'IF(OBJECT_ID(''' + @objectname + N''') Is Not Null) BEGIN TRUNCATE TABLE ' + @objectname + ' END';

			-- Detailed Table Space Used 
			EXECUTE SP_D365_CLEAN_SPACEUSED @stage, @objecttype, @objectname  

			IF (@run_command) = 1 EXECUTE sp_Executesql @command;
			IF (@run_verbose) = 1 PRINT CONCAT(@stage,' - SQL Command : ', @command);

			FETCH NEXT
			FROM tablecursor
			INTO @tableid, @tablename, @schemaid, @schemaname;
		END;

		-- Close and deallocate the cursor.
		CLOSE tablecursor;
		DEALLOCATE tablecursor;

		-- Drop the temporary tables.
		DROP TABLE IF EXISTS #staging_work_to_do;

		-- Summary DB Space Used AFTER stage execution
		EXECUTE SP_D365_CLEAN_SPACEUSED @stage
		SELECT @saving = [dbo].[GET_D365_CLEAN_SAVING_EST] (@stage)
	
		PRINT CONCAT('Cleansing ', @stage,' tables Completed! / Elapsed Time: ', CAST(DATEDIFF(MILLISECOND, @timestart, GETDATE()) AS VARCHAR(10)), ' Milliseconds / Estimated Saving of : ', @saving, ' MB');
		IF (@run_command = 0) PRINT '!!! SIMULATION ONLY, @RunCommand was set = 0 !!!'
	END;

	IF (@cleanup_regular = 1 AND @cleanup_regular_list <> '')
	BEGIN
		-- Drop the temporary tables.
		DROP TABLE IF EXISTS #regular_work_to_do;

		-- Variables setting 
		SET @timestart = GETDATE();
		SET @stage = 'REGULAR'
		SET @objecttype = 'TABLE'

		-- Summary DB Space Used BEFORE executing stage
		EXECUTE SP_D365_CLEAN_SPACEUSED @stage
		
		DECLARE @list NVARCHAR(MAX)
		SET @list = @cleanup_regular_list;

		SELECT
			a.[object_id] AS tableid,
			a.[name] AS tablename,
			a.[schema_id] as schemaid,
			b.[name] as schemaname
		INTO #regular_work_to_do
		FROM sys.tables as a WITH (NOLOCK)
		join sys.schemas as b WITH (NOLOCK)
		on b.schema_id = a.schema_id
		WHERE a.type = 'U'			-- Include USER_TABLE tables only 
		and b.name = 'dbo'			-- Include tables on DBO schema only 
		and a.name in (SELECT value FROM STRING_SPLIT(@list, ','))

		-- Declare the cursor for the list of partitions to be processed.
		DECLARE tablecursor CURSOR READ_ONLY FAST_FORWARD FOR SELECT * FROM #regular_work_to_do;
	
		-- Open the cursor.
		OPEN tablecursor;
	
		-- Loop through the cursor.
		FETCH NEXT
			FROM tablecursor
			INTO @tableid, @tablename, @schemaid, @schemaname;
		
		WHILE @@FETCH_STATUS = 0
		BEGIN
			SET @objectname = CONCAT(@schemaname,'.',@tablename );
			SET @command = N'IF(OBJECT_ID(''' + @objectname + N''') Is Not Null) BEGIN TRUNCATE TABLE ' + @objectname + ' END';

			-- Detailed Table Space Used 
			EXECUTE SP_D365_CLEAN_SPACEUSED @stage, @objecttype, @objectname;

			IF (@run_command) = 1 EXECUTE sp_Executesql @command;
			IF (@run_verbose) = 1 PRINT CONCAT(@stage,' - SQL Command: ', @command);

			FETCH NEXT
				FROM tablecursor
				INTO @tableid, @tablename, @schemaid, @schemaname;
		END

		-- Close and deallocate the cursor.	
		CLOSE tablecursor;
		DEALLOCATE tablecursor;

		-- Drop the temporary tables.
		DROP TABLE IF EXISTS #regular_work_to_do;

		-- Summary DB Space Used AFTER stage execution
		EXECUTE SP_D365_CLEAN_SPACEUSED @stage
		SELECT @saving = [dbo].[GET_D365_CLEAN_SAVING_EST] (@stage)
		
		PRINT CONCAT('Cleansing ', @stage,' tables Completed! / Elapsed Time: ', CAST(DATEDIFF(MILLISECOND, @timestart, GETDATE()) AS VARCHAR(10)), ' Milliseconds / Estimated Saving of : ', @saving, ' MB');
		IF (@run_command = 0) PRINT '!!! SIMULATION ONLY, @RunCommand was set = 0 !!!'
	END;

	IF (@cleanup_index = 1)
	BEGIN
		-- Drop the temporary tables.
		DROP TABLE IF EXISTS #index_work_to_do;

		-- Variables setting 
		SET @timestart = GETDATE();
		SET @stage = 'INDEXES'
		SET @objecttype = 'INDEX'

		-- Summary DB Space Used BEFORE executing stage
		EXECUTE SP_D365_CLEAN_SPACEUSED @stage

		SELECT	a.[object_id] AS tableid,
				a.[name] AS tablename,
				a.[schema_id] as schemaid,
				b.[name] as schemaname,
				c.[index_id] as indexid,
				c.[name] as indexname
		INTO #index_work_to_do
		FROM sys.tables as a WITH (NOLOCK)
		join sys.schemas as b WITH (NOLOCK)
		on b.schema_id = a.schema_id
		join sys.indexes as c WITH (NOLOCK)
		on c.object_id = a.object_id
		WHERE a.type = 'U'			-- Include USER_TABLE tables only 
		and b.name = 'dbo'			-- Include tables on DBO schema only 
		and c.type = 2				-- Include NONCLUSTERED Indexes only 
		and c.name not like 'I_%'	-- Index Name begin with I_*
		--- Tables Excluded (Model, Security, System tables/indexes, ... ) ---
		and a.name not like 'AUTOTUNE%' 
		and a.name not like 'BATCH_%' 
		and a.name not like 'CLASS%'
		and a.name not like 'CONFIGKEY%' 
		and a.name not like 'DAMS%' 
		and a.name not like 'DATAAREA%'
		and a.name not like 'DB%' 
		and a.name not like 'DMF%'
		and a.name not like 'DTA_%'
		and a.name not like 'ENUM%'
		and a.name not like 'Licens%' 
		and a.name not like 'Model%'
		and a.name not like 'PERSPEC%' 
		and a.name not like 'Security%'
		and a.name not like 'SOURCE%' 
		and a.name not like 'SQL%' 
		and a.name not like 'SYS%'
		and a.name not like 'Table%'
		and a.name not like 'TYPEID%'
		and a.name not like 'USER%' 
		and a.name not like 'WD_%'
		and a.name not like 'WORKFLOW%'
		-----------------------------------------------------------------------
		order by a.name asc

		-- Declare the cursor for the list of partitions to be processed.
		DECLARE tablecursor CURSOR READ_ONLY FAST_FORWARD FOR SELECT * FROM #index_work_to_do;
	
		-- Open the cursor.
		OPEN tablecursor;
	
		-- Loop through the cursor.
		FETCH NEXT
			FROM tablecursor
			INTO @tableid, @tablename, @schemaid, @schemaname, @indexid, @indexname;
		
		WHILE @@FETCH_STATUS = 0
		BEGIN			
			SET @objectname = CONCAT(@schemaname,'.',@tablename );
			SET @command = N'DROP INDEX IF EXISTS [' + @indexname + '] ON ' + @objectname;

			-- Detailed Index  Space Used 
			EXECUTE SP_D365_CLEAN_SPACEUSED @stage, @objecttype, @objectname, @indexname;

			IF (@run_command) = 1 EXECUTE sp_Executesql @command;
			IF (@run_verbose) = 1 PRINT CONCAT(@stage,' - SQL Command : ', @command);

			FETCH NEXT
				FROM tablecursor
				INTO @tableid, @tablename, @schemaid, @schemaname, @indexid, @indexname;
		END

		-- Close and deallocate the cursor.	
		CLOSE tablecursor;
		DEALLOCATE tablecursor;

		-- Drop the temporary tables.
		DROP TABLE IF EXISTS #index_work_to_do;

		-- Summary DB Space Used AFTER stage execution
		EXECUTE SP_D365_CLEAN_SPACEUSED @stage
		SELECT @saving = [dbo].[GET_D365_CLEAN_SAVING_EST] (@stage)

		PRINT CONCAT('Cleansing ', @stage,' tables Completed! / Elapsed Time: ', CAST(DATEDIFF(MILLISECOND, @timestart, GETDATE()) AS VARCHAR(10)), ' Milliseconds / Estimated Saving of : ', @saving, ' MB');
		IF (@run_command = 0) PRINT '!!! SIMULATION ONLY, @RunCommand was set = 0 !!!'
	END;

	IF (@cleanup_idx_local = 1 AND @cleanup_idx_local_list <> '')
	BEGIN
		-- Drop the temporary tables.
		DROP TABLE IF EXISTS #idx_localization;

		-- Variables setting 
		SET @timestart = GETDATE();
		SET @stage = 'LOCAL'
		SET @objecttype = 'INDEX'

		-- Summary DB Space Used BEFORE executing stage
		EXECUTE SP_D365_CLEAN_SPACEUSED @stage

		SELECT value
		INTO #idx_localization
		FROM STRING_SPLIT(@cleanup_idx_local_list, ',')

		-- Declare the cursor for the list of partitions to be processed.
		DECLARE tablecursor CURSOR READ_ONLY FAST_FORWARD FOR SELECT * FROM #idx_localization;
		
		-- Open the cursor.
		OPEN tablecursor;

		FETCH NEXT FROM tablecursor INTO @localization;
			
		WHILE @@FETCH_STATUS = 0
		BEGIN
			SELECT	a.[object_id] AS tableid,
					a.[name] AS tablename,
					a.[schema_id] as schemaid,
					b.[name] as schemaname,
					c.[index_id] as indexid,
					c.[name] as indexname	
			INTO #idx_local_work_to_do
			FROM sys.tables as a WITH (NOLOCK)
			join sys.schemas as b WITH (NOLOCK)
			on b.schema_id = a.schema_id
			join sys.indexes as c WITH (NOLOCK)
			on c.object_id = a.object_id
			WHERE a.type = 'U'			-- Include USER_TABLE tables only 
			and b.name = 'dbo'			-- Include tables on DBO schema only 
			and c.type = 2				-- Include NONCLUSTERED Indexes only 
			and (c.name like CONCAT('I_%', '^', @localization) ESCAPE '^'		-- !!! DO NOT REMOVE ESCAPE CHAR FROM HERE !!!
			or c.name like CONCAT('I_%', '^', @localization, 'IDX') ESCAPE '^')	-- !!! DO NOT REMOVE ESCAPE CHAR FROM HERE !!!
			--- Tables Excluded (Model, Security, System tables/indexes, ... ) ----------------------------------------
			and a.name not like 'AUTOTUNE%' 
			and a.name not like 'BATCH_%' 
			and a.name not like 'CLASS%'
			and a.name not like 'CONFIGKEY%' 
			and a.name not like 'DAMS%' 
			and a.name not like 'DATAAREA%'
			and a.name not like 'DB%' 
			and a.name not like 'DMF%'
			and a.name not like 'DTA_%'
			and a.name not like 'ENUM%'
			and a.name not like 'Licens%' 
			and a.name not like 'Model%'
			and a.name not like 'PERSPEC%' 
			and a.name not like 'Security%'
			and a.name not like 'SOURCE%' 
			and a.name not like 'SQL%' 
			and a.name not like 'SYS%'
			and a.name not like 'Table%'
			and a.name not like 'TYPEID%'
			and a.name not like 'USER%' 
			and a.name not like 'WD_%'
			and a.name not like 'WORKFLOW%'
			-----------------------------------------------------------------------------------------------------------
			order by a.name asc

			-- Declare the cursor for the list of partitions to be processed.
			DECLARE tablecursor_sub CURSOR READ_ONLY FAST_FORWARD FOR SELECT * FROM #idx_local_work_to_do;
		
			-- Open the cursor.
			OPEN tablecursor_sub;
		
			-- Loop through the cursor.
			FETCH NEXT
				FROM tablecursor_sub
				INTO @tableid, @tablename, @schemaid, @schemaname, @indexid, @indexname;
			
			WHILE @@FETCH_STATUS = 0
			BEGIN

				SET @objectname = CONCAT(@schemaname,'.',@tablename );
				SET @command = N'ALTER INDEX [' + @indexname + '] ON ' + @objectname + ' DISABLE';

				-- Detailed Index Space Used 
				EXECUTE SP_D365_CLEAN_SPACEUSED @stage, @objecttype, @objectname, @indexname;

				IF (@run_command) = 1 EXECUTE sp_Executesql @command;
				IF (@run_verbose) = 1 PRINT CONCAT(@stage,' - SQL Command: ', @command);

				FETCH NEXT
					FROM tablecursor_sub
					INTO @tableid, @tablename, @schemaid, @schemaname, @indexid, @indexname;
			END

			-- Close and deallocate the cursor.	
			CLOSE tablecursor_sub;
			DEALLOCATE tablecursor_sub;

			-- Drop the temporary tables.
			DROP TABLE IF EXISTS #idx_local_work_to_do;

			FETCH NEXT FROM tablecursor INTO @localization;
		END

		-- Close and deallocate the cursor.	
		CLOSE tablecursor;
		DEALLOCATE tablecursor;

		-- Drop the temporary tables.
		DROP TABLE IF EXISTS #idx_localization;

		-- Summary DB Space Used AFTER stage execution
		EXECUTE SP_D365_CLEAN_SPACEUSED @stage
		SELECT @saving = [dbo].[GET_D365_CLEAN_SAVING_EST] (@stage)

		PRINT CONCAT('Cleansing ', @stage,' tables Completed! / Elapsed Time: ', CAST(DATEDIFF(MILLISECOND, @timestart, GETDATE()) AS VARCHAR(10)), ' Milliseconds / Estimated Saving of : ', @saving, ' MB');
		IF (@run_command = 0) PRINT '!!! SIMULATION ONLY, @RunCommand was set = 0 !!!'
	END;
	
	IF (@cleanup_dmf = 1)
	BEGIN
		-- Drop the temporary tables.
		DROP TABLE IF EXISTS #dmf_work_to_do;

		-- Variables setting 
		SET @timestart = GETDATE();
		SET @stage = 'DMF'
		SET @objecttype = 'TABLE'

		-- Summary DB Space Used BEFORE executing stage
		EXECUTE SP_D365_CLEAN_SPACEUSED @stage

		SELECT
			a.[object_id] AS tableid,
			a.[name] AS tablename,
			a.[schema_id] as schemaid,
			b.[name] as schemaname
		INTO #dmf_work_to_do
		FROM sys.tables as a WITH (NOLOCK) 
		join sys.schemas as b WITH (NOLOCK)
		on b.schema_id = a.schema_id
		WHERE a.type = 'U'			-- Include USER_TABLE tables only 
		and b.name = 'dbo'			-- Include tables on DBO schema only 
		and a.name LIKE 'DMF%';
	
		-- Declare the cursor for the list of partitions to be processed.
		DECLARE tablecursor CURSOR READ_ONLY FAST_FORWARD FOR SELECT * FROM #dmf_work_to_do;
	
		-- Open the cursor.
		OPEN tablecursor;
	
		-- Loop through the cursor.
		FETCH NEXT
			FROM tablecursor
			INTO @tableid, @tablename, @schemaid, @schemaname;

		WHILE @@FETCH_STATUS = 0
		BEGIN

			SET @objectname = CONCAT(@schemaname,'.',@tablename );
			SET @command = N'IF(OBJECT_ID(''' + @objectname + N''') Is Not Null) BEGIN TRUNCATE TABLE ' + @objectname + ' END';

			-- Detailed Table Space Used 
			EXECUTE SP_D365_CLEAN_SPACEUSED @stage, @objecttype, @objectname;
		
			IF (@run_command) = 1 EXECUTE sp_Executesql @command;
			IF (@run_verbose) = 1 PRINT CONCAT(@stage,' - SQL Command: ', @command);

			FETCH NEXT
			FROM tablecursor
			INTO @tableid, @tablename, @schemaid, @schemaname;
		END 

		-- Close and deallocate the cursor.
		CLOSE tablecursor;
		DEALLOCATE tablecursor;
	
		-- Drop the temporary tables.
		DROP TABLE IF EXISTS #dmf_work_to_do;

		-- DB Space Used AFTER stage execution
		EXECUTE SP_D365_CLEAN_SPACEUSED @stage
		SELECT @saving = [dbo].[GET_D365_CLEAN_SAVING_EST] (@stage)

		PRINT CONCAT('Cleansing ', @stage,' tables Completed! / Elapsed Time: ', CAST(DATEDIFF(MILLISECOND, @timestart, GETDATE()) AS VARCHAR(10)), ' Milliseconds / Estimated Saving of : ', @saving, ' MB');
		IF (@run_command = 0) PRINT '!!! SIMULATION ONLY, @RunCommand was set = 0 !!!'
	END;
END
GO
--------------------------------------------------------------------------------------------------------------------------------
--- SP_D365_CLEAN_MAINPROCEDURE --- END !!!
--------------------------------------------------------------------------------------------------------------------------------