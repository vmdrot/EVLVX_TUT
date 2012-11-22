-- =============================================
-- Script Template
-- =============================================
CREATE TABLE tbl1 (id int AUTO_INCREMENT not null primary key, nm varchar(32) NOT NULL UNIQUE, is_valid BIT, price DECIMAL(9,2), descr NVARCHAR(255), cnt INT NULL);
