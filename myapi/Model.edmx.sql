




-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 05/21/2016 11:25:51
-- Generated from EDMX file: C:\Users\Владимир\documents\visual studio 2013\Projects\myapi\myapi\Model.edmx
-- Target version: 3.0.0.0
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `Instructors` DROP CONSTRAINT `FK_InstructorInstructorPosition`;
--    ALTER TABLE `WaterCrafts` DROP CONSTRAINT `FK_WaterCraftWatercraftType`;
--    ALTER TABLE `WaterCrafts` DROP CONSTRAINT `FK_WaterCraftWatercraftCondition`;
--    ALTER TABLE `RentJournalSet` DROP CONSTRAINT `FK_RentJournalClient`;
--    ALTER TABLE `RentJournalSet` DROP CONSTRAINT `FK_RentJournalWater`;
--    ALTER TABLE `RentJournalSet` DROP CONSTRAINT `FK_RentJournalPayMethod`;
--    ALTER TABLE `RentJournalSet` DROP CONSTRAINT `FK_RentJournalWaterCraft`;
--    ALTER TABLE `RentJournalSet` DROP CONSTRAINT `FK_RentJournalInstructionType`;
--    ALTER TABLE `RentJournalSet` DROP CONSTRAINT `FK_RentJournalInstructor`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Clients`;
    DROP TABLE IF EXISTS `Waters`;
    DROP TABLE IF EXISTS `PayMethods`;
    DROP TABLE IF EXISTS `InstructorPositions`;
    DROP TABLE IF EXISTS `Instructors`;
    DROP TABLE IF EXISTS `InstructionTypes`;
    DROP TABLE IF EXISTS `WatercraftTypes`;
    DROP TABLE IF EXISTS `WatercraftConditions`;
    DROP TABLE IF EXISTS `WaterCrafts`;
    DROP TABLE IF EXISTS `RentJournalSet`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

CREATE TABLE `Clients`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`FIO` varchar (250) NOT NULL, 
	`Passport` bigint NOT NULL);

ALTER TABLE `Clients` ADD PRIMARY KEY (Id);




CREATE TABLE `Waters`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (250) NOT NULL);

ALTER TABLE `Waters` ADD PRIMARY KEY (Id);




CREATE TABLE `PayMethods`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (250) NOT NULL);

ALTER TABLE `PayMethods` ADD PRIMARY KEY (Id);




CREATE TABLE `InstructorPositions`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (250) NOT NULL);

ALTER TABLE `InstructorPositions` ADD PRIMARY KEY (Id);




CREATE TABLE `Instructors`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`FIO` varchar (250) NOT NULL, 
	`InstructorPositionId` int NOT NULL);

ALTER TABLE `Instructors` ADD PRIMARY KEY (Id);




CREATE TABLE `InstructionTypes`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (250) NOT NULL);

ALTER TABLE `InstructionTypes` ADD PRIMARY KEY (Id);




CREATE TABLE `WatercraftTypes`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Type` varchar (250) NOT NULL);

ALTER TABLE `WatercraftTypes` ADD PRIMARY KEY (Id);




CREATE TABLE `WatercraftConditions`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Condition` varchar (250) NOT NULL);

ALTER TABLE `WatercraftConditions` ADD PRIMARY KEY (Id);




CREATE TABLE `WaterCrafts`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (250) NOT NULL, 
	`WatercraftTypeId` int NOT NULL, 
	`WatercraftConditionId` int NOT NULL, 
	`CostRate` decimal( 9, 2 )  NOT NULL);

ALTER TABLE `WaterCrafts` ADD PRIMARY KEY (Id);




CREATE TABLE `RentJournalSet`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ClientId` int NOT NULL, 
	`WaterId` int NOT NULL, 
	`PayMethodId` int NOT NULL, 
	`WaterCraftId` int NOT NULL, 
	`InstructionTypeId` int NOT NULL, 
	`InstructorId` int NOT NULL, 
	`Date` datetime NOT NULL, 
	`Cost` decimal( 9, 2 )  NOT NULL);

ALTER TABLE `RentJournalSet` ADD PRIMARY KEY (Id);






-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `InstructorPositionId` in table 'Instructors'

ALTER TABLE `Instructors`
ADD CONSTRAINT `FK_InstructorInstructorPosition`
    FOREIGN KEY (`InstructorPositionId`)
    REFERENCES `InstructorPositions`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_InstructorInstructorPosition'

CREATE INDEX `IX_FK_InstructorInstructorPosition` 
    ON `Instructors`
    (`InstructorPositionId`);

-- Creating foreign key on `WatercraftTypeId` in table 'WaterCrafts'

ALTER TABLE `WaterCrafts`
ADD CONSTRAINT `FK_WaterCraftWatercraftType`
    FOREIGN KEY (`WatercraftTypeId`)
    REFERENCES `WatercraftTypes`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_WaterCraftWatercraftType'

CREATE INDEX `IX_FK_WaterCraftWatercraftType` 
    ON `WaterCrafts`
    (`WatercraftTypeId`);

-- Creating foreign key on `WatercraftConditionId` in table 'WaterCrafts'

ALTER TABLE `WaterCrafts`
ADD CONSTRAINT `FK_WaterCraftWatercraftCondition`
    FOREIGN KEY (`WatercraftConditionId`)
    REFERENCES `WatercraftConditions`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_WaterCraftWatercraftCondition'

CREATE INDEX `IX_FK_WaterCraftWatercraftCondition` 
    ON `WaterCrafts`
    (`WatercraftConditionId`);

-- Creating foreign key on `ClientId` in table 'RentJournalSet'

ALTER TABLE `RentJournalSet`
ADD CONSTRAINT `FK_RentJournalClient`
    FOREIGN KEY (`ClientId`)
    REFERENCES `Clients`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RentJournalClient'

CREATE INDEX `IX_FK_RentJournalClient` 
    ON `RentJournalSet`
    (`ClientId`);

-- Creating foreign key on `WaterId` in table 'RentJournalSet'

ALTER TABLE `RentJournalSet`
ADD CONSTRAINT `FK_RentJournalWater`
    FOREIGN KEY (`WaterId`)
    REFERENCES `Waters`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RentJournalWater'

CREATE INDEX `IX_FK_RentJournalWater` 
    ON `RentJournalSet`
    (`WaterId`);

-- Creating foreign key on `PayMethodId` in table 'RentJournalSet'

ALTER TABLE `RentJournalSet`
ADD CONSTRAINT `FK_RentJournalPayMethod`
    FOREIGN KEY (`PayMethodId`)
    REFERENCES `PayMethods`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RentJournalPayMethod'

CREATE INDEX `IX_FK_RentJournalPayMethod` 
    ON `RentJournalSet`
    (`PayMethodId`);

-- Creating foreign key on `WaterCraftId` in table 'RentJournalSet'

ALTER TABLE `RentJournalSet`
ADD CONSTRAINT `FK_RentJournalWaterCraft`
    FOREIGN KEY (`WaterCraftId`)
    REFERENCES `WaterCrafts`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RentJournalWaterCraft'

CREATE INDEX `IX_FK_RentJournalWaterCraft` 
    ON `RentJournalSet`
    (`WaterCraftId`);

-- Creating foreign key on `InstructionTypeId` in table 'RentJournalSet'

ALTER TABLE `RentJournalSet`
ADD CONSTRAINT `FK_RentJournalInstructionType`
    FOREIGN KEY (`InstructionTypeId`)
    REFERENCES `InstructionTypes`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RentJournalInstructionType'

CREATE INDEX `IX_FK_RentJournalInstructionType` 
    ON `RentJournalSet`
    (`InstructionTypeId`);

-- Creating foreign key on `InstructorId` in table 'RentJournalSet'

ALTER TABLE `RentJournalSet`
ADD CONSTRAINT `FK_RentJournalInstructor`
    FOREIGN KEY (`InstructorId`)
    REFERENCES `Instructors`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RentJournalInstructor'

CREATE INDEX `IX_FK_RentJournalInstructor` 
    ON `RentJournalSet`
    (`InstructorId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
