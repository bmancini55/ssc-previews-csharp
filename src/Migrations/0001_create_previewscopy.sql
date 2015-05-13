drop table if exists previewscopy;
create table previewscopy (
	copyid int primary key not null auto_increment,
    stocknumber nvarchar(255) not null,
    price nvarchar(255) not null,
    title text not null,    
    preview text not null,
    description text not null
) engine=innodb;