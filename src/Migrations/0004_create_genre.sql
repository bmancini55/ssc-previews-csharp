drop table if exists genre;
create table genre (
  genreid int auto_increment,
  genrecode nvarchar(2) not null,
  genrename nvarchar(255) not null
  primary key (genreid)
) engine=innodb;

insert into genre (genrecode, genrename) values ('AA', 'Action / Adventure');
insert into genre (genrecode, genrename) values ('AD', 'Adult');
insert into genre (genrecode, genrename) values ('AN', 'Anthology');
insert into genre (genrecode, genrename) values ('AP', 'Anthropomorphics');
insert into genre (genrecode, genrename) values ('AS', 'Art Supplies');
insert into genre (genrecode, genrename) values ('CJ', 'Comics Journalism');
insert into genre (genrecode, genrename) values ('CR', 'Crime');
insert into genre (genrecode, genrename) values ('DR', 'Drama');
insert into genre (genrecode, genrename) values ('DT', 'Designer Toys');
insert into genre (genrecode, genrename) values ('FA', 'Fantasy');
insert into genre (genrecode, genrename) values ('GA', 'Gaming / Role Playing');
insert into genre (genrecode, genrename) values ('HA', 'Halloween');
insert into genre (genrecode, genrename) values ('HO', 'Horror');
insert into genre (genrecode, genrename) values ('HS', 'Historical');
insert into genre (genrecode, genrename) values ('HT', 'How to Draw');
insert into genre (genrecode, genrename) values ('HU', 'Humor/Comedy');
insert into genre (genrecode, genrename) values ('KI', 'Kids');
insert into genre (genrecode, genrename) values ('LG', 'Legend');
insert into genre (genrecode, genrename) values ('LT', 'Literary');
insert into genre (genrecode, genrename) values ('MA', 'Manga');
insert into genre (genrecode, genrename) values ('MS', 'Mystery');
insert into genre (genrecode, genrename) values ('MU', 'Music');
insert into genre (genrecode, genrename) values ('MV', 'Movie/TV');
insert into genre (genrecode, genrename) values ('PC', 'Pop Culture');
insert into genre (genrecode, genrename) values ('PK', 'Pokémon');
insert into genre (genrecode, genrename) values ('RB', 'Reality-Based');
insert into genre (genrecode, genrename) values ('RF', 'Reference/Art Books/How To');
insert into genre (genrecode, genrename) values ('RL', 'Religious');
insert into genre (genrecode, genrename) values ('RO', 'Romance');
insert into genre (genrecode, genrename) values ('SF', 'Science Fiction');
insert into genre (genrecode, genrename) values ('SH', 'Super-Hero');
insert into genre (genrecode, genrename) values ('SN', 'Seasonal');
insert into genre (genrecode, genrename) values ('SP', 'Sports');
insert into genre (genrecode, genrename) values ('SU', 'Surreal/Non-Linear');
insert into genre (genrecode, genrename) values ('TY', 'Toy');
insert into genre (genrecode, genrename) values ('WR', 'War');
insert into genre (genrecode, genrename) values ('WS', 'Western');
insert into genre (genrecode, genrename) values ('XX', 'Not Specieid');
insert into genre (genrecode, genrename) values ('YA', 'Yaoi');