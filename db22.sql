-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: localhost    Database: db22
-- ------------------------------------------------------
-- Server version	8.0.36

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
-- Table structure for table `category`
--

DROP TABLE IF EXISTS `category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `category` (
  `CategoryID` int NOT NULL AUTO_INCREMENT,
  `CategoryName` varchar(45) NOT NULL,
  PRIMARY KEY (`CategoryID`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `category`
--

LOCK TABLES `category` WRITE;
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
INSERT INTO `category` VALUES (1,'Увлажняющее'),(2,'Против акне'),(3,'Успокаивающее'),(4,'Заживляющее'),(5,'Скрабирующее');
/*!40000 ALTER TABLE `category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `client`
--

DROP TABLE IF EXISTS `client`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `client` (
  `ClientID` int NOT NULL AUTO_INCREMENT,
  `ClientSurname` varchar(45) NOT NULL,
  `ClientName` varchar(45) NOT NULL,
  `ClientPatronumic` varchar(45) NOT NULL,
  `ClientNumberPhone` varchar(20) NOT NULL,
  PRIMARY KEY (`ClientID`)
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `client`
--

LOCK TABLES `client` WRITE;
/*!40000 ALTER TABLE `client` DISABLE KEYS */;
INSERT INTO `client` VALUES (1,'Ковалевская','Юлия','Игоревна','79123456789'),(2,'Михайлова','Татьяна','Валерьевна','79134567890'),(3,'Сидоров','Алексей','Николаевич','79145678901'),(4,'Ширяев','Игорь','Александрович','79156789012'),(5,'Фролова','Анна','Дмитриевна','79167890123'),(6,'Тихонова','Светлана','Игоревна','79178901234'),(7,'Баранова','Марина','Юрьевна','79189012345'),(8,'Яковлева','Полина','Сергеевна','79190123456'),(9,'Лебедева','Дарья','Андреевна','79201234567'),(10,'Петрова','Виктория','Николаевна','79212345678'),(11,'Сафонов','Олег','Владимирович','79223456789'),(12,'Григорьев','Сергей','Анатольевич','79234567890'),(13,'Кузнецова','Екатерина','Валерьевна','79245678901'),(14,'Куликова','Наталья','Игоревна','79256789012'),(15,'Соловьев','Илья','Андреевич','79267890123'),(16,'Ермакова','Ольга','Владимировна','79278901234'),(17,'Лазарева','Елена','Сергеевна','79289012345'),(18,'Никифоров','Роман','Алексеевич','79290123456'),(19,'Зайцев','Алексей','Сергеевич','79301234567'),(20,'Федорова','Марина','Юрьевна','79312345678'),(21,'Костюков','Николай','Иванович','79323456789'),(22,'Степанова','Светлана','Владимировна','79334567890'),(23,'Шевцов','Артем','Валерьевич','79345678901'),(24,'Синицына','Татьяна','Игоревна','79356789012'),(25,'Левченко','Виктория','Николаевна','79367890123'),(26,'Киселева','Анна','Сергеевна','79378901234'),(27,'Чистяков','Роман','Анатольевич','79389012345'),(28,'Тарасова','Ольга','Валерьевна','79390123456'),(29,'Громов','Сергей','Ильич','79401234567'),(30,'Беловская','Ирина','Владимировна','79412345678'),(31,'Ковалев','Игорь','Андреевич','79423456789'),(32,'Мартынова','Дарья','Николаевна','79434567890'),(33,'Фролова','Юлия','Александровна','79445678901'),(34,'Сидорович','Алексей','Викторович','79456789012'),(35,'Петрова','Наталья','Сергеевна','79467890123'),(36,'Кузнецова','Оксана','Валерьевна','79478901234'),(37,'Лебедев','Павел','Игоревич','79489012345'),(38,'Ермакова','Светлана','Анатольевна','79490123456'),(39,'Сафонов','Илья','Николаевич','79501234567'),(40,'Яковлева','Татьяна','Владимировна','79512345678'),(41,'Григорьевна','Анна','Сергеевна','79523456789'),(42,'Никифоров','Виктор','Валентинович','79534567890'),(43,'Тихонов','Сергей','Анатольевич','79545678901'),(44,'Костина','Мария','Игоревна','79556789012'),(45,'Синицын','Роман','Владимирович','79567890123'),(46,'Лазарев','Алексей','Юрьевич','79578901234'),(47,'Федотова','Ольга','Алексеевна','79589012345'),(48,'Чистяков','Игорь','Сергеевич','79590123456'),(49,'Сидорова','Галина','Петровна','79601234567'),(50,'Баранов','Михаил','Александрович','79600234547');
/*!40000 ALTER TABLE `client` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `order`
--

DROP TABLE IF EXISTS `order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `order` (
  `OrderID` int NOT NULL AUTO_INCREMENT,
  `OrderStatus` varchar(45) DEFAULT NULL,
  `OrderDate` varchar(20) DEFAULT NULL,
  `OrderCompound` varchar(45) DEFAULT NULL,
  `OrderClient` int NOT NULL,
  `OrderUser` int NOT NULL,
  PRIMARY KEY (`OrderID`,`OrderClient`,`OrderUser`),
  KEY `fk_Order_Client1_idx` (`OrderClient`),
  KEY `fr_Order_User1_idx` (`OrderUser`),
  CONSTRAINT `fk_Order_Client1` FOREIGN KEY (`OrderClient`) REFERENCES `client` (`ClientID`),
  CONSTRAINT `user_order` FOREIGN KEY (`OrderUser`) REFERENCES `user` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `order`
--

LOCK TABLES `order` WRITE;
/*!40000 ALTER TABLE `order` DISABLE KEYS */;
INSERT INTO `order` VALUES (1,'Новый','17.11.2024','A1234, A1B2C',1,1);
/*!40000 ALTER TABLE `order` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orderproduct`
--

DROP TABLE IF EXISTS `orderproduct`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orderproduct` (
  `OrderID` int NOT NULL,
  `ProductArticleNumber` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  PRIMARY KEY (`OrderID`,`ProductArticleNumber`),
  KEY `ProductArticleNumber` (`ProductArticleNumber`),
  CONSTRAINT `orderproduct_ibfk_1` FOREIGN KEY (`OrderID`) REFERENCES `order` (`OrderID`),
  CONSTRAINT `orderproduct_ibfk_2` FOREIGN KEY (`ProductArticleNumber`) REFERENCES `product` (`ProductArticleNumber`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orderproduct`
--

LOCK TABLES `orderproduct` WRITE;
/*!40000 ALTER TABLE `orderproduct` DISABLE KEYS */;
INSERT INTO `orderproduct` VALUES (1,'A1234'),(1,'A1B2C');
/*!40000 ALTER TABLE `orderproduct` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `product` (
  `ProductArticleNumber` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `ProductName` text,
  `ProductDescription` text,
  `ProductPhoto` varchar(20) DEFAULT NULL,
  `ProductCost` int DEFAULT NULL,
  `ProductQuantityInStock` int DEFAULT NULL,
  `ProductCategory` int NOT NULL,
  `ProductWight` int DEFAULT NULL,
  PRIMARY KEY (`ProductArticleNumber`,`ProductCategory`),
  KEY `fk_product_category1_idx1` (`ProductCategory`),
  CONSTRAINT `fk_product_category1` FOREIGN KEY (`ProductCategory`) REFERENCES `category` (`CategoryID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product`
--

LOCK TABLES `product` WRITE;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
INSERT INTO `product` VALUES ('A1234',' Лаванда и мед ',' Успокаивающее мыло с ароматом лаванды и натуральным медом, увлажняет и расслабляет.',NULL,80,10,1,150),('A1B2C',' Лавровый лист ',' Мыло с экстрактом лаврового листа, дарующее успокаивающий эффект.',NULL,50,10,3,150),('B5678',' Цитрусовый заряд ',' Освежающее мыло с экстрактами лимона и апельсина, придающее бодрость и энергию.',NULL,80,10,1,150),('B9C0D',' Гречка и мед ',' Питательное мыло с гречкой и медом, идеально для увлажнения кожи.',NULL,50,10,4,150),('C7D8E',' Сказочный лес ',' Мыло с древесными нотами и экстрактами лесных трав, дарующее свежесть.',NULL,50,10,5,150),('C9101',' Шоколадный рай ',' Нежное мыло с добавлением какао и масла ши, оставляющее на коже сладкий аромат.',NULL,80,10,1,150),('D1121',' Морская свежесть ',' Мыло с морской солью и экстрактом водорослей, очищает и тонизирует кожу.',NULL,80,10,1,150),('D3E4F',' Огуречная свежесть ',' Освежающее мыло с огуречным экстрактом, идеально для лета.',NULL,50,10,3,150),('E1F2G',' Кокос и ананас ',' Тропическое мыло с ароматами кокоса и ананаса, создающее атмосферу отпуска.',NULL,50,10,4,150),('E3141',' Розовая мечта ',' Ароматное мыло с экстрактом розы и маслами, дарующее ощущение романтики и нежности.',NULL,80,10,1,150),('F5161',' Кофейное наслаждение ',' Мыло с молотым кофе, отшелушивает кожу и придаёт ей свежесть.',NULL,80,10,1,150),('F9G0H',' Спелая груша ',' Нежное мыло с ароматом груши, создающее атмосферу уюта.',NULL,50,10,5,100),('G5H6I',' Фиалковое утро ',' Нежное мыло с ароматом фиалок, создающее атмосферу весны.',NULL,50,10,3,100),('H3I4J',' Леденец из смородины ',' Нежное мыло с ароматом смородины, дарующее сладкое настроение.',NULL,50,10,4,100),('I1J2K',' Зимний вечер ',' Мыло с ароматом корицы и гвоздики, создающее тепло и уют.',NULL,50,10,5,100),('I2232',' Кокосовый рай ',' Мыло с кокосовым молоком и маслом, идеально увлажняет и питает кожу.',NULL,80,10,1,100),('J3242',' Мелисса и мята ',' Освежающее мыло с экстрактами мелиссы и мяты, придающее ощущение прохлады.',NULL,80,10,1,100),('J7K8L',' Шоколадный мятный коктейль ',' Мыло с ароматом шоколада и мяты, дарующее сладкое наслаждение.',NULL,50,10,4,100),('K4252',' Череда и ромашка ',' Успокаивающее мыло с экстрактами череды и ромашки, идеально для чувствительной кожи.',NULL,80,10,2,100),('K5L6M',' Сладкая дыня ',' Освежающее мыло с экстрактом дыни, идеально для летнего ухода.',NULL,50,10,4,100),('L3M4N',' Тайна розового масла ',' Нежное мыло с экстрактом розового масла, придающее коже мягкость.',NULL,50,10,5,150),('L5262',' Тропический микс ',' Мыло с экзотическими фруктами, дарующее яркий аромат и увлажнение.',NULL,80,10,2,150),('M6272',' Сладкая корица ',' Теплое мыло с ароматом корицы, создающее атмосферу домашнего уюта.',NULL,80,10,2,150),('M9N0O',' Крем - брюле',' Нежное мыло с ароматом крем-брюле, создающее уютное настроение.',NULL,50,10,4,150),('N7O8P',' Лесная ягода ',' Мыло с экстрактами лесных ягод, дарующее яркий и насыщенный аромат.',NULL,50,10,5,150),('O5P6Q',' Летний вечер ',' Освежающее мыло с нотами цитрусовых и зелени, идеально для теплого времени года.',NULL,50,10,5,100),('O8292',' Лимонный бальзам ',' Освежающее мыло с экстрактом лимонного бальзама, помогает взбодриться.',NULL,80,10,2,100),('P1Q2R',' Лаванда и шалфей ',' Успокаивающее мыло с экстрактами лаванды и шалфея, идеальное для вечернего ухода.',NULL,50,10,4,100),('P9303',' Ароматный сад ',' Мыло с экстрактами различных цветов, дарующее ощущение прогулки по саду.',NULL,80,10,2,100),('Q9R0S',' Карамельный сюрприз ',' Нежное мыло с ароматом карамели, создающее атмосферу праздника.',NULL,50,10,5,100),('R1323',' Сосновый лес ',' Мыло с экстрактами хвои и сосны, создающее ощущение свежести леса.',NULL,80,10,2,100),('R7S8T',' Сказочный мандарин ',' Яркое мыло с ароматом мандарина, дарующее радость и позитив.',NULL,50,10,5,100),('S2333',' Клубничное удовольтвие ',' Нежное мыло с экстрактом клубники, дарующее сладкий аромат и увлажнение.',NULL,80,10,2,150),('S3T4U',' Тайна леса ',' Мыло с древесными нотами и экстрактами лесных трав, дарующее свежесть.',NULL,50,10,4,150),('T1U2V',' Нежный йогурт ',' Питательное мыло с добавлением йогурта, увлажняющее и смягчающее кожу.',NULL,50,10,5,150),('T3343',' Энергия чая ',' Мыло с экстрактом зеленого чая, известное своими антиоксидантными свойствами.',NULL,80,10,3,150),('U4353',' Шиповник и жасмин ',' Ароматное мыло, которое сочетает в себе нотки шиповника и жасмина.',NULL,80,10,3,150),('V5363',' Кедр и сандал ',' Мыло с древесными нотами кедра и сандала, создающее атмосферу спокойствия.',NULL,80,10,3,150),('V5W6X',' Морозная малина ',' Яркое мыло с экстрактом малины, дарующее сладкий и освежающий аромат.',NULL,50,10,4,150),('W3X4Y',' Сладкий миндаль ',' Мыло с ароматом миндаля, дарующее нежность и комфорт.',NULL,50,10,5,150),('W6373',' Лимонный пирог ',' Нежное мыло с ароматом лимонного пирога, дарящее сладкое настроение.',NULL,80,10,3,100),('X7383',' Молоко и мед ',' Питательное мыло с молоком и медом, идеально для увлажнения кожи.',NULL,80,10,3,100),('Y7Z8A',' Солнечный апельсин ',' Мыло с ароматом апельсина, наполняющее энергией и радостью.',NULL,50,10,4,100),('Y8393',' Персиковая нежность ',' Мыло с экстрактом персика, дарующее мягкость и свежесть.',NULL,50,10,3,100),('Z5A6B',' Теплый каштан ',' Мыло с экстрактом каштана, создающее уютное и теплое настроение.',NULL,50,10,5,100),('Z9404',' Чайное дерево ',' Мыло с маслом чайного дерева, известное своими антисептическими свойствами.',NULL,50,10,3,100);
/*!40000 ALTER TABLE `product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `role` (
  `RoleID` int NOT NULL AUTO_INCREMENT,
  `RoleName` varchar(100) NOT NULL,
  PRIMARY KEY (`RoleID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role`
--

LOCK TABLES `role` WRITE;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` VALUES (1,'Администратор'),(2,'Продавец');
/*!40000 ALTER TABLE `role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `UserID` int NOT NULL AUTO_INCREMENT,
  `UserSurname` varchar(100) NOT NULL,
  `UserName` varchar(100) NOT NULL,
  `UserPatronymic` varchar(100) NOT NULL,
  `UserLogin` text NOT NULL,
  `UserPassword` text NOT NULL,
  `UserRole` int NOT NULL,
  PRIMARY KEY (`UserID`),
  KEY `UserRole` (`UserRole`),
  CONSTRAINT `user_ibfk_1` FOREIGN KEY (`UserRole`) REFERENCES `role` (`RoleID`)
) ENGINE=InnoDB AUTO_INCREMENT=201 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'Иванов','Иван','Иванович','manager','manager',1),(2,'Петров','Петр','Петрович','admin','admin',1),(3,'Сидоров','Сидор','Сидорович','user3','HelloWorld789#',1),(4,'Смирнова','Мария','Ивановна','user4','TestUser',1),(5,'Кузнецов','Алексей','Сергеевич','root','root',2),(6,'Попова','Анна','Викторовна','user6','SampleLogin345^',2),(7,'Васильев','Дмитрий','Андреевич','user7','RandomUser',1),(8,'Сергеева','Ольга','Николаевна','user8','ExamplePass901*',1),(9,'Михайлов','Михаил','Валерьевич','user9','SecureLogin234(',1),(10,'Федорова','Екатерина','Александровна','user10','StrongPass567)',2),(11,'Новиков','Андрей','Владимирович','user11','UserName111!',2),(12,'Морозова','Наталья','Петровна','user12','PassWord222@',2),(13,'Лебедев','Сергей','Юрьевич','user13','LoginUser',1),(14,'Соколова','Татьяна','Дмитриевна','user14','Test1234$',1),(15,'Ковалев','Николай','Викторович','user15','Hello12345%',2),(16,'Григорьева','Светлана','Анатольевна','user16','RandomPass678^',2),(17,'Степанов','Артем','Сергеевич','user17','ExampleUser',2),(18,'Алексеева','Юлия','Михайловна','user18','SecurePass234*',2),(19,'Тихонов','Денис','Александрович','user19','UserTest567(',1),(20,'Павлова','Валентина','Игоревна','user20','StrongUser',1),(21,'Баранов','Илья','Васильевич','user21','SampleLogin135!',1),(22,'Орлова','Дарья','Сергеевна','user22','Password246@',1),(23,'Фролов','Виктор','Викторович','user23','TestUser',1),(24,'Костина','Лариса','Андреевна','user24','MySecretPass468$',2),(25,'Яковлев','Роман','Владимирович','user25','RandomUser',1),(26,'Зайцева','Оксана','Николаевна','user26','ExamplePass680^',2),(27,'Киселев','Владислав','Сергеевич','user27','SecureLogin791&',1),(28,'Сафонова','Нина','Дмитриевна','user28','StrongPass802*',2),(29,'Ермаков','Кирилл','Алексеевич','user29','UserName913(',2),(30,'Громова','Надежда','Васильевна','user30','PassWord024)',1),(31,'Лазарев','Константин','Артемович','user31','LoginUser',1),(32,'Мельникова','Ирина','Юрьевна','user32','Test45678@',1),(33,'Тарасов','Валерий','Иванович','user33','Hello98765#',1),(34,'Шевченко','Светлана','Валерьевна','user34','RandomPass543$',1),(35,'Куликов','Алексей','Николаевич','user35','ExampleUser',1),(36,'Соловьева','Виктория','Юрьевна','user36','SecurePass321^',2),(37,'Романов','Сергей','Александрович','user37','UserTest432&',2),(38,'Кузьмина','Галина','Игоревна','user38','StrongUser',2),(39,'Климов','Дмитрий','Валентинович','user39','SampleLogin654(',2),(40,'Чистякова','Елена','Олеговна','user40','Password765)',2),(41,'Левин','Ярослав','Владимирович','user41','TestUser',1),(42,'Никифорова','Анна','Сергеевна','user42','MySecretPass987@',2),(43,'Синицын','Артем','Валерьевич','user43','RandomUser',1),(44,'Золотарева','Надежда','Алексеевна','user44','ExamplePass109$',2),(45,'Белов','Михаил','Ильич','user45','SecureLogin210%',1),(46,'Костенко','Татьяна','Сергеевна','user46','StrongPass321^',1),(47,'Мартынов','Николай','Владимирович','user47','UserName432&',2),(48,'Гусева','Ольга','Валерьевна','user48','PassWord543*',1),(49,'Федотов','Роман','Сергеевич','user49','LoginUser',2),(50,'Сидорова','Елена','Александровна','user50','Test98765)',2);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-12-02  0:03:57
