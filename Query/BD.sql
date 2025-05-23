-- Crear base de datos
CREATE DATABASE ExamenU3Tienda;
GO

-- Usar la base de datos
USE ExamenU3Tienda;
GO

-- Crear tabla Productos b�sica
CREATE TABLE Productos (
    IdProducto INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
	Descripcion  VARCHAR(255)NOT NULL,
    Stock INT NOT NULL
);
GO

INSERT INTO Productos (Nombre, Precio, Descripcion, Stock) VALUES
('Laptop Lenovo IdeaPad 3', 8999.99, 'Laptop con procesador Ryzen 5, 8GB RAM y 512GB SSD', 15),
('Mouse Inal�mbrico Logitech M170', 299.99, 'Mouse inal�mbrico compacto con receptor USB', 40),
('Teclado Mec�nico Redragon Kumara', 899.50, 'Teclado mec�nico retroiluminado con switches Outemu Blue', 25),
('Monitor Samsung 24" FHD', 3899.00, 'Monitor LED Full HD con HDMI y VGA', 10),
('Silla Gamer Cougar Explore S', 4599.99, 'Silla ergon�mica para gaming con ajuste de altura', 8),
('Disco Duro Externo 1TB Toshiba', 1299.50, 'Disco duro port�til USB 3.0 de 1TB', 20),
('Memoria RAM 8GB DDR4 2666MHz', 549.00, 'Memoria RAM para laptop o PC de alto rendimiento', 35),
('Webcam Logitech C920', 1999.00, 'Webcam Full HD con micr�fono est�reo', 12),
('Aud�fonos Sony WH-CH520', 1199.00, 'Aud�fonos inal�mbricos con micr�fono integrado', 18),
('Bocinas Bluetooth JBL Go 3', 899.00, 'Bocina port�til con sonido potente y resistente al agua', 22),
('Tablet Samsung Galaxy Tab A8', 5999.00, 'Tablet de 10.5 pulgadas con 64GB de almacenamiento', 14),
('Cargador Universal para Laptop', 499.99, 'Cargador compatible con m�ltiples marcas y voltajes', 30),
('Cable HDMI 2 Metros', 129.00, 'Cable HDMI de alta velocidad para audio y video', 50),
('Impresora HP DeskJet 2775', 1599.00, 'Multifuncional inal�mbrica para impresi�n dom�stica', 9),
('Router TP-Link Archer C6', 1099.00, 'Router inal�mbrico de doble banda y alta velocidad', 16),
('Tarjeta de Video GTX 1650', 3999.00, 'Tarjeta gr�fica Nvidia de 4GB GDDR5', 6),
('Mousepad Gamer XXL', 299.00, 'Alfombrilla para mouse de gran tama�o y superficie suave', 27),
('Kit Herramientas Electr�nicas', 199.00, 'Kit de destornilladores y herramientas para reparaci�n', 45),
('Cable USB Tipo C 1 Metro', 89.00, 'Cable de carga y datos para dispositivos con puerto Tipo C', 60),
('Power Bank 10000mAh Xiaomi', 499.00, 'Bater�a port�til para recarga de dispositivos', 20),
('Smartwatch Amazfit Bip U', 1799.00, 'Reloj inteligente con monitor de frecuencia cardiaca', 13),
('Disco SSD 240GB Kingston', 799.00, 'Unidad de estado s�lido para acelerar tu computadora', 19),
('Auriculares Gamer HyperX Cloud Stinger', 1499.00, 'Aud�fonos con micr�fono para videojuegos', 11),
('Soporte para Celular de Escritorio', 149.00, 'Soporte ajustable para tel�fonos m�viles', 37),
('Adaptador USB Wi-Fi TP-Link', 299.00, 'Adaptador USB para conexi�n Wi-Fi en PC o laptop', 29),
('Memoria MicroSD 64GB Sandisk', 279.00, 'Tarjeta de memoria compatible con smartphones y c�maras', 41),
('L�mpara LED de Escritorio', 399.00, 'L�mpara ajustable con luz blanca y c�lida', 23),
('C�mara de Seguridad Wi-Fi', 899.00, 'C�mara IP con visi�n nocturna y detecci�n de movimiento', 17),
('Control Xbox Series X', 1699.00, 'Control inal�mbrico para consolas y PC', 7),
('Enfriador para Laptop', 349.00, 'Base con ventiladores para reducir temperatura', 31);
