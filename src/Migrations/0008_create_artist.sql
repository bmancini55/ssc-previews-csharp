drop table if exists artist;
create table artist (
  artistid int not null auto_increment,
  artistname nvarchar(255) not null,
  primary key (artistid)
) engine = innodb;