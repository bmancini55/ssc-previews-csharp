drop table if exists item;
create table item (
  itemid int not null auto_increment,
  stocknumber nvarchar(255) not null,
  parentitem int not null,
  title text not null,
  description text not null,
  variantdescription text not null,
  seriesid int null,
  issuenumber nvarchar(255) null,
  issuesequencenumber nvarchar(255) null,
  volume nvarchar(255) null,
  maxissue nvarchar(255) null,
  price int not null,
  publisherid int null,
  upc nvarchar(255) null,
  isbn nvarchar(255) null,
  ean nvarchar(255) null,
  categoryid int null,
  genreid int null,
  brandid int null,
  writerid int null,
  artistid int null,
  coverartistid int null,
  primary key (itemid),
  index fk_item_category_idx (categoryid asc),
  index fk_item_genre_idx (genreid asc),
  index fk_item_brand_idx (brandid asc),
  index fk_item_writer_idx (writerid asc),
  index fk_item_artist_idx (artistid asc),
  index fk_item_coverartist_idx (coverartistid asc),
  index fk_item_publisher_idx (publisherid asc),
  index fk_item_series_idx (seriesid asc),
  constraint fk_item_category
    foreign key (categoryid)
    references category (categoryid)
    on delete set null
    on update set null,
  constraint fk_item_genre
    foreign key (genreid)
    references genre (genreid)
    on delete set null
    on update set null,
  constraint fk_item_brand
    foreign key (brandid)
    references brand (brandid)
    on delete set null
    on update set null,
  constraint fk_item_writer
    foreign key (writerid)
    references writer (writerid)
    on delete set null
    on update set null,
  constraint fk_item_artist
    foreign key (artistid)
    references artist (artistid)
    on delete set null
    on update set null,
  constraint fk_item_coverartist
    foreign key (coverartistid)
    references coverartist (coverartistid)
    on delete set null
    on update set null,
  constraint fk_item_publisher
    foreign key (publisherid)
    references publisher (publisherid)
    on delete set null
    on update set null,
  constraint fk_item_series
    foreign key (seriesid)
    references series (seriesid)
    on delete set null
    on update set null
) engine = innodb;