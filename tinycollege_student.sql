-- MySQL dump 10.13  Distrib 8.0.41, for Win64 (x86_64)
--
-- Host: localhost    Database: tinycollege
-- ------------------------------------------------------
-- Server version	8.0.41

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `student`
--

DROP TABLE IF EXISTS `student`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `student` (
  `STU_NUM` varchar(10) NOT NULL,
  `DEPT_CODE` varchar(10) DEFAULT NULL,
  `STU_LNAME` varchar(50) DEFAULT NULL,
  `STU_FNAME` varchar(50) DEFAULT NULL,
  `STU_INITIAL` varchar(5) DEFAULT NULL,
  `STU_EMAIL` varchar(100) DEFAULT NULL,
  `PROF_NUM` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`STU_NUM`),
  KEY `DEPT_CODE` (`DEPT_CODE`),
  KEY `PROF_NUM` (`PROF_NUM`),
  CONSTRAINT `student_ibfk_1` FOREIGN KEY (`DEPT_CODE`) REFERENCES `department` (`DEPT_CODE`),
  CONSTRAINT `student_ibfk_2` FOREIGN KEY (`PROF_NUM`) REFERENCES `professor` (`PROF_NUM`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `student`
--

LOCK TABLES `student` WRITE;
/*!40000 ALTER TABLE `student` DISABLE KEYS */;
INSERT INTO `student` VALUES ('1','1','recto','janice','m','janice@mail.com','1'),('2','1','hernandez','james','p','james@mail.com','1'),('3','2','garcia','tristan','m','aba@mail.com','2'),('4','3','devera','paul','i','pol@mail.com','2'),('5','4','garcia','glen','i','glen@mail.com','3'),('6','4','decastro','brix','i','brix@mail.com','2');
/*!40000 ALTER TABLE `student` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-04-11 22:03:36
