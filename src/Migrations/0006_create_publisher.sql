drop table if exists publisher;
create table publisher (
  publisherid int not null,
  publishername nvarchar(255) not null,
  primary key (publisherid)
) engine = innodb;