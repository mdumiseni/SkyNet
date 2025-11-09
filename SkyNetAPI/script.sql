CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Waybill" (
    "Id" uuid NOT NULL,
    "WaybillNumber" text,
    "ServiceType" text NOT NULL,
    "SenderSuburb" text NOT NULL,
    "SenderPostalCode" text NOT NULL,
    "RecipientSuburb" text NOT NULL,
    "RecipientPostalCode" text NOT NULL,
    "CreateOnDateTime" timestamp with time zone NOT NULL,
    "ModifiedOnDateTime" timestamp with time zone,
    "IsActive" boolean NOT NULL,
    CONSTRAINT "PK_Waybill" PRIMARY KEY ("Id")
);

CREATE TABLE "Parcel" (
    "Id" uuid NOT NULL,
    "ParcelNumber" text NOT NULL,
    "Length" numeric(18,4) NOT NULL,
    "Breadth" numeric(18,4) NOT NULL,
    "Height" numeric(18,4) NOT NULL,
    "Mass" numeric(18,4) NOT NULL,
    "WaybillId" uuid NOT NULL,
    "CreateOnDateTime" timestamp with time zone NOT NULL,
    "ModifiedOnDateTime" timestamp with time zone,
    "IsActive" boolean NOT NULL,
    CONSTRAINT "PK_Parcel" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Parcel_Waybill_WaybillId" FOREIGN KEY ("WaybillId") REFERENCES "Waybill" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Parcel_ParcelNumber" ON "Parcel" ("ParcelNumber");

CREATE INDEX "IX_Parcel_WaybillId" ON "Parcel" ("WaybillId");

CREATE INDEX "IX_Waybill_WaybillNumber" ON "Waybill" ("WaybillNumber");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20251109171527_InitialCreate', '8.0.21');

COMMIT;

