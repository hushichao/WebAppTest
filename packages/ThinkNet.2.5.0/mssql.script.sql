CREATE TABLE [dbo].[thinknet_events](
    [AggregateId]        VARCHAR(36) NOT NULL,
	[AggregateTypeCode]  INT NOT NULL,
    [Version]            INT NOT NULL,
    [Payload]            VARCHAR(MAX) NOT NULL,
    [CorrelationId]      VARCHAR(36) NOT NULL,
	[OnCreated]          DATETIME NOT NULL,
CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED
(
    [AggregateId]        ASC,
	[AggregateTypeCode]  ASC,
    [Version]            ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[thinknet_handlers](
    [CorrelationId]    VARCHAR(36) NOT NULL,
	[MessageTypeCode]         INT NOT NULL,
    [HandlerTypeCode]  INT NOT NULL,
	[OnCreated]        DATETIME NOT NULL,
CONSTRAINT [PK_Handlers] PRIMARY KEY CLUSTERED
(
    [CorrelationId]    ASC,
	[TypeCode]         ASC,
	[HandlerTypeCode]  ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[thinknet_snapshots](
    [AggregateId]        VARCHAR(36) NOT NULL,
    [AggregateTypeCode]  INT NOT NULL,
    [Version]            INT NOT NULL,
    [Data]               VARCHAR(MAX) NOT NULL,
	[OnCreated]          DATETIME NOT NULL,
CONSTRAINT [PK_Snapshots] PRIMARY KEY CLUSTERED
(
    [AggregateId]        ASC,
	[AggregateTypeCode]  ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO