create table Products(
Id int identity(1,1) primary key,
Name varchar(50),
Description nvarchar(max),
ShopFavorites bit,
CustomerFavorites bit,
Model int,
Company varchar(50),
ImageUrl nvarchar(max),
CategoryId int,
FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);
select* from Products

CREATE PROCEDURE CP_SaveProduct
    @Name varchar(50),
    @Description nvarchar(max),
    @ShopFavorites bit,
    @CustomerFavorites bit,
    @Model int,
    @Company varchar(50),
    @ImageUrl nvarchar(max),
    @CategoryId int
AS
BEGIN
    INSERT INTO Products (Name, Description, ShopFavorites, CustomerFavorites, Model, Company, ImageUrl, CategoryId)
    VALUES (@Name, @Description, @ShopFavorites, @CustomerFavorites, @Model, @Company, @ImageUrl, @CategoryId);
END;

CREATE PROCEDURE CP_GetProducts
AS
BEGIN
    SELECT * FROM Products;
END;

CREATE PROCEDURE CP_GetProduct
    @Id int
AS
BEGIN
    SELECT * FROM Products WHERE Id = @Id;
END;

create proc CP_DeleteProduct
@Id int
as
begin
delete from Products where Id=@Id
end

CREATE PROCEDURE CP_UpdateProduct
    @Id int,
    @Name varchar(50),
    @Description nvarchar(max),
    @ShopFavorites bit,
    @CustomerFavorites bit,
    @Model int,
    @Company varchar(50),
    @ImageUrl nvarchar(max),
    @CategoryId int
AS
BEGIN
    UPDATE Products
    SET 
        Name = @Name,
        Description = @Description,
        ShopFavorites = @ShopFavorites,
        CustomerFavorites = @CustomerFavorites,
        Model = @Model,
        Company = @Company,
        ImageUrl = @ImageUrl,
        CategoryId = @CategoryId
    WHERE
        Id = @Id;
END;