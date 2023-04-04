CREATE TABLE roles (
id_role int primary key auto_increment,
name varchar (30) not null unique
);

CREATE TABLE users(
id_user int primary key auto_increment,
email varchar (50) not null unique,
password varchar(50) not null,
id_role int,
constraint fk_user_rol foreign key (id_role) references roles(id_role)
);

CREATE TABLE departamento(
id_departamento int primary key auto_increment,
name varchar(50) not null
);

CREATE TABLE municipio (
id_municipio int primary key auto_increment,
name varchar(50) not null,
id_departamento int,
constraint fk_municipio_departamento foreign key (id_departamento) references departamento(id_departamento) ON DELETE CASCADE
);

CREATE TABLE contact(
id_contact int primary key auto_increment,
first_name varchar(50) not null,
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

insert into roles (name) values ('admin');
insert into roles (name) values ('user');
insert into roles (name) values ('client');

insert into users (email, password, id_role) values('adminuser@gmail.com','test123',1);

