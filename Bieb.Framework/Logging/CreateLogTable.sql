BEGIN TRAN

/* Bit of a cheap workaround, but including the logging table creation here
   verbatim is better than nothing at all, I suppose. 

   Requires some work.
*/

CREATE TABLE Log (
	[LogId] BIGINT NOT NULL IDENTITY(1,1) CONSTRAINT PK_Log PRIMARY KEY	,
	[Date] DATETIME2,
	[Thread] NVARCHAR(255),
	[Level] NVARCHAR(50),
	[Logger] NVARCHAR(255),
	[Message] NVARCHAR(4000),
	[Exception] NVARCHAR(2000)
)

ROLLBACK TRAN