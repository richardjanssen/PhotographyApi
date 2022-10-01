BEGIN TRANSACTION;

ALTER TABLE "Images" RENAME COLUMN "Path" TO "Guid";

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220925171031_ImageGuid', '6.0.7');

COMMIT;

BEGIN TRANSACTION;

ALTER TABLE "Images" ADD "Extension" TEXT NOT NULL DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220925175949_ImageExtension', '6.0.7');

COMMIT;

BEGIN TRANSACTION;

CREATE TABLE "Accounts" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Accounts" PRIMARY KEY AUTOINCREMENT,
    "UserName" TEXT NOT NULL,
    "PasswordHash" TEXT NOT NULL,
    "Salt" TEXT NOT NULL
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221001084918_account', '6.0.7');

COMMIT;

