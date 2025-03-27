CREATE DATABASE Dilivery2DB
go
use Dilivery2DB
go

CREATE TABLE customers (
    customer_id INT PRIMARY KEY IDENTITY(1,1),
    customer_name NVARCHAR(100) NOT NULL,
    customer_address NVARCHAR(255) NOT NULL,
    customer_phone NVARCHAR(15) NOT NULL
);

CREATE TABLE restaurants (
    restaurant_id INT PRIMARY KEY IDENTITY(1,1),
    restaurant_name NVARCHAR(100) NOT NULL,
    restaurant_address NVARCHAR(255) NOT NULL,
    restaurant_phone NVARCHAR(15) NOT NULL
);

CREATE TABLE menu_items (
    menu_item_id INT PRIMARY KEY IDENTITY(1,1),
    restaurant_id INT NOT NULL,
    item_name NVARCHAR(100) NOT NULL,
    item_description NVARCHAR(255) NOT NULL,
    item_price DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (restaurant_id) REFERENCES restaurants(restaurant_id)
);

CREATE TABLE orders (
    order_id INT PRIMARY KEY IDENTITY(1,1),
    customer_id INT NOT NULL,
    restaurant_id INT NOT NULL,
    total_amount DECIMAL(10, 2) NOT NULL,
    order_date DATETIME NOT NULL,
    FOREIGN KEY (customer_id) REFERENCES customers(customer_id),
    FOREIGN KEY (restaurant_id) REFERENCES restaurants(restaurant_id)
);

CREATE TABLE order_items (
    order_item_id INT PRIMARY KEY IDENTITY(1,1),
    order_id INT NOT NULL,
    menu_item_id INT NOT NULL,
    quantity INT NOT NULL,
    FOREIGN KEY (order_id) REFERENCES orders(order_id),
    FOREIGN KEY (menu_item_id) REFERENCES menu_items(menu_item_id)
);

CREATE TABLE couriers (
    courier_id INT PRIMARY KEY IDENTITY(1,1),
    courier_name NVARCHAR(100) NOT NULL,
    courier_phone NVARCHAR(15) NOT NULL,
    vehicle_type NVARCHAR(50) NOT NULL
);

CREATE TABLE deliveries (
    delivery_id INT PRIMARY KEY IDENTITY(1,1),
    order_id INT NOT NULL UNIQUE,
    courier_id INT NOT NULL,
    delivery_status NVARCHAR(50) NOT NULL,
    delivery_date DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (order_id) REFERENCES orders(order_id),
    FOREIGN KEY (courier_id) REFERENCES couriers(courier_id)
);

CREATE TABLE reviews (
    review_id INT PRIMARY KEY IDENTITY(1,1),
    restaurant_id INT NOT NULL,
    customer_id INT NOT NULL,
    rating INT CHECK (rating BETWEEN 1 AND 5),
    comment NVARCHAR(MAX),
    review_date DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (restaurant_id) REFERENCES restaurants(restaurant_id),
    FOREIGN KEY (customer_id) REFERENCES customers(customer_id)
);
go

CREATE TABLE History 
(
    Id INT IDENTITY PRIMARY KEY,
    ProductId INT NOT NULL,
    Operation NVARCHAR(200) NOT NULL,
    CreateAt DATETIME NOT NULL DEFAULT GETDATE(),
);
go

CREATE TRIGGER Orders_INSERT
ON orders
AFTER INSERT
AS
INSERT INTO History (ProductId, Operation)
SELECT order_id, 'Заказчик ' + customer_id + 'Ресторан ' + restaurant_id + 'Итого' + total_amount
FROM INSERTED

CREATE TRIGGER couriers_DELETE
ON couriers
AFTER DELETE
AS
INSERT INTO History (ProductId, Operation)
SELECT courier_id, 'Удален курьер ' + courier_name + 'номер тел. ' + courier_phone
FROM DELETED

CREATE TRIGGER MenuItem_UPDATE
ON menu_items
AFTER UPDATE
AS
INSERT INTO History (ProductId, Operation)
SELECT menu_item_id, 'Обновлен предмет ' + item_name + 'Цена ' + item_price
FROM INSERTED
