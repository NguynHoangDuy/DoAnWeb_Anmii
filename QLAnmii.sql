create database QLAnmii
go
use QLAnmii
go


create table NHAN_VIEN (
   MANV                 varchar(30)          not null,
   HOTENNV              nvarchar(100)         null,
   GIOITINH             bit                  null,
   NGAYSINH             date                 null,
   SDT                  varchar(10)          null,
   DIACHI               nvarchar(200)         null,
   ANHNV                varchar(200)         null,
   EMAIL                varchar(200)         null,
   MATKHAU              varchar(20)          null,
   constraint PK_NHAN_VIEN primary key nonclustered (MANV)
)
go
insert into NHAN_VIEN values
('NV01',N'Nguyễn Hoàng Duy',1,CAST('2001-11-29' as Date),'0927345635',N'Diên Khánh, Khánh Hòa',N'employee.png','duy@gmail.com','123'),
('NV02',N'Tôn Huỳnh Phương Lan',0,CAST('2001-05-16' as Date),'0923127732',N'Diên Khánh, Khánh Hòa',N'employee.png','lan@gmail.com','123'),
('NV03',N'Nguyễn Mai Thi',0,CAST('2001-01-29' as Date),'0937554343',N'Ninh Hòa, Khánh Hòa',N'employee.png','thi@gmail.com','123')
go

create table LOAIMONAN (
   MALOAIMONAN          varchar(30)          not null,
   TENLOAI              nvarchar(100)         null,
   constraint PK_LOAIMONAN primary key nonclustered (MALOAIMONAN)
)
go
insert into LOAIMONAN values
('MA001',N'Bánh mì'),
('MA002',N'Lẩu'),
('MA003',N'Đồ nướng'),
('MA004',N'Nước ngọt'),
('MA005',N'Trà')
go

create table HOA_DON (
   MAHD                 varchar(30)          not null,
   HOTENKH              nvarchar(100)		 null,
   DIACHI				nvarchar(100)		null,
   SDT					varchar(11)          null,
   MANV                 varchar(30)           null,
   THOIGIANDAT          datetime             null,
   THOIGIANGIAO         datetime             null,
   TONGTIEN				float				null,
   constraint PK_HOA_DON primary key nonclustered (MAHD),
   constraint FK_NV foreign key (MANV) references NHAN_VIEN(MANV)
	ON UPDATE CASCADE
	ON DELETE CASCADE,
)
go
INSERT INTO HOA_DON VALUES ('HD001','Nguyễn Hoàng Duy','Suối hiệp','0369454037','NV01','2022-05-22 17:21:05.027','2022-05-22 19:21:05.027',	24000)
create table MON_AN (
   MAMONAN              varchar(30)          not null,
   MALOAIMONAN          varchar(30)          not null,
   TENMONAN             nvarchar(100)         null,
   MOTA                 varchar(300)         null,
   ANHMONAN             varchar(200)         null,
   DONGIA               int                  null,
   constraint PK_MON_AN primary key nonclustered (MAMONAN),
   constraint FK_LMA foreign key (MALOAIMONAN) references LOAIMONAN(MALOAIMONAN)
	ON UPDATE CASCADE
	ON DELETE CASCADE,
)
go
insert into MON_AN values
('MM001','MA001',N'Bánh mì chả cá','banh_mi_cha_ca.jpeg',null,12000),
('MM002','MA001',N'Bánh mì chả giò','banh_mi_cha_gio.jpg',null,13000),
('MM003','MA001',N'Bánh mì thịt nướng','banh_mi_thit_nuong.jpg',null,20000),
('MM004','MA001',N'Bánh mì bơ sữa','banh_mi_bo_sua.jpg',null,10000),
('MM005','MA001',N'Bánh mì không','banh_mi_khong.jpg',null,3000),
('MM006','MA001',N'Bánh mì cá','banh_mi_ca.png',null,15000),
('MM007','MA001',N'Bánh mì trứng','banh_mi_trung.jpg',null,10000),
('MM008','MA002',N'Lẩu gà lá é ','lau_ga_la_e.jpg',null,150000),
('MM009','MA002',N'Lẩu bò','lau_bo.jpg',null,200000),
('MM010','MA002',N'Lẩu gà lá giang ','lau_ga_la_giang.jpg',null,150000),
('MM011','MA002',N'Lẩu hải sản ','lau_hai_san.jpeg',null,180000),
('MM012','MA003',N'Thịt ba chỉ nướng','thit_ba_chi_nuong.jpg',null,50000),
('MM013','MA003',N'Thịt ếch nướng ','thit_ech_nuong.jpg',null,60000),
('MM014','MA003',N'Hải sản nướng ','hai_san_nuong.png',null,150000),
('MM015','MA003',N'Thịt gà nướng ','thit_ga_nuong.jpg',null,50000),
('MM016','MA004',N'Coca Cola','coca_cola.jpg',null,10000),
('MM017','MA004',N'7up','7up.jpg',null,10000),
('MM018','MA004',N'Nước suối','nuoc_suoi.jpg',null,6000),
('MM019','MA004',N'Mirinda Xá Xị ','mirinda_xa_xi.jpg',null,10000),
('MM020','MA004',N'Mirinda Cam ','mirinda_cam.jpg',null,10000),
('MM021','MA004',N'Mirinda Soda Kem ','mirinda_soda_kem.jpg',null,10000),
('MM022','MA004',N'Mirinda Đá me ','mirinda_da_me.jpg',null,10000),
('MM023','MA005',N'Trà đào','tra_dao.jpg',null,15000),
('MM024','MA005',N'Trà tắc','tra_tac.jpg',null,10000),
('MM025','MA005',N'Trà sữa Anmii','tra_sua_anmii.jpg',null,15000),
('MM026','MA005',N'Trà dâu','tra_dau.jpg',null,15000),
('MM027','MA005',N'Trà vải','tra_vai.jpg',null,10000),
('MM028','MA005',N'Trà đào cam xả','tra_dao_cam_sa.jpg',null,20000),
('MM029','MA005',N'Trà chanh','tra_chanh.jpg',null,15000),
('MM030','MA005',N'Trà trái cây','tra_trai_cay.jpg',null,15000)
go


create table CHI_TIET_HOA_HON (
   MAMONAN              varchar(30)          ,
   MAHD                 varchar(30)           ,
   SOLUONG              int                  null,
   constraint PK_CTHD primary key (MAHD, MAMONAN),
   constraint FK_MA foreign key (MAMONAN) references MON_AN(MAMONAN)
    ON UPDATE CASCADE
	ON DELETE CASCADE,
	constraint FK_HD2 foreign key (MAHD) references HOA_DON(MAHD)
	ON UPDATE CASCADE
	ON DELETE CASCADE,
)
go



create table LIENHE (
   MALIENHE              varchar(30)   PRIMARY KEY        not null,
   HOTEN				nvarchar(100)         null,
   SDT             nvarchar(10)         null,
   EMAIL                 varchar(100)         null,
   TIEUDE             nvarchar(100)         null,
   NOIDUNG               nvarchar(500)                  null,
)
go
insert into LIENHE values
('LH0001','Nguyen Hoang Duy','0369454937','nduy12310@gmail.com', 'Khieu Nai', 'Banh mi ngon qua')
go
 

--Thong ke hoa don
CREATE PROCEDURE HoaDon_ThongKe
    @tgbd datetime null,
	@tgkt datetime null
AS
BEGIN
SELECT *
FROM HOA_DON
WHERE THOIGIANDAT BETWEEN @tgbd AND @tgkt
END






