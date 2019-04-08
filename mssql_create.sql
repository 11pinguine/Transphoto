CREATE TABLE Country (
  id			INTEGER PRIMARY KEY IDENTITY,
  name			VARCHAR(50) NOT NULL,
  continent		VARCHAR(12));

SET IDENTITY_INSERT Country ON
INSERT INTO Country(id, name, continent) VALUES(1, 'Česká republika', 'Evropa');
INSERT INTO Country(id, name, continent) VALUES(2, 'Německo', 'Evropa');
INSERT INTO Country(id, name, continent) VALUES(3, 'Ukrajina', 'Evropa');
INSERT INTO Country(id, name, continent) VALUES(4, 'Itálie', 'Evropa');
INSERT INTO Country(id, name, continent) VALUES(5, 'Maďarsko', 'Evropa');
SET IDENTITY_INSERT Country OFF

go

SELECT * FROM Country;

CREATE TABLE City (
 id							INTEGER NOT NULL PRIMARY KEY IDENTITY,
 name                       VARCHAR(50) NOT NULL,
 region                     VARCHAR(50),
 country_id					int, 
 FOREIGN KEY (country_id)   REFERENCES Country(id)
);
GO

SET IDENTITY_INSERT City on
INSERT INTO City(id, name, region, country_id) VALUES(1,'Praha', 'Hlavní město Praha', 1);
INSERT INTO City(id, name, region, country_id) VALUES(2,'Brno', 'Jihomoravský kraj', 1);
INSERT INTO City(id, name, region, country_id) VALUES(3, 'Ostrava', 'Moravskoslezský kraj', 1);
INSERT INTO City(id, name, region, country_id) VALUES(4, 'Plzeň', 'Plzeňský kraj', 1);
INSERT INTO City(id, name, region, country_id) VALUES(5,'Olomouc', 'Olomoucký kraj', 1);
INSERT INTO City(id, name, region, country_id) VALUES(6, 'Opava', 'Moravskoslezský kraj', 1);
INSERT INTO City(id, name, region, country_id) VALUES(7, 'Karviná', 'Moravskoslezský kraj', 1);
INSERT INTO City(id, name, region, country_id) VALUES(8, 'Nový Jičín', 'Moravskoslezský kraj', 1);
INSERT INTO City(id, name, region, country_id) VALUES(9, 'Havířov', 'Moravskoslezský kraj', 1);
INSERT INTO City(id, name, region, country_id) VALUES(10, 'Frýdek-Místek', 'Moravskoslezský kraj', 1);
INSERT INTO City(id, name, region, country_id) VALUES(11, 'Vsetín', 'Zlínský kraj', 1);
INSERT INTO City(id, name, region, country_id) VALUES(12, 'Zlín', 'Zlínský kraj', 1);
INSERT INTO City(id, name, region, country_id) VALUES(13,'Přerov', 'Olomoucký kraj', 1);
INSERT INTO City(id, name, region, country_id) VALUES(14, 'Frankfurt am Main', 'Hessen', 2);
INSERT INTO City(id, name, region, country_id) VALUES(15, 'Budapest', 'Budapest', 5);
INSERT INTO City(id, name, region, country_id) VALUES(16, 'Užhorod', 'Zakarpatská oblast', 3);
INSERT INTO City(id, name, region, country_id) VALUES(17, 'Venezia-Mestre', 'Veneto',4);
SET IDENTITY_INSERT City OFF
go

--SELECT * FROM City
--JOIN Country ON country_id = city.country_id;






CREATE TABLE Place (
  id			INTEGER PRIMARY KEY IDENTITY,
  name			VARCHAR(50) NOT NULL,
  city_id					int, 
  FOREIGN KEY (city_id)   REFERENCES Country(id)
  );
go

SET IDENTITY_INSERT Place ON
INSERT INTO Place(id, name, city_id) VALUES(1, 'Ulice 28. října', 3);
INSERT INTO Place(id, name, city_id) VALUES(2, 'Ulice Opavská', 3);
INSERT INTO Place(id, name, city_id) VALUES(3, 'Ulice Martinovská', 3);
INSERT INTO Place(id, name, city_id) VALUES(4, 'Ulice 17. listopadu', 3);
INSERT INTO Place(id, name, city_id) VALUES(5, 'Ulice U Dílen', 3);
INSERT INTO Place(id, name, city_id) VALUES(6, 'Hlavní třída', 3);
INSERT INTO Place(id, name, city_id) VALUES(7, 'Ulice Průběžná', 3);
INSERT INTO Place(id, name, city_id) VALUES(8, 'Ústřední autobusové nádraží', 3);
INSERT INTO Place(id, name, city_id) VALUES(9, 'Garáže Arriva Ostrava', 3);
INSERT INTO Place(id, name, city_id) VALUES(10, 'Ulice Mongolská', 3);
INSERT INTO Place(id, name, city_id) VALUES(11, 'Dílny DPO', 3);
INSERT INTO Place(id, name, city_id) VALUES(12, 'Вулиця Бабяка', 16);
INSERT INTO Place(id, name, city_id) VALUES(13, 'Проспект Свободи', 16);
INSERT INTO Place(id, name, city_id) VALUES(14, 'Kossuth Lájos Tér', 15);
INSERT INTO Place(id, name, city_id) VALUES(15, 'Römer', 14);
INSERT INTO Place(id, name, city_id) VALUES(16, 'Ulice Vítkovická', 3);
SET IDENTITY_INSERT Place OFF
go

--SELECT * FROM Place;

CREATE TABLE Company (
  id			INTEGER PRIMARY KEY IDENTITY,
  name			VARCHAR(50) NOT NULL,
  city_id					int, 
  FOREIGN KEY (city_id)   REFERENCES City(id)
  );

go 
SET IDENTITY_INSERT Company ON
INSERT INTO Company(id, name, city_id) VALUES(1, 'Dopravní podnik Ostrava', 3);
INSERT INTO Company(id, name, city_id) VALUES(2, 'Arriva Morava', 5);
INSERT INTO Company(id, name, city_id) VALUES(3, 'ČSAD Karviná', 7);
INSERT INTO Company(id, name, city_id) VALUES(4, 'ČSAD Havířov', 9);
INSERT INTO Company(id, name, city_id) VALUES(5, 'ČSAD Frýdek-Místek', 10);
INSERT INTO Company(id, name, city_id) VALUES(6, 'ČSAD Vsetín', 11);
SET IDENTITY_INSERT Company OFF
go

--DROP Table Company;

CREATE TABLE Vehicle_Model (
  id					INTEGER PRIMARY KEY IDENTITY,
  manufacturer			VARCHAR(30) NOT NULL ,
  model					VARCHAR(40) NOT NULL ,
  capacity_seating      INTEGER,
  capacity_standing		INTEGER,
  vehicle_type			VARCHAR(25) NOT NULL,
  length				FLOAT,
  width					FLOAT,
  height				FLOAT,
  weight				INTEGER,
  max_speed				INTEGER,
  low_floor				bit NOT NULL,
  powered_by			VARCHAR(25) NOT NULL
  );

go 
SET IDENTITY_INSERT Vehicle_Model ON
INSERT INTO Vehicle_Model(id, manufacturer, model, capacity_seating, capacity_standing, vehicle_type, length, width, height, weight, max_speed, low_floor, powered_by) 
VALUES (1, 'Tatra', 'T3', 31, 72, 'tramvaj', 14, 2.5, 3.05, 16300, 65, 0, 'elektricky' ),
(2, 'Tatra', 'KT8D5.RN1', 63, 157, 'tramvaj', 30.3, 2.48, 3.55, 38500, 65, 1, 'elektricky' ),
(3, 'Tatra', 'T6A5', 30, 85, 'tramvaj', 14.7, 2.5, 3.16, 19500, 65, 0, 'elektricky'),
(4, 'Škoda', 'Astra', 41, 113, 'tramvaj', 19.79, 2.46, 3.46, 24200, 70, 1, 'elektricky' ),
(5, 'Inekon', 'Trio', 41, 99, 'tramvaj', 20.13, 2.46, 3.46, 26000, 70, 1, 'elektricky' ),
(6, 'Tatra', 'T3R.EV', 32, 78, 'tramvaj', 14, 2.5, null, null,65, 0, 'elektricky' ),
(7, 'Vario', 'LFR', 33, 60, 'tramvaj', 15.1, 2.5, null, 21200,65, 1, 'elektricky' ),
(8, 'Vario', 'VV60LF', 30, 48, 'tramvaj', 10.36, 2.5, 3.2, 8500,70, 1, 'elektricky' ),
(9, 'Vario', 'LF3', 61, 168, 'tramvaj', 30.1, 2.5, null, 38000,65, 1, 'elektricky' ),
(10, 'Vario', 'LF2', 46, 94, 'tramvaj', 22.6, 2.48, null, 31500,65, 1, 'elektricky' ),
(11, 'Vario', 'LF3/2', 54, 175, 'tramvaj', 31.2, 2.48, 2.62, 41500,65, 1, 'elektricky' ),
(12, 'Vario', 'LF2 plus', 52, 91, 'tramvaj', 23.7, 2.48, 3.18, 30000,65, 1, 'elektricky' ),
(13, 'Stadler', 'NF2 nOVA', 61, 127, 'tramvaj', 24.9, 2.5, 3.6, null,80, 1, 'elektricky' );
SET IDENTITY_INSERT Vehicle_Model OFF
go


SELECT * FROM Vehicle_Model;
	

CREATE TABLE Depot (
  id			INTEGER PRIMARY KEY IDENTITY,
  name			VARCHAR(50) NOT NULL,
  company_id					int, 
  FOREIGN KEY (company_id)   REFERENCES Company(id)
  );
go
SET IDENTITY_INSERT Depot ON
INSERT INTO Depot(id, name, company_id) VALUES(1, 'Vozovna Poruba', 1);
INSERT INTO Depot(id, name, company_id) VALUES(2, 'Vozovna Moravská Ostrava (Křivá)', 1);
INSERT INTO Depot(id, name, company_id) VALUES(3, 'Garáže Poruba', 1);
INSERT INTO Depot(id, name, company_id) VALUES(4, 'Garáže Hranečník', 1);
INSERT INTO Depot(id, name, company_id) VALUES(5, 'Garáže Arriva Ostrava', 2);
SET IDENTITY_INSERT Depot OFF
go

SELECT * FROM Depot;
DROP TABLE Vehicle
CREATE TABLE Vehicle (
  id					INTEGER PRIMARY KEY IDENTITY,
  construction_year		INTEGER,
  state					VARCHAR(40) NOT NULL ,
  evidence_id			VARCHAR(40),
  main_photo_path		VARCHAR(80),
  car_license_plate		VARCHAR(40),
  vehicle_model_id		int, 
  FOREIGN KEY (vehicle_model_id)   REFERENCES Vehicle_Model(id),
  city_id		int, 
  FOREIGN KEY (city_id)   REFERENCES City(id),
  depot_id		int, 
  FOREIGN KEY (depot_id)   REFERENCES Depot(id),
  podtyp	VARCHAR(20)
  );

go 
SET IDENTITY_INSERT Vehicle ON
INSERT INTO Vehicle (id, construction_year, state, evidence_id, main_photo_path, car_license_plate, vehicle_model_id, city_id, depot_id, podtyp) 
VALUES 
(1, 1982, 'provozní', 902, null, null, 1, 3, 2, 'T3SU' ),
(2, 1983, 'provozní', 914, null, null, 1, 3, 2, 'T3SUCS' ),
(3, 1983, 'provozní', 919, null, null, 1, 3, 1, 'T3R.P' ),
(4, 1985, 'provozní', 929, null, null, 1, 3, 2, 'T3SUCS' ),
(5, 1985, 'provozní', 933, null, null, 1, 3, 2, 'T3SUCS' ),
(6, 1985, 'provozní', 937, null, null, 1, 3, 1, 'T3R.P' ),
(7, 1985, 'provozní', 938, null, null, 1, 3, 1, 'T3R.P' ),
(8, 1985, 'provozní', 944, null, null, 1, 3, 1, 'T3R.P' ),
(9, 1985, 'provozní', 947, null, null, 1, 3, 1, 'T3R.P' ),
(10, 1985, 'provozní', 948, null, null, 1, 3, 1, 'T3R.E' ),

(11, 2018, 'provozní', 1701, null, null, 13, 3, 1, 'NF2'),

--SELECT * FROM Vehicle;
--všechny Stadlery
(12, 2018, 'provozní', 1701, null, null, 13, 3, 1, 'NF2'),
(13, 2018, 'provozní', 1702, null, null, 13, 3, 1, 'NF2'),
(14, 2018, 'provozní', 1703, null, null, 13, 3, 1, 'NF2'),
(15, 2018, 'provozní', 1704, null, null, 13, 3, 1, 'NF2'),
(16, 2018, 'provozní', 1705, null, null, 13, 3, 1, 'NF2'),
(17, 2018, 'provozní', 1707, null, null, 13, 3, 1, 'NF2'),
(18, 2018, 'provozní', 1708, null, null, 13, 3, 1, 'NF2'),
(19, 2018, 'provozní', 1709, null, null, 13, 3, 1, 'NF2'),
(20, 2018, 'provozní', 1710, null, null, 13, 3, 1, 'NF2'),
(21, 2018, 'provozní', 1711, null, null, 13, 3, 1, 'NF2'),
(22, 2018, 'provozní', 1712, null, null, 13, 3, 1, 'NF2'),
(23, 2018, 'provozní', 1713, null, null, 13, 3, 1, 'NF2'),
(24, 2018, 'provozní', 1714, null, null, 13, 3, 1, 'NF2'),
(25, 2018, 'provozní', 1715, null, null, 13, 3, 1, 'NF2'),
(26, 2018, 'provozní', 1716, null, null, 13, 3, 1, 'NF2'),
(27, 2018, 'provozní', 1717, null, null, 13, 3, 1, 'NF2'),
(28, 2018, 'provozní', 1718, null, null, 13, 3, 1, 'NF2'),
(29, 2018, 'provozní', 1719, null, null, 13, 3, 1, 'NF2'),
(30, 2018, 'provozní', 1720, null, null, 13, 3, 1, 'NF2'),
(31, 2018, 'provozní', 1721, null, null, 13, 3, 1, 'NF2'),
(32, 2018, 'provozní', 1722, null, null, 13, 3, 1, 'NF2'),
(33, 2018, 'provozní', 1723, null, null, 13, 3, 1, 'NF2'),
(34, 2018, 'provozní', 1724, null, null, 13, 3, 1, 'NF2'),
(35, 2018, 'provozní', 1725, null, null, 13, 3, 1, 'NF2'),
(36, 2018, 'provozní', 1726, null, null, 13, 3, 1, 'NF2'),
(37, 2018, 'provozní', 1727, null, null, 13, 3, 1, 'NF2'),
(38, 2018, 'provozní', 1728, null, null, 13, 3, 1, 'NF2'),
(39, 2018, 'provozní', 1729, null, null, 13, 3, 1, 'NF2'),
(40, 2018, 'provozní', 1730, null, null, 13, 3, 1, 'NF2'),
(41, 2018, 'provozní', 1731, null, null, 13, 3, 1, 'NF2'),

--kačeny
(42, 1984, 'provozní', 1500, null, null, 2, 3, 2, 'KT8D5R.N1'),
(43, 1989, 'provozní', 1501, null, null, 2, 3, 2, 'KT8D5R.N1'),
(44, 1989, 'provozní', 1502, null, null, 2, 3, 2, 'KT8D5R.N1'),
(45, 1989, 'provozní', 1503, null, null, 2, 3, 2, 'KT8D5R.N1'),
(46, 1989, 'provozní', 1504, null, null, 2, 3, 2, 'KT8D5R.N1'),
(47, 1989, 'provozní', 1505, null, null, 2, 3, 2, 'KT8D5R.N1'),
(48, 1989, 'provozní', 1506, null, null, 2, 3, 2, 'KT8D5R.N1'),
(49, 1989, 'provozní', 1507, null, null, 2, 3, 2, 'KT8D5R.N1'),
(50, 1989, 'provozní', 1508, null, null, 2, 3, 2, 'KT8D5R.N1'),
(51, 1989, 'provozní', 1509, null, null, 2, 3, 2, 'KT8D5R.N1'),
(52, 1989, 'provozní', 1510, null, null, 2, 3, 2, 'KT8D5R.N1'),
(53, 1989, 'provozní', 1511, null, null, 2, 3, 2, 'KT8D5R.N1'),
(54, 1989, 'provozní', 1512, null, null, 2, 3, 2, 'KT8D5R.N1'),
(55, 1989, 'provozní', 1513, null, null, 2, 3, 2, 'KT8D5R.N1'),
(56, 1989, 'provozní', 1514, null, null, 2, 3, 2, 'KT8D5R.N1'),
(57, 1989, 'provozní', 1515, null, null, 2, 3, 2, 'KT8D5R.N1')


SET IDENTITY_INSERT Vehicle OFF

SELECT Vehicle.evidence_id, vehicle_model.manufacturer, Vehicle_Model.model FROM Vehicle
JOIN Vehicle_Model ON Vehicle.vehicle_model_id=Vehicle_Model.id;

CREATE TABLE Vehicle_History (
  id					INTEGER PRIMARY KEY IDENTITY,
  evidence_id			VARCHAR(40),
  start_date			DATE,
  end_date				DATE,
  car_license_plate		VARCHAR(40),

  vehicle_id		int, 
  FOREIGN KEY (vehicle_id)   REFERENCES Vehicle(id),
  city_id		int, 
  FOREIGN KEY (city_id)   REFERENCES City(id),
  depot_id		int, 
  FOREIGN KEY (depot_id)   REFERENCES Depot(id),
  podtyp	VARCHAR(20)
  );

go 
SET IDENTITY_INSERT Vehicle_History ON
INSERT INTO Vehicle_History (id, evidence_id, start_date, end_date, car_license_plate, vehicle_id, city_id, depot_id, podtyp) 
VALUES 
(1, 902, '1983-01-01','2018-12-01', null, 1, 3, 1, 'T3SU' ),
(2, 914, '1984-01-01','2018-12-01', null, 1, 3, 1, 'T3SUCS' )








CREATE TABLE Photo (
  id					INTEGER PRIMARY KEY IDENTITY,
 
  );

go 
SET IDENTITY_INSERT Photo ON
INSERT INTO Vehicle_History (id, evidence_id, start_date, end_date, car_license_plate, vehicle_id, city_id, depot_id, podtyp) 
VALUES 
(1, 902, '1983-01-01','2018-12-01', null, 1, 3, 1, 'T3SU' ),
