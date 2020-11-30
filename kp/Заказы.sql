use kursovoi
go
create table Заказы(
id_машины int identity(1,1)not null,
 Автомобиль nvarchar(50)not null,
 Марка nvarchar(50) not null,
 Модель nvarchar(50) not null,
 Количество nvarchar(10) not null,
 Цена_за_одну int not null,
 Общая_цена int not null,
Дата_поставки date not null,
primary key(id_машины)
)
