CREATE DATABASE ResourceHub
go
USE ResourceHub
go

CREATE TABLE Course(
    Course_ID        int             IDENTITY(1,1),
    CourseName       varchar(100)    NOT NULL,
    Department_ID    int             NOT NULL,
    CONSTRAINT PK3 PRIMARY KEY NONCLUSTERED (Course_ID)
)
go


IF OBJECT_ID('Course') IS NOT NULL
    PRINT '<<< CREATED TABLE Course >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE Course >>>'
go

CREATE TABLE Department(
    Department_ID    int             IDENTITY(1,1),
    Dep_Name         varchar(100)    NOT NULL,
    CONSTRAINT PK2 PRIMARY KEY NONCLUSTERED (Department_ID)
)
go

IF OBJECT_ID('Department') IS NOT NULL
    PRINT '<<< CREATED TABLE Department >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE Department >>>'
go

CREATE TABLE Resource(
    Resource_ID    varchar(100)      NOT NULL,
    User_ID        int               NOT NULL,
    Course_ID      int               NOT NULL,
    Topic          varchar(100)      NOT NULL,
    Name           varchar(100)      NOT NULL,
    Resource       varbinary(255)    NOT NULL,
    Comment        varchar(255)      NULL,
    CONSTRAINT PK4 PRIMARY KEY NONCLUSTERED (Resource_ID, User_ID)
)
go

IF OBJECT_ID('Resource') IS NOT NULL
    PRINT '<<< CREATED TABLE Resource >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE Resource >>>'
go

CREATE TABLE Users(
    User_ID      int             IDENTITY(1,1),
    FirstName    varchar(100)    NOT NULL,
    LastName     varchar(100)    NOT NULL,
    Email        varchar(100)    NOT NULL,
    Phone        varchar(100)    NOT NULL,
    CONSTRAINT PK1 PRIMARY KEY NONCLUSTERED (User_ID)
)
go

IF OBJECT_ID('Users') IS NOT NULL
    PRINT '<<< CREATED TABLE Users >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE Users >>>'
go

ALTER TABLE Course ADD CONSTRAINT RefDepartment2 
    FOREIGN KEY (Department_ID)
    REFERENCES Department(Department_ID)
go

ALTER TABLE Resource ADD CONSTRAINT RefUsers1 
    FOREIGN KEY (User_ID)
    REFERENCES Users(User_ID)
go

ALTER TABLE Resource ADD CONSTRAINT RefCourse3 
    FOREIGN KEY (Course_ID)
    REFERENCES Course(Course_ID)
go


