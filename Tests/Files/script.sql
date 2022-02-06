-- MySQL Script generated by MySQL Workbench

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema DFMS
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `DFMS` ;

-- -----------------------------------------------------
-- Schema DFMS
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `DFMS` DEFAULT CHARACTER SET utf8 ;
USE `DFMS` ;

-- -----------------------------------------------------
-- Table `DFMS`.`form.predefinied_field`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `DFMS`.`form.predefinied_field` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `add_login` VARCHAR(32) NOT NULL,
  `add_date` TIMESTAMP(4) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modif_login` VARCHAR(32) NULL,
  `modif_date` TIMESTAMP(4) NULL,
  `active` TINYINT NOT NULL DEFAULT 1,
  `base_definition_id` INT NOT NULL,
  `value_type_id` INT NOT NULL,
  `title` VARCHAR(64) NOT NULL,
  `global` TINYINT(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`),
  INDEX `fk_predefinied_field_base_definition_idx` (`base_definition_id` ASC),
  INDEX `fk_predefinied_field_value_type_idx` (`value_type_id` ASC),
  UNIQUE INDEX `title_UNIQUE` (`title` ASC),
  CONSTRAINT `fk_fpf_field_definition`
    FOREIGN KEY (`base_definition_id`)
    REFERENCES `DFMS`.`form.field_definition` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_fpf_value_type`
    FOREIGN KEY (`value_type_id`)
    REFERENCES `DFMS`.`form.value_type` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `DFMS`.`form.predefinied_field_option`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `DFMS`.`form.predefinied_field_option` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `add_login` VARCHAR(32) NOT NULL,
  `add_date` TIMESTAMP(4) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modif_login` VARCHAR(32) NULL,
  `modif_date` TIMESTAMP(4) NULL,
  `active` TINYINT NOT NULL DEFAULT 1,
  `predefinied_field_id` INT NOT NULL,
  `date` DATETIME NULL,
  `string` VARCHAR(4000) NULL,
  `boolean` TINYINT(1) NULL,
  `integer` INT NULL,
  `decimal` DECIMAL(9,3) NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_predefinied_option_field_idx` (`predefinied_field_id` ASC),
  CONSTRAINT `fk_fpfo_predefinied_field`
    FOREIGN KEY (`predefinied_field_id`)
    REFERENCES `DFMS`.`form.predefinied_field` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `DFMS`.`form.field_definition_value_type`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `DFMS`.`form.field_definition_value_type` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `add_login` VARCHAR(32) NOT NULL,
  `add_date` TIMESTAMP(4) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modif_login` VARCHAR(32) NULL,
  `modif_date` TIMESTAMP(4) NULL,
  `active` TINYINT NOT NULL DEFAULT 1,
  `field_definition_id` INT NOT NULL,
  `value_type_id` INT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_field_definition_value_type_idx` (`value_type_id` ASC),
  INDEX `fk_field_definition_value_type_field_idx` (`field_definition_id` ASC),
  CONSTRAINT `fk_ffdvt_value_type`
    FOREIGN KEY (`value_type_id`)
    REFERENCES `DFMS`.`form.value_type` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_ffdvt_field_definition`
    FOREIGN KEY (`field_definition_id`)
    REFERENCES `DFMS`.`form.field_definition` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `DFMS`.`form.field_definition`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `DFMS`.`form.field_definition` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `add_login` VARCHAR(32) NOT NULL,
  `add_date` TIMESTAMP(4) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modif_login` VARCHAR(32) NULL,
  `modif_date` TIMESTAMP(4) NULL,
  `active` TINYINT NOT NULL DEFAULT 1,
  `title` VARCHAR(64) NOT NULL,
  `code` VARCHAR(64) NOT NULL,
  `visible` TINYINT(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `name_UNIQUE` (`title` ASC),
  UNIQUE INDEX `type_UNIQUE` (`code` ASC))
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `DFMS`.`form.value_type`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `DFMS`.`form.value_type` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `add_login` VARCHAR(32) NOT NULL,
  `add_date` TIMESTAMP(4) NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modif_login` VARCHAR(32) NULL,
  `modif_date` TIMESTAMP(4) NULL,
  `active` TINYINT NOT NULL DEFAULT 1,
  `title` VARCHAR(64) NOT NULL,
  `code` VARCHAR(64) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `name_UNIQUE` (`code` ASC),
  UNIQUE INDEX `title_UNIQUE` (`title` ASC))
ENGINE = InnoDB;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;