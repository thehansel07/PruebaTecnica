---Tabla Roles
create table Roles
(
RolId int primary key identity(1,1) not null,
RolDescripcion varchar(150) not null,
RolCondicion bit default(1),
Rowguid UNIQUEIDENTIFIER 
)

go 

---Tabla Usuario
create table Usuario
(
IdUsuario int primary key identity(1,1) not null,
UsuNombre varchar(150) not null, 
UsuApellidos varchar(150) not null,
UsuNumDocumento varchar(20) null,
UsuDireccion varchar(70) null,
Usutelefono varchar(20) null,
UsuEmail varchar(50) not null,
UsuContraseña varbinary(max) not null,
UsuContraseñaConfirma varbinary(max) not null,
Usucondicion bit default(1),
RolesRolId int, 
Rowguid UNIQUEIDENTIFIER
FOREIGN KEY (RolesRolId) REFERENCES Roles (RolId)
)


---Tabla Vuelos

Create table Vuelos
(
idVuelos int identity(1,1) primary key, 
idPaisOrigenVuelo int,
idPaisDestinoVuelo int, 
cantPersonasVuelo int,
fechaInicial date, 
fechaFinal date,
idUsuario int, 
Rowguid UNIQUEIDENTIFIER,
)


---Tabla Pais

create table Pais
(
idPais int identity(1,1) primary key, 
paisNombre varchar(150) not null

)