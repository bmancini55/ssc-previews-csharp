drop table if exists series;
create table series (
  seriesid int not null,
  seriesname nvarchar(255) not null,
  inactive tinyint not null default 0,
  primary key (seriesid)
) engine = innodb;