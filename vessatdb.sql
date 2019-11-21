create database Vessat
go
use Vessat
go
create table WCt(
WC_ID int not null primary key identity(0, 1),
Nimi nvarchar(40) not null,
Kaupunki nvarchar(30) not null,
Katuosoite nvarchar(70) not null,
Postinro nvarchar(5) not null,
Unisex bit null,
Saavutettava bit null,
Ilmainen bit not null,
Aukioloajat nvarchar(70),
Koodi nvarchar(30),
Ohjeet nvarchar(200),
Lis�tty datetime default getDate()not null,
Muokattu datetime null,
K�ytt�j�_ID int null,
Sijainti geography not null,
Lat decimal(11,8)not null,
Long decimal(11,8) not null
)
go
create table Kommentit(
Kommentti_ID int not null primary key identity(0, 1),
Lis�tty datetime default getDate()not null,
Arvio int not null check (arvio between 1 and 5),
Sis�lt� nvarchar (200),
WC_ID int not null,
K�ytt�j�_ID int null)
go
alter table Kommentit ADD constraint
fk_Kommentit_WCt
foreign key(WC_ID)
references WCt(WC_ID);
go
create table K�ytt�j�t(
K�ytt�j�_ID int not null primary key identity(1000, 1),
Nimimerkki nvarchar (40)not null,
Salasana nvarchar(40) not null,
S�hk�posti nvarchar(320) not null)
go
insert into WCt (Nimi, Kaupunki, Katuosoite, Postinro, Sijainti, Ilmainen, Lat, Long )
values ('KirjastoWC', 'Helsinki', 'Mannerheimintie 22 ', '00100', geography::STPointFromText('POINT(24.941119 60.201369)', 4326), 1, 60.201369, 24.941119)
insert into WCt (Nimi, Kaupunki, Katuosoite, Postinro, Sijainti, Ilmainen, Lat, Long )
values ('PuistoWC', 'Helsinki', 'Mannerheimintie 25 ', '00101', geography::STPointFromText('POINT(24.941119 60.20136)', 4326), 1, 24.941119, 24.941119)
insert into WCt (Nimi, Kaupunki, Katuosoite, Postinro, Sijainti, Ilmainen, Lat, Long )
values ('MetroWC', 'Helsinki', 'Mannerheimintie 8 ', '00100', geography::STPointFromText('POINT(24.941119 60.20131)', 4326), 1, 24.941119, 60.20131)
select *from wct