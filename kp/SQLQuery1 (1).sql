
use kursovoi
go
create table roles(
id int primary key identity(1, 1),
name nvarchar(50))
use kursovoi
go

create table registr(
id int primary key identity(1, 1),
name nvarchar(50) not null,
surname nvarchar(50) not null,
login_ nvarchar(50) not null,
password_ int not null,
email nvarchar(50) not null,
role_id int foreign key references roles(id))

drop table registr;
drop table roles;


