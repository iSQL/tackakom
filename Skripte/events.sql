USE [DB_37619_baza]
GO
/****** Object:  Table [dbo].[Events]    Script Date: 06/24/2012 21:33:16 ******/
SET IDENTITY_INSERT [dbo].[Events] ON
INSERT [dbo].[Events] ([Id], [CreateTime], [StartDate], [EndDate], [Title], [Description], [Entry],  [Host_Id], [EventCategory_Id]) VALUES (2, CAST(0x0000A07A00000000 AS DateTime), CAST(0x0000A07A00000000 AS DateTime), CAST(0x0000A07A00000000 AS DateTime), N'Event 1', N'Diskripshon 1', N'Slobodan',  1, 1)
INSERT [dbo].[Events] ([Id], [CreateTime], [StartDate], [EndDate], [Title], [Description], [Entry],  [Host_Id], [EventCategory_Id]) VALUES (3, CAST(0x0000A07A00000000 AS DateTime), CAST(0x0000A07A00000000 AS DateTime), CAST(0x0000A07A00000000 AS DateTime), N'Event 2', N'Descrtiption 2', N'50 dinara', 1, 2)
INSERT [dbo].[Events] ([Id], [CreateTime], [StartDate], [EndDate], [Title], [Description], [Entry],  [Host_Id], [EventCategory_Id]) VALUES (4, CAST(0x0000A07A00000000 AS DateTime), CAST(0x0000A07A00000000 AS DateTime), CAST(0x0000A07A00000000 AS DateTime), N'Event 3', N'Description 3', N'Slobodan',  1, 2)
INSERT [dbo].[Events] ([Id], [CreateTime], [StartDate], [EndDate], [Title], [Description], [Entry],  [Host_Id], [EventCategory_Id]) VALUES (5, CAST(0x0000A07A00000000 AS DateTime), CAST(0x0000A07A00000000 AS DateTime), CAST(0x0000A07A00000000 AS DateTime), N'Event 4', N'Description 4', N'Slobodan',  1, 1)
INSERT [dbo].[Events] ([Id], [CreateTime], [StartDate], [EndDate], [Title], [Description], [Entry],  [Host_Id], [EventCategory_Id]) VALUES (6, CAST(0x0000A07A00000000 AS DateTime), CAST(0x0000A07A00000000 AS DateTime), CAST(0x0000A07A00000000 AS DateTime), N'Event 5', N'Desctiption', N'500 dinara',  1, 1)
SET IDENTITY_INSERT [dbo].[Events] OFF
