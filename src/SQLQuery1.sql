SELECT * FROM Orders as orders
inner join OrderDetails as orddet
on orders.Id = orddet.OrderId
inner join Customers as cus
on cus.Id = orders.CustomerId
inner join Products as pro
on pro.Id = orddet.ProductId
inner join Categories as cate
on cate.Id = pro.CategoryId
WHERE OrderId = 1;
