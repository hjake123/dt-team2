/****** Object:  Database [dt-team2]    Script Date: 4/23/2022 6:35:07 PM ******/
CREATE DATABASE [dt-team2]  (EDITION = 'Basic', SERVICE_OBJECTIVE = 'Basic', MAXSIZE = 2 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS;
GO
ALTER DATABASE [dt-team2] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [dt-team2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dt-team2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dt-team2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dt-team2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dt-team2] SET ARITHABORT OFF 
GO
ALTER DATABASE [dt-team2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dt-team2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dt-team2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dt-team2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dt-team2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dt-team2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dt-team2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dt-team2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dt-team2] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [dt-team2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dt-team2] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [dt-team2] SET  MULTI_USER 
GO
ALTER DATABASE [dt-team2] SET ENCRYPTION ON
GO
ALTER DATABASE [dt-team2] SET QUERY_STORE = ON
GO
ALTER DATABASE [dt-team2] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 7), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 10, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO
-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO
/****** Object:  Schema [Test]    Script Date: 4/23/2022 6:35:07 PM ******/
CREATE SCHEMA [Test]
GO
/****** Object:  Table [dbo].[ArtPieces]    Script Date: 4/23/2022 6:35:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArtPieces](
	[PieceID] [int] NOT NULL,
	[Title] [varchar](200) NULL,
	[Creator] [varchar](50) NULL,
	[PieceDescription] [varchar](400) NULL,
	[PlaceOfOrigin] [varchar](50) NULL,
	[DateCreated] [date] NULL,
	[DateSubmitted] [date] NULL,
	[Dimensions] [varchar](30) NULL,
	[Source] [varchar](300) NULL,
	[Medium] [int] NULL,
 CONSTRAINT [PK_ArtPieces] PRIMARY KEY CLUSTERED 
(
	[PieceID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Collections]    Script Date: 4/23/2022 6:35:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Collections](
	[CollectionName] [varchar](300) NOT NULL,
	[Description] [varchar](500) NULL,
 CONSTRAINT [PK_Collection] PRIMARY KEY CLUSTERED 
(
	[CollectionName] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Exhibitions]    Script Date: 4/23/2022 6:35:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Exhibitions](
	[ExhibitionName] [varchar](300) NOT NULL,
	[Description] [varchar](3000) NOT NULL,
	[ListOfPieces] [varchar](100) NULL,
	[Arranger] [varchar](100) NULL,
	[Location] [varchar](1000) NULL,
	[DateEnd] [datetime] NULL,
 CONSTRAINT [PK_Exhibition] PRIMARY KEY CLUSTERED 
(
	[ExhibitionName] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InCollectionRelationship]    Script Date: 4/23/2022 6:35:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InCollectionRelationship](
	[CollectionName] [varchar](300) NOT NULL,
	[PieceID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InExhibitionRelationship]    Script Date: 4/23/2022 6:35:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InExhibitionRelationship](
	[ExhibitionName] [varchar](300) NOT NULL,
	[PieceID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lookup_AccessType]    Script Date: 4/23/2022 6:35:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_AccessType](
	[AccessType] [int] NOT NULL,
	[AccessTypeLabel] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Lookup_AccessType] PRIMARY KEY CLUSTERED 
(
	[AccessType] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lookup_Item]    Script Date: 4/23/2022 6:35:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_Item](
	[Item] [int] NOT NULL,
	[ItemLabel] [nchar](500) NOT NULL,
 CONSTRAINT [PK_Lookup_Item] PRIMARY KEY CLUSTERED 
(
	[Item] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lookup_Medium]    Script Date: 4/23/2022 6:35:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_Medium](
	[Medium] [int] NOT NULL,
	[MediumLabel] [varchar](50) NULL,
 CONSTRAINT [PK_Lookup_Medium] PRIMARY KEY CLUSTERED 
(
	[Medium] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lookup_Membership]    Script Date: 4/23/2022 6:35:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_Membership](
	[TypeID] [tinyint] NOT NULL,
	[Membership] [nchar](10) NOT NULL,
	[Fee] [smallint] NULL,
 CONSTRAINT [PK_Lookup_Membership] PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lookup_TicketType]    Script Date: 4/23/2022 6:35:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup_TicketType](
	[TicketType] [int] NOT NULL,
	[TicketTypeLabel] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Lookup_TicketType] PRIMARY KEY CLUSTERED 
(
	[TicketType] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Members]    Script Date: 4/23/2022 6:35:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Members](
	[MemberID] [char](10) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[CardNumber] [numeric](18, 0) NOT NULL,
	[LastVisit] [date] NOT NULL,
 CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED 
(
	[MemberID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_login]    Script Date: 4/23/2022 6:35:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_login](
	[USER_NAME] [varchar](100) NULL,
	[PASSWORD] [varchar](100) NULL,
	[USER_ROLE] [varchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 4/23/2022 6:35:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[TransactionID] [int] NOT NULL,
	[Item] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Price] [money] NOT NULL,
	[IsTicket] [bit] NOT NULL,
	[TicketID] [int] NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[TransactionID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionsTicket]    Script Date: 4/23/2022 6:35:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionsTicket](
	[TicketID] [int] IDENTITY(1,1) NOT NULL,
	[ExpirationDate] [date] NOT NULL,
	[AccessType] [int] NOT NULL,
	[TicketType] [int] NOT NULL,
	[TransactionID] [int] NOT NULL,
 CONSTRAINT [PK__Transact__712CC607456792D2] PRIMARY KEY CLUSTERED 
(
	[TicketID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ArtPieces]  WITH CHECK ADD  CONSTRAINT [FK_ArtPieces_Lookup_Medium] FOREIGN KEY([Medium])
REFERENCES [dbo].[Lookup_Medium] ([Medium])
GO
ALTER TABLE [dbo].[ArtPieces] CHECK CONSTRAINT [FK_ArtPieces_Lookup_Medium]
GO
ALTER TABLE [dbo].[InCollectionRelationship]  WITH CHECK ADD  CONSTRAINT [FK_InCollectionRelationship_ArtPieces] FOREIGN KEY([PieceID])
REFERENCES [dbo].[ArtPieces] ([PieceID])
GO
ALTER TABLE [dbo].[InCollectionRelationship] CHECK CONSTRAINT [FK_InCollectionRelationship_ArtPieces]
GO
ALTER TABLE [dbo].[InCollectionRelationship]  WITH CHECK ADD  CONSTRAINT [FK_InCollectionRelationship_Collection] FOREIGN KEY([CollectionName])
REFERENCES [dbo].[Collections] ([CollectionName])
GO
ALTER TABLE [dbo].[InCollectionRelationship] CHECK CONSTRAINT [FK_InCollectionRelationship_Collection]
GO
ALTER TABLE [dbo].[InExhibitionRelationship]  WITH CHECK ADD  CONSTRAINT [FK_InExhibitionRelationship_ArtPieces] FOREIGN KEY([PieceID])
REFERENCES [dbo].[ArtPieces] ([PieceID])
GO
ALTER TABLE [dbo].[InExhibitionRelationship] CHECK CONSTRAINT [FK_InExhibitionRelationship_ArtPieces]
GO
ALTER TABLE [dbo].[InExhibitionRelationship]  WITH CHECK ADD  CONSTRAINT [FK_InExhibitionRelationship_Exhibition] FOREIGN KEY([ExhibitionName])
REFERENCES [dbo].[Exhibitions] ([ExhibitionName])
GO
ALTER TABLE [dbo].[InExhibitionRelationship] CHECK CONSTRAINT [FK_InExhibitionRelationship_Exhibition]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Lookup_Item] FOREIGN KEY([Item])
REFERENCES [dbo].[Lookup_Item] ([Item])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Lookup_Item]
GO
ALTER TABLE [dbo].[TransactionsTicket]  WITH CHECK ADD  CONSTRAINT [FK__Transacti__Trans__06CD04F7] FOREIGN KEY([TransactionID])
REFERENCES [dbo].[Transactions] ([TransactionID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TransactionsTicket] CHECK CONSTRAINT [FK__Transacti__Trans__06CD04F7]
GO
ALTER TABLE [dbo].[TransactionsTicket]  WITH CHECK ADD  CONSTRAINT [FK_TransactionsTicket_Lookup_AccessType] FOREIGN KEY([AccessType])
REFERENCES [dbo].[Lookup_AccessType] ([AccessType])
GO
ALTER TABLE [dbo].[TransactionsTicket] CHECK CONSTRAINT [FK_TransactionsTicket_Lookup_AccessType]
GO
ALTER TABLE [dbo].[TransactionsTicket]  WITH CHECK ADD  CONSTRAINT [FK_TransactionsTicket_Lookup_TicketType] FOREIGN KEY([TicketType])
REFERENCES [dbo].[Lookup_TicketType] ([TicketType])
GO
ALTER TABLE [dbo].[TransactionsTicket] CHECK CONSTRAINT [FK_TransactionsTicket_Lookup_TicketType]
GO
/****** Object:  StoredProcedure [dbo].[AddNewCollection]    Script Date: 4/23/2022 6:35:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[AddNewCollection]  
(  
   @CollectionName varchar (300),  
   @Description varchar (500)
)  
as  
begin  
   Insert into Collections values(@CollectionName,@Description)  
End
GO
ALTER DATABASE [dt-team2] SET  READ_WRITE 
GO
