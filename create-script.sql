CREATE DATABASE "Prologica"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'en_US.utf8'
    LC_CTYPE = 'en_US.utf8'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;


CREATE TABLE IF NOT EXISTS public."Consoles"
(
    "Id" integer NOT NULL DEFAULT nextval('"Console_Id_seq"'::regclass),
    "Name" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Console_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Consoles"
    OWNER to postgres;


CREATE TABLE IF NOT EXISTS public."Games"
(
    "Id" integer NOT NULL DEFAULT nextval('"Games_Id_seq"'::regclass),
    "ConsoleId" integer NOT NULL,
    "Name" text COLLATE pg_catalog."default" NOT NULL,
    "Price" double precision,
    CONSTRAINT "Games_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Games"
    OWNER to postgres;