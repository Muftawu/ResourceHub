-- populate table with department, topic and course

use ResourceHub;
go

insert into Department (Name) values ("Department of Computer Engineering");
go

insert into Department (Name) values ("Department of Natural Resource");
go

insert into Department (Name) values ("Department of Pharmacy");
go

insert into Course (Name, DepartmentId) values ("Computer Engineering", 1);
go

insert into Course (Name, DepartmentId) values ("Natural Resource", 2);
go

insert into Course (Name, DepartmentId) values ("Pharmacy", 3);
go

insert into Topics (Name, CourseId) values ("Embedded Systems", 1);
go

insert into Topics (Name, CourseId) values ("Basic Genetics", 2);
go

insert into Topics (Name, CourseId) values ("Narcotics", 3);
go


