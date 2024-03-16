create table ProductPrices(
Id int identity(1,1) primary key,
ProductId int,
Duration nvarchar(50),
Price float
FOREIGN KEY (ProductId) REFERENCES Products(Id)
);
select* from ProductPrices

CREATE PROCEDURE CP_SaveProductPrice
    @ProductId int,
    @Duration nvarchar(50),
    @Price float
AS
BEGIN
    INSERT INTO ProductPrices (ProductId, Duration, Price)
    VALUES (@ProductId, @Duration, @Price);
END;


CREATE PROCEDURE CP_GetProductPrices
AS
BEGIN
    SELECT * FROM ProductPrices;
END;

CREATE PROCEDURE CP_GetProductPrice
    @Id int
AS
BEGIN
    SELECT * FROM ProductPrices WHERE Id = @Id;
END;


create proc CP_DeleteProductPrice
@Id int
as
begin
delete from ProductPrices where Id=@Id
end

CREATE PROCEDURE CP_UpdateProductPrice
    @Id int,
    @ProductId int,
    @Duration nvarchar(50),
    @Price float
AS
BEGIN
    UPDATE ProductPrices
    SET 
        ProductId = @ProductId,
        Duration = @Duration,
        Price = @Price
    WHERE
        Id = @Id;
END;
