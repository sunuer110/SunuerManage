
GO
/****** Object:  Table [dbo].[SMS]    Script Date: 2025/3/15 17:14:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SMS](
	[SMSID] [int] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NULL,
	[Phone] [nvarchar](50) NULL,
	[ClassID] [int] NULL,
	[Number] [int] NULL,
	[Flag] [int] NULL,
	[IsOk] [int] NULL,
	[IP] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[SMSID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SMSRule]    Script Date: 2025/3/15 17:14:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SMSRule](
	[Rid] [int] IDENTITY(1,1) NOT NULL,
	[Statue] [int] NULL,
	[RIP] [int] NULL,
	[RNum] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Rid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[SMSRule] ON 
GO
INSERT [dbo].[SMSRule] ([Rid], [Statue], [RIP], [RNum]) VALUES (1, 0, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[SMSRule] OFF
GO
ALTER TABLE [dbo].[SMS] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[SMS] ADD  DEFAULT ((0)) FOR [ClassID]
GO
ALTER TABLE [dbo].[SMS] ADD  DEFAULT ((0)) FOR [Number]
GO
ALTER TABLE [dbo].[SMS] ADD  DEFAULT ((0)) FOR [Flag]
GO
ALTER TABLE [dbo].[SMS] ADD  DEFAULT ((0)) FOR [IsOk]
GO
ALTER TABLE [dbo].[SMSRule] ADD  DEFAULT ((0)) FOR [Statue]
GO
ALTER TABLE [dbo].[SMSRule] ADD  DEFAULT ((0)) FOR [RIP]
GO
ALTER TABLE [dbo].[SMSRule] ADD  DEFAULT ((0)) FOR [RNum]
GO
/****** Object:  StoredProcedure [dbo].[SMS_Add]    Script Date: 2025/3/15 17:14:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--添加一条信息
--项目名称：Sunuer Manage
--表名：SMS
--BY:HaiDong
--DateTime:2025-01-06 08:40:06
------------------------------------
CREATE  procedure [dbo].[SMS_Add]
(
@Phone nvarchar(50),
@ClassID int,
@Number nvarchar(10),
@IP nvarchar(50)
)
as
declare @fanhui as int--返回参数
declare @SMSID as int--验证码ID
declare @Statue as int
declare @RIP as int
declare @RNum as int
set @fanhui=0
set @RIP=0
set @RNum=0

select top 1 @Statue=Statue,@RIP=RIP,@RNum=RNum from dbo.SMSRule order by 1 desc--获取最新一条信息规则

if(select count(SMSID) from SMS where Phone=@Phone and datediff(second,CreateDate,GETDATE())<61)=0--60秒内是否已发送过验证码
	begin
        if @RIP=0--IP不受限制
           begin
              if @RNum=0--用户发送条数不受限制
				   begin
		             insert SMS(Phone,ClassID,Number,IP)values(@Phone,@ClassID,@Number,@IP)
		           set  @fanhui=-3--添加成功
				   end
				else --用户发送条数受限制
				   begin
		              if(select count(SMSID) from SMS where Phone=@Phone and datediff(day,Createdate,GETDATE())<1)<@RNum--今天内是否发送数小于设置数
							begin--小于插入发送
							   insert SMS(Phone,ClassID,Number,IP)values(@Phone,@ClassID,@Number,@IP)
							  set @fanhui=-3--添加成功
							end
					  else--大于不发送返回通知
							begin
							 set @fanhui=-2--今天发送条数已超额
							end
				   end
           end
        else--IP受限制
           begin
              if(select count(SMSID) from SMS where IP=@ip and datediff(day,Createdate,GETDATE())<1)<@RIP--IP今天内是否发送数小于设置数
					begin
						  if @RNum=0--用户发送条数不受限制
						   begin
							 insert SMS(Phone,ClassID,Number,IP)values(@Phone,@ClassID,@Number,@IP)
							set @fanhui=-3
						   end
						else --用户发送条数受限制
						   begin
							  if(select count(SMSID) from SMS where Phone=@Phone and datediff(day,Createdate,GETDATE())<1)<@RNum--今天内是否发送数小于设置数
									begin--小于插入发送
									   insert SMS(Phone,ClassID,Number,IP)values(@Phone,@ClassID,@Number,@IP)
									  set @fanhui=-3
									end
							  else--大于不发送返回通知
									begin
									 set @fanhui=-2
									end
						   end
					end
			    else
			        begin
			           set @fanhui=-4--同一IP发送数量超过设置数量
			        end	
             
             
           end
       
	end
else 
	begin
        set @fanhui=-1 --60秒内不能重复发送
	end

return @fanhui
GO
/****** Object:  StoredProcedure [dbo].[SMS_Check]    Script Date: 2025/3/15 17:14:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--验证信息是否正确
--项目名称：Sunuer Manage
--表名：SMS
--BY:HaiDong
--DateTime:2025-01-06 08:40:06
------------------------------------
CREATE  procedure [dbo].[SMS_Check]
(
@Phone nvarchar(50),
@ClassID int,
@Number  nvarchar(10),
@IP nvarchar(50)
)
as

if(select count(SMSID) from SMS where Phone=@Phone and ClassID=@ClassID  and Number=@Number  and IP=@IP  and datediff(second,Createdate,GETDATE())<301)=1
	begin
	update  SMS set IsOk=1 where Phone=@Phone and ClassID=@ClassID  and Number=@Number and IP=@IP
	  return -1--完全符合要求
	end 
else
    begin
    update  SMS set Flag=Flag+1 where Phone=@Phone and ClassID=@ClassID  and Number=@Number and IP=@IP
      return -2--不符合检索
    end
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'验证码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMS', @level2type=N'COLUMN',@level2name=N'SMSID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMS', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMS', @level2type=N'COLUMN',@level2name=N'Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据类型(1：注册验证，2：找回密码验证)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMS', @level2type=N'COLUMN',@level2name=N'ClassID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'验证码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMS', @level2type=N'COLUMN',@level2name=N'Number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'不符合检索条件验证次数（不是本人输入的）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMS', @level2type=N'COLUMN',@level2name=N'Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用0未使用1使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMS', @level2type=N'COLUMN',@level2name=N'IsOk'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IP' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMS', @level2type=N'COLUMN',@level2name=N'IP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'验证码规则' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMSRule', @level2type=N'COLUMN',@level2name=N'Rid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否生效0生效1不生效' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMSRule', @level2type=N'COLUMN',@level2name=N'Statue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IP是否受限制0不限制1限制当天1个IP一天能发几条' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMSRule', @level2type=N'COLUMN',@level2name=N'RIP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'条数是否受限制0不限制1限制：当天' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SMSRule', @level2type=N'COLUMN',@level2name=N'RNum'
GO
