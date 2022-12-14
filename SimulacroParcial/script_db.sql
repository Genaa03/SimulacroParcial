CREATE DATABASE recetas_db2


USE recetas_db2

CREATE TABLE [dbo].[Recetas](
	[id_receta] [int] NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[cheff] [varchar](100) NULL,
	[tipo_receta] [int] NOT NULL,
 CONSTRAINT [PK_Recetas] PRIMARY KEY CLUSTERED
(
	[id_receta] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Ingredientes](
	[id_ingrediente] [int] NOT NULL,
	[n_ingrediente] [varchar](50) NOT NULL,
	[unidad_medida] [varchar](50) NULL,
 CONSTRAINT [PK_Ingredientes] PRIMARY KEY CLUSTERED
(
	[id_ingrediente] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[Detalles_Receta]
(
    id_detalle       int identity(1,1) not null,
    [id_receta]      [int] NOT NULL,
    [id_ingrediente] [int] NOT NULL,
    [cantidad]       int   NOT NULL,
    constraint pk_detalles primary key (id_detalle)
)







CREATE PROCEDURE [dbo].[SP_CONSULTAR_INGREDIENTES]
AS
BEGIN

	SELECT * from Ingredientes ORDER BY 2;
END
go

CREATE PROCEDURE [dbo].[SP_INSERTAR_DETALLES]
	@id_receta int,
	@id_ingrediente int,
	@cantidad int
AS
BEGIN
	INSERT INTO DETALLES_RECETA(id_receta,id_ingrediente,cantidad)
    VALUES (@id_receta, @id_ingrediente, @cantidad);

END
go

CREATE PROCEDURE SP_INSERTAR_RECETA
    @id_receta int,
	@tipo_receta int,
	@nombre varchar(50),
	@cheff varchar(100)
AS
BEGIN
	INSERT INTO Recetas (id_receta, nombre, cheff , tipo_receta)
    VALUES (@id_receta, @nombre, @cheff, @tipo_receta );


END
go

CREATE PROCEDURE [dbo].[SP_PROXIMO_ID]
	@next int output
AS
BEGIN
	SET @next = (SELECT MAX(id_receta)+1  FROM Recetas);

END
go

INSERT [dbo].[Ingredientes] ([id_ingrediente], [n_ingrediente], [unidad_medida]) VALUES (1, N'Sal', N'gramos')
INSERT [dbo].[Ingredientes] ([id_ingrediente], [n_ingrediente], [unidad_medida]) VALUES (2, N'Pimienta', N'gramos')
INSERT [dbo].[Ingredientes] ([id_ingrediente], [n_ingrediente], [unidad_medida]) VALUES (3, N'Agua', N'cm3')
INSERT [dbo].[Ingredientes] ([id_ingrediente], [n_ingrediente], [unidad_medida]) VALUES (4, N'Aceite', N'cm3')
INSERT [dbo].[Ingredientes] ([id_ingrediente], [n_ingrediente], [unidad_medida]) VALUES (5, N'carne', N'gramos')
INSERT [dbo].[Ingredientes] ([id_ingrediente], [n_ingrediente], [unidad_medida]) VALUES (6, N'caldo', N'cm3')
INSERT [dbo].[Ingredientes] ([id_ingrediente], [n_ingrediente], [unidad_medida]) VALUES (7, N'Azucar', N'gramos')

INSERT [dbo].[Recetas] ([id_receta], [nombre], [cheff], [tipo_receta]) VALUES (1, N'TEST', N'Test Cheff', 2)


