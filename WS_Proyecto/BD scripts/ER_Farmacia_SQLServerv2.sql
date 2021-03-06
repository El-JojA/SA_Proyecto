USE [SA_Proyecto]
GO
/****** Object:  Table [dbo].[Callcenter]    Script Date: 31/12/2015 12:22:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Callcenter](
	[id_callcenter] [int] IDENTITY(1,1) NOT NULL,
	[nombre_callcenter] [nvarchar](100) NOT NULL,
	[telefono_callcenter] [nvarchar](20) NULL,
	[estado] [tinyint] NULL,
 CONSTRAINT [Callcenter_PK] PRIMARY KEY CLUSTERED 
(
	[id_callcenter] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 31/12/2015 12:22:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[id_cliente] [int] IDENTITY(1,1) NOT NULL,
	[nombre_cliente] [nvarchar](100) NOT NULL,
	[apellido_cliente] [nvarchar](100) NOT NULL,
	[telefono_cliente] [nvarchar](20) NULL,
	[estado] [tinyint] NULL,
 CONSTRAINT [Cliente_PK] PRIMARY KEY CLUSTERED 
(
	[id_cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Detalle_Factura]    Script Date: 31/12/2015 12:22:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Detalle_Factura](
	[id_detalle_factura] [int] IDENTITY(1,1) NOT NULL,
	[id_producto_fk] [int] NOT NULL,
	[id_factura_fk] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[precio_unidad] [decimal](28, 2) NOT NULL,
	[cantidad_total] [decimal](28, 2) NULL,
 CONSTRAINT [Detalle_Factura_PK] PRIMARY KEY CLUSTERED 
(
	[id_detalle_factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Empleado]    Script Date: 31/12/2015 12:22:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleado](
	[id_empleado] [int] IDENTITY(1,1) NOT NULL,
	[nombre_empleado] [nvarchar](100) NULL,
	[apellido_empleado] [nvarchar](100) NOT NULL,
	[tipo_empleado] [int] NOT NULL,
	[estado] [tinyint] NULL,
 CONSTRAINT [Empleado_PK] PRIMARY KEY CLUSTERED 
(
	[id_empleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Envio]    Script Date: 31/12/2015 12:22:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Envio](
	[id_envio] [int] IDENTITY(1,1) NOT NULL,
	[id_factura_fk] [int] NOT NULL,
	[id_callcenter_fk] [int] NOT NULL,
	[direccion_envio] [nvarchar](255) NOT NULL,
	[estado] [tinyint] NULL,
 CONSTRAINT [Envio_PK] PRIMARY KEY CLUSTERED 
(
	[id_envio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Factura]    Script Date: 31/12/2015 12:22:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factura](
	[id_factura] [int] IDENTITY(1,1) NOT NULL,
	[fecha_factura] [datetime] NULL,
	[id_cliente_fk] [int] NOT NULL,
	[id_farmacia_fk] [int] NOT NULL,
	[id_empleado_fk] [int] NOT NULL,
	[estado] [tinyint] NULL,
 CONSTRAINT [Factura_PK] PRIMARY KEY CLUSTERED 
(
	[id_factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Producto]    Script Date: 31/12/2015 12:22:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[id_producto] [int] IDENTITY(1,1) NOT NULL,
	[nombre_producto] [nvarchar](100) NULL,
	[disponible_producto] [int] NOT NULL,
	[img_producto] [nvarchar](max) NULL,
	[precio_producto] [decimal](28, 2) NOT NULL,
	[estado] [tinyint] NULL DEFAULT ((1)),
 CONSTRAINT [Producto_PK] PRIMARY KEY CLUSTERED 
(
	[id_producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tienda]    Script Date: 31/12/2015 12:22:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tienda](
	[id_tienda] [int] IDENTITY(1,1) NOT NULL,
	[nombre_tienda] [nvarchar](100) NOT NULL,
	[telefono_tienda] [nvarchar](20) NULL,
	[direccion_tienda] [nvarchar](200) NULL,
	[estado] [tinyint] NULL,
 CONSTRAINT [Tienda_PK] PRIMARY KEY CLUSTERED 
(
	[id_tienda] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Callcenter] ADD  DEFAULT ((1)) FOR [estado]
GO
ALTER TABLE [dbo].[Cliente] ADD  DEFAULT ((1)) FOR [estado]
GO
ALTER TABLE [dbo].[Empleado] ADD  DEFAULT ((1)) FOR [estado]
GO
ALTER TABLE [dbo].[Envio] ADD  DEFAULT ((1)) FOR [estado]
GO
ALTER TABLE [dbo].[Factura] ADD  DEFAULT ((1)) FOR [estado]
GO
ALTER TABLE [dbo].[Tienda] ADD  DEFAULT ((1)) FOR [estado]
GO
ALTER TABLE [dbo].[Detalle_Factura]  WITH CHECK ADD  CONSTRAINT [Detalle_Factura_Factura_FK] FOREIGN KEY([id_factura_fk])
REFERENCES [dbo].[Factura] ([id_factura])
GO
ALTER TABLE [dbo].[Detalle_Factura] CHECK CONSTRAINT [Detalle_Factura_Factura_FK]
GO
ALTER TABLE [dbo].[Detalle_Factura]  WITH CHECK ADD  CONSTRAINT [Detalle_Factura_Producto_FK] FOREIGN KEY([id_producto_fk])
REFERENCES [dbo].[Producto] ([id_producto])
GO
ALTER TABLE [dbo].[Detalle_Factura] CHECK CONSTRAINT [Detalle_Factura_Producto_FK]
GO
ALTER TABLE [dbo].[Envio]  WITH CHECK ADD  CONSTRAINT [Envio_Callcenter_FK] FOREIGN KEY([id_callcenter_fk])
REFERENCES [dbo].[Callcenter] ([id_callcenter])
GO
ALTER TABLE [dbo].[Envio] CHECK CONSTRAINT [Envio_Callcenter_FK]
GO
ALTER TABLE [dbo].[Envio]  WITH CHECK ADD  CONSTRAINT [Envio_Factura_FK] FOREIGN KEY([id_factura_fk])
REFERENCES [dbo].[Factura] ([id_factura])
GO
ALTER TABLE [dbo].[Envio] CHECK CONSTRAINT [Envio_Factura_FK]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [Factura_Cliente_FK] FOREIGN KEY([id_cliente_fk])
REFERENCES [dbo].[Cliente] ([id_cliente])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [Factura_Cliente_FK]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [Factura_Empleado_FK] FOREIGN KEY([id_empleado_fk])
REFERENCES [dbo].[Empleado] ([id_empleado])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [Factura_Empleado_FK]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [Factura_Tienda_FK] FOREIGN KEY([id_farmacia_fk])
REFERENCES [dbo].[Tienda] ([id_tienda])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [Factura_Tienda_FK]
GO
/****** Object:  StoredProcedure [dbo].[Producto_Insert]    Script Date: 31/12/2015 12:22:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Producto_Insert]
	@nombre as varchar(100),
	@disponible as int,
	@img as varchar(200), 
	@precio as Decimal(28)
AS
BEGIN
	INSERT INTO Producto (nombre_producto, disponible_producto, img_producto, precio_producto)
	values (@nombre, @disponible, @img, @precio)
END

GO
/****** Object:  StoredProcedure [dbo].[Producto_Update]    Script Date: 31/12/2015 12:22:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Producto_Update]
	@Id as int,
	@nombre as varchar(100) = null,
	@disponible as int = null,
	@img as varchar(200) = null, 
	@precio as Decimal(28) = null
AS
BEGIN
	UPDATE Producto 
	SET nombre_producto = COALESCE(@nombre,nombre_producto) ,
	disponible_producto = COALESCE(@disponible, disponible_producto),
	img_producto = COALESCE(@img, img_producto),
	precio_producto = COALESCE(@precio,precio_producto)
	WHERE id_producto = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[Prueba]    Script Date: 31/12/2015 12:22:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Prueba] 
	-- Add the parameters for the stored procedure here
	@id as int
AS
BEGIN
	SELECT *
	FROM Cliente
	WHERE id_cliente = @id
END

GO
