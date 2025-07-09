--ALTA DE TABLA
CREATE TABLE QR (
	idQR in not null primary key identity(1,1),
	descripcion varchar(max) not null,
	partNumber varchar(25) not null,
	quantity int not null,
	poNumber varchar(55) not null,
	trace varchar(25) not null,
	serialNumber varchar(25) not null,
	timestamp datetime not null getdate,
	idUsuario int not null
)

--ACTUALIZACION 03/06/2025
ALTER TABLE QR ADD ediciones int default 0;
ALTER TABLE QR ADD activo bit default 1;
UPDATE QR SET activo = 1

-- ACTUALIZACION 25-06-2025
CREATE TABLE OdT1 (
	idOdT int not null primary key identity(1,1),
	idProducto int not null,
	poNumber varchar(55) not null,
	totalKgOT decimal(10,2) not null,
	idUsuario int not null,
	timestamp datetime not null getdate,
)

CREATE TABLE producto1 (
	idProducto int not null primary key identity(1,1),
	descripcion varchar(150)
	partNumber varchar(25) not null,
	idUsuario int not null,
	timestamp datetime not null getdate,
)

CREATE TABLE movimientosOT1 (
	idMovto int not null primary key identity(1,1),
	idOdT int not null,
	idProducto int not null,
	serialNumber varchar(25) not null,
	quantity int not null,
	idUsuario int not null,
	timestamp datetime not null getdate,
)