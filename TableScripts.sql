
/****** Object:  Table [dbo].[NotificationDataSeeds]    Script Date: 01/13/2016 07:59:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationDataSeeds](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](20) NOT NULL,
	[FirstName] [nvarchar](100) NULL,
	[Surname] [nvarchar](100) NOT NULL,
	[CountryCode] [nvarchar](2) NOT NULL,
	[CountryName] [nvarchar](50) NULL,
	[ResortCode] [nvarchar](4) NOT NULL,
	[ResortName] [nvarchar](50) NULL,
	[ArrivalPoint] [nvarchar](5) NOT NULL,
	[Role] [nvarchar](100) NOT NULL,
	[MobileNo] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotificationData]    Script Date: 01/13/2016 07:59:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationData](
	[BookingRef] [int] NOT NULL,
	[TourOpCode] [nvarchar](10) NOT NULL,
	[PassengerId] [int] NOT NULL,
	[Title] [nvarchar](20) NOT NULL,
	[FirstName] [nvarchar](100) NULL,
	[Surname] [nvarchar](100) NOT NULL,
	[MobileNo] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[DirectOrAgent] [nvarchar](10) NOT NULL,
	[StartDate] [date] NOT NULL,
	[DeparturePoint] [nvarchar](5) NULL,
	[ArrivalPoint] [nvarchar](5) NULL,
	[TravelDate] [nvarchar](10) NULL,
	[TravelDepatureTime] [nvarchar](20) NULL,
	[TravelArrivalTime] [nvarchar](20) NULL,
	[TravelDirection] [nvarchar](1) NULL,
	[TransportCarrier] [nvarchar](10) NULL,
	[TransportNumber] [nvarchar](10) NULL,
	[TransportType] [nvarchar](5) NULL,
	[TransportChain] [nvarchar](2) NULL,
	[CountryCode] [nvarchar](2) NULL,
	[CountryName] [nvarchar](50) NULL,
	[ResortCode] [nvarchar](4) NULL,
	[ResortName] [nvarchar](50) NULL,
	[AccommodationCode] [nvarchar](8) NULL,
	[AccommodationName] [nvarchar](50) NULL,
	[Seed] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailLogReport]    Script Date: 01/13/2016 07:59:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailLogReport](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [int] NULL,
	[Email] [nvarchar](100) NULL,
	[Message] [nvarchar](1000) NULL,
	[Status] [nvarchar](10) NULL,
	[LogComment] [nvarchar](4000) NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_EmailLogReport] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF__Notificati__Seed__00CA12DE]    Script Date: 01/13/2016 07:59:44 ******/
ALTER TABLE [dbo].[NotificationData] ADD  CONSTRAINT [DF__Notificati__Seed__00CA12DE]  DEFAULT ((0)) FOR [Seed]
GO
