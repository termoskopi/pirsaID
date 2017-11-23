create table [User] (
	[Id] integer not null primary key,
	[Username] nvarchar (128) not null,
	[Password] nvarchar (256) not null,
	[LastLogin] datetime not null default current_timestamp
);

create table [SystemTask] (
	[Id] integer not null primary key,
	[Activity] nvarchar (256) not null,
	[Status] int not null,
	[StatusText] nvarchar (32) not null,
	[QueuedDate] datetime,
	[StartDate] datetime,
	[FinishedDate] datetime
);

create table [History] (
	[Id] integer not null primary key,
	[Activity] nvarchar (512) not null,
	[Source] nvarchar (256),
	[ActionDate] datetime not null default current_timestamp
);

create table [Results] (
	[Id] integer not null primary key,
	[TaskId] int not null,
	[VideoSource] nvarchar (256) not null
);

create table [ResultItem] (
	[Id] integer not null primary key,
	[ResultId] int not null,
	[DetectionResult] nvarchar (64) not null
);

create table [AccessToken] (
	[Token] nvarchar (256) not null primary key,
	[UserId] int not null,
	[CreatedDate] datetime not null default current_timestamp,
	[IsActive] int not null
);

insert into [User] ([Username], [Password]) values ('administrator', 'admin');
insert into [User] ([Username], [Password]) values ('application1', 'app1');
insert into [User] ([Username], [Password]) values ('application2', 'app2');
insert into [AccessToken] ([Token], [UserId], [IsActive]) values ('4194d1706ed1f408d5e02d672777019f4d5385c766a8c6ca8acba3167d36a7b9', 1, 1);
insert into [AccessToken] ([Token], [UserId], [IsActive]) values ('9d174dea750c91505e2c72eb0c00ceed6f4afed40f7ebea1873e2d2638dd6f8f', 2, 1);
insert into [AccessToken] ([Token], [UserId], [IsActive]) values ('231966438f42737f00ed00676df1bbdb9b84a4db124ed4c96e149de42ba4da84', 3, 1);


