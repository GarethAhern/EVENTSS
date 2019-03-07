USE EventManagement

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Gareth Ahern
-- Create date: 17 May 2018
-- Description:	This will create a new Event Type
-- =============================================
CREATE PROCEDURE [dbo].[spCreateEventType]
	@NewEventType VARCHAR(100),
	@id int = 0 output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO dbo.Event_Types (Event_Type)
	VALUES (@NewEventType)

	SELECT @id = Scope_identity() 
END
GO

-- =============================================
-- Author:		Gareth Ahern
-- Create date: 17 May 2018
-- Description:	Deletes an Event Type by Id
-- =============================================
CREATE PROCEDURE [dbo].[spDeleteEventTypes_ById]
	@id as int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	delete dbo.Event_Types
	WHERE id = @id

END
GO

-- =============================================
-- Author:		Gareth Ahern
-- Create date: 17 May 2018
-- Description:	Selects all data for one client in the Clients table
-- =============================================
CREATE PROCEDURE [dbo].[spGetClient_ById]
	@Id int
AS
BEGIN
	SET NOCOUNT ON;

	select * from dbo.Clients
	where id = @Id
END
GO

-- =============================================
-- Author:		Gareth Ahern
-- Create date: 17 May 2018
-- Description:	Selects all data from all clients in Clients table
-- =============================================
CREATE PROCEDURE [dbo].[spGetClients_All]
AS
BEGIN
	SET NOCOUNT ON;

	select * from dbo.Clients
END
GO

-- =============================================
-- Author:		Gareth Ahern
-- Create date: 17 May 2018
-- Description:	Selects all possible events owners
-- =============================================
CREATE PROCEDURE [dbo].[spGetEventOwner_All]

AS
BEGIN
	SET NOCOUNT ON;

	select * from dbo.Event_Owner

END
GO

-- =============================================
-- Author:		Gareth Ahern
-- Create date: 17 May 2018
-- Description:	Selects all events - orders by event date
-- =============================================
CREATE PROCEDURE [dbo].[spGetEvents_All]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select * from dbo.Events order by Event_Date asc

END
GO

-- =============================================
-- Author:		Gareth Ahern
-- Create date: 17 May 2018
-- Description:	Selects all data for one event
-- =============================================
CREATE PROCEDURE [dbo].[spGetEvents_ByEventTypeID] 
	@EventTypeID int
AS
BEGIN

	SET NOCOUNT ON;

	select * from dbo.Events
	where Event_Type_Id = @EventTypeID

END
GO

-- =============================================
-- Author:		Gareth Ahern
-- Create date: 17 May 2018
-- Description:	Returns one Event Type
-- =============================================
CREATE PROCEDURE [dbo].[spGetEventType_ById]
	@id int
AS
BEGIN
	SET NOCOUNT ON;

	select * 
	from dbo.Event_Types
	where Id= @id
END
GO

-- =============================================
-- Author:		Gareth Ahern
-- Create date: 17 May 2018
-- Description:	Selects all data from table Event Types 
-- =============================================
CREATE PROCEDURE [dbo].[spGetEventTypes_All]

AS
BEGIN
	SET NOCOUNT ON;

	select * from dbo.Event_Types order by Event_Type
END
GO

-- =============================================
-- Author:		Gareth Ahern
-- Create date: 17 May 2018
-- Description:	Selects all event types that are currently in use
--				If an entry isn't in this list then it can be deleted....
-- =============================================
CREATE PROCEDURE [dbo].[spGetEventTypes_InUse]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT distinct et.*
	FROM dbo.Event_Types et 
	JOIN dbo.Events e on et.id=e.Event_Type_Id
END
GO

-- =============================================
-- Author:		Gareth Ahern
-- Create date: 17 May 2018
-- Description:	Get all clients that could be invited to an event
--				Client excluded if they are not currently registered (need to re-register every year)
--				Client excluded if they have already been shortlisted / invited / accepted/rejected invite
-- =============================================
CREATE PROCEDURE [dbo].[spGetInviteableClients_ByEventId]
	@EventId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * 
	FROM   dbo.Clients c 
		   JOIN dbo.Registration_Emails re 
			 ON c.id = re.Client_Id 
		   LEFT JOIN (SELECT Client_Id 
					  FROM   dbo.Event_Invite_List 
					  WHERE  Event_Id = @EventId) il 
				  ON c.id = il.Client_Id 
	WHERE  re.Reply_Of_Approval = 1 
		   AND re.Date_Email_Sent > Dateadd(year, -1, Getdate()) 
		   AND il.Client_Id IS NULL 
END
GO

-- =============================================
-- Author:		Gareth Ahern
-- Create date: 17 May 2018
-- Description:	Selects all intive list information for anyone who has been shortlisted / invited to an event
-- =============================================
CREATE PROCEDURE [dbo].[Events_spGetInviteList_ByEvent]
	@EventId int
AS
BEGIN
	SET NOCOUNT ON;

	select il.*
	from dbo.Event_Invite_List il
	join dbo.Clients c on il.Client_Id=c.Id
	where il.Event_Id = @EventId
END
GO

-- =============================================
-- Author:		Gareth Ahern
-- Create date: 17 May 2018
-- Description:	Either inserts a new event or updates all the information of an existing event
-- =============================================
CREATE PROCEDURE [dbo].[spUpSertEvent_ByEventID]
	@EventName varchar(100),
	@EventDate date,
	@EventVenue varchar(100),
	@EventVenueFullAddress varchar(3000),
	@EventDescription varchar(max),
	@EventTypeID int,
	@EventOwnerID int,
	@MaxNumberOfGuests smallint,
	@id int = 0 output
AS
BEGIN
	SET NOCOUNT ON;

	IF @id = 0

		BEGIN

			-- Insert statements for procedure here
			INSERT INTO dbo.Events
				   (Event_Name, Event_Date, Event_Venue, Event_Venue_Full_Address, Event_Description, Event_Type_Id, Event_Owner_Id, Max_Number_Of_Guests)
			 VALUES
				   (@EventName, @EventDate, @EventVenue, @EventVenueFullAddress, @EventDescription, @EventTypeID, @EventOwnerID, @MaxNumberOfGuests)

			SELECT @id = Scope_identity() 

		END

	ELSE

		BEGIN

			UPDATE dbo.Events
			SET Event_Name = @EventName, 
				Event_Date = @EventDate, 
				Event_Venue = @EventVenue, 
				Event_Venue_Full_Address = @EventVenueFullAddress, 
				Event_Description = @EventDescription, 
				Event_Type_Id = @EventTypeID, 
				Event_Owner_Id = @EventOwnerID,
				Max_Number_Of_Guests = @MaxNumberOfGuests
			WHERE id = @id

			SELECT @id 

		END
END
GO

-- =============================================
-- Author:		Gareth Ahern
-- Create date: 17 May 2018
-- Description:	Either inserts a new event or updates an invite for an event
-- =============================================
CREATE PROCEDURE [dbo].[spUpSertInviteInfo_ByInviteId]
	@EventId int,
	@ClientId int,
	@EmailSentDate datetime,
	@ClientComing bit,
	@id int = 0 output
AS
BEGIN
	SET NOCOUNT ON;

IF @id = 0
	BEGIN
		--Do I want to be able to insert all this information in one go?
		--to avoid accidental invites i want invites to be a two stage process
		--also doubt we will every invite somone knowing they will come
		INSERT INTO dbo.Event_Invite_List(Event_Id, Client_Id, Email_Sent_Date, Client_Coming)
		VALUES(@EventId, @ClientId, @EmailSentDate, @ClientComing)
	
		SELECT @id = Scope_identity() 

	END
ELSE
	BEGIN
		--I am not going to give the user the ability to change EventId or ClientId
		UPDATE dbo.Event_Invite_List
		SET 
			Email_Sent_Date = @EmailSentDate,
			Client_Coming = @ClientComing
		WHERE  id = @id 

		SELECT @id
	END
END