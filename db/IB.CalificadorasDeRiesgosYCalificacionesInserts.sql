USE [IB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

delete from ClienteTituloCalificado;
DBCC CHECKIDENT('ClienteTituloCalificado', RESEED, 0)

delete from CalificadorasRiesgosCalificacion;
DBCC CHECKIDENT('CalificadorasRiesgosCalificacion', RESEED, 0)
delete from CalificadorasRiesgos;
DBCC CHECKIDENT('CalificadorasRiesgos', RESEED, 0)

delete from ReferenciaCapitalesRangos;
DBCC CHECKIDENT('ReferenciaCapitalesRangos', RESEED, 0)
delete from ReferenciaCapitales;
DBCC CHECKIDENT('ReferenciaCapitales', RESEED, 0)

delete from CalificacionesBCRACodigo;
DBCC CHECKIDENT('CalificacionesBCRACodigo', RESEED, 0)
delete from CalificacionesBCRA;
DBCC CHECKIDENT('CalificacionesBCRA', RESEED, 0)

INSERT INTO [dbo].CalificacionesBCRA
           ([Nombre]
           ,[Fecha_Inicio]
           ,[PeriodoActivo]
           ,[DateCreated]
           ,[DateAproved]
           ,[DateRemoved]
           ,[UserCreated]
           ,[UserAproved]
           ,[UserRemoved]
           ,[Comments]
           ,[Version]
           ,[Status])
     VALUES
           ('Version1'
           ,'2000-01-01'
           ,1
           ,'2023-08-10'
           ,NULL
           ,NULL
           ,'SYSTEM'
           ,'SYSTEM'
           ,NULL
           ,NULL
           ,1
           ,1)
GO

DECLARE @IdCalificacionesBCRA INT = @@IDENTITY;

INSERT INTO [dbo].[CalificacionesBCRACodigo]
           ([Calificacion]
           ,[Calificaciones_BCRA_Id]
           ,[ValorNumerico])
     VALUES
           ('AAA',@IdCalificacionesBCRA,1),
		   ('AA+',@IdCalificacionesBCRA,2),
		   ('AA',@IdCalificacionesBCRA,3),
		   ('AA-',@IdCalificacionesBCRA,4),
		   ('A+',@IdCalificacionesBCRA,5),
		   ('A',@IdCalificacionesBCRA,6),
		   ('A-',@IdCalificacionesBCRA,7),
		   ('BBB+',@IdCalificacionesBCRA,8),
		   ('BBB',@IdCalificacionesBCRA,9),
		   ('BBB-',@IdCalificacionesBCRA,10),
		   ('BB+',@IdCalificacionesBCRA,11),
		   ('BB',@IdCalificacionesBCRA,12),
		   ('BB-',@IdCalificacionesBCRA,13),
		   ('B+',@IdCalificacionesBCRA,14),
		   ('B',@IdCalificacionesBCRA,15),
		   ('B-',@IdCalificacionesBCRA,16),
		   ('CCC+',@IdCalificacionesBCRA,17),
		   ('CCC',@IdCalificacionesBCRA,18),
		   ('CCC-',@IdCalificacionesBCRA,19),
		   ('CC',@IdCalificacionesBCRA,20);

INSERT INTO [dbo].[ReferenciaCapitales]
           ([Nombre]
           ,[Clave]
           ,[CalificacionBCRAId]
           ,[Fecha_Desde]
           ,[Fecha_Hasta]
           ,[DateCreated]
           ,[DateAproved]
           ,[DateRemoved]
           ,[UserCreated]
           ,[UserAproved]
           ,[UserRemoved]
           ,[Comments]
           ,[Version]
           ,[Status])
     VALUES
           ('Versión 1'
           ,1
           ,@IdCalificacionesBCRA
           ,'2000-01-01'
           ,'2023-08-10'
           ,'2000-01-01'
           ,'2000-01-01'
           ,null
           ,'System'
           ,'System'
           ,null
           ,''
           ,1
           ,1)

DECLARE @IdReferenciaCapitales INT = @@IDENTITY;

INSERT INTO ReferenciaCapitalesRangos
		([Rango_Desde_Id], [Rango_Hasta_Id], [Referencia_Capitales_Id])
	VALUES
		(1, 7, @IdReferenciaCapitales), 
		(2, 7, @IdReferenciaCapitales),
		(3, 7, @IdReferenciaCapitales),
		(4, 7, @IdReferenciaCapitales),
		(5, 7, @IdReferenciaCapitales),
		(6, 7, @IdReferenciaCapitales),
		(7, 7, @IdReferenciaCapitales),
		(8, 10, @IdReferenciaCapitales),
		(9, 10, @IdReferenciaCapitales),
		(10, 10, @IdReferenciaCapitales),
		(11, 17, @IdReferenciaCapitales),
		(12, 17, @IdReferenciaCapitales),
		(13, 17, @IdReferenciaCapitales),
		(14, 17, @IdReferenciaCapitales),
		(15, 17, @IdReferenciaCapitales),
		(16, 17, @IdReferenciaCapitales),
		(17, 17, @IdReferenciaCapitales),
		(18, 20, @IdReferenciaCapitales),
		(19, 20, @IdReferenciaCapitales),
		(20, 20, @IdReferenciaCapitales);
GO