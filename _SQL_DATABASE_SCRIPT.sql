﻿USE [master]
GO
CREATE LOGIN Buy2SellUser WITH PASSWORD = 'Buy2SellLogin'
GO
CREATE DATABASE Buy2Sell
GO
USE Buy2Sell
GO
CREATE TABLE Brand(
	brd_Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	brd_Name VARCHAR(30) NOT NULL
)
GO
CREATE TABLE BrandAlias(
	ali_Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	ali_Alias VARCHAR(30) NOT NULL,
	brd_Id INT NOT NULL REFERENCES Brand(brd_Id)
)
GO
CREATE TABLE ItemGroup(
	grp_Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	grp_Name VARCHAR(30) NOT NULL
)
GO
CREATE TABLE ItemGroupSpecification(
	grpspec_Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	grpspec_Name VARCHAR(30) NOT NULL,
	grp_Id INT REFERENCES ItemGroup(grp_Id) NOT NULL
)
GO
CREATE TABLE StandardSpecification(
	stspec_Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	stspec_Name VARCHAR(30) NOT NULL
)
GO
CREATE TABLE Product(
	prd_Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	prd_Name VARCHAR(30) NULL,
	prd_ProductNumber VARCHAR(30) NOT NULL,
	prd_TypeNumber VARCHAR(30) NULL,
	prd_EAN_GLR VARCHAR(18) NULL,
	prd_UPC VARCHAR(12) NULL,
	prd_ProductText TEXT NULL,
	grp_Id INT NULL REFERENCES ItemGroup(grp_Id),
	brd_Id INT NULL REFERENCES Brand(brd_Id),
	spec_Json TEXT NULL
)
GO