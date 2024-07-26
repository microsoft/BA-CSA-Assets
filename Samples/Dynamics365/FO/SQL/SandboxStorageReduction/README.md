# Manual sandbox storage reduction

The procedure is designed to be a starting point that we can refine based on your testing needs in each sandbox.
The procedure reduces the size of the sandboxes with by removing «non-essential data». What constitutes essential data will depend on your testing goals for each sandbox. This script is designed as a starting point with the most commonly removed data from sandboxes. We can run this script as a simulation to see what data it will remove, and based on the result make adjustments.
 
### The process is as follows:
1. Connect to your sandbox database using SQL Server Management Studio (SSMS). You can refer to this blog here on how to request just-in-time database access to the AXDB database and set up the connection in SSMS. The database you are requesting access to in this blog is the same database that we will be working with in the next steps. 
2. After connecting to the database from the first step, expand the Databases. 
3. Right click the AXDB database you requested just-in-time (JIT) database credentials for and select **Reports > Standard Reports > Disk Usage by Top Tables**



This step will allow you to have an overview of the top tables and to see the effect after running the script (when you run it without simulation).
 
1. To remove data, you will need the attached SQL files and execute them sequentially: 
- Step 1 - Create Objects.sql 
- Step 2 - Run Procedure.sql 
- Step 3 - Check Progress.sql 
- Step 4 - Remove Objects.sql 
 
The scripts must be run on the AxDB and their details continue below:
 


 
1. Run **Create Objects**: to create all the objects required to this process; it creates a couple of Store procedures, functions, and tables.  
2. Run **Run Procedure**: it’s the main procedure which requires some parameter to be executed. The procedure is built by steps, each step is executed only if the corresponding parameter is set to 1. 
```sql
--------------------------------------------------------------------------------------------------------------------------------
--- PARAMETERS Settings -----------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------
-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- 
-- --> @run_command           -- --> 1 = Run SQL Command; 0 = Simulation
-- --> @run_verbose           -- --> 1 = Print verbose details; 0 = Print only stage's execution status
-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- 
-- --> @cleanup_staging       -- --> 1 = TRUNCATE All Staging Tables; 0 = cleansing skipped
-- --> @cleanup_regular       -- --> 1 = TRUNCATE All Regular Tables (by the predefined input list); 0 = cleansing skipped
-- --> @cleanup_regular_list  -- --> comma separated list of Regular Tables to truncate (if empty cleansing skipped)
-- --> @cleanup_index         -- --> 1 = DROP all 'DAMS'/Autocreated indexes; 0 = cleansing skipped
-- --> @cleanup_idx_local     -- --> 1 = DISABLE all 'Country Specific' indexes; 0 = cleansing skipped
-- --> @cleanup_idx_local_list-- --> comma separated list of 'Country Specific' code to disable (if empty cleansing skipped)
-- --> @cleanup_dmf           -- --> 1 = TRUNCATE all 'DMF_*' tables (framework's working tables); 0 = cleansing skipped
--------------------------------------------------------------------------------------------------------------------------------
```

**Before running the procedure to execute the command and wipe-out the data (@run_command = 1) I strongly recommend trying ONLY the Simulation (@run_command = 0) + to get the full details of message (@run_verbose = 1), doing so you can understand exactly which objects you are going to handle and the estimated saving of each step.**
 
1. Run **Check Progress**: this script queries the procedure’s “log” tables to check/monitoring the progress and result of the procedure. These tables are always populated, even with the simulation only. 
 
This is an execution sample from my VM. This is dummy data, so the numbers are low, but the idea is to show the results of the clean-ups:


 
 
1. Run **Remove Objects**: This script removes all the objects created from the first script.  
 
### Some recommendations:
- You can add more regular tables to truncate with @cleanup_regular_list in step 2. For example, perhaps you can add BATCHHISTORY and RETAILEVENTNOTIFICATIONACTION here. 
- Define the list of index localization to disable with @cleanup_idx_local_list. This is the list of localization prefix of the localizations that are not implemented in the environment. 
- Execute the script just after DB movement from production, before to open the environment to the business users and/or batch scheduling. 
- Once completed remember to execute script #4 to remove all the objects created for this procedure, otherwise you might have issues with dbsync. 
 

