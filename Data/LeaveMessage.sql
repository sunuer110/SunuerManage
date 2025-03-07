/****** Object:  Table [dbo].[LeaveMessage]    Script Date: 2025/3/7 14:08:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveMessage](
	[LeaveMessageID] [int] IDENTITY(1,1) NOT NULL,
	[CreateUserID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUserID] [int] NULL,
	[UpdateDate] [datetime] NULL,
	[Del] [int] NULL,
	[Title] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
	[Content] [nvarchar](500) NULL,
	[Email] [nvarchar](50) NULL,
	[AuditUserID] [int] NULL,
	[AuditStatus] [int] NULL,
	[AuditTime] [datetime] NULL,
	[AuditDesn] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[LeaveMessageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LeaveMessageLog]    Script Date: 2025/3/7 14:08:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveMessageLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LogDate] [datetime] NULL,
	[LogUserID] [int] NULL,
	[LogAbout] [nvarchar](200) NULL,
	[LeaveMessageID] [int] NOT NULL,
	[CreateUserID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUserID] [int] NULL,
	[UpdateDate] [datetime] NULL,
	[Del] [int] NULL,
	[Title] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
	[Content] [nvarchar](500) NULL,
	[Email] [nvarchar](50) NULL,
	[AuditUserID] [int] NULL,
	[AuditStatus] [int] NULL,
	[AuditTime] [datetime] NULL,
	[AuditDesn] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LeaveMessageSet]    Script Date: 2025/3/7 14:08:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveMessageSet](
	[SetID] [int] IDENTITY(1,1) NOT NULL,
	[CreateUserID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUserID] [int] NULL,
	[UpdateDate] [datetime] NULL,
	[Del] [int] NULL,
	[PhoneRequire] [int] NULL,
	[NameRequire] [int] NULL,
	[EmailRequire] [int] NULL,
	[SystemEmail] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[SetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LeaveMessageSetLog]    Script Date: 2025/3/7 14:08:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveMessageSetLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LogDate] [datetime] NULL,
	[LogUserID] [int] NULL,
	[LogAbout] [nvarchar](200) NULL,
	[SetID] [int] NOT NULL,
	[CreateUserID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[UpdateUserID] [int] NULL,
	[UpdateDate] [datetime] NULL,
	[Del] [int] NULL,
	[PhoneRequire] [int] NULL,
	[NameRequire] [int] NULL,
	[EmailRequire] [int] NULL,
	[SystemEmail] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[LeaveMessageSet] ON 
GO
INSERT [dbo].[LeaveMessageSet] ([SetID], [CreateUserID], [CreateDate], [UpdateUserID], [UpdateDate], [Del], [PhoneRequire], [NameRequire], [EmailRequire], [SystemEmail]) VALUES (1, 0, CAST(N'2025-02-10T14:07:59.200' AS DateTime), 1, CAST(N'2025-02-11T17:01:10.720' AS DateTime), 0, 1, 1, 0, N'105088229@qq.com')
GO
SET IDENTITY_INSERT [dbo].[LeaveMessageSet] OFF
GO
ALTER TABLE [dbo].[LeaveMessage] ADD  DEFAULT ((0)) FOR [CreateUserID]
GO
ALTER TABLE [dbo].[LeaveMessage] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[LeaveMessage] ADD  DEFAULT ((0)) FOR [UpdateUserID]
GO
ALTER TABLE [dbo].[LeaveMessage] ADD  DEFAULT ((0)) FOR [Del]
GO
ALTER TABLE [dbo].[LeaveMessage] ADD  DEFAULT ((0)) FOR [AuditUserID]
GO
ALTER TABLE [dbo].[LeaveMessage] ADD  DEFAULT ((0)) FOR [AuditStatus]
GO
ALTER TABLE [dbo].[LeaveMessageLog] ADD  DEFAULT (getdate()) FOR [LogDate]
GO
ALTER TABLE [dbo].[LeaveMessageLog] ADD  DEFAULT ((0)) FOR [LogUserID]
GO
ALTER TABLE [dbo].[LeaveMessageLog] ADD  DEFAULT ((0)) FOR [CreateUserID]
GO
ALTER TABLE [dbo].[LeaveMessageLog] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[LeaveMessageLog] ADD  DEFAULT ((0)) FOR [UpdateUserID]
GO
ALTER TABLE [dbo].[LeaveMessageLog] ADD  DEFAULT ((0)) FOR [Del]
GO
ALTER TABLE [dbo].[LeaveMessageLog] ADD  DEFAULT ((0)) FOR [AuditUserID]
GO
ALTER TABLE [dbo].[LeaveMessageLog] ADD  DEFAULT ((0)) FOR [AuditStatus]
GO
ALTER TABLE [dbo].[LeaveMessageSet] ADD  DEFAULT ((0)) FOR [CreateUserID]
GO
ALTER TABLE [dbo].[LeaveMessageSet] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[LeaveMessageSet] ADD  DEFAULT ((0)) FOR [UpdateUserID]
GO
ALTER TABLE [dbo].[LeaveMessageSet] ADD  DEFAULT ((0)) FOR [Del]
GO
ALTER TABLE [dbo].[LeaveMessageSetLog] ADD  DEFAULT (getdate()) FOR [LogDate]
GO
ALTER TABLE [dbo].[LeaveMessageSetLog] ADD  DEFAULT ((0)) FOR [LogUserID]
GO
ALTER TABLE [dbo].[LeaveMessageSetLog] ADD  DEFAULT ((0)) FOR [CreateUserID]
GO
ALTER TABLE [dbo].[LeaveMessageSetLog] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[LeaveMessageSetLog] ADD  DEFAULT ((0)) FOR [UpdateUserID]
GO
ALTER TABLE [dbo].[LeaveMessageSetLog] ADD  DEFAULT ((0)) FOR [Del]
GO
/****** Object:  StoredProcedure [dbo].[LeaveMessage_Add]    Script Date: 2025/3/7 14:08:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--添加一条记录
--项目名称：Sunuer Manage
--表名：LeaveMessage留言
--BY:HaiDong
--DateTime:2025-02-10 15:18:15
------------------------------------
CREATE Procedure [dbo].[LeaveMessage_Add]
    @CreateUserID int,
    @Title nvarchar(50),
    @Phone nvarchar(50),
    @UserName nvarchar(50),
    @Content nvarchar(500),
    @Email nvarchar(50)
as
begin
	declare @fanhui int =0

    insert into [dbo].[LeaveMessage] ([CreateUserID], [Title], [Phone], [UserName], [Content], [Email])
    values (@CreateUserID, @Title, @Phone, @UserName, @Content, @Email)
    set @fanhui= @@identity

	---插入日志语句
    insert into [dbo].[LeaveMessageLog] ([LogDate],[LogUserID],[LogAbout],[LeaveMessageID], [CreateUserID], [CreateDate], [UpdateUserID], [UpdateDate], [Del], [Title], [Phone], [UserName], [Content], [Email], [AuditUserID], [AuditStatus], [AuditTime], [AuditDesn])
    select  getdate(),CreateUserID,'提交留言',LeaveMessageID, CreateUserID, CreateDate, UpdateUserID, UpdateDate, Del, Title, Phone, UserName, Content, Email, AuditUserID, AuditStatus, AuditTime, AuditDesn from LeaveMessage where LeaveMessageID=@fanhui
	return @fanhui
end
GO
/****** Object:  StoredProcedure [dbo].[LeaveMessage_Audit]    Script Date: 2025/3/7 14:08:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


------------------------------------
--留言审核
--项目名称：Sunuer Manage
--表名称：LeaveMessage留言
--BY:HaiDong
--DateTime:2025-02-11 10:00:17
------------------------------------
CREATE Procedure [dbo].[LeaveMessage_Audit]
    @LeaveMessageID int,
    @UpdateUserID int,
	@AuditStatus int,
	@AuditDesn nvarchar(500)
AS
begin
	declare @fanhui int = 0
	-- 编辑审核人、审核状态等数据
    update [dbo].[LeaveMessage]
    set  [UpdateUserID] = @UpdateUserID, [UpdateDate] = GETDATE(),  [AuditUserID] = @UpdateUserID, 
	[AuditStatus] = @AuditStatus, [AuditTime] = GETDATE(), [AuditDesn] = @AuditDesn
    where [LeaveMessageID] = @LeaveMessageID
	select @fanhui=@@ROWCOUNT
	---插入日志语句
    insert into [dbo].[LeaveMessageLog] ([LogDate],[LogUserID],[LogAbout],[LeaveMessageID], [CreateUserID], [CreateDate], [UpdateUserID], [UpdateDate], [Del], [Title], [Phone], [UserName], [Content], [Email], [AuditUserID], [AuditStatus], [AuditTime], [AuditDesn])
    select  getdate(),UpdateUserID,'审核留言',LeaveMessageID, CreateUserID, CreateDate, UpdateUserID, UpdateDate, Del, Title, Phone, UserName, Content, Email, AuditUserID, AuditStatus, AuditTime, AuditDesn from LeaveMessage where LeaveMessageID=@LeaveMessageID
	
    return @fanhui
end
GO
/****** Object:  StoredProcedure [dbo].[LeaveMessage_Delete]    Script Date: 2025/3/7 14:08:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--删除一条记录
--项目名称：Sunuer Manage
--表名：LeaveMessage留言
--BY:HaiDong
--DateTime:2025-02-11 17:23:08
------------------------------------
CREATE Procedure [dbo].[LeaveMessage_Delete]
    @LeaveMessageID int,
    @UpdateUserID int
as
begin
declare @fanhui int = 0
    update  [dbo].[LeaveMessage] set Del=1 ,UpdateUserID = @UpdateUserID, UpdateDate = getdate() where [LeaveMessageID] = @LeaveMessageID
    select @fanhui=@@ROWCOUNT
	---插入日志语句
    insert into [dbo].[LeaveMessageLog] ([LogDate],[LogUserID],[LogAbout],[LeaveMessageID], [CreateUserID], [CreateDate], [UpdateUserID], [UpdateDate], [Del], [Title], [Phone], [UserName], [Content], [Email], [AuditUserID], [AuditStatus], [AuditTime], [AuditDesn])
    select  getdate(),UpdateUserID,'留言删除',LeaveMessageID, CreateUserID, CreateDate, UpdateUserID, UpdateDate, Del, Title, Phone, UserName, Content, Email, AuditUserID, AuditStatus, AuditTime, AuditDesn from LeaveMessage where LeaveMessageID=@LeaveMessageID
	
    return @fanhui
end
GO
/****** Object:  StoredProcedure [dbo].[LeaveMessage_Get]    Script Date: 2025/3/7 14:08:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--读取记录列表
--项目名称：Sunuer Manage
--表名：LeaveMessage留言
--BY:HaiDong
--DateTime:2025-02-11 09:07:26
------------------------------------
CREATE Procedure [dbo].[LeaveMessage_Get]
    @StartRecord int, 
    @EndRecord int,
    @Title nvarchar(50),
    @UserName nvarchar(50),
	@AuditStatus int
as
begin
    declare @s nvarchar(MAX)
    declare @searchs nvarchar(2000) = ''
    if @Title <> ''
    begin
    set @searchs = @searchs + ' and charindex(@Title, n.Title) > 0';
    end
    if @UserName <> ''
    begin
    set @searchs = @searchs + ' and charindex(@UserName, n.UserName) > 0';
    end
	 if @AuditStatus <> -1
    begin
    set @searchs = @searchs + ' and n.AuditStatus=@AuditStatus'
    end
    set @s ='select a.*from(
    select row_number() over(order by  n.[LeaveMessageID] desc) as aid ,n.*,ad.AdminNick as AuditUserName FROM  [dbo].[LeaveMessage] as n 
	left join [Admin] as ad on ad.AdminID=n.AuditUserID
	where n.del=0  '+@searchs+') as a
    where a.aid between @StartRecord and (@EndRecord+@StartRecord-1) order by aid asc '
    print @s;
    EXEC sp_executesql @s, N'@Title nvarchar(50),@UserName nvarchar(50),@AuditStatus int,@StartRecord int,@EndRecord int', @Title,@UserName,@AuditStatus,@StartRecord,@EndRecord 
end
GO
/****** Object:  StoredProcedure [dbo].[LeaveMessage_GetCount]    Script Date: 2025/3/7 14:08:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--读取记录列表
--项目名称：Sunuer Manage
--表名：LeaveMessage留言
--BY:HaiDong
--DateTime:2025-02-11 09:07:26
------------------------------------
CREATE Procedure [dbo].[LeaveMessage_GetCount]
     @Title nvarchar(50),
    @UserName nvarchar(50),
	@AuditStatus int

as
begin
    declare @s nvarchar(MAX)
    declare @searchs nvarchar(2000) = ''
    if @Title <> ''
    begin
    set @searchs = @searchs + ' and charindex(@Title, n.Title) > 0';
    end
    if @UserName <> ''
    begin
    set @searchs = @searchs + ' and charindex(@UserName, n.UserName) > 0';
    end
	 if @AuditStatus <> -1
    begin
    set @searchs = @searchs + ' and n.AuditStatus=@AuditStatus'
    end
    set @s ='select Count(n.[LeaveMessageID]) as Num from  [dbo].[LeaveMessage] as n where n.del=0 '+@searchs
    print @s;
    EXEC sp_executesql @s, N'@Title nvarchar(50),@UserName nvarchar(50),@AuditStatus int', @Title,@UserName,@AuditStatus
end
GO
/****** Object:  StoredProcedure [dbo].[LeaveMessage_GetModel]    Script Date: 2025/3/7 14:08:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--获取一条记录
--项目名称：Sunuer Manage
--表名：LeaveMessage留言
--BY:HaiDong
--DateTime:2025-03-07 13:27:02
------------------------------------
Create Procedure [dbo].[LeaveMessage_GetModel]
    @LeaveMessageID int
AS
begin
    select * from [dbo].[LeaveMessage]
    where Del=0 and [LeaveMessageID] = @LeaveMessageID
end
GO
/****** Object:  StoredProcedure [dbo].[LeaveMessageSet_GetModel]    Script Date: 2025/3/7 14:08:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--获取一条记录
--项目名称：Sunuer Manage
--表名：LeaveMessageSet留言配置表
--BY:HaiDong
--DateTime:2025-02-10 13:34:03
------------------------------------
CREATE Procedure [dbo].[LeaveMessageSet_GetModel]
    @SetID int
AS
begin
    select * from [dbo].[LeaveMessageSet]
    where Del=0 and [SetID] = @SetID
end
GO
/****** Object:  StoredProcedure [dbo].[LeaveMessageSet_Update]    Script Date: 2025/3/7 14:08:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--更新一条记录
--项目名称：Sunuer Manage
--表名：LeaveMessageSet留言配置表
--BY:HaiDong
--DateTime:2025-02-10 13:34:03
------------------------------------
CREATE Procedure [dbo].[LeaveMessageSet_Update]
    @SetID int,
    @UpdateUserID int,
    @PhoneRequire int,
    @NameRequire int,
    @EmailRequire int,
    @SystemEmail nvarchar(50)
AS
begin

	declare @fanhui int=0
    update [dbo].[LeaveMessageSet]
    set [UpdateUserID] = @UpdateUserID, [UpdateDate] = GETDATE(),  [PhoneRequire] = @PhoneRequire, [NameRequire] = @NameRequire, [EmailRequire] = @EmailRequire, [SystemEmail] = @SystemEmail
    where [SetID] = @SetID
	set @fanhui=@@rowcount
	---插入日志语句
    insert into [dbo].[LeaveMessageSetLog] ([LogDate],[LogUserID],[LogAbout],[SetID], [CreateUserID], [CreateDate], [UpdateUserID], [UpdateDate], [Del], [PhoneRequire], [NameRequire], [EmailRequire], [SystemEmail])
    select  getdate(),UpdateUserID,'更新配置',SetID, CreateUserID, CreateDate, UpdateUserID, UpdateDate, Del, PhoneRequire, NameRequire, EmailRequire, SystemEmail from LeaveMessageSet where SetID=@SetID
    return @fanhui
end
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessage', @level2type=N'COLUMN',@level2name=N'LeaveMessageID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessage', @level2type=N'COLUMN',@level2name=N'CreateUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessage', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessage', @level2type=N'COLUMN',@level2name=N'UpdateUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessage', @level2type=N'COLUMN',@level2name=N'UpdateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除0否1是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessage', @level2type=N'COLUMN',@level2name=N'Del'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessage', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessage', @level2type=N'COLUMN',@level2name=N'Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessage', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessage', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮箱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessage', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessage', @level2type=N'COLUMN',@level2name=N'AuditUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核状态0未审核1审核通过2审核未通过' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessage', @level2type=N'COLUMN',@level2name=N'AuditStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessage', @level2type=N'COLUMN',@level2name=N'AuditTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessage', @level2type=N'COLUMN',@level2name=N'AuditDesn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageLog', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageLog', @level2type=N'COLUMN',@level2name=N'LogDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageLog', @level2type=N'COLUMN',@level2name=N'LogUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageLog', @level2type=N'COLUMN',@level2name=N'LogAbout'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageLog', @level2type=N'COLUMN',@level2name=N'LeaveMessageID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageLog', @level2type=N'COLUMN',@level2name=N'CreateUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageLog', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageLog', @level2type=N'COLUMN',@level2name=N'UpdateUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageLog', @level2type=N'COLUMN',@level2name=N'UpdateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除0否1是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageLog', @level2type=N'COLUMN',@level2name=N'Del'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageLog', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageLog', @level2type=N'COLUMN',@level2name=N'Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageLog', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageLog', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮箱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageLog', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageLog', @level2type=N'COLUMN',@level2name=N'AuditUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核状态0未审核1审核通过2审核未通过' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageLog', @level2type=N'COLUMN',@level2name=N'AuditStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageLog', @level2type=N'COLUMN',@level2name=N'AuditTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageLog', @level2type=N'COLUMN',@level2name=N'AuditDesn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageSet', @level2type=N'COLUMN',@level2name=N'SetID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageSet', @level2type=N'COLUMN',@level2name=N'CreateUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageSet', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageSet', @level2type=N'COLUMN',@level2name=N'UpdateUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageSet', @level2type=N'COLUMN',@level2name=N'UpdateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除0否1是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageSet', @level2type=N'COLUMN',@level2name=N'Del'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号0非必填1必填' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageSet', @level2type=N'COLUMN',@level2name=N'PhoneRequire'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'姓名0非必填1必填' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageSet', @level2type=N'COLUMN',@level2name=N'NameRequire'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮箱0非必填1必填' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageSet', @level2type=N'COLUMN',@level2name=N'EmailRequire'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言系统邮箱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageSet', @level2type=N'COLUMN',@level2name=N'SystemEmail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageSetLog', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageSetLog', @level2type=N'COLUMN',@level2name=N'LogDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageSetLog', @level2type=N'COLUMN',@level2name=N'LogUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageSetLog', @level2type=N'COLUMN',@level2name=N'LogAbout'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageSetLog', @level2type=N'COLUMN',@level2name=N'SetID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageSetLog', @level2type=N'COLUMN',@level2name=N'CreateUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageSetLog', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageSetLog', @level2type=N'COLUMN',@level2name=N'UpdateUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageSetLog', @level2type=N'COLUMN',@level2name=N'UpdateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除0否1是' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageSetLog', @level2type=N'COLUMN',@level2name=N'Del'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号0非必填1必填' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageSetLog', @level2type=N'COLUMN',@level2name=N'PhoneRequire'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'姓名0非必填1必填' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageSetLog', @level2type=N'COLUMN',@level2name=N'NameRequire'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮箱0非必填1必填' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageSetLog', @level2type=N'COLUMN',@level2name=N'EmailRequire'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言系统邮箱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LeaveMessageSetLog', @level2type=N'COLUMN',@level2name=N'SystemEmail'
GO

SET IDENTITY_INSERT [dbo].[AdminPower] ON 
GO
INSERT [dbo].[AdminPower] ([PowerID], [CreateUserID], [CreateDate], [UpdateDate], [UpdateUserID], [Del], [ParentID], [PowerTitle]) VALUES (404, 0, CAST(N'2025-02-12T08:32:08.340' AS DateTime), NULL, 0, 0, 400, N'留言删除')
GO
INSERT [dbo].[AdminPower] ([PowerID], [CreateUserID], [CreateDate], [UpdateDate], [UpdateUserID], [Del], [ParentID], [PowerTitle]) VALUES (400, 0, CAST(N'2025-02-11T14:44:51.500' AS DateTime), NULL, 0, 0, 0, N'留言管理')
GO
INSERT [dbo].[AdminPower] ([PowerID], [CreateUserID], [CreateDate], [UpdateDate], [UpdateUserID], [Del], [ParentID], [PowerTitle]) VALUES (401, 0, CAST(N'2025-02-11T14:45:32.523' AS DateTime), NULL, 0, 0, 400, N'留言配置')
GO
INSERT [dbo].[AdminPower] ([PowerID], [CreateUserID], [CreateDate], [UpdateDate], [UpdateUserID], [Del], [ParentID], [PowerTitle]) VALUES (402, 0, CAST(N'2025-02-11T14:45:47.317' AS DateTime), NULL, 0, 0, 400, N'留言列表')
GO
INSERT [dbo].[AdminPower] ([PowerID], [CreateUserID], [CreateDate], [UpdateDate], [UpdateUserID], [Del], [ParentID], [PowerTitle]) VALUES (403, 0, CAST(N'2025-02-11T14:45:59.737' AS DateTime), NULL, 0, 0, 400, N'留言审核')
GO

SET IDENTITY_INSERT [dbo].[AdminPower] OFF
GO