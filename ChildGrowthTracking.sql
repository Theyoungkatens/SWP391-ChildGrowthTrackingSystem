USE [SWP391_ChildGrowthTracking]
GO
/****** Object:  Table [dbo].[ALERT]    Script Date: 2/9/2025 3:28:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ALERT](
	[alert_id] [int] IDENTITY(1,1) NOT NULL,
	[child_id] [int] NULL,
	[alert_type] [varchar](100) NULL,
	[alert_date] [datetime] NULL,
	[message] [text] NULL,
	[is_read] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[alert_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BLOG]    Script Date: 2/9/2025 3:28:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BLOG](
	[blog_id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](255) NULL,
	[content] [nvarchar](max) NULL,
	[created_date] [datetime] NULL,
	[modified_date] [datetime] NULL,
	[tags] [nvarchar](255) NULL,
	[image] [nvarchar](255) NULL,
	[status] [nvarchar](50) NULL,
	[category] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[blog_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CHILDRECORD]    Script Date: 2/9/2025 3:28:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHILDRECORD](
	[child_id] [int] NOT NULL,
	[record_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[child_id] ASC,
	[record_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CHILDS]    Script Date: 2/9/2025 3:28:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CHILDS](
	[child_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NULL,
	[name] [varchar](255) NULL,
	[date_of_birth] [date] NULL,
	[gender] [varchar](10) NULL,
	[birth_weight] [float] NULL,
	[birth_height] [float] NULL,
	[blood_type] [varchar](10) NULL,
	[allergies] [text] NULL,
	[status] [text] NULL,
	[relationship] [nvarchar](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[child_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CONSULTATION_REQUEST]    Script Date: 2/9/2025 3:28:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CONSULTATION_REQUEST](
	[request_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NULL,
	[child_id] [int] NULL,
	[request_date] [datetime] NULL,
	[description] [text] NULL,
	[status] [varchar](50) NULL,
	[urgency] [varchar](50) NULL,
	[attachments] [varchar](255) NULL,
	[category] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[request_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CONSULTATION_RESPONSE]    Script Date: 2/9/2025 3:28:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CONSULTATION_RESPONSE](
	[response_id] [int] IDENTITY(1,1) NOT NULL,
	[request_id] [int] NULL,
	[doctor_id] [int] NULL,
	[response_date] [datetime] NULL,
	[content] [varchar](1) NULL,
	[attachments] [varchar](255) NULL,
	[Status] [bit] NULL,
	[diagnosis] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[response_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DOCTOR]    Script Date: 2/9/2025 3:28:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOCTOR](
	[doctor_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[specialization] [varchar](255) NULL,
	[email] [varchar](255) NULL,
	[phone_number] [varchar](20) NULL,
	[degree] [varchar](255) NULL,
	[hospital] [varchar](255) NULL,
	[license_number] [varchar](255) NULL,
	[biography] [text] NULL,
	[user_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[doctor_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[phone_number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[license_number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GROWTH_RECORDS]    Script Date: 2/9/2025 3:28:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GROWTH_RECORDS](
	[record_id] [int] IDENTITY(1,1) NOT NULL,
	[month] [datetime] NULL,
	[weight] [float] NULL,
	[height] [float] NULL,
	[bmi] [float] NULL,
	[head_circumference] [float] NULL,
	[upper_arm_circumference] [float] NULL,
	[recorded_by_user] [bit] NULL,
	[notes] [varchar](1) NULL,
	[old] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[record_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MEMBERSHIP_PACKAGES]    Script Date: 2/9/2025 3:28:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MEMBERSHIP_PACKAGES](
	[package_id] [int] IDENTITY(1,1) NOT NULL,
	[package_name] [varchar](255) NULL,
	[description] [text] NULL,
	[price] [decimal](10, 2) NULL,
	[duration_months] [int] NULL,
	[features] [text] NULL,
	[max_children_allowed] [int] NULL,
	[status] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[package_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PAYMENT]    Script Date: 2/9/2025 3:28:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PAYMENT](
	[payment_id] [int] IDENTITY(1,1) NOT NULL,
	[payment_date] [datetime] NULL,
	[payment_amount] [decimal](10, 2) NULL,
	[transaction_id] [varchar](255) NULL,
	[status] [varchar](50) NULL,
	[membershipid] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[payment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[transaction_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[membershipid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RATING_FEEDBACK]    Script Date: 2/9/2025 3:28:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RATING_FEEDBACK](
	[feedback_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NULL,
	[doctor_id] [int] NULL,
	[rating] [int] NULL,
	[comment] [varchar](1) NULL,
	[feedback_date] [datetime] NULL,
	[feedback_type] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[feedback_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USER_MEMBERSHIP]    Script Date: 2/9/2025 3:28:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USER_MEMBERSHIP](
	[membershipid] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NULL,
	[package_id] [int] NULL,
	[start_date] [datetime] NULL,
	[end_date] [datetime] NULL,
	[subscription_status] [varchar](50) NULL,
	[coupon_code] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[membershipid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USERACCOUNT]    Script Date: 2/9/2025 3:28:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USERACCOUNT](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](255) NULL,
	[email] [varchar](255) NULL,
	[password] [varchar](255) NULL,
	[phone_number] [varchar](255) NULL,
	[registration_date] [datetime] NULL,
	[last_login] [datetime] NULL,
	[profile_picture] [varchar](255) NULL,
	[address] [varchar](255) NULL,
	[status] [varchar](50) NULL,
	[role] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[phone_number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ALERT] ADD  DEFAULT ((0)) FOR [is_read]
GO
ALTER TABLE [dbo].[BLOG] ADD  DEFAULT (getdate()) FOR [created_date]
GO
ALTER TABLE [dbo].[CONSULTATION_REQUEST] ADD  DEFAULT (getdate()) FOR [request_date]
GO
ALTER TABLE [dbo].[CONSULTATION_REQUEST] ADD  DEFAULT ('pending') FOR [status]
GO
ALTER TABLE [dbo].[PAYMENT] ADD  DEFAULT (getdate()) FOR [payment_date]
GO
ALTER TABLE [dbo].[PAYMENT] ADD  DEFAULT ('success') FOR [status]
GO
ALTER TABLE [dbo].[USER_MEMBERSHIP] ADD  DEFAULT ('active') FOR [subscription_status]
GO
ALTER TABLE [dbo].[USERACCOUNT] ADD  DEFAULT (getdate()) FOR [registration_date]
GO
ALTER TABLE [dbo].[USERACCOUNT] ADD  DEFAULT ('active') FOR [status]
GO
ALTER TABLE [dbo].[ALERT]  WITH CHECK ADD FOREIGN KEY([child_id])
REFERENCES [dbo].[CHILDS] ([child_id])
GO
ALTER TABLE [dbo].[CHILDRECORD]  WITH CHECK ADD  CONSTRAINT [FK_CHILDRECORD_CHILDS] FOREIGN KEY([child_id])
REFERENCES [dbo].[CHILDS] ([child_id])
GO
ALTER TABLE [dbo].[CHILDRECORD] CHECK CONSTRAINT [FK_CHILDRECORD_CHILDS]
GO
ALTER TABLE [dbo].[CHILDRECORD]  WITH CHECK ADD  CONSTRAINT [FK_CHILDRECORD_GROWTH_RECORDS] FOREIGN KEY([record_id])
REFERENCES [dbo].[GROWTH_RECORDS] ([record_id])
GO
ALTER TABLE [dbo].[CHILDRECORD] CHECK CONSTRAINT [FK_CHILDRECORD_GROWTH_RECORDS]
GO
ALTER TABLE [dbo].[CHILDS]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[USERACCOUNT] ([user_id])
GO
ALTER TABLE [dbo].[CONSULTATION_REQUEST]  WITH CHECK ADD FOREIGN KEY([child_id])
REFERENCES [dbo].[CHILDS] ([child_id])
GO
ALTER TABLE [dbo].[CONSULTATION_REQUEST]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[USERACCOUNT] ([user_id])
GO
ALTER TABLE [dbo].[CONSULTATION_RESPONSE]  WITH CHECK ADD FOREIGN KEY([doctor_id])
REFERENCES [dbo].[DOCTOR] ([doctor_id])
GO
ALTER TABLE [dbo].[CONSULTATION_RESPONSE]  WITH CHECK ADD FOREIGN KEY([request_id])
REFERENCES [dbo].[CONSULTATION_REQUEST] ([request_id])
GO
ALTER TABLE [dbo].[DOCTOR]  WITH CHECK ADD  CONSTRAINT [FK_DOCTOR_USERACCOUNT] FOREIGN KEY([user_id])
REFERENCES [dbo].[USERACCOUNT] ([user_id])
GO
ALTER TABLE [dbo].[DOCTOR] CHECK CONSTRAINT [FK_DOCTOR_USERACCOUNT]
GO
ALTER TABLE [dbo].[PAYMENT]  WITH CHECK ADD  CONSTRAINT [FK_PAYMENT_USER_MEMBERSHIP] FOREIGN KEY([membershipid])
REFERENCES [dbo].[USER_MEMBERSHIP] ([membershipid])
GO
ALTER TABLE [dbo].[PAYMENT] CHECK CONSTRAINT [FK_PAYMENT_USER_MEMBERSHIP]
GO
ALTER TABLE [dbo].[RATING_FEEDBACK]  WITH CHECK ADD FOREIGN KEY([doctor_id])
REFERENCES [dbo].[DOCTOR] ([doctor_id])
GO
ALTER TABLE [dbo].[RATING_FEEDBACK]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[USERACCOUNT] ([user_id])
GO
ALTER TABLE [dbo].[USER_MEMBERSHIP]  WITH CHECK ADD FOREIGN KEY([package_id])
REFERENCES [dbo].[MEMBERSHIP_PACKAGES] ([package_id])
GO
ALTER TABLE [dbo].[USER_MEMBERSHIP]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[USERACCOUNT] ([user_id])
GO
