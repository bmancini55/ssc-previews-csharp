create table previewsitem (
  itemid int(11) not null auto_increment,
  diamondnumber varchar(255),
  stocknumber varchar(255),
  parentitem varchar(255),
  bounceuseitem varchar(255),
  fulltitle text,
  maindescription text,
  variantdescription text,
  seriescode varchar(255),
  issuenumber varchar(255),
  issuesequencenumber varchar(255),
  volumetag varchar(255),
  maxissue varchar(255),
  price varchar(255),
  publisher varchar(255),
  upcnumber varchar(255),
  shortisbnnumber varchar(255),
  eannumber varchar(255),
  cardperpack varchar(255),
  packperbox varchar(255),
  boxpercase varchar(255),
  discountcode varchar(255),
  increment varchar(255),
  printdate varchar(255),
  focvendor varchar(255),
  shipdate varchar(255),
  standardretailprice varchar(255),
  category varchar(255),
  genre varchar(255),
  brandcode varchar(255),
  mature varchar(255),
  adult varchar(255),
  offeredagain varchar(255),
  caution1 varchar(255),
  caution2 varchar(255),
  caution3 varchar(255),
  resolicited varchar(255),
  noteprice varchar(255),
  orderformnotes varchar(255),
  page varchar(255),
  writer varchar(255),
  artist varchar(255),
  coverartist varchar(255),
  alliancesku varchar(255),
  focdate varchar(255),
  primary key (itemid)
) engine=innodb;

