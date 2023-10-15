﻿Create View [Code_v1].[CustomerInfo]
As
Select	C.[CustomerId] As [Id],
		Cast(C.[CustomerKey] As uniqueidentifier) As [Key],
		C.[FirstName], 
		C.[MiddleName], 
		C.[LastName], 
		C.[BirthDate], 
		C.[GenderId],
		C.CustomerTypeId,
		C.[CreatedDate], 
		C.[ModifiedDate]
From	[Customer].[Customer] C
Join	[Customer].[CustomerType] CT On C.CustomerTypeId = CT.CustomerTypeId
