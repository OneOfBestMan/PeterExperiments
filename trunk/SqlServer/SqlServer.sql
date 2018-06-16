获取所有可空的表字段
为可空字段增加默认值约束
更新有空值的字段
修改字段为非空
注意：有非聚集索引的和统计信息需要先删除
删除的话，可以用生成脚本的方法把所有的索引先保存一份，等任务完成后，可以再创建索引

--SELECT  TABLE_NAME, COLUMN_NAME,DATA_TYPE,CHARACTER_MAXIMUM_LENGTH,COLUMN_DEFAULT
--FROM INFORMATION_SCHEMA.COLUMNS
--WHERE IS_NULLABLE = 'YES'
--AND COLUMN_NAME not in ('DeletionTime','DeleterUserId','LastModificationTime','LastModifierUserId','CreatorUserId')
-- And Table_Name like 'Youzy_%' 
-- and Table_Name Not like '%_bak%'
--  and Table_Name Not like '%_old%'
--    and Table_Name Not like '%_0606%'

 --truncate table A_AChangeNotNull
 --select * from A_AChangeNotNull where IsPass=0

Declare @TableName nvarchar(200)
declare @ColumnName nvarchar(200)
declare @DataType nvarchar(50)
declare @ColumnLength int
declare @ColumnDefault nvarchar(200)
declare @exeSql nvarchar(4000)
declare @err_msg AS NVARCHAR(MAX);
declare @DefaultValue nvarchar(200)
 
declare  TempChangeNotNullCursor cursor for
SELECT  TABLE_NAME, COLUMN_NAME,DATA_TYPE,CHARACTER_MAXIMUM_LENGTH,COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS
WHERE IS_NULLABLE = 'YES'
AND COLUMN_NAME not in ('DeletionTime','DeleterUserId','LastModificationTime','LastModifierUserId','CreatorUserId')
 And Table_Name like 'Youzy_%' 
 and Table_Name Not like '%_bak%'
 and Table_Name Not like '%_old%'
 and Table_Name Not like '%_0606%'
open TempChangeNotNullCursor
fetch next from TempChangeNotNullCursor into @TableName,@ColumnName,@DataType,@ColumnLength,@ColumnDefault

while @@FETCH_STATUS=0
begin
      -- print @TableName
       set @exeSql=''
		BEGIN TRY
		if @ColumnDefault is null
		begin

		set @DefaultValue=case when @DataType='char' or @DataType='nchar' or @DataType='nvarchar' or @DataType='varchar' or @DataType='text'  or @DataType='ntext' then '''''' 
						       when @DataType='int' or @DataType='decimal' or @DataType='bit' then '''0''' 
							   when @DataType='datetime' then '(GETDATE())'
							   else '''' end
		set @exeSql=@exeSql + 'ALTER TABLE '+@TableName+'
					 ADD CONSTRAINT '+@ColumnName+'_Con'+Replace(Replace(Replace(convert(nvarchar(20),Getdate(),120),' ',''),':',''),'-','')+(cast(floor(1000 * RAND(convert(varbinary, newid())))  as nvarchar(10)))+' DEFAULT '+@DefaultValue+'  for ['+@ColumnName+'];'
		end
		set @exeSql=@exeSql + '  UPDATE '+@TableName+' SET ['+@ColumnName+'] ='+
		case when @DataType='char' or @DataType='nchar' or @DataType='nvarchar' or @DataType='varchar'  or @DataType='text'  or @DataType='ntext' then '''''' 
						       when @DataType='int' or @DataType='decimal' or @DataType='bit' then '0'
							   when @DataType='datetime' then '''1753-1-1'''
							   else '' end		
		+' where ['+@ColumnName+'] IS NULL;'

		set @exeSql=@exeSql+ 'ALTER TABLE '+@TableName+' ALTER COLUMN ['+@ColumnName+'] '+
		case when @ColumnLength> 10000000 or  @ColumnLength is null then  ' '+@DataType+'  NOT NULL;' 
			 when @ColumnLength < 10000000 and @ColumnLength >0 then ' '+@DataType+'('+ cast(@ColumnLength as nvarchar(10))+')	NOT NULL;'
			 when @ColumnLength=-1 then ' '+@DataType+'(max) NOT NULL;'
			 else  ' '+@DataType+'  NOT NULL;'  end

		

		exec(@exeSql)

		insert into A_AChangeNotNull values(@TableName,@ColumnName,1,GETDATE(),'')
		END TRY
		BEGIN CATCH
		 SET @err_msg = ERROR_MESSAGE();
		 print @TableName
		 print @err_msg
		  print @exeSql
		insert into A_AChangeNotNull values(@TableName,@ColumnName,0,GETDATE(),@err_msg)
		END CATCH

		fetch next from TempChangeNotNullCursor into @TableName,@ColumnName,@DataType,@ColumnLength,@ColumnDefault
end
close TempChangeNotNullCursor
deallocate TempChangeNotNullCursor


