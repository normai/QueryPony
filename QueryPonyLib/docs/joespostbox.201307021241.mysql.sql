-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.1.44-community - MySQL Community Server (GPL)
-- Server OS:                    Win32
-- HeidiSQL version:             7.0.0.4053
-- Date/time:                    2013-07-03 21:37:10
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET FOREIGN_KEY_CHECKS=0 */;

-- Dumping database structure for joespostbox
DROP DATABASE IF EXISTS `joespostbox`;
CREATE DATABASE IF NOT EXISTS `joespostbox` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `joespostbox`;


-- Dumping structure for table joespostbox.addresses
DROP TABLE IF EXISTS `addresses`;
CREATE TABLE IF NOT EXISTS `addresses` (
  `id` bigint(19) unsigned NOT NULL COMMENT '(field # 20130703.1112)',
  `personid` bigint(19) unsigned NOT NULL COMMENT '(field # 20130703.1113)',
  `name1` varchar(64) DEFAULT NULL COMMENT '(field # 20130703.1114)',
  `name2` varchar(64) DEFAULT NULL COMMENT '(field # 20130703.1115)',
  `name3` varchar(64) DEFAULT NULL COMMENT '(field # 20130703.1116)',
  `country` bigint(32) unsigned DEFAULT NULL COMMENT '(field # 20130703.1117)',
  `zipcode` varchar(8) DEFAULT NULL COMMENT '(field # 20130703.1118)',
  `city` varchar(64) DEFAULT NULL COMMENT '(field # 20130703.1119)',
  `street` varchar(64) DEFAULT NULL COMMENT '(field # 20130703.1120)',
  `phone` varchar(32) DEFAULT NULL COMMENT '(field # 20130703.1121)',
  `email` varchar(64) DEFAULT NULL COMMENT '(field # 20130703.1122)',
  `comment` text COMMENT '(field # 20130703.1123)',
  PRIMARY KEY (`id`),
  KEY `Street` (`street`,`city`,`id`),
  KEY `Name` (`name2`,`id`),
  KEY `city` (`city`,`street`,`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COMMENT='(table # 20130703.1111)';

-- Dumping data for table joespostbox.addresses: 3 rows
/*!40000 ALTER TABLE `addresses` DISABLE KEYS */;
REPLACE INTO `addresses` (`id`, `personid`, `name1`, `name2`, `name3`, `country`, `zipcode`, `city`, `street`, `phone`, `email`, `comment`) VALUES
	(20130703123201, 20130702123101, '', 'Joe Jackson', '', 20130703123307, '91809', 'Wellheim', 'Burgstraße 23', '', '', ''),
	(20130703123102, 20130702123101, '', 'Joe\'s Garage Limited', '', 20130703123307, '91809', 'Wellheim', 'Burgstraße 21', '', '', ''),
	(20130703123103, 20130702123102, '', 'Deap Sea Travels', '', 20130703123307, '17094', 'Burg Stargard', 'Quastenberger Damm 41', '', '', '');
/*!40000 ALTER TABLE `addresses` ENABLE KEYS */;


-- Dumping structure for table joespostbox.countries
DROP TABLE IF EXISTS `countries`;
CREATE TABLE IF NOT EXISTS `countries` (
  `Id` bigint(19) unsigned NOT NULL COMMENT '(field # 20130703.1212)',
  `Name` varchar(56) NOT NULL COMMENT '(field # 20130703.1213)',
  `Postalcode` varchar(4) DEFAULT NULL COMMENT '(field # 20130703.1214)',
  `IsoAlphaTwo` varchar(4) DEFAULT NULL COMMENT '(field # 20130703.1215)',
  `IsoAlphaThree` varchar(4) DEFAULT NULL COMMENT '(field # 20130703.1216)',
  `IsoNumeric` int(8) unsigned DEFAULT NULL COMMENT '(field # 20130703.1217)',
  `Iana` varchar(8) DEFAULT NULL COMMENT '(field # 20130703.1218)',
  `Calling` varchar(8) DEFAULT NULL COMMENT '(field # 20130703.1219)',
  `Comment` text COMMENT '(field # 20130703.1220)'
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COMMENT='(table # 20130703.1211)';

-- Dumping data for table joespostbox.countries: 12 rows
/*!40000 ALTER TABLE `countries` DISABLE KEYS */;
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
/*!40000 ALTER TABLE `countries` ENABLE KEYS */;


-- Dumping structure for table joespostbox.emailaddresses
DROP TABLE IF EXISTS `emailaddresses`;
CREATE TABLE IF NOT EXISTS `emailaddresses` (
  `id` bigint(19) unsigned NOT NULL COMMENT '(field # 20130703.1152)',
  `personid` bigint(19) unsigned NOT NULL COMMENT '(field # 20130703.1153)',
  `emailaddress` varchar(96) DEFAULT NULL COMMENT '(field # 20130703.1154)',
  `comment` text COMMENT '(field # 20130703.1155)',
  PRIMARY KEY (`id`),
  KEY `Emailaddress` (`emailaddress`,`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COMMENT='(table # 20130702.1151)';

-- Dumping data for table joespostbox.emailaddresses: 3 rows
/*!40000 ALTER TABLE `emailaddresses` DISABLE KEYS */;
REPLACE INTO `emailaddresses` (`id`, `personid`, `emailaddress`, `comment`) VALUES
	(20130703123401, 20130703123101, 'joe@joejackson.lum', ''),
	(20130703123403, 20130703123102, 'mtaucher@www.caroline.lum', ''),
	(20130703123402, 20130703123101, 'info@joesgarage.lum', '');
/*!40000 ALTER TABLE `emailaddresses` ENABLE KEYS */;


-- Dumping structure for table joespostbox.letters
DROP TABLE IF EXISTS `letters`;
CREATE TABLE IF NOT EXISTS `letters` (
  `id` bigint(19) unsigned NOT NULL COMMENT '(field # 20130703.1202)',
  `personidfro` bigint(19) unsigned NOT NULL COMMENT '(field # 20130703.1203)',
  `personidto` bigint(19) unsigned DEFAULT NULL COMMENT '(field # 20130703.1204)',
  `lettertype` varchar(32) DEFAULT NULL COMMENT '(field # 20130703.1205)',
  `sent` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '(field # 20130703.1206)',
  `received` timestamp NULL DEFAULT '0000-00-00 00:00:00' COMMENT '(field # 20130703.1206)',
  `content` text COMMENT '(field # 20130703.1206)',
  `comment` text COMMENT '(field # 20130703.1207)'
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COMMENT='(table # 20130702.1201)';

-- Dumping data for table joespostbox.letters: 4 rows
/*!40000 ALTER TABLE `letters` DISABLE KEYS */;
REPLACE INTO `letters` (`id`, `personidfro`, `personidto`, `lettertype`, `sent`, `received`, `content`, `comment`) VALUES
	(20130703123601, 20130703123101, 20130703123102, 'email', '2013-07-03 21:30:20', '2013-07-02 12:28:02', 'Hello Deep Sea Travel,\r\n\r\nhow are you?\r\n\r\nBye,\r\nJoe\r\n', ''),
	(20130703123602, 20130703123102, 20130703123101, 'email', '2013-07-03 21:30:26', '2013-07-02 12:29:02', 'Hello Joe,\r\n\r\nwe are fine.\r\n\r\nBye,\r\nDeap Sea Travels', ''),
	(20130703123603, 20130703123102, 20130703123101, 'postletter', '2013-07-03 21:30:33', '2013-07-03 12:34:01', 'Hello Joe,\r\n\r\nwe like to order a book about submarines.\r\n\r\nBye,\r\nDeap Sea Travels', ''),
	(20130703123604, 20130703123101, 20130702123102, 'postletter', '2013-07-03 21:29:37', '2013-07-04 12:34:01', 'Hello Deap Sea Travels,\r\n\r\nOur book about submarines will be published next year.\r\n\r\nBye,\r\nJoe', '');
/*!40000 ALTER TABLE `letters` ENABLE KEYS */;


-- Dumping structure for table joespostbox.persons
DROP TABLE IF EXISTS `persons`;
CREATE TABLE IF NOT EXISTS `persons` (
  `id` bigint(19) unsigned NOT NULL COMMENT '(field # 20130703.1132)',
  `title` varchar(64) DEFAULT NULL COMMENT '(field # 20130703.1133)',
  `forename` varchar(64) DEFAULT NULL COMMENT '(field # 20130703.1134)',
  `surname` varchar(64) DEFAULT NULL COMMENT '(field # 20130703.1135)',
  `additionalname` varchar(64) DEFAULT NULL COMMENT '(field # 20130703.1136)',
  `birthday` date DEFAULT NULL COMMENT '(field # 20130703.1136)',
  `comment` text COMMENT '(field # 20130703.1137)',
  PRIMARY KEY (`id`),
  KEY `Surename` (`surname`,`forename`,`title`,`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COMMENT='(table # 20130702.1131)';

-- Dumping data for table joespostbox.persons: 2 rows
/*!40000 ALTER TABLE `persons` DISABLE KEYS */;
REPLACE INTO `persons` (`id`, `title`, `forename`, `surname`, `additionalname`, `birthday`, `comment`) VALUES
	(20130703123101, 'Herr', 'Jackson', 'Joe', NULL, NULL, NULL),
	(20130703123102, 'Reisebüro', 'Deap Sea Travels', '', NULL, NULL, NULL);
/*!40000 ALTER TABLE `persons` ENABLE KEYS */;


-- Dumping structure for table joespostbox.phones
DROP TABLE IF EXISTS `phones`;
CREATE TABLE IF NOT EXISTS `phones` (
  `id` bigint(19) unsigned NOT NULL COMMENT '(field # 20130703.1142)',
  `personid` bigint(19) unsigned NOT NULL COMMENT '(field # 20130703.1143)',
  `phonetype` varchar(8) DEFAULT NULL COMMENT '(field # 20130703.1143)',
  `number` varchar(32) DEFAULT NULL COMMENT '(field # 20130703.1144)',
  `comment` text COMMENT '(field # 20130703.1145)',
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COMMENT='(# 20130702.1141)';

-- Dumping data for table joespostbox.phones: 5 rows
/*!40000 ALTER TABLE `phones` DISABLE KEYS */;
REPLACE INTO `phones` (`id`, `personid`, `phonetype`, `number`, `comment`) VALUES
	(20130703123501, 20130703123101, 'landline', '0 (0987) 45678 98716-231', NULL),
	(20130703123502, 20130703123101, 'fax', '0 (0987) 45678 456790', NULL),
	(20130703123503, 20130703123101, 'landline', '0 (0987) 456789-301', NULL),
	(20130703123504, 20130703123101, 'fax', '0 (0987) 456789-302', NULL),
	(20130703123505, 20130703123102, 'mobile', '0', NULL);
/*!40000 ALTER TABLE `phones` ENABLE KEYS */;
/*!40014 SET FOREIGN_KEY_CHECKS=1 */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
