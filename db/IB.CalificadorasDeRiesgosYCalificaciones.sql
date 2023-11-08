USE [IB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClienteTituloCalificado]') AND type in (N'U'))
DROP TABLE [dbo].[ClienteTituloCalificado]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReferenciaCapitalesRangos]') AND type in (N'U'))
DROP TABLE [dbo].[ReferenciaCapitalesRangos]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ReferenciaCapitales]') AND type in (N'U'))
DROP TABLE [dbo].[ReferenciaCapitales]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CalificadorasRiesgosCalificacion]') AND type in (N'U'))
DROP TABLE [dbo].[CalificadorasRiesgosCalificacion]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CalificadorasRiesgosPeriodo]') AND type in (N'U'))
DROP TABLE [dbo].[CalificadorasRiesgosPeriodo]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CalificadorasRiesgos]') AND type in (N'U'))
DROP TABLE [dbo].[CalificadorasRiesgos]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CalificacionesBCRACodigo]') AND type in (N'U'))
DROP TABLE [dbo].[CalificacionesBCRACodigo]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CalificacionesBCRA]') AND type in (N'U'))
DROP TABLE [dbo].[CalificacionesBCRA]
GO




CREATE TABLE [dbo].CalificacionesBCRA(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](250) not null,
	[Fecha_Inicio] [datetime2](7) not NULL,
	[PeriodoActivo] [bit] not null,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateAproved] [datetime2](7) NULL,
	[DateRemoved] [datetime2](7) NULL,
	[UserCreated] [nvarchar](50) NOT NULL,
	[UserAproved] [nvarchar](50) NULL,
	[UserRemoved] [nvarchar](50) NULL,
	[Comments] [nvarchar](255) NULL,
	[Version] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_CalificacionesBCRA] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].CalificacionesBCRA ADD  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].CalificacionesBCRA ADD  DEFAULT ('SYSTEM') FOR [UserCreated]
GO

ALTER TABLE [dbo].CalificacionesBCRA ADD  DEFAULT ((1)) FOR [Version]
GO

ALTER TABLE [dbo].CalificacionesBCRA ADD  DEFAULT ((0)) FOR [Status]
GO

CREATE TABLE [dbo].CalificacionesBCRACodigo(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Calificacion] [varchar](10) not null,
	[Calificaciones_BCRA_Id] [int] not NULL,
	[ValorNumerico][int] not null,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateAproved] [datetime2](7) NULL,
	[DateRemoved] [datetime2](7) NULL,
	[UserCreated] [nvarchar](50) NOT NULL,
	[UserAproved] [nvarchar](50) NULL,
	[UserRemoved] [nvarchar](50) NULL,
	[Comments] [nvarchar](255) NULL,
	[Version] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_CalificacionesBCRACodigo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].CalificacionesBCRACodigo ADD CONSTRAINT FK_CalificacionesBCRACodigo_Id_CalificacionesBCRA
FOREIGN KEY (Calificaciones_BCRA_Id) REFERENCES CalificacionesBCRA([Id])
GO

CREATE TABLE [dbo].[CalificadorasRiesgos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](255) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateAproved] [datetime2](7) NULL,
	[UserCreated] [nvarchar](50) NOT NULL,
	[UserAproved] [nvarchar](50) NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_CalificadorasRiesgos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CalificadorasRiesgos] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[CalificadorasRiesgos] ADD  DEFAULT ('SYSTEM') FOR [UserCreated]
GO

ALTER TABLE [dbo].[CalificadorasRiesgos] ADD  DEFAULT ((0)) FOR [Status]
GO

CREATE TABLE [dbo].CalificadorasRiesgosPeriodo(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CalificadoraRiesgosId] [int] not null,
	[FechaAlta] [datetime2](7) not null,
	[FechaBaja] [datetime2](7) not null,
	[FechaNotificacionAlta] [datetime2](7) not null,
	[FechaNotificacionBaja] [datetime2](7) not null,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateAproved] [datetime2](7) NULL,
	[DateRemoved] [datetime2](7) NULL,
	[UserCreated] [nvarchar](50) NOT NULL,
	[UserAproved] [nvarchar](50) NULL,
	[UserRemoved] [nvarchar](50) NULL,
	[Comments] [nvarchar](255) NULL,
	[Version] [int] NOT NULL,
	[Status] [int] NOT NULL,
CONSTRAINT [PK_CalificadorasRiesgosPeriodo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].CalificadorasRiesgosPeriodo ADD CONSTRAINT FK_CalificadorasRiesgosPeriodo_Id_CalificadorasRiesgos
FOREIGN KEY (CalificadoraRiesgosId) REFERENCES CalificadorasRiesgos([Id])
GO

CREATE TABLE [dbo].CalificadorasRiesgosCalificacion(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PeriodoId] [int] not null,
	[CalificacionCalificadora] [varchar](10) not null,
	[CalificacionesBcraCodigoId] [int] not null,
CONSTRAINT [PK_CalificadorasRiesgosCalificacion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].CalificadorasRiesgosCalificacion ADD CONSTRAINT FK_CalificadorasRiesgosCalificacion_Id_CalificadorasRiesgosPeriodo
FOREIGN KEY (PeriodoId) REFERENCES CalificadorasRiesgosPeriodo([Id])
GO

ALTER TABLE [dbo].CalificadorasRiesgosCalificacion ADD CONSTRAINT FK_CalificadorasRiesgosCalificacion_Id_CalificacionesBCRACodigo
FOREIGN KEY (CalificacionesBCRACodigoId) REFERENCES CalificacionesBCRACodigo([Id])
GO

CREATE TABLE [dbo].ReferenciaCapitales(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) not null,
	[Clave] [int] NOT NULL,
	[CalificacionBCRAId] [int] not null,
	[Fecha_Desde] [datetime2](7) not NULL,
	[Fecha_Hasta] [datetime2](7) not NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateAproved] [datetime2](7) NULL,
	[DateRemoved] [datetime2](7) NULL,
	[UserCreated] [nvarchar](50) NOT NULL,
	[UserAproved] [nvarchar](50) NULL,
	[UserRemoved] [nvarchar](50) NULL,
	[Comments] [nvarchar](255) NULL,
	[Version] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_ReferenciaCapitales] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].ReferenciaCapitales ADD CONSTRAINT FK_ReferenciaCapitales_Id_CalificacionesBCRA
FOREIGN KEY (CalificacionBCRAid) REFERENCES CalificacionesBCRA([Id])
GO

ALTER TABLE [dbo].ReferenciaCapitales ADD  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].ReferenciaCapitales ADD  DEFAULT ('SYSTEM') FOR [UserCreated]
GO

ALTER TABLE [dbo].ReferenciaCapitales ADD  DEFAULT ((1)) FOR [Version]
GO

ALTER TABLE [dbo].ReferenciaCapitales ADD  DEFAULT ((0)) FOR [Status]
GO

CREATE TRIGGER TRG_UpdateClaveReferenciaCapitales
ON [dbo].[ReferenciaCapitales]
AFTER INSERT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM INSERTED WHERE Clave = 0)
    BEGIN
		
		DECLARE @maxClave INT;
        SELECT @maxClave = ISNULL(MAX(Clave), 0) FROM [dbo].[ReferenciaCapitales];

        UPDATE c
        SET c.Clave = @maxClave + 1
        FROM [dbo].[ReferenciaCapitales] c
        INNER JOIN INSERTED i ON c.Id = i.Id
        WHERE i.Clave = 0;
    END
END;
GO

CREATE TABLE [dbo].ReferenciaCapitalesRangos(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Rango_Desde_Id] [int] not null,
	[Rango_Hasta_Id] [int] not null,
	[Referencia_Capitales_Id] [int] not null,
CONSTRAINT [PK_ReferenciaCapitalesRangos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].ReferenciaCapitalesRangos ADD CONSTRAINT FK_ReferenciaCapitalesRangos_Id_ReferenciaCapitales
FOREIGN KEY (Referencia_Capitales_Id) REFERENCES ReferenciaCapitales([Id])
GO

CREATE TABLE [dbo].[ClienteTituloCalificado](
    [Id] [int] NOT NULL IDENTITY(1,1),
    [TipoCalificado] [int] NOT NULL,
	[CalificadoraRiesgos] [int] NOT NULL,
    [IdentificacionClienteTitulo] [nchar](11) NOT NULL,
    [CalificacionBcra] [int] NOT NULL,
    [DateCreated] [datetime2](7) NOT NULL,
	[DateAproved] [datetime2](7) NULL,
	[UserCreated] [nvarchar](50) NOT NULL,
	[UserAproved] [nvarchar](50) NULL,
	[Comments] [nvarchar](255) NULL,
	[Status] [int] NOT NULL,
CONSTRAINT [PK_ClienteTituloCalificado] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[ClienteTituloCalificado] ADD  DEFAULT ((0)) FOR [Status]
GO

ALTER TABLE [dbo].[ClienteTituloCalificado] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[ClienteTituloCalificado] ADD  DEFAULT ('SYSTEM') FOR [UserCreated]
GO
