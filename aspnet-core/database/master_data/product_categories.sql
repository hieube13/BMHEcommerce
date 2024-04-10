INSERT INTO [dbo].[AppProductCategories]
           ([Id]
           ,[Name]
           ,[Code]
           ,[Slug]
           ,[SortOrder]
           ,[CoverPicture]
           ,[Visibility]
           ,[IsActive]
           ,[ParentId]
           ,[SeoMetaDescription]
           ,[ExtraProperties]
           ,[ConcurrencyStamp]
           ,[CreationTime]
           ,[CreatorId])
     VALUES
           (newid()
           ,N'Điện thoại'
           ,'C1'
           ,'dien-thoai'
           ,1
           ,0
           ,1
           ,1
           ,null
           ,'danh mục điện thoại'
           ,null
           ,null
           ,getdate()
           ,null)


INSERT INTO [dbo].[AppProductCategories]
           ([Id]
           ,[Name]
           ,[Code]
           ,[Slug]
           ,[SortOrder]
           ,[CoverPicture]
           ,[Visibility]
           ,[IsActive]
           ,[ParentId]
           ,[SeoMetaDescription]
           ,[ExtraProperties]
           ,[ConcurrencyStamp]
           ,[CreationTime]
           ,[CreatorId])
     VALUES
           (newid()
           ,N'PLaptop'
           ,'C2'
           ,'lap-top'
           ,1
           ,0
           ,1
           ,1
           ,null
           ,'máy tính xách tay'
           ,null
           ,null
           ,getdate()
           ,null)