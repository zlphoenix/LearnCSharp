
    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK30C56FC3DAD41B94]') AND parent_object_id = OBJECT_ID('CATS'))

alter table CATS  drop constraint FK30C56FC3DAD41B94




    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK30C56FC385895A50]') AND parent_object_id = OBJECT_ID('CATS'))

alter table CATS  drop constraint FK30C56FC385895A50




    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK8848AC76FB09B598]') AND parent_object_id = OBJECT_ID('Answers'))

alter table Answers  drop constraint FK8848AC76FB09B598




    if exists (select * from dbo.sysobjects where id = object_id(N'CATS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CATS


    if exists (select * from dbo.sysobjects where id = object_id(N'Questions') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Questions


    if exists (select * from dbo.sysobjects where id = object_id(N'Answers') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Answers


    if exists (select * from dbo.sysobjects where id = object_id(N'hibernate_unique_key') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table hibernate_unique_key


    create table CATS (
        id BIGINT not null,
       subclass NCHAR(1) not null,
       BirthDate DATE null,
       Color NVARCHAR(255) not null,
       Sex NCHAR(1) not null,
       Weight REAL null,
       Name NVARCHAR(255) null,
       mate_id BIGINT null,
       mother_id BIGINT null,
       primary key (id)
    )


    create table Questions (
        id BIGINT not null,
       Name NVARCHAR(255) null,
       primary key (id)
    )


    create table Answers (
        id BIGINT not null,
       Name NVARCHAR(255) null,
       question_id BIGINT null,
       primary key (id)
    )


    alter table CATS 
        add constraint FK30C56FC3DAD41B94 
        foreign key (mate_id) 
        references CATS


    alter table CATS 
        add constraint FK30C56FC385895A50 
        foreign key (mother_id) 
        references CATS


    alter table Answers 
        add constraint FK8848AC76FB09B598 
        foreign key (question_id) 
        references Questions


    create table hibernate_unique_key (
         next_hi BIGINT 
    )


    insert into hibernate_unique_key values ( 1 )

