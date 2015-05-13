drop table if exists brand;
create table brand (
  brandid int auto_increment,
  brandcode nvarchar(2) not null,
  brandname nvarchar(255) not null
  primary key (brandid)
) engine = innodb;


insert into brand (brandcode, brandname) values ('AL', 'Aliens');
insert into brand (brandcode, brandname) values ('AM', 'Aftermath');
insert into brand (brandcode, brandname) values ('AN', 'Anime');
insert into brand (brandcode, brandname) values ('BB', 'Baseball');
insert into brand (brandcode, brandname) values ('BH', 'Bobbing Heads');
insert into brand (brandcode, brandname) values ('BK', 'Basketball');
insert into brand (brandcode, brandname) values ('BL', 'Babylon 5');
insert into brand (brandcode, brandname) values ('BM', 'Batman');
insert into brand (brandcode, brandname) values ('BU', 'Buffy the Vampire Slayer');
insert into brand (brandcode, brandname) values ('CC', 'Candy & Confections');
insert into brand (brandcode, brandname) values ('CI', 'Classics Illustrated');
insert into brand (brandcode, brandname) values ('CN', 'Cartoon Network');
insert into brand (brandcode, brandname) values ('CO', 'Conan');
insert into brand (brandcode, brandname) values ('DH', 'Dark Horse Heroes');
insert into brand (brandcode, brandname) values ('DM', 'Dark Horse Merchandise');
insert into brand (brandcode, brandname) values ('DS', 'Disney');
insert into brand (brandcode, brandname) values ('DU', 'DC Universe');
insert into brand (brandcode, brandname) values ('DW', 'Doctor Who');
insert into brand (brandcode, brandname) values ('FB', 'Football');
insert into brand (brandcode, brandname) values ('FM', 'Frank Miller');
insert into brand (brandcode, brandname) values ('GJ', 'G.I. Joe');
insert into brand (brandcode, brandname) values ('GZ', 'Godzilla');
insert into brand (brandcode, brandname) values ('HB', 'Highbrow');
insert into brand (brandcode, brandname) values ('HC', 'Hercules');
insert into brand (brandcode, brandname) values ('HE', 'Helix');
insert into brand (brandcode, brandname) values ('HK', 'Hockey');
insert into brand (brandcode, brandname) values ('IC', 'Image Comic');
insert into brand (brandcode, brandname) values ('ID', 'IDW Publishing');
insert into brand (brandcode, brandname) values ('IJ', 'Indiana Jones');
insert into brand (brandcode, brandname) values ('JD', 'Judge Dredd');
insert into brand (brandcode, brandname) values ('LS', 'Lost In Space');
insert into brand (brandcode, brandname) values ('LT', 'Looney Tunes');
insert into brand (brandcode, brandname) values ('MA', 'Manga');
insert into brand (brandcode, brandname) values ('MG', 'Magic: The Gathering');
insert into brand (brandcode, brandname) values ('MH', 'Marvel Heroes');
insert into brand (brandcode, brandname) values ('MK', 'Maverick');
insert into brand (brandcode, brandname) values ('MM', 'Mike Mignola');
insert into brand (brandcode, brandname) values ('MT', 'Marvel Tech');
insert into brand (brandcode, brandname) values ('MV', 'Movie/TV');
insert into brand (brandcode, brandname) values ('NK', 'Nickelodeon');
insert into brand (brandcode, brandname) values ('PD', 'Paradox');
insert into brand (brandcode, brandname) values ('PR', 'Predator');
insert into brand (brandcode, brandname) values ('PW', 'Professional Wrestling');
insert into brand (brandcode, brandname) values ('RC', 'Racing');
insert into brand (brandcode, brandname) values ('RP', 'Roleplaying');
insert into brand (brandcode, brandname) values ('SF', 'Street Fighter');
insert into brand (brandcode, brandname) values ('SI', 'Studio Ice');
insert into brand (brandcode, brandname) values ('SM', 'Spider-Man');
insert into brand (brandcode, brandname) values ('SS', 'Simpsons'); 
insert into brand (brandcode, brandname) values ('ST', 'Star Trek'); 
insert into brand (brandcode, brandname) values ('SU', 'Superman');
insert into brand (brandcode, brandname) values ('SW', 'Star Wars');
insert into brand (brandcode, brandname) values ('TC', 'Top Cow');
insert into brand (brandcode, brandname) values ('TM', 'Spawn');
insert into brand (brandcode, brandname) values ('UD', 'Udon Studios');
insert into brand (brandcode, brandname) values ('VA', 'Valiant Heroes');
insert into brand (brandcode, brandname) values ('VG', 'Video Game/Software Tie-In');
insert into brand (brandcode, brandname) values ('VT', 'Vertigo');
insert into brand (brandcode, brandname) values ('WB', 'Web Comic');
insert into brand (brandcode, brandname) values ('WK', 'WizKids');
insert into brand (brandcode, brandname) values ('WS', 'WildStorm'); 
insert into brand (brandcode, brandname) values ('WW', 'World of Warcraft');
insert into brand (brandcode, brandname) values ('XE', 'Xena');
insert into brand (brandcode, brandname) values ('XF', 'X-Files');
insert into brand (brandcode, brandname) values ('XM', 'X-Men');
insert into brand (brandcode, brandname) values ('XX', 'Not Specified');
insert into brand (brandcode, brandname) values ('YA', 'Yaoi');
insert into brand (brandcode, brandname) values ('YR', 'For Young Readers');