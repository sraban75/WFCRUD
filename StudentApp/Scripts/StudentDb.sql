USE [master]
GO
/****** Object:  Database [StudentDb]    Script Date: 3/9/2019 8:40:06 AM ******/
CREATE DATABASE [StudentDb]
 
USE [StudentDb]
GO
/****** Object:  Table [dbo].[Districts]    Script Date: 3/9/2019 8:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Districts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Districts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Students]    Script Date: 3/9/2019 8:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[SubDistrictId] [int] NOT NULL,
	[RegNo] [nvarchar](50) NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SubDistricts]    Script Date: 3/9/2019 8:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubDistricts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DistrictId] [int] NOT NULL,
 CONSTRAINT [PK_SubDistricts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[VW_GetStudentInfo]    Script Date: 3/9/2019 8:40:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[VW_GetStudentInfo]
AS

SELECT
s.Id,s.Name,s.Address, sd.Name SubDistrictName, d.Name DIstrictName

FROM Students s


LEFT JOIN SubDistricts sd ON sd.Id= s.SubDistrictId
LEFT JOIN Districts d ON d.Id=sd.DistrictId
GO
SET IDENTITY_INSERT [dbo].[Districts] ON 

INSERT [dbo].[Districts] ([Id], [Name]) VALUES (2, N'CTG')
INSERT [dbo].[Districts] ([Id], [Name]) VALUES (4, N'Noakhali')
SET IDENTITY_INSERT [dbo].[Districts] OFF
SET IDENTITY_INSERT [dbo].[Students] ON 

INSERT [dbo].[Students] ([Id], [Name], [Address], [SubDistrictId], [RegNo]) VALUES (2, N'Nafiz', N'302, House', 4, N'001')
INSERT [dbo].[Students] ([Id], [Name], [Address], [SubDistrictId], [RegNo]) VALUES (4, N'Akram', N'Dhaka', 3, N'002')
INSERT [dbo].[Students] ([Id], [Name], [Address], [SubDistrictId], [RegNo]) VALUES (5, N'Akram Uddin', N'Dhaka', 3, N'003')
INSERT [dbo].[Students] ([Id], [Name], [Address], [SubDistrictId], [RegNo]) VALUES (6, N'Arman Malik', N'Kumarkhali', 3, N'004')
INSERT [dbo].[Students] ([Id], [Name], [Address], [SubDistrictId], [RegNo]) VALUES (7, N'Hillol', N'Noa', 6, N'005')
INSERT [dbo].[Students] ([Id], [Name], [Address], [SubDistrictId], [RegNo]) VALUES (8, N'Akram Uddin', N'Dhaka', 3, NULL)
SET IDENTITY_INSERT [dbo].[Students] OFF
SET IDENTITY_INSERT [dbo].[SubDistricts] ON 

INSERT [dbo].[SubDistricts] ([Id], [Name], [DistrictId]) VALUES (3, N'Kadamtali', 2)
INSERT [dbo].[SubDistricts] ([Id], [Name], [DistrictId]) VALUES (4, N'Pahartali', 2)
INSERT [dbo].[SubDistricts] ([Id], [Name], [DistrictId]) VALUES (5, N'HaliShahar', 2)
INSERT [dbo].[SubDistricts] ([Id], [Name], [DistrictId]) VALUES (6, N'Sonaimuri', 4)
INSERT [dbo].[SubDistricts] ([Id], [Name], [DistrictId]) VALUES (7, N'Maijdee', 4)
SET IDENTITY_INSERT [dbo].[SubDistricts] OFF
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_SubDistricts] FOREIGN KEY([SubDistrictId])
REFERENCES [dbo].[SubDistricts] ([Id])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_SubDistricts]
GO
ALTER TABLE [dbo].[SubDistricts]  WITH CHECK ADD  CONSTRAINT [FK_SubDistricts_Districts] FOREIGN KEY([DistrictId])
REFERENCES [dbo].[Districts] ([Id])
GO
ALTER TABLE [dbo].[SubDistricts] CHECK CONSTRAINT [FK_SubDistricts_Districts]
GO
USE [master]
GO
ALTER DATABASE [StudentDb] SET  READ_WRITE 
GO
