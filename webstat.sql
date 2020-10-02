create database web_stationary
use web_stationary
create table tbl_admin(
a_id int primary key identity(1,1) not null,
a_email nvarchar(max),
a_password nvarchar(max)
)

create table tbl_product(
p_id int primary key identity(1,1) not null,
p_name nvarchar(max),
p_price float,
p_qty float,
p_date date,
p_des nvarchar(max),
)

create table tbl_stock(
s_id int primary key identity(1,1) not null,
s_name nvarchar(max),
s_price float,
s_qty float,
s_date date,
s_des nvarchar(max),
s_status nvarchar(max)
)

create table tbl_role(
r_id int primary key identity(1,1) not null,
r_type nvarchar(max),
)

create table tbl_user(
u_id int primary key identity(1,1) not null,
u_name nvarchar(max),
u_email nvarchar(max),
u_pass nvarchar(max),
u_grade nvarchar(max),
u_registernumber nvarchar(max),
u_location nvarchar(max),
r_id int foreign key references tbl_role(r_id)
)

create table tbl_stationaryRequest
(
st_reqid int primary key identity(1,1) not null,
s_id int foreign key references tbl_stock(s_id),
user_id int foreign key references tbl_user(u_id),
item_name nvarchar(max),
req_qty float,
req_totalprice float,
request_date date,
approvecancel_date date,
req_status nvarchar(max),
)
select user_id,sum(req_totalprice) as t from tbl_stationaryRequest group by user_id order by user_id 
 <add name="web_stationaryEntities" connectionString="metadata=res://*/Models.Model1.csdl|res://*/Models.Model1.ssdl|res://*/Models.Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=DESKTOP-NV6DJV1;initial catalog=web_stationary;user id=sa;password=123;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
create table question_answers(
id int primary key identity(1,1) not null,
messages nvarchar(max),
froms nvarchar(max),
tos nvarchar(max),
dates date
)

create table tbl_notification(
not_id int primary key identity(1,1) not null,
not_action nvarchar(max),
not_date date
)