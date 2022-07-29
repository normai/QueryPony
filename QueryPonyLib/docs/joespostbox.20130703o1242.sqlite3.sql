-- file : id20130703o1242.joespostbox-sqlite.sql
-- id : # 20130703.1442
-- title : SQLite readable SQL file creating database 'joespostbox'
-- note : This file was copied from # 20130703.1441 and adjusted so SQLite will read it.
--          From this file, the sqlite database file # 20130703.1443 is derived.
-- note : Copied from file # 20130702.1441 which was created by MySQL
--
-- note : What is different in SQLite:
--        (1) The 'DROP DATABASE' statement is unknown.
--        (2) The 'CREATE DATABASE' statement is unknown.
--        (3) The 'USE' statement is unknown, as e.g. in 'USE `joespostbox`'.
--        (4) Indices cannot be created with the 'KEY' keyword inside the 'CREATE' statement
--             block. They must be created with a separate 'CREATE INDEX' statement.
--        (5) Problems with sequence 'DEFAULT CHARSET=utf8' in the 'CREATE' statement. The manuals
--             on http://www.sqlite.org/lang_createtable.html tell, there exists 'DEFAULT' for collation
--             but we couldn´t make it run quickly. So we leave this away until that is cleared.
--        (6) Inside a text value, the escape sequence "Joe\'s" is misinterpreted. SQLite handles
--             his character by doubling it. So if it reads "Joe''s", then it works.
--        (7) With 'bigint', the 'unsigned' keyword seems unknow.
--        (8) The 'COMMENT' keyword with a field definition is probably unknown.
--        (9)
--
-- note : Miscellaneous notes about SQLite:
--        (1) About the woed 'databases'
--        (1.1) It looks as if one file is one database. But the command '.databases' lists beside the probaly
--               mandatory 'main' database sometimes a 'temp' database.
--        (1.2) There is the SQL statement 'ATTACH DATABASE' (see # 20130703.1303) which takes a
--                filename. So the two databases seem to be not inside one file, but in two files.
--
-- ref : SQLite manual 'SQL As Understood By SQLite' on http://www.sqlite.org/lang.html (ws # 20130703.1301)
-- ref : Thread 'Using SQLite how do I index columns in a CREATE TABLE statement?' on (# 20130703.1302)
--        http://stackoverflow.com/questions/1676448/using-sqlite-how-do-i-index-columns-in-a-create-table-statement
-- ref : SQLite manual 'SQL As Understood By SQLite - ATTACH DATABASE' on
--        on http://www.sqlite.org/lang_attach.html (# 20130703.1303)


-- ****************************************************************************
-- Create and fill table `addresses`
-- ****************************************************************************

DROP TABLE IF EXISTS `addresses`;
CREATE TABLE IF NOT EXISTS `addresses` (
  `id` bigint(19) NOT NULL,
  `personid` bigint(19) NOT NULL,
  `name1` varchar(64),
  `name2` varchar(64),
  `name3` varchar(64),
  `country` varchar(32),
  `zipcode` varchar(8),
  `city` varchar(64),
  `street` varchar(64),
  `phone` varchar(32),
  `email` varchar(64),
  `comment` text,
  PRIMARY KEY (`id`)
);

CREATE INDEX addresses_street ON `addresses` (`street`,`city`,`id`);
CREATE INDEX addresses_name ON `addresses` (`name2`,`id`);
CREATE INDEX addresses_city ON `addresses` (`city`,`street`,`id`);


REPLACE INTO `addresses` (`id`, `personid`, `name1`, `name2`, `name3`, `country`, `zipcode`, `city`, `street`, `phone`, `email`, `comment`) VALUES
   (20130703123201, 20130703123101, '', 'Joe Jackson', '', '', '91809', 'Wellheim', 'Burgstraße 23', '', '', ''),
   (20130703123102, 20130703123101, '', 'Joe''s Garage Limited', '', '', '91809', 'Wellheim', 'Burgstraße 21', '', '', ''),
   (20130703123103, 20130703123102, '', 'Deap Sea Travels', '', '', '17094', 'Burg Stargard', 'Quastenberger Damm 41', '', '', '');


-- ****************************************************************************
-- Create and fill table `countries`
-- ****************************************************************************


DROP TABLE IF EXISTS `countries`;
CREATE TABLE IF NOT EXISTS `countries` (
  `Id` bigint(19) NOT NULL,
  `Name` varchar(56),
  `Postalcode` varchar(4),
  `IsoAlphaTwo` varchar(4),
  `IsoAlphaThree` varchar(4),
  `IsoNumeric` int(8),
  `Iana` varchar(8),
  `Calling` varchar(8),
  `Comment` text,
  PRIMARY KEY (`Id`)
);


REPLACE INTO `countries` (`Id`, `Name`, `Postalcode`, `IsoAlphaTwo`, `IsoAlphaThree`, `IsoNumeric`, `Iana`, `Calling`, `Comment`) VALUES
	(20130703123301, 'Afghanistan', NULL, 'AF', 'AFG', 4, '.af', '93', NULL),
	(20130703123307, 'Germany', 'DE', 'DE', 'DEU', 276, '.de', '49', NULL),
	(20130703123304, 'Czech Republic', 'CZ', 'CZ', 'CZE', 203, '.cz', '357', NULL),
	(20130703123306, 'France', 'FR', 'FR', 'FRA', 250, '.fr', '33', NULL),
	(20130703123309, 'Netherlands', 'NL', 'NL', 'NLD', 528, '.nl', '31', NULL),
	(20130703123311, 'Swizerland', 'CH', 'CH', 'CHE', 756, '.ch', '41', NULL),
	(20130703123302, 'Austria', 'AT', 'AT', 'AUT', 40, '.at', '43', NULL),
	(20130703123310, 'Poland', 'PL', 'PL', 'POL', 616, '.pl', '48', NULL),
	(20130703123305, 'Denmark', 'DK', 'DK', 'DNK', 208, '.dk', '420', NULL),
	(20130703123303, 'Belgium', 'BE', 'BE', 'BEL', 56, '.be', '32', NULL),
	(20130703123308, 'Luxembourg', 'LU', 'LU', 'LUX', 442, '.lu', '352', NULL),
	(20130703123312, 'Zimbabwe', NULL, 'ZW', 'ZWE', 716, '.zw', '263', NULL);


-- ****************************************************************************
-- Create and fill table `emailaddresses`
-- ****************************************************************************


DROP TABLE IF EXISTS `emailaddresses`;
CREATE TABLE IF NOT EXISTS `emailaddresses` (
  `id` bigint(19) NOT NULL,
  `personid` bigint(19) NOT NULL,
  `emailaddress` varchar(96),
  `comment` text,
  PRIMARY KEY (`id`)
);

CREATE INDEX emailaddresses_emailaddress ON `emailaddresses` (`emailaddress`,`id`);


REPLACE INTO `emailaddresses` (`id`, `personid`, `emailaddress`, `comment`) VALUES
	(20130703123401, 20130703123101, 'joe@joejackson.lum', ''),
	(20130703123403, 20130703123102, 'mtaucher@www.caroline.lum', ''),
	(20130703123402, 20130703123101, 'info@joesgarage.lum', '');


-- ****************************************************************************
-- Create and fill table Letters
-- ****************************************************************************


DROP TABLE IF EXISTS `letters`;
CREATE TABLE IF NOT EXISTS `letters` (
  `id` bigint(19) NOT NULL,
  `personidfro` bigint(19) NOT NULL,
  `personidto` bigint(19),
  `lettertype` varchar(32),
  `sent` timestamp,
  `received` timestamp,
  `content` text,
  `comment` text,
  PRIMARY KEY (`id`)
);


REPLACE INTO `letters` (`id`, `personidfro`, `personidto`, `lettertype`, `sent`, `received`, `content`, `comment`) VALUES
	(20130703123601, 20130703123101, 20130703123102, 'email', '2013-07-03 21:30:20', '2013-07-02 12:28:02', 'Hello Deep Sea Travel,\r\n\r\nhow are you?\r\n\r\nBye,\r\nJoe\r\n', ''),
	(20130703123602, 20130703123102, 20130703123101, 'email', '2013-07-03 21:30:26', '2013-07-02 12:29:02', 'Hello Joe,\r\n\r\nwe are fine.\r\n\r\nBye,\r\nDeap Sea Travels', ''),
	(20130703123603, 20130703123102, 20130703123101, 'postletter', '2013-07-03 21:30:33', '2013-07-03 12:34:01', 'Hello Joe,\r\n\r\nwe like to order a book about submarines.\r\n\r\nBye,\r\nDeap Sea Travels', ''),
	(20130703123604, 20130703123101, 20130702123102, 'postletter', '2013-07-03 21:29:37', '2013-07-04 12:34:01', 'Hello Deap Sea Travels,\r\n\r\nOur book about submarines will be published next year.\r\n\r\nBye,\r\nJoe', '');


-- ****************************************************************************
-- Create and fill table `persons`
-- ****************************************************************************


DROP TABLE IF EXISTS `persons`;
CREATE TABLE IF NOT EXISTS `persons` (
  `id` bigint(19) NOT NULL,
  `title` varchar(64),
  `forename` varchar(64),
  `surname` varchar(64),
  `additionalname` varchar(64),
  `birthday` date,
  `comment` text,
  PRIMARY KEY (`id`)
);

CREATE INDEX persons_surename ON `persons` (`surname`,`forename`,`title`,`id`);


REPLACE INTO `persons` (`id`, `title`, `forename`, `surname`, `additionalname`, `birthday`, `comment`) VALUES
	(20130703123101, 'Herr', 'Jackson', 'Joe', NULL, NULL, NULL),
	(20130703123102, 'Reisebüro', 'Deap Sea Travels', '', NULL, NULL, NULL);


-- ****************************************************************************
-- Create and fill table `phones`
-- ****************************************************************************


DROP TABLE IF EXISTS `phones`;
CREATE TABLE IF NOT EXISTS `phones` (
  `id` bigint(19) NOT NULL,
  `personid` bigint(19) NOT NULL,
  `phonetype` varchar(8),
  `number` varchar(32),
  `comment` text,
  PRIMARY KEY (`id`)
);


REPLACE INTO `phones` (`id`, `personid`, `phonetype`, `number`, `comment`) VALUES
	(20130703123501, 20130703123101, 'landline', '0 (0987) 45678 98716-231', NULL),
	(20130703123502, 20130703123101, 'fax', '0 (0987) 45678 456790', NULL),
	(20130703123503, 20130703123101, 'landline', '0 (0987) 456789-301', NULL),
	(20130703123504, 20130703123101, 'fax', '0 (0987) 456789-302', NULL),
	(20130703123505, 20130703123102, 'mobile', '0', NULL);
