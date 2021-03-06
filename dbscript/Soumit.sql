USE [Electricity]
GO
/****** Object:  Table [dbo].[users]    Script Date: 3/24/2019 3:54:23 PM ******/
DROP TABLE [dbo].[users]
GO
/****** Object:  Table [dbo].[user_token_mapper]    Script Date: 3/24/2019 3:54:23 PM ******/
DROP TABLE [dbo].[user_token_mapper]
GO
/****** Object:  Table [dbo].[states]    Script Date: 3/24/2019 3:54:23 PM ******/
DROP TABLE [dbo].[states]
GO
/****** Object:  Table [dbo].[role]    Script Date: 3/24/2019 3:54:23 PM ******/
DROP TABLE [dbo].[role]
GO
/****** Object:  Table [dbo].[employee_role]    Script Date: 3/24/2019 3:54:23 PM ******/
DROP TABLE [dbo].[employee_role]
GO
/****** Object:  Table [dbo].[employee]    Script Date: 3/24/2019 3:54:23 PM ******/
DROP TABLE [dbo].[employee]
GO
/****** Object:  StoredProcedure [dbo].[validate_token]    Script Date: 3/24/2019 3:54:23 PM ******/
DROP PROCEDURE [dbo].[validate_token]
GO
/****** Object:  StoredProcedure [dbo].[valid_user_to_refer]    Script Date: 3/24/2019 3:54:23 PM ******/
DROP PROCEDURE [dbo].[valid_user_to_refer]
GO
/****** Object:  StoredProcedure [dbo].[refer_user_with_token]    Script Date: 3/24/2019 3:54:23 PM ******/
DROP PROCEDURE [dbo].[refer_user_with_token]
GO
/****** Object:  StoredProcedure [dbo].[qualify_referer_user]    Script Date: 3/24/2019 3:54:23 PM ******/
DROP PROCEDURE [dbo].[qualify_referer_user]
GO
/****** Object:  StoredProcedure [dbo].[get_users]    Script Date: 3/24/2019 3:54:23 PM ******/
DROP PROCEDURE [dbo].[get_users]
GO
/****** Object:  StoredProcedure [dbo].[get_user]    Script Date: 3/24/2019 3:54:23 PM ******/
DROP PROCEDURE [dbo].[get_user]
GO
/****** Object:  StoredProcedure [dbo].[get_roles]    Script Date: 3/24/2019 3:54:23 PM ******/
DROP PROCEDURE [dbo].[get_roles]
GO
/****** Object:  StoredProcedure [dbo].[get_referer_token]    Script Date: 3/24/2019 3:54:23 PM ******/
DROP PROCEDURE [dbo].[get_referer_token]
GO
/****** Object:  StoredProcedure [dbo].[get_employees]    Script Date: 3/24/2019 3:54:23 PM ******/
DROP PROCEDURE [dbo].[get_employees]
GO
/****** Object:  StoredProcedure [dbo].[get_employee_role]    Script Date: 3/24/2019 3:54:23 PM ******/
DROP PROCEDURE [dbo].[get_employee_role]
GO
/****** Object:  StoredProcedure [dbo].[find_users]    Script Date: 3/24/2019 3:54:23 PM ******/
DROP PROCEDURE [dbo].[find_users]
GO
USE [master]
GO
/****** Object:  Database [Electricity]    Script Date: 3/24/2019 3:54:23 PM ******/
DROP DATABASE [Electricity]
GO
/****** Object:  Database [Electricity]    Script Date: 3/24/2019 3:54:23 PM ******/
CREATE DATABASE [Electricity]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Electricity', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Electricity.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Electricity_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Electricity_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Electricity] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Electricity].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Electricity] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Electricity] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Electricity] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Electricity] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Electricity] SET ARITHABORT OFF 
GO
ALTER DATABASE [Electricity] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Electricity] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Electricity] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Electricity] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Electricity] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Electricity] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Electricity] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Electricity] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Electricity] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Electricity] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Electricity] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Electricity] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Electricity] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Electricity] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Electricity] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Electricity] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Electricity] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Electricity] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Electricity] SET RECOVERY FULL 
GO
ALTER DATABASE [Electricity] SET  MULTI_USER 
GO
ALTER DATABASE [Electricity] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Electricity] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Electricity] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Electricity] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Electricity', N'ON'
GO
USE [Electricity]
GO
/****** Object:  StoredProcedure [dbo].[find_users]    Script Date: 3/24/2019 3:54:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[find_users] 
@user_name varchar(50),
@password nvarchar(50)	
AS
BEGIN
	select count(*) 
	from dbo.[users] as u 
	where 
	u.user_name=@user_name 
	AND 
	u.[password]=@password 
END

GO
/****** Object:  StoredProcedure [dbo].[get_employee_role]    Script Date: 3/24/2019 3:54:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[get_employee_role] 
	
AS
BEGIN
	select * from dbo.employee_role
END

GO
/****** Object:  StoredProcedure [dbo].[get_employees]    Script Date: 3/24/2019 3:54:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[get_employees] 
	
AS
BEGIN
	select * from dbo.employee
END

GO
/****** Object:  StoredProcedure [dbo].[get_referer_token]    Script Date: 3/24/2019 3:54:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[get_referer_token] 
@user_name varchar(50)
AS
BEGIN
	
     DECLARE @USERTOKEN NVARCHAR(MAX)
	 select @USERTOKEN = security_stamp from dbo.[users] as u
	 where 
	u.user_name=@user_name 

	DECLARE @RAND_NUM NVARCHAR(100)
	SELECT @RAND_NUM=convert(numeric(12,0),rand() * 899999999999) + 100000000000;

	insert into
    [dbo].[user_token_mapper]
	values 
	(@USERTOKEN,@RAND_NUM,GETDATE(),10,0,NULL)
	SELECT @RAND_NUM AS TOKEN
END

GO
/****** Object:  StoredProcedure [dbo].[get_roles]    Script Date: 3/24/2019 3:54:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[get_roles] 
	
AS
BEGIN
	select * from dbo.role
END

GO
/****** Object:  StoredProcedure [dbo].[get_user]    Script Date: 3/24/2019 3:54:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[get_user] 
@user_name varchar(50)	
AS
BEGIN
	select u.user_id,u.user_name,u.email,u.security_stamp,u.first_name,u.last_name,u.father_name,u.dob,u.mobile_number,u.pan_card,u.aadhar_card,u.address,u.post_office,u.police_station,u.district,u.city,s.state_name,u.pin,u.sex,r.role_name
	from dbo.[users] as u,dbo.[role] as r,dbo.[states] as s where u.role_id=r.role_id and u.[state]=s.[state_id] and user_name=@user_name
END

GO
/****** Object:  StoredProcedure [dbo].[get_users]    Script Date: 3/24/2019 3:54:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[get_users] 
	
AS
BEGIN
	select u.user_id,u.user_name,u.email,u.security_stamp,u.role_id, r.role_name as [role_name] from dbo.[users] as u,dbo.role as r where u.role_id=r.role_id
END

GO
/****** Object:  StoredProcedure [dbo].[qualify_referer_user]    Script Date: 3/24/2019 3:54:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[qualify_referer_user] 
@user_name varchar(50)
AS
BEGIN
	select count(*) 
	from dbo.[users] as u,dbo.[role] as r
	where 
	u.user_name=@user_name 
	AND
	u.role_id=r.role_id
	AND 
	r.role_name<>'user'

END

GO
/****** Object:  StoredProcedure [dbo].[refer_user_with_token]    Script Date: 3/24/2019 3:54:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[refer_user_with_token] 
@userid nvarchar(max),
@token nvarchar(max),
@bit BIT OUTPUT
AS
BEGIN
	declare @isused int
	declare @securitystamp nvarchar(max)
	set @isused=0
	select @isused=count(*) from  [dbo].[user_token_mapper]
	where security_number=@token
	and 
	is_used=1

	if(@isused>0)
	BEGIN
		SET @bit=0
	END
	else
	BEGIN
	select @securitystamp=security_stamp from  [dbo].[users]
	where user_name=@userid


	update [dbo].[user_token_mapper]
	set is_used=1,refered_user_token=@securitystamp
	where security_number=@token 
	SET @bit=1
	END

END

GO
/****** Object:  StoredProcedure [dbo].[valid_user_to_refer]    Script Date: 3/24/2019 3:54:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[valid_user_to_refer] 
@user_name varchar(50),
@bit BIT OUTPUT
AS
BEGIN
declare @ispresent int
	select @ispresent=count(*) 
	from dbo.[users] as u,dbo.[role] as r, [dbo].[user_token_mapper] as m
	where 
	u.user_name=@user_name 
	AND
	u.role_id=r.role_id
	AND 
	r.role_name<>'admin'
	AND
	u.security_stamp<>m.refered_user_token


	if(@ispresent>0)
	BEGIN
	SET @bit=0

	END
	else
	BEGIN
	SET @bit=1
	END
END

GO
/****** Object:  StoredProcedure [dbo].[validate_token]    Script Date: 3/24/2019 3:54:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[validate_token] 
@token nvarchar(max),
@bit BIT OUTPUT
AS
BEGIN
     declare @expiredate datetime
	declare @datecreate datetime
	declare @expiredays int
     select @datecreate=created_date,@expiredays=expiration_days from 
    [dbo].[user_token_mapper]
where
security_number=@token
and
is_used=0

SELECT @expiredate= DATEADD(day, @expiredays, @datecreate);
print @expiredate
if(@expiredate>=GETDATE())
BEGIN
SET @bit=1
END
else
begin
set @bit=0
end


END

GO
/****** Object:  Table [dbo].[employee]    Script Date: 3/24/2019 3:54:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employee](
	[id] [int] NOT NULL,
	[name] [nchar](10) NOT NULL,
	[address] [nchar](1000) NULL,
 CONSTRAINT [PK_employee] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[employee_role]    Script Date: 3/24/2019 3:54:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employee_role](
	[employee_id] [int] NOT NULL,
	[role_id] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[role]    Script Date: 3/24/2019 3:54:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[role_id] [int] NOT NULL,
	[role_name] [nchar](10) NOT NULL,
 CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[states]    Script Date: 3/24/2019 3:54:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[states](
	[state_id] [int] IDENTITY(1,1) NOT NULL,
	[state_name] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[user_token_mapper]    Script Date: 3/24/2019 3:54:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[user_token_mapper](
	[user_token_key] [nvarchar](max) NULL,
	[security_number] [nvarchar](max) NULL,
	[created_date] [datetime] NULL,
	[expiration_days] [int] NULL,
	[is_used] [char](1) NULL,
	[refered_user_token] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[users]    Script Date: 3/24/2019 3:54:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[user_id] [int] NOT NULL,
	[user_name] [nvarchar](50) NOT NULL,
	[role_id] [int] NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[security_stamp] [nvarchar](max) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[first_name] [nvarchar](100) NULL,
	[last_name] [nvarchar](100) NULL,
	[father_name] [nvarchar](100) NULL,
	[dob] [datetime] NULL,
	[mobile_number] [nvarchar](20) NULL,
	[pan_card] [nvarchar](20) NULL,
	[aadhar_card] [nvarchar](100) NULL,
	[address] [nvarchar](max) NULL,
	[post_office] [nvarchar](100) NULL,
	[police_station] [nvarchar](100) NULL,
	[district] [nvarchar](100) NULL,
	[city] [nvarchar](100) NULL,
	[state] [int] NULL,
	[pin] [nvarchar](10) NULL,
	[sex] [nchar](10) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
INSERT [dbo].[employee] ([id], [name], [address]) VALUES (1, N'Soumit    ', N'Barrackpore                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             ')
INSERT [dbo].[employee] ([id], [name], [address]) VALUES (2, N'Pramit    ', N'Palta                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   ')
INSERT [dbo].[employee_role] ([employee_id], [role_id]) VALUES (1, 1)
INSERT [dbo].[employee_role] ([employee_id], [role_id]) VALUES (2, 2)
INSERT [dbo].[role] ([role_id], [role_name]) VALUES (1, N'admin     ')
INSERT [dbo].[role] ([role_id], [role_name]) VALUES (2, N'employee  ')
INSERT [dbo].[role] ([role_id], [role_name]) VALUES (3, N'user      ')
SET IDENTITY_INSERT [dbo].[states] ON 

INSERT [dbo].[states] ([state_id], [state_name]) VALUES (1, N'Andhra Pradesh')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (2, N'Arunachal Pradesh')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (3, N'Assam')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (4, N'Bihar')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (5, N'Chhattisgarh')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (6, N'Goa')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (7, N'Gujarat')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (8, N'Haryana')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (9, N'Himachal Pradesh')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (10, N'Jammu & Kashmir')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (11, N'Jharkhand')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (12, N'Karnataka')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (13, N'Kerala')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (14, N'Madhya Pradesh')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (15, N'Maharashtra')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (16, N'Manipur')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (17, N'Meghalaya')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (18, N'Mizoram')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (19, N'Nagaland')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (20, N'Odisha')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (21, N'Punjab')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (22, N'Rajasthan')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (23, N'Sikkim')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (24, N'Tamil Nadu')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (25, N'Telangana')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (26, N'Tripura')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (27, N'Uttarakhand')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (28, N'Uttar Pradesh')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (29, N'West Bengal')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (30, N'Andaman and Nicobar Islands')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (31, N'Chandigarh')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (32, N'Dadra and Nagar Haveli')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (33, N'Daman & Diu')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (34, N'The Government of NCT of Delhi')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (35, N'Lakshadweep')
INSERT [dbo].[states] ([state_id], [state_name]) VALUES (36, N'Puducherry')
SET IDENTITY_INSERT [dbo].[states] OFF
INSERT [dbo].[user_token_mapper] ([user_token_key], [security_number], [created_date], [expiration_days], [is_used], [refered_user_token]) VALUES (N'7AE67B5E-4C7B-4892-8F7E-554834FDC208', N'945575645562', CAST(0x0000AA1901724206 AS DateTime), 10, N'0', NULL)
INSERT [dbo].[users] ([user_id], [user_name], [role_id], [email], [security_stamp], [password], [first_name], [last_name], [father_name], [dob], [mobile_number], [pan_card], [aadhar_card], [address], [post_office], [police_station], [district], [city], [state], [pin], [sex]) VALUES (1, N'soumit.nag', 1, N'nag.soumit@gmail.com', N'7AE67B5E-4C7B-4892-8F7E-554834FDC208', N'P@ssw0rd', N'Soumit', N'Nag', N'Sarit nag', CAST(0x000076E100000000 AS DateTime), N'9073780790', N'AKOPN1134C', N'3322566FDR3', N'28C arabinda sarani mathpara NC pukur Barrackpore', N'NCPUKUR', N'Titagarh', N'24 PGS North', N'Barrackpore', 29, N'700122', N'Male      ')
INSERT [dbo].[users] ([user_id], [user_name], [role_id], [email], [security_stamp], [password], [first_name], [last_name], [father_name], [dob], [mobile_number], [pan_card], [aadhar_card], [address], [post_office], [police_station], [district], [city], [state], [pin], [sex]) VALUES (2, N'pramit.das', 2, N'das.pramit@hotmail.com', N'F2213FF8-1F0D-4002-9032-A21C2602E2CA', N'P@ssw0rd', N'Pramit', N'Das', N'Poritosh Das', CAST(0x0000862D00000000 AS DateTime), N'9073780790', N'AKOPN1134C', N'3322566FDR3', N'28C arabinda sarani mathpara NC pukur Barrackpore', N'NCPUKUR', N'Barasat', N'24 PGS North', N'Palta', 29, N'700122', N'Male      ')
INSERT [dbo].[users] ([user_id], [user_name], [role_id], [email], [security_stamp], [password], [first_name], [last_name], [father_name], [dob], [mobile_number], [pan_card], [aadhar_card], [address], [post_office], [police_station], [district], [city], [state], [pin], [sex]) VALUES (3, N'krishna.nag', 2, N'nag.krishna', N'A4443C87-4C55-4A84-9607-1B54A06E7BDB', N'P@ssw0rd', N'Krishna', N'Nag', N'Kalipada Bose', CAST(0x00005A5800000000 AS DateTime), N'9073780790', N'AKOPN1134C', N'3322566FDR3', N'28C arabinda sarani mathpara NC pukur Barrackpore', N'NCPUKUR', N'Titagarh', N'24 PGS North', N'Barrackpore', 29, N'700122', N'Female    ')
INSERT [dbo].[users] ([user_id], [user_name], [role_id], [email], [security_stamp], [password], [first_name], [last_name], [father_name], [dob], [mobile_number], [pan_card], [aadhar_card], [address], [post_office], [police_station], [district], [city], [state], [pin], [sex]) VALUES (4, N'sarit.nag', 3, N'nag.sait', N'0D4DC12A-C93A-4D68-8F90-7AB171FEA7BB', N'P@ssw0rd', N'Sarit', N'Nag', N'Anil Nag', CAST(0x00004C1400000000 AS DateTime), N'9073780790', N'AKOPN1134C', N'3322566FDR3', N'28C arabinda sarani mathpara NC pukur Barrackpore', N'NCPUKUR', N'Birbhum', N'24 PGS North', N'Rampurhat', 29, N'700122', N'Male      ')
USE [master]
GO
ALTER DATABASE [Electricity] SET  READ_WRITE 
GO
