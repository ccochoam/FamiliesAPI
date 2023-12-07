USE [master]
GO
/****** Object:  Database [MidasoftTest]    Script Date: 7/12/2023 1:10:17 p. m. ******/
CREATE DATABASE [MidasoftTest]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MidasoftTest', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\MidasoftTest.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MidasoftTest_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\MidasoftTest_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [MidasoftTest] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MidasoftTest].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MidasoftTest] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MidasoftTest] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MidasoftTest] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MidasoftTest] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MidasoftTest] SET ARITHABORT OFF 
GO
ALTER DATABASE [MidasoftTest] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MidasoftTest] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MidasoftTest] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MidasoftTest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MidasoftTest] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MidasoftTest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MidasoftTest] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MidasoftTest] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MidasoftTest] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MidasoftTest] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MidasoftTest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MidasoftTest] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MidasoftTest] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MidasoftTest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MidasoftTest] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MidasoftTest] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MidasoftTest] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MidasoftTest] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MidasoftTest] SET  MULTI_USER 
GO
ALTER DATABASE [MidasoftTest] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MidasoftTest] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MidasoftTest] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MidasoftTest] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MidasoftTest] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MidasoftTest] SET QUERY_STORE = OFF
GO
USE [MidasoftTest]
GO
/****** Object:  Table [dbo].[FamilyGroup]    Script Date: 7/12/2023 1:10:17 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FamilyGroup](
	[FamilyGroupId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NULL,
	[NumberId] [varchar](50) NULL,
	[Created] [datetime] NULL,
	[Updated] [datetime] NULL,
 CONSTRAINT [PK_FamilyGroup] PRIMARY KEY CLUSTERED 
(
	[FamilyGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LogDB]    Script Date: 7/12/2023 1:10:17 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogDB](
	[LogId] [bigint] IDENTITY(1,1) NOT NULL,
	[Created] [datetime] NULL,
	[Username] [varchar](200) NULL,
	[Process] [varchar](500) NULL,
	[Request] [varchar](max) NULL,
	[Response] [varchar](max) NULL,
	[Successful] [bit] NULL,
	[Exception] [varchar](max) NULL,
 CONSTRAINT [PK_LogDB] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 7/12/2023 1:10:17 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[NumberId] [varchar](50) NULL,
	[GenderId] [varchar](50) NULL,
	[Relationship] [varchar](50) NULL,
	[Age] [int] NULL,
	[UnderAge] [bit] NULL,
	[Birthdate] [date] NULL,
	[FamilyGroupId] [int] NULL,
	[Username] [varchar](200) NULL,
	[Password] [varchar](200) NULL,
	[HashKey] [varchar](200) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[addFamilyGroupDB]    Script Date: 7/12/2023 1:10:17 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[addFamilyGroupDB]
@FamilyGroupId bigint = null,
@Name varchar(100) = null,
@NumberId varchar(50) = null

AS
	declare @id bigint = 0
	insert into FamilyGroup values(@Name ,@NumberId, getdate(), null);
	set @id = Scope_identity()
	select * from FamilyGroup where FamilyGroupId = @id
GO
/****** Object:  StoredProcedure [dbo].[addUserDB]    Script Date: 7/12/2023 1:10:17 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[addUserDB]
@UserId bigint = null,
@Username varchar(200) = null,
@Password varchar(200) = null,
@HashKey varchar(200) = null,
@Name varchar(100) = null,
@LastName varchar(100) = null,
@NumberId varchar(50) = null,
@GenderId int = null,
@Relationship varchar(50) = null,
@Age int = null,
@UnderAge bit = null,
@Birthdate varchar(50) = null,
@FamilyGroupId int = null

AS
	if (select count(*) from [User] where NumberId = @NumberId or Username = @Username) = 0
		begin
			insert into [user] values (@Name, @LastName ,@NumberId ,@GenderId ,@Relationship ,@Age ,@UnderAge, CONVERT(datetime, @Birthdate, 105), 
			@FamilyGroupId, @Username, @Password, @HashKey)
			select Username, Password, Name, HashKey, LastName, NumberId, GenderId, Relationship, Age, UnderAge, convert(varchar, Birthdate, 103) Birthdate, FamilyGroupId
			from [user] where UserId = SCOPE_IDENTITY()
		end
GO
/****** Object:  StoredProcedure [dbo].[deleteFamilyGroupDB]    Script Date: 7/12/2023 1:10:17 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[deleteFamilyGroupDB]
@FamilyGroupId int = null

AS
	delete from FamilyGroup where FamilyGroupId = @FamilyGroupId
GO
/****** Object:  StoredProcedure [dbo].[deleteUserDB]    Script Date: 7/12/2023 1:10:17 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[deleteUserDB]
@id bigint = 0
AS
	Delete from [user] where UserId = @id
GO
/****** Object:  StoredProcedure [dbo].[getAllFamilyGroupsDB]    Script Date: 7/12/2023 1:10:17 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[getAllFamilyGroupsDB]
@FamilyGroupId int = null

AS
	select * from FamilyGroup
GO
/****** Object:  StoredProcedure [dbo].[getAllUsersDB]    Script Date: 7/12/2023 1:10:17 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[getAllUsersDB]
AS
	select UserId, Username, Password, Name, LastName, NumberId, GenderId, Relationship, Age, UnderAge, convert(varchar, Birthdate, 103) Birthdate, FamilyGroupId
	from [user]
GO
/****** Object:  StoredProcedure [dbo].[getFamilyGroupDB]    Script Date: 7/12/2023 1:10:17 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[getFamilyGroupDB]
@FamilyGroupId int = null

AS
	select * from FamilyGroup where FamilyGroupId = @FamilyGroupId
GO
/****** Object:  StoredProcedure [dbo].[getUserDB]    Script Date: 7/12/2023 1:10:17 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[getUserDB]
@action varchar(20) = null,
@UserId bigint = 0,
@Username varchar(100) = null
AS
	if @action = 'byId'
	begin
		select UserId, Username, Password, Name, HashKey, LastName, NumberId, GenderId, Relationship, Age, UnderAge, convert(varchar, Birthdate, 103) Birthdate, FamilyGroupId
		from [user] where UserId = UserId
	end
	else
	begin
		select UserId, Username, Password, Name, HashKey, LastName, NumberId, GenderId, Relationship, Age, UnderAge, convert(varchar, Birthdate, 103) Birthdate, FamilyGroupId
		from [user] where Username = @Username
	end
GO
/****** Object:  StoredProcedure [dbo].[LogTraceabilityDB]    Script Date: 7/12/2023 1:10:17 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LogTraceabilityDB]
@action varchar(20) = null,
@LogId bigint = 0,
@Created datetime = null,
@Username varchar(200) = null,
@Process varchar(500) = null,
@Request varchar(max) = null,
@Response varchar(max) = null,
@Successful bit = null,
@Exception varchar(max) = null


AS
	if @action = 'add'
		begin
			insert into LogDB values(GETDATE(), @Username, @Process, @Request, @Response, @Successful, @Exception)
		end
	if @action = 'username'
		begin
			select * from logdb where Username = @Username
		end
	if @action = 'process'
		begin
			select * from logdb where Process = @Process
		end
GO
/****** Object:  StoredProcedure [dbo].[updateFamilyGroupDB]    Script Date: 7/12/2023 1:10:17 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[updateFamilyGroupDB]
@FamilyGroupId bigint = null,
@Name varchar(100) = null,
@NumberId varchar(50) = null

AS
	update FamilyGroup set [Name] = @Name, updated = getdate() where FamilyGroupId = @FamilyGroupId
	select Scope_identity() as IdFG;
GO
/****** Object:  StoredProcedure [dbo].[updateUserDB]    Script Date: 7/12/2023 1:10:17 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[updateUserDB]
@UserId bigint = 0,
@Username varchar(200) = null,
@Password varchar(200) = null,
@HashKey varchar(200) = null,
@Name varchar(100) = null,
@LastName varchar(100) = null,
@NumberId varchar(50) = null,
@GenderId int = null,
@Relationship varchar(50) = null,
@Age int = null,
@UnderAge bit = null,
@Birthdate varchar(50) = null,
@FamilyGroupId int = null
AS
	update [user] set [Name] = @Name, LastName = @LastName, NumberId = @NumberId, GenderId = @GenderId, Relationship = @Relationship, Age = @Age,
	UnderAge = @UnderAge, Birthdate = CONVERT(datetime, @Birthdate, 105), FamilyGroupId = @FamilyGroupId, Username = @Username, [Password] = @Password,
	HashKey = @HashKey where UserId = @UserId
	select SCOPE_IDENTITY()
GO
USE [master]
GO
ALTER DATABASE [MidasoftTest] SET  READ_WRITE 
GO
