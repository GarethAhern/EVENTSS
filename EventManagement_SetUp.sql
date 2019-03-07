USE EventManagement
SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[Events](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Event_Name] [varchar](100) NULL,
	[Event_Date] [date] NOT NULL,
	[Event_Venue] [varchar](100) NULL,
	[Event_Venue_Full_Address] [varchar](3000) NULL,
	[Event_Description] [varchar](max) NULL,
	[Event_Type_Id] [int] NULL,
	[Event_Owner_Id] [int] NULL,
	[Max_Number_Of_Guests] [smallint] NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


CREATE TABLE [dbo].[Registration_Emails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Client_Id] [int] NOT NULL,
	[Date_Email_Sent] [datetime] NOT NULL,
	[Reply_Of_Approval] [bit] NOT NULL CONSTRAINT [DF__Registration_Emails_Reply_Of_Approval]  DEFAULT ((0)),
 CONSTRAINT [PK_SentEmails] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[Clients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[First_Name] [varchar](100) NULL,
	[Last_Name] [varchar](100) NULL,
	[Title] [varchar](100) NULL,
	[Formal_Salutation] [bit] NULL CONSTRAINT [DF_Clients__Formal_Salutation]  DEFAULT ((1)),
	[Business_Name] [varchar](100) NULL,
	[Email_Address] [varchar](100) NULL,
	[Cell_Phone] [varchar](50) NULL,
	[Work_Phone] [varchar](50) NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[Addresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Address_Type_Id] [int] NOT NULL,
	[Address1] [varchar](100) NOT NULL,
	[Address2] [varchar](100) NULL,
	[Address3] [varchar](100) NULL,
	[Address4] [varchar](100) NULL,
	[PostCode] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[Address_Type](
	[Id] [int] NOT NULL,
	[Address_Type] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Address_Type] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[Address_Link](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Client_Id] [int] NOT NULL,
	[Address_Id] [int] NOT NULL,
 CONSTRAINT [PK_Address_Link] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[Event_Invite_List](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Event_Id] [int] NOT NULL,
	[Client_Id] [int] NOT NULL,
	[Email_Sent_Date] [datetime] NULL,
	[Client_Coming] [bit] NULL,
 CONSTRAINT [PK_Event_Invite_List] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[Event_Owner](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Full_Name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Event_Owner] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[Event_Types](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Event_Type] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Event_Types] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]




ALTER TABLE [dbo].[Registration_Emails]  WITH CHECK ADD  CONSTRAINT [FK_Registration_Emails__Clients] FOREIGN KEY([Client_Id])
REFERENCES [dbo].[Clients] ([Id])

ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Address_Type] FOREIGN KEY([Address_Type_Id])
REFERENCES [dbo].[Address_Type] ([Id])

ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Address_Type]

ALTER TABLE [dbo].[Event_Invite_List]  WITH CHECK ADD  CONSTRAINT [FK_Event_Invite_List_Clients] FOREIGN KEY([Client_Id])
REFERENCES [dbo].[Clients] ([Id])

ALTER TABLE [dbo].[Event_Invite_List] CHECK CONSTRAINT [FK_Event_Invite_List_Clients]

ALTER TABLE [dbo].[Event_Invite_List]  WITH CHECK ADD  CONSTRAINT [FK_Event_Invite_List_Events] FOREIGN KEY([Event_Id])
REFERENCES [dbo].[Events] ([Id])

ALTER TABLE [dbo].[Event_Invite_List] CHECK CONSTRAINT [FK_Event_Invite_List_Events]

ALTER TABLE [dbo].[Registration_Emails] CHECK CONSTRAINT [FK_Registration_Emails__Clients]

ALTER TABLE [dbo].[Address_Link]  WITH CHECK ADD  CONSTRAINT [FK_Address_Link_Addresses] FOREIGN KEY([Address_Id])
REFERENCES [dbo].[Addresses] ([Id])

ALTER TABLE [dbo].[Address_Link] CHECK CONSTRAINT [FK_Address_Link_Addresses]

ALTER TABLE [dbo].[Address_Link]  WITH CHECK ADD  CONSTRAINT [FK_Address_Link_Clients] FOREIGN KEY([Client_Id])
REFERENCES [dbo].[Clients] ([Id])

ALTER TABLE [dbo].[Address_Link] CHECK CONSTRAINT [FK_Address_Link_Clients]