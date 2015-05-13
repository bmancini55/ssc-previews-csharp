drop table if exists coverartist;
create table coverartist (
  coverartistid int not null auto_increment,
  coverartistname nvarchar(255) not null,
  primary key (coverartistid)
) engine = innodb;