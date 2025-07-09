ALTER TABLE OdT1 ALTER COLUMN totalKgOT int not null;

CREATE TABLE BitacoraDeCarga1 (
	idBitCarga INT not null PRIMARY KEY IDENTITY(1,1),
	idOdT INT not null DEFAULT 0,
	mensaje varchar(150) not null
)

alter table bitacoradecarga1 add idUsuario int not null default 1;
alter table bitacoradecarga1 add timestamp datetime;
