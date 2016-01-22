
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[NotificationMessageReceived](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[From] [nvarchar](50) NULL,
	[To] [nvarchar](50) NULL,
	[Message] [nvarchar](max) NULL,
	[ReceivedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.NotificationMessageReceived] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


