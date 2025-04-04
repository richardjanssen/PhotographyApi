-- Update SQL below to correct database version

------ DO NOT UPDATE THIS SECTION ------

--BEGIN TRANSACTION;

--CREATE TABLE IF NOT EXISTS "DbVersion" (
--    "Version" NUMBER NOT NULL CONSTRAINT "PK___Version" PRIMARY KEY
--);

---- Insert value if not exists
--INSERT INTO "DbVersion" ("Version")
--SELECT 0
--WHERE NOT EXISTS (SELECT * FROM "DbVersion");

--COMMIT;

---- Create a temporary table to contain version number
--CREATE TEMP TABLE "VersionNumber" ("Version" NUMBER);
--INSERT INTO "VersionNumber" VALUES (1); -- Update this value to the current DB Version (last script number executed)

---- Create a dummy table used to throw error when version number is incorrect
--CREATE TEMP TABLE "Dummy" ("DummyCol" TEXT);

--DROP TRIGGER IF EXISTS "VersionCheck";
--CREATE TRIGGER IF NOT EXISTS "VersionCheck"
--BEFORE INSERT ON "Dummy"
--BEGIN
--    SELECT RAISE(ABORT, 'Incorrect DB Version')
--    WHERE (SELECT "Version" FROM "DbVersion" LIMIT 1) != (SELECT "Version" FROM "VersionNumber" LIMIT 1);
--END;

--INSERT INTO "Dummy" VALUES ('-');

-------- INSERT MIGRATION SQL SCRIPT BELOW ------

--CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
--    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
--    "ProductVersion" TEXT NOT NULL
--);

--BEGIN TRANSACTION;

--CREATE TABLE "Recipes" (
--    "Id" INTEGER NOT NULL CONSTRAINT "PK_Recipes" PRIMARY KEY AUTOINCREMENT,
--    "Name" TEXT NOT NULL,
--    "Ingredients" TEXT NOT NULL,
--    "Preparation" TEXT NOT NULL,
--    "RowVersion" INTEGER NOT NULL,
--    "DateModifiedUtc" TEXT NOT NULL
--);

--INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
--VALUES ('20250125200525_InitialCreate', '8.0.10');

--COMMIT;

-------- END OF MIGRATION SQL SCRIPT ------

-------- DO NOT UPDATE THIS SECTION ------

--UPDATE "DbVersion"
--SET "Version" = ((SELECT "Version" FROM "VersionNumber" LIMIT 1) + 1);

--COMMIT;