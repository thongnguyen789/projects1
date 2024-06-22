Use Master
GO
    IF exists(Select name From sys.databases Where name='MayinShop' )
    DROP Database Shopprinter
GO
    Create Database Shopprinter
GO

USE Shopprinter;

CREATE TABLE CUAHANG(
	MaCH int primary key identity(1,1),
	Ten nvarchar(100) not null,
	DienThoai varchar(20),
	DiaChi nvarchar(100)
) 
GO

CREATE TABLE DANHMUC(
	MaDM int primary key identity(1,1),
	Ten nvarchar(100) not null
) 
GO

CREATE TABLE MATHANG(
	MaMH int primary key identity(1,1),
	Ten nvarchar(100) not null,
	GiaGoc int default 0,
	GiaBan int default 0,
	SoLuong smallint default 0,
	MoTa nvarchar(1000),
	HinhAnh varchar(255),
	Hinh1 varchar(255),
	Hinh2 varchar(255),
	Hinh3 varchar(255),
	MaDM int not null foreign key(MaDM) references DANHMUC(MaDM),
	LuotXem int default 0,
	LuotMua int default 0
) 
GO

CREATE TABLE CHUCVU(
	MaCV int primary key identity(1,1),
	Ten nvarchar(100) not null,
	HeSo float default 1.0
) 
GO

CREATE TABLE NHANVIEN(
	MaNV int primary key identity(1,1),
	Ten nvarchar(100) not null,
	MaCV int not null foreign key(MaCV) references CHUCVU(MaCV),
	DienThoai varchar(20),
	Email varchar(50),
	MatKhau varchar(50)	
) 
GO

CREATE TABLE KHACHHANG(
	MaKH int primary key identity(1,1),
	Ten nvarchar(100) not null,
	DienThoai varchar(20),
	Email varchar(50),
	MatKhau varchar(255)
) 
GO

CREATE TABLE DIACHI(	
	MaDC int primary key identity(1,1),
	MaKH int not null foreign key(MaKH) references KHACHHANG(MaKH),
	DiaChi nvarchar(100) not null,
	PhuongXa varchar(20) default N'Đông Xuyên',
	QuanHuyen varchar(50) default N'TP. Long Xuyên',
	TinhThanh varchar(50) default N'An Giang',
	MacDinh int default 1	
) 
GO

CREATE TABLE HOADON(
	MaHD int primary key identity(1,1),
	Ngay datetime default getdate(),
	TongTien int default 0,
	MaKH int not null foreign key(MaKH) references KHACHHANG(MaKH),
	TrangThai int default 0
) 
GO

CREATE TABLE CTHOADON(
	MaCTHD int primary key identity(1,1),
	MaHD int not null foreign key(MaHD) references HOADON(MaHD),	
	MaMH int not null foreign key(MaMH) references MATHANG(MaMH),
	DonGia int default 0,
	SoLuong smallint default 1,
	ThanhTien int
) 
GO

-- Dữ liệu bảng CUA_HANG
INSERT INTO CUAHANG(Ten, DienThoai, DiaChi) VALUES(N'Cửa hàng máy in','0296-3841190',N'18 Ung Văn Khiêm, P Đông Xuyên, TP Long Xuyên, An Giang');

-- Dữ liệu bảng LOAI_HANG
INSERT INTO DANHMUC(Ten) VALUES(N'Máy in Brother');
INSERT INTO DANHMUC(Ten) VALUES(N'Máy in Canon');
INSERT INTO DANHMUC(Ten) VALUES(N'Máy in Hp');
INSERT INTO DANHMUC(Ten) VALUES(N'Máy in Epson');

-- Dữ liệu bảng MAT_HANG
INSERT INTO MATHANG(Ten,MoTa,GiaGoc,GiaBan,SoLuong,HinhAnh,Hinh1,Hinh2,Hinh3,MaDM,LuotXem,LuotMua) VALUES(N'Máy in phun màu đa năng Epson L3250',N'Máy in Epson L3250 được thiết kế nhỏ gọn, tiết kiệm không gian trưng bày. Tích hợp nhiều tính năng cực kỳ quan trọng cho văn phòng là in, scan, copy, lọ mực hoạt động với năng suất cao, đổ đầy cũng không bị lỗi hoặc tràn bình, cho phép in trực tiếp từ các thiết bị như điện thoại, tablet, laptop, máy tính bàn. Năng suất in cao lên đến 4.500 đối với trang đen trắng và 7.500 với trang màu. Độ phân giải lên đến 5760 x 1440 dpi, đảm bảo in nhanh chóng và chất lượng, sử dụng kết nối có dây USB 2.0 hoặc chế độ in không dây qua Wifi rất tiện lợi. Máy in màu còn có khả năng in ảnh không viền lên đến 4R, thao tác dễ sử dụng',5990000,4990000,26,'Epson-l3250-1.jpg','Epson-l3250-2.jpg','Epson-l3250-3.jpg','Epson-l3250-4.jpg',4,0,0);
INSERT INTO MATHANG(Ten,MoTa,GiaGoc,GiaBan,SoLuong,HinhAnh,Hinh1,Hinh2,Hinh3,MaDM,LuotXem,LuotMua) VALUES(N'Máy in màu HP Color Laser 150NW-4ZB95A',N'Công nghệ in Laser tiên tiến, cho kết quả hoàn hảo, tốc độ in trắng/đen lên đến 18 ppm, 4ppm cho in màu, bộ nhớ 64MB dễ dàng in giấy số lượng lớn trong 1 lần, tích hợp Wifi 802.11b/g/n có tốc độ đường truyền cao, màn hình LED chất lượng cao, dễ dàng điều khiển máy. Chỉ sử dụng hộp mực đến từ HP, tăng độ bền, an toàn',7590000,6590000,30,'Hp-150nw-4zb95a-01.jpg','Hp-150nw-4zb95a-02.jpg','Hp-150nw-4zb95a-03.jpg','Hp-150nw-4zb95a-04.jpg',3,0,0);
INSERT INTO MATHANG(Ten,MoTa,GiaGoc,GiaBan,SoLuong,HinhAnh,Hinh1,Hinh2,Hinh3,MaDM,LuotXem,LuotMua) VALUES(N'Máy in laser trắng đen HP 107A-4ZB77A',N'Máy in có kiểu dáng nhỏ gọn sang trọng, tiết kiệm không gian, máy có tốc độ in lên đến 20 trang/phút, tiết kiệm thời gian in, độ phân giải 1200 x 1200 dpi cho chất lượng bản in rõ nét, bảng điều khiển gồm các nút bấm và đèn báo dễ theo dõi sử dụng, máy in laser trắng đen HP 107A-4ZB77A hỗ trợ in nhiều khổ giấy',2890000,2190000,20,'Hp-107a-4zb77a-1.jpg','Hp-107a-4zb77a-2.jpg','Hp-107a-4zb77a-3.jpg','Hp-107a-4zb77a-4.jpg',3,0,0);
INSERT INTO MATHANG(Ten,MoTa,GiaGoc,GiaBan,SoLuong,HinhAnh,Hinh1,Hinh2,Hinh3,MaDM,LuotXem,LuotMua) VALUES(N'Máy in phun đa năng Brother DCP-T220',N'Tốc độ in nhanh lên tới 16 ảnh/phút (đơn sắc) và 9 ảnh/phút (in màu), khay nạp giấy 150 tờ có thể điều chỉnh linh hoạt nhiều khổ giấy khác nhau, bình mực dung tích, lên đến 7500 trang in trắng đen và 5000 trang in màu, mực in chính hãng Brother kéo dài tuổi thọ máy, tiết kiệm hơn trên mỗi trang in, khay chứa mực in phun với thiết kế nắp trong suốt, dễ quan sát và nạp mực. Giao diện người dùng được thiết kế thân thiện giúp người dùng sử dụng dễ dàng',3800000,3800000,10,'Brother-dcp-t220-1.jpg','Brother-dcp-t220-2.jpg','Brother-dcp-t220-3.jpg','Brother-dcp-t220-4.jpg',1,0,0);

INSERT INTO MATHANG(Ten,MoTa,GiaGoc,GiaBan,SoLuong,HinhAnh,Hinh1,Hinh2,Hinh3,MaDM,LuotXem,LuotMua) VALUES(N'Máy in đa năng HP Laser MFP 137fnw 4ZB84A',N'Wifi 802.11b/g/n tích hợp sẵn, tốc độ kết nối nhanh, ổn định, công nghệ in tia Lazer cho thông tin, hình ảnh được sắc nét, tốc độ in cao lên đến 20 trang/phút, chất lượng in ấn tượng, chỉ sử dụng hộp mực chính hãng đến từ HP, tăng độ bền bỉ, độ phân giải 1200 x 1200 dpi cho bản in chất lượng, rõ nét',5490000,4590000,18,'Hp-laser-mfp-137fnw-4zb84a-01.jpg','Hp-laser-mfp-137fnw-4zb84a-02.jpg','Hp-laser-mfp-137fnw-4zb84a-03.jpg','Hp-laser-mfp-137fnw-4zb84a-04.jpg',3,0,0);
INSERT INTO MATHANG(Ten,MoTa,GiaGoc,GiaBan,SoLuong,HinhAnh,Hinh1,Hinh2,Hinh3,MaDM,LuotXem,LuotMua) VALUES(N'Máy in HP LaserJet M211D 9YF82A',N'Máy có thiết kế gọn nhẹ, tinh tế có thể đặt ngay trên bàn làm việc tiện lợi, văn bản in rõ nét, không bị mờ, mất chữ với độ phân giải 600 x 600 dpi, tốc độ in mạnh mẽ 29 trang/phút, công suất in lên đến 20.000 trang/tháng, khay nạp giấy 150 tờ, khay giấy ra 100 tờ đáp ứng nhu cầu in tần suất cao, dễ dàng kết nối với USB 2.0, sử dụng được với hệ điều hành Windows, Mac OS',3890000,3890000,32,'Hp-m211d-9yf82a-1.jpg','Hp-m211d-9yf82a-2.jpg','Hp-m211d-9yf82a-3.jpg','Hp-m211d-9yf82a-4.jpg',3,0,0);
INSERT INTO MATHANG(Ten,MoTa,GiaGoc,GiaBan,SoLuong,HinhAnh,Hinh1,Hinh2,Hinh3,MaDM,LuotXem,LuotMua) VALUES(N'Máy in laser trắng đen Canon LBP6030W',N'Kết nối không dây Wifi dễ dàng hơn chỉ với 1 nút nhấn WPS, dùng mực Canon 325 chính hãng kéo dài tuổi thọ của máy, tốc độ in vượt trội đến 18 trang/phút giải quyết tốt công việc, chế độ chờ, tắt máy tự động khi không sử dụng tiết kiệm điện, độ phân giải 600 x 600 dpi cho bản in rõ nét, cổng kết nối USB 2.0 tương thích mọi hệ điều hành Windows',3790000,3390000,26,'Canon-lbp6030w-01.jpg','Canon-lbp6030w-02.jpg','Canon-lbp6030w-03.jpg','Canon-lbp6030w-04.jpg',2,0,0);
INSERT INTO MATHANG(Ten,MoTa,GiaGoc,GiaBan,SoLuong,HinhAnh,Hinh1,Hinh2,Hinh3,MaDM,LuotXem,LuotMua) VALUES(N'Máy in phun đa năng Brother DCP-T226',N'Dòng máy in phun mới, thiết kế để nâng cao tối đa hiệu suất làm việc, máy in Brother DCP-T226 với 3 chức năng in, photo và scan, khay giấy lên tới 150 tờ có thể điều chỉnh cho các kích cỡ giấy khác nhau, giảm chi phí cho mỗi bản in với bình mực dung tích cực lớn của Brother, hệ thống nạp mực tự động phía trước máy, nạp mực dễ dàng và chính xác',3590000,3590000,14,'Brother-dcp-t226-1.jpg','Brother-dcp-t226-2.jpg','Brother-dcp-t226-3.jpg','Brother-dcp-t226-4.jpg',1,0,0);

INSERT INTO MATHANG(Ten,MoTa,GiaGoc,GiaBan,SoLuong,HinhAnh,Hinh1,Hinh2,Hinh3,MaDM,LuotXem,LuotMua) VALUES(N'Máy in phun màu Canon Pixma G3010',N'Máy in đa chức năng: in, scan, photocopy hỗ trợ công việc, hoạt động trơn tru, hạn chế các sự cố kẹt giấy, tràn mực, dùng mực Canon GI-790 chính hãng kéo dài tuổi thọ máy, chất lượng bản in tốt với độ phẩn giải  4800 x 1200 dpi, tốc độ in nhanh, in màu 5 ảnh/phút, trắng đen 8.8 ảnh/phút, kết nối Wi-Fi, LAN in ấn từ các thiết bị di động dễ dàng',5900000,5490000,22,'Canon-g3010-den-01.jpg','Canon-g3010-den-02.jpg','Canon-g3010-den-03.jpg','Canon-g3010-den-04.jpg',2,0,0);
INSERT INTO MATHANG(Ten,MoTa,GiaGoc,GiaBan,SoLuong,HinhAnh,Hinh1,Hinh2,Hinh3,MaDM,LuotXem,LuotMua) VALUES(N'Máy in phun màu đa năng Epson L3210',N'Thiết kế sang trọng, hiện đại và chống bám bụi giúp cho máy in luôn sạch sẽ, tính năng chuyển máy sang chế độ nghỉ khi không dùng đến, giúp tiết kiệm điện, trang bị tính năng in tràn viền mang lại những bản in chất lượng và đầy màu sắc, công nghệ in không nhiệt, đẩy nhanh tốc độ in với mức tiêu thụ điện năng thấp, độ phân giải lên đến 5760 dpi x 1440 dpi, mang lại những bản in đảm bảo sắc nét, máy in có mức tiêu thụ điện năng thấp hơn 70% so với máy in laser truyền thống. Bạn có thể tự bơm mực cho máy in với thao tác đơn giản mà không cần kỹ thuật viên',4490000,4490000,31,'Epson-l3210-1.jpg','Epson-l3210-2.jpg','Epson-l3210-3.jpg','Epson-l3210-4.jpg',4,0,0);
INSERT INTO MATHANG(Ten,MoTa,GiaGoc,GiaBan,SoLuong,HinhAnh,Hinh1,Hinh2,Hinh3,MaDM,LuotXem,LuotMua) VALUES(N'Máy in phun màu Canon G570',N'Bình mực lớn tiếp mực liên tục cho phép bạn in nhiều ảnh với chi phí thấp, 6 màu mực khác nhau giúp mở rộng dải màu tạo ra bức ảnh chất lượng cao. Năng suất in lớn, số lượng bản in lên tới 3,800 ảnh 4R với mỗi bình mực đơn, máy in in khổ A4 kéo dài lên đến 1m2 dễ dàng in biểu ngữ, cataloge,... loại bỏ khả năng bị rơi rớt mực với lọ mực chống tràn và nhỏ mực, in dễ dàng bằng các thiết bị di động, dễ dàng bảo trì và gia tăng tuổi thọ',7690000,6790000,18,'Canon-g570-1.jpg','Canon-g570-2.jpg','Canon-g570-3.jpg','Canon-g570-4.jpg',2,0,0);
INSERT INTO MATHANG(Ten,MoTa,GiaGoc,GiaBan,SoLuong,HinhAnh,Hinh1,Hinh2,Hinh3,MaDM,LuotXem,LuotMua) VALUES(N'Máy in phun đa năng Brother MFC-T920DW',N'Máy in Brother cho bản in rõ ràng với độ phân giải lên đến 1200 x 6000 dpi, tốc độ in tài liệu lên đến 17 ảnh/phút (in đơn sắc) và 16,5 ảnh/phút (in màu), khay nạp giấy 150 tờ có thể điều chỉnh linh hoạt cho các khổ giấy khác nhau. Bình mực dung tích lớn, lên đến 7500 trang in trắng đen và 5000 trang in màu, mực in chính hãng Brother kéo dài tuổi thọ máy, tiết kiệm hơn trên mỗi trang in. Khay chứa mực in phun với thiết kế nắp trong suốt dễ dàng quan sát và nạp mực, tính năng in di động trực tiếp thuận tiện cho việc in ấn từ các thiết bị di động',8300000,7990000,12,'Brother-mfc-t920dw-1.jpg','Brother-mfc-t920dw-2.jpg','Brother-mfc-t920dw-3.jpg','Brother-mfc-t920dw-4.jpg',1,0,0);

-- Dữ liệu bảng CHUC_VU
INSERT INTO CHUCVU(Ten) VALUES(N'Quản lý');
INSERT INTO CHUCVU(Ten) VALUES(N'Nhân viên thu ngân');
INSERT INTO CHUCVU(Ten) VALUES(N'Nhân viên kho');

-- Dữ liệu bảng NHANVIEN
INSERT INTO NHANVIEN(Ten,MaCV,DienThoai,Email,MatKhau) VALUES(N'Admin',1,'0912347283','Admin@gmail.com','123');

-- Dữ liệu bảng KHACHHANG
--INSERT INTO KHACHHANG(Ten,DienThoai,Email,MatKhau) VALUES(N'','','','');

-- Dữ liệu bảng DIACHI
--INSERT INTO DIACHI(MaKH,DiaChi,PhuongXa,QuanHuyen,TinhThanh,MacDinh) VALUES(1,N'',N'',N'',N'',1);

-- Dữ liệu bảng HOADON
--INSERT INTO HOADON(TongTien,MaKH,TrangThai) VALUES(70000,1,0);


-- Dữ liệu bảng CTHOA_DON
--INSERT INTO CTHOADON(MaHD,MaMH,DonGia,SoLuong,ThanhTien) VALUES(1,2,23000,1,23000);

GO
SELECT * FROM NHANVIEN
SELECT * FROM DANHMUC
SELECT * FROM MATHANG
SELECT * FROM KHACHHANG
SELECT * FROM CUAHANG
SELECT * FROM CHUCVU
SELECT * FROM DIACHI
SELECT * FROM CTHOADON
SELECT * FROM HOADON










