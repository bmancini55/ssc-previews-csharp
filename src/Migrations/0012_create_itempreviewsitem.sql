drop table if exists itempreviewsitem;
create table itempreviewsitem (
  itemid int not null,
  previewitemid int not null,
  previewscode nvarchar(255) null,
  primary key (itemid, previewitemid),
  index fk_previewsitem_idx (previewitemid asc),
  constraint fk_item
    foreign key (itemid)
    references item (itemid)
    on delete cascade
    on update cascade,
  constraint fk_previewsitem
    foreign key (previewitemid)
    references previewsitem (itemid)
    on delete cascade
    on update cascade
) engine = innodb;