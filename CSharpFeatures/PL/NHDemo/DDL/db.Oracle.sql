
    drop table CATS cascade constraints;



    drop table Questions cascade constraints;



    drop table Answers cascade constraints;



    drop table hibernate_unique_key cascade constraints;



    create table CATS (
        id NUMBER(20,0) not null,
       subclass NCHAR(1) not null,
       BirthDate DATE,
       Color NVARCHAR2(255) not null,
       Sex NVARCHAR2(255) not null,
       Weight FLOAT(24),
       Name NVARCHAR2(255),
       mate_id NUMBER(20,0),
       mother_id NUMBER(20,0),
       primary key (id)
    );



    create table Questions (
        id NUMBER(20,0) not null,
       Name NVARCHAR2(255),
       primary key (id)
    );



    create table Answers (
        id NUMBER(20,0) not null,
       Name NVARCHAR2(255),
       question_id NUMBER(20,0),
       primary key (id)
    );



    alter table CATS 
        add constraint FK30C56FC3DAD41B94 
        foreign key (mate_id) 
        references CATS;



    alter table CATS 
        add constraint FK30C56FC385895A50 
        foreign key (mother_id) 
        references CATS;



    alter table Answers 
        add constraint FK8848AC76FB09B598 
        foreign key (question_id) 
        references Questions;



    create table hibernate_unique_key (
         next_hi NUMBER(20,0) 
    );



    insert into hibernate_unique_key values ( 1 );

