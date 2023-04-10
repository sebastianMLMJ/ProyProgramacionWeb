last_name varchar(50) not null,
phone_number varchar(8) not null,
home_address varchar(50) not null,
id_user int,
id_municipio int,
constraint fk_contacts_user foreign key (id_user) references users(id_user) ON DELETE CASCADE,
constraint fk_contacts_municipio foreign key (id_municipio) references municipio(id_municipio)ON DELETE CASCADE
);

CREATE TABLE card(
id_card int primary key auto_increment,
cardtype varchar(20) not null,
number varchar(16) not null,
exp_month varchar(2) not null,
exp_year varchar(2) not null,
id_user int,
constraint fk_card_user foreign key (id_user) references users(id_user) ON DELETE CASCADE
);

CREATE TABLE product(
id_product int primary key auto_increment,
name varchar(50) not null,
description varchar (300) not null,
price varchar(50) not null,
stock int not null,
photo varchar(500) not null
);

CREATE TABLE shoppingcart (
id_shoppingcart int primary key auto_increment,
id_product int,
id_user int,
constraint fk_cart_user foreign key (id_user) references users (id_user) ON DELETE CASCADE,
constraint fk_cart_product foreign key (id_product) references product(id_product) ON DELETE CASCADE
);

CREATE TABLE order_header (
id_order int primary key auto_increment,
order_date date not null,
order_status varchar(10) not null,
total varchar(50) not null,
id_card int,
id_contact int,
constraint fk_order_card foreign key (id_card) references card (id_card) ON DELETE SET NULL,
constraint fk_order_contact foreign key (id_contact) references contact (id_contact) ON DELETE SET NULL
);

CREATE TABLE order_item (
id_order_item int primary key auto_increment,
id_product int,
id_order int,
constraint fk_item_order foreign key(id_order) references order_header(id_order) ON DELETE SET NULL,
constraint fk_item_product foreign key (id_product) references product (id_product) ON DELETE SET NULL
);

select * from users;
select * from product;
select * from shoppingcart;

insert into roles (name) values ('admin');
insert into roles (name) values ('user');
insert into roles (name) values ('client');

insert into users (email, password, id_role) values('adminuser@gmail.com','test123',1);

select * from card
