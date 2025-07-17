USE [PolyempaquesOT]
GO

/****** Object:  Table [dbo].[BitacoraDeCarga1]    Script Date: 17/07/2025 01:21:29 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BitacoraDeCarga1](
	[idBitCarga] [int] IDENTITY(1,1) NOT NULL,
	[idOdT] [int] NOT NULL,
	[mensaje] [varchar](150) NOT NULL,
	[idUsuario] [int] NOT NULL,
	[timestamp] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idBitCarga] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BitacoraDeCarga1] ADD  DEFAULT ((0)) FOR [idOdT]
GO

ALTER TABLE [dbo].[BitacoraDeCarga1] ADD  DEFAULT ((1)) FOR [idUsuario]
GO

