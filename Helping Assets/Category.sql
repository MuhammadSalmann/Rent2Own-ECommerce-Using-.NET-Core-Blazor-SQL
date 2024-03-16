create table Categories(
Id int identity(1,1) primary key,
Name varchar(50),
CreatedDate datetime2
) 
select* from Categories

create proc CP_SaveCategory
@Name varchar(50),
@CreatedDate datetime2
as
begin
insert into Categories(Name, CreatedDate)values(@Name, @CreatedDate)
end

create proc CP_GetCategories
as
begin
select* from Categories
end

create proc CP_GetCategory 
@Id int
as
begin
select* from Categories where Id=@Id
end

create proc CP_DeleteCategory
@Id int
as
begin
delete from Categories where Id=@Id
end

create proc CP_UpdateCategory
@Id int,
@Name varchar(50),
@CreatedDate datetime2
as
begin 
update Categories
set Name=@Name,
CreatedDate=@CreatedDate

where Id=@Id
end  