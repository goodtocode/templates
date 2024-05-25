CREATE PROCEDURE [Activity].[ExceptionLogInsertByException]	
AS	
	-- Build data into message
	Declare @CustomMessage nvarchar(max)	
	Select @CustomMessage = 'Number: ' + Cast(ERROR_NUMBER() As nvarchar(100))
							+ ' Severity: ' + Cast(ERROR_SEVERITY() As nvarchar(100))
							+ ' State: ' + Cast(ERROR_STATE() As nvarchar(100))
							+ ' Procedure: ' + Cast(ERROR_PROCEDURE() As nvarchar(500))
							+ ' Line: ' + Cast(ERROR_LINE() As nvarchar(100))
	INSERT INTO [Activity].[ExceptionLog] ([Message], [CustomMessage])
		   SELECT ERROR_MESSAGE() AS [Message], IsNull(@CustomMessage, '') As [CustomMessage]