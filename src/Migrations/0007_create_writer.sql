drop table if eixsts writer;
create table writer (
  writerid int not null auto_increment,
  writername nvarchar(255) not null,
  primary key (writerid)
) engine = innodb;