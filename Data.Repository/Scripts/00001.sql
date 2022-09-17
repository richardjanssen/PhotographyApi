CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "Photos" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Photos" PRIMARY KEY AUTOINCREMENT,
    "Date" TEXT NOT NULL
);

CREATE TABLE "Images" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Images" PRIMARY KEY AUTOINCREMENT,
    "WidthPx" INTEGER NOT NULL,
    "HeightPx" INTEGER NOT NULL,
    "Path" TEXT NOT NULL,
    "PhotoId" INTEGER NULL,
    CONSTRAINT "FK_Images_Photos_PhotoId" FOREIGN KEY ("PhotoId") REFERENCES "Photos" ("Id")
);

CREATE INDEX "IX_Images_PhotoId" ON "Images" ("PhotoId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220917132509_InitialCreate', '6.0.7');

COMMIT;

