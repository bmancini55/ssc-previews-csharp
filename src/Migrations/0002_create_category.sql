drop table if exists category;
create table category (
  categoryid int(11) not null
  categoryname nvarchar(255) not null,
  consumer tinyint(4) null default 1,
  primary key (categoryid)
) engine = innodb;

insert into category (categoryid, categoryname, consumer) values(1, 'Comics', 1);
insert into category (categoryid, categoryname, consumer) values(2, 'Magazines', 1);
insert into category (categoryid, categoryname, consumer) values(3, 'Graphic Novels / Trade Paperbacks', 1);
insert into category (categoryid, categoryname, consumer) values(4, 'Books', 1);
insert into category (categoryid, categoryname, consumer) values(5, 'Gagmes', 1);
insert into category (categoryid, categoryname, consumer) values(6, 'Cards', 1);
insert into category (categoryid, categoryname, consumer) values(7, 'Comic Novelties', 1);
insert into category (categoryid, categoryname, consumer) values(8, 'Non-Comic Novelties', 1);
insert into category (categoryid, categoryname, consumer) values(9, 'Apparel', 1);
insert into category (categoryid, categoryname, consumer) values(10, 'Toys and Models', 1);
insert into category (categoryid, categoryname, consumer) values(11, 'Card Supplies', 1);
insert into category (categoryid, categoryname, consumer) values(12, 'Comic Supplies', 1);
insert into category (categoryid, categoryname, consumer) values(13, 'Retailer Sales Tools', 0);
insert into category (categoryid, categoryname, consumer) values(14, 'Diamond Publications', 0);
insert into category (categoryid, categoryname, consumer) values(15, 'Posters and Printed Stuff', 1);
insert into category (categoryid, categoryname, consumer) values(16, 'Videos and Video Games', 1);