drop table if exists itemtext;
create table itemtext (
  itemid int not null,
  itemtext text null,
  primary key (itemid),
  fulltext index idx_itemtext (itemtext asc)
) engine = myisam;