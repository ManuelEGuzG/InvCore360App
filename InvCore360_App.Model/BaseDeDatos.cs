/*
 * Esta clase solamente se utiliza para documentar el script SQL que crea la base de datos.
 * 
 * 
 * 
 * -- =============================================
-- SCRIPT DE BASE DE DATOS - SISTEMA DE GESTIÓN DE NEGOCIO
-- Versión: 2.0 CORREGIDA
-- Motor: SQL Server 2022
-- Instrucciones: Copiar y pegar completo en SSMS y ejecutar
-- =============================================

-- Usar master para poder eliminar la base de datos si está en uso
USE master;
GO

-- Eliminar base de datos si existe
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'SistemaGestionNegocio')
BEGIN
    ALTER DATABASE SistemaGestionNegocio SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE SistemaGestionNegocio;
END
GO

-- Crear la base de datos
CREATE DATABASE SistemaGestionNegocio;
GO

-- Cambiar a la nueva base de datos
USE SistemaGestionNegocio;
GO

-- =============================================
-- TABLA: Categorias
-- =============================================
CREATE TABLE Categorias (
    CategoriaID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(500),
    Activo BIT DEFAULT 1,
    FechaCreacion DATETIME DEFAULT GETDATE()
);
GO

-- =============================================
-- TABLA: Proveedores
-- =============================================
CREATE TABLE Proveedores (
    ProveedorID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(200) NOT NULL,
    Contacto NVARCHAR(100),
    Telefono NVARCHAR(20),
    Email NVARCHAR(100),
    Direccion NVARCHAR(300),
    Activo BIT DEFAULT 1,
    FechaCreacion DATETIME DEFAULT GETDATE()
);
GO

-- =============================================
-- TABLA: Usuarios
-- =============================================
CREATE TABLE Usuarios (
    UsuarioID INT PRIMARY KEY IDENTITY(1,1),
    NombreUsuario NVARCHAR(100) UNIQUE NOT NULL,
    Contrasena NVARCHAR(255) NOT NULL,
    NombreCompleto NVARCHAR(200) NOT NULL,
    Email NVARCHAR(100),
    Rol NVARCHAR(50) NOT NULL,
    Activo BIT DEFAULT 1,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    UltimoAcceso DATETIME
);
GO

-- =============================================
-- TABLA: Productos
-- =============================================
CREATE TABLE Productos (
    ProductoID INT PRIMARY KEY IDENTITY(1,1),
    CategoriaID INT,
    ProveedorID INT,
    CodigoBarras NVARCHAR(50) UNIQUE,
    Nombre NVARCHAR(200) NOT NULL,
    Descripcion NVARCHAR(500),
    PrecioCosto DECIMAL(18,2) NOT NULL,
    PrecioVenta DECIMAL(18,2) NOT NULL,
    StockActual INT NOT NULL DEFAULT 0,
    StockMinimo INT NOT NULL DEFAULT 5,
    StockMaximo INT,
    UnidadMedida NVARCHAR(50) DEFAULT 'Unidad',
    Activo BIT DEFAULT 1,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FechaModificacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CategoriaID) REFERENCES Categorias(CategoriaID),
    FOREIGN KEY (ProveedorID) REFERENCES Proveedores(ProveedorID),
    CONSTRAINT CHK_PrecioVenta CHECK (PrecioVenta >= PrecioCosto),
    CONSTRAINT CHK_StockActual CHECK (StockActual >= 0),
    CONSTRAINT CHK_StockMinimo CHECK (StockMinimo >= 0)
);
GO

-- =============================================
-- TABLA: HistorialPrecios
-- =============================================
CREATE TABLE HistorialPrecios (
    HistorialID INT PRIMARY KEY IDENTITY(1,1),
    ProductoID INT NOT NULL,
    PrecioCostoAnterior DECIMAL(18,2),
    PrecioCostoNuevo DECIMAL(18,2),
    PrecioVentaAnterior DECIMAL(18,2),
    PrecioVentaNuevo DECIMAL(18,2),
    FechaCambio DATETIME DEFAULT GETDATE(),
    UsuarioID INT,
    Motivo NVARCHAR(200),
    FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID),
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID)
);
GO

-- =============================================
-- TABLA: Ventas
-- =============================================
CREATE TABLE Ventas (
    VentaID INT PRIMARY KEY IDENTITY(1,1),
    NumeroVenta NVARCHAR(50) UNIQUE NOT NULL,
    FechaVenta DATETIME DEFAULT GETDATE(),
    Subtotal DECIMAL(18,2) NOT NULL,
    Impuesto DECIMAL(18,2) DEFAULT 0,
    Descuento DECIMAL(18,2) DEFAULT 0,
    Total DECIMAL(18,2) NOT NULL,
    MetodoPago NVARCHAR(50) NOT NULL,
    EstadoVenta NVARCHAR(50) DEFAULT 'Completada',
    UsuarioID INT,
    Observaciones NVARCHAR(500),
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID),
    CONSTRAINT CHK_Total CHECK (Total >= 0)
);
GO

-- =============================================
-- TABLA: DetallesVenta
-- =============================================
CREATE TABLE DetallesVenta (
    DetalleVentaID INT PRIMARY KEY IDENTITY(1,1),
    VentaID INT NOT NULL,
    ProductoID INT NOT NULL,
    NombreProducto NVARCHAR(200) NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    PrecioCosto DECIMAL(18,2) NOT NULL,
    Subtotal DECIMAL(18,2) NOT NULL,
    Descuento DECIMAL(18,2) DEFAULT 0,
    Total DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (VentaID) REFERENCES Ventas(VentaID) ON DELETE CASCADE,
    FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID),
    CONSTRAINT CHK_Cantidad CHECK (Cantidad > 0),
    CONSTRAINT CHK_PrecioUnitario CHECK (PrecioUnitario >= 0)
);
GO

-- =============================================
-- TABLA: MovimientosInventario
-- =============================================
CREATE TABLE MovimientosInventario (
    MovimientoID INT PRIMARY KEY IDENTITY(1,1),
    ProductoID INT NOT NULL,
    TipoMovimiento NVARCHAR(50) NOT NULL,
    Cantidad INT NOT NULL,
    StockAnterior INT NOT NULL,
    StockNuevo INT NOT NULL,
    Motivo NVARCHAR(200),
    VentaID INT NULL,
    UsuarioID INT,
    FechaMovimiento DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID),
    FOREIGN KEY (VentaID) REFERENCES Ventas(VentaID),
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID)
);
GO

-- =============================================
-- TABLA: Gastos
-- =============================================
CREATE TABLE Gastos (
    GastoID INT PRIMARY KEY IDENTITY(1,1),
    Concepto NVARCHAR(200) NOT NULL,
    Descripcion NVARCHAR(500),
    Monto DECIMAL(18,2) NOT NULL,
    Categoria NVARCHAR(100),
    FechaGasto DATETIME NOT NULL,
    MetodoPago NVARCHAR(50),
    UsuarioID INT,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID),
    CONSTRAINT CHK_MontoGasto CHECK (Monto > 0)
);
GO

-- =============================================
-- TABLA: AlertasInventario
-- =============================================
CREATE TABLE AlertasInventario (
    AlertaID INT PRIMARY KEY IDENTITY(1,1),
    ProductoID INT NOT NULL,
    TipoAlerta NVARCHAR(50) NOT NULL,
    StockActual INT NOT NULL,
    StockMinimo INT NOT NULL,
    FechaAlerta DATETIME DEFAULT GETDATE(),
    EstadoAlerta NVARCHAR(50) DEFAULT 'Pendiente',
    FechaResolucion DATETIME,
    FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID)
);
GO

-- =============================================
-- TABLA: ConfiguracionNegocio
-- =============================================
CREATE TABLE ConfiguracionNegocio (
    ConfigID INT PRIMARY KEY IDENTITY(1,1),
    NombreNegocio NVARCHAR(200) NOT NULL,
    RUC NVARCHAR(50),
    Direccion NVARCHAR(300),
    Telefono NVARCHAR(20),
    Email NVARCHAR(100),
    PorcentajeImpuesto DECIMAL(5,2) DEFAULT 0,
    MonedaPredeterminada NVARCHAR(10) DEFAULT 'CRC',
    AlertasHabilitadas BIT DEFAULT 1,
    DiasParaReportes INT DEFAULT 30,
    FechaCreacion DATETIME DEFAULT GETDATE()
);
GO

-- =============================================
-- ÍNDICES PARA OPTIMIZACIÓN
-- =============================================
CREATE INDEX IDX_Productos_Categoria ON Productos(CategoriaID);
CREATE INDEX IDX_Productos_Proveedor ON Productos(ProveedorID);
CREATE INDEX IDX_Productos_StockActual ON Productos(StockActual);
CREATE INDEX IDX_Ventas_Fecha ON Ventas(FechaVenta);
CREATE INDEX IDX_Ventas_Estado ON Ventas(EstadoVenta);
CREATE INDEX IDX_DetallesVenta_VentaID ON DetallesVenta(VentaID);
CREATE INDEX IDX_DetallesVenta_ProductoID ON DetallesVenta(ProductoID);
CREATE INDEX IDX_MovimientosInventario_Producto ON MovimientosInventario(ProductoID);
CREATE INDEX IDX_MovimientosInventario_Fecha ON MovimientosInventario(FechaMovimiento);
CREATE INDEX IDX_AlertasInventario_Estado ON AlertasInventario(EstadoAlerta);
CREATE INDEX IDX_Gastos_Fecha ON Gastos(FechaGasto);
GO

-- =============================================
-- VISTAS PARA REPORTES Y ANÁLISIS
-- =============================================

-- Vista: Productos con stock bajo
CREATE VIEW vw_ProductosStockBajo AS
SELECT 
    p.ProductoID,
    p.CodigoBarras,
    p.Nombre,
    c.Nombre AS Categoria,
    p.StockActual,
    p.StockMinimo,
    p.PrecioVenta,
    p.PrecioCosto,
    (p.StockMinimo - p.StockActual) AS CantidadFaltante
FROM Productos p
LEFT JOIN Categorias c ON p.CategoriaID = c.CategoriaID
WHERE p.StockActual <= p.StockMinimo AND p.Activo = 1;
GO

-- Vista: Ventas del día
CREATE VIEW vw_VentasHoy AS
SELECT 
    v.VentaID,
    v.NumeroVenta,
    v.FechaVenta,
    v.Total,
    v.MetodoPago,
    v.EstadoVenta,
    COUNT(dv.DetalleVentaID) AS CantidadProductos,
    SUM(dv.Cantidad) AS UnidadesVendidas
FROM Ventas v
LEFT JOIN DetallesVenta dv ON v.VentaID = dv.VentaID
WHERE CAST(v.FechaVenta AS DATE) = CAST(GETDATE() AS DATE)
    AND v.EstadoVenta = 'Completada'
GROUP BY v.VentaID, v.NumeroVenta, v.FechaVenta, v.Total, v.MetodoPago, v.EstadoVenta;
GO

-- Vista: Productos más vendidos
CREATE VIEW vw_ProductosMasVendidos AS
SELECT 
    p.ProductoID,
    p.Nombre,
    p.CodigoBarras,
    c.Nombre AS Categoria,
    SUM(dv.Cantidad) AS TotalVendido,
    SUM(dv.Total) AS IngresoTotal,
    SUM(dv.Total - (dv.PrecioCosto * dv.Cantidad)) AS GananciaTotal,
    COUNT(DISTINCT dv.VentaID) AS NumeroVentas
FROM DetallesVenta dv
INNER JOIN Productos p ON dv.ProductoID = p.ProductoID
LEFT JOIN Categorias c ON p.CategoriaID = c.CategoriaID
INNER JOIN Ventas v ON dv.VentaID = v.VentaID
WHERE v.EstadoVenta = 'Completada'
GROUP BY p.ProductoID, p.Nombre, p.CodigoBarras, c.Nombre;
GO

-- Vista: Valor total del inventario
CREATE VIEW vw_ValorInventario AS
SELECT 
    p.ProductoID,
    p.Nombre,
    p.CodigoBarras,
    p.StockActual,
    p.PrecioCosto,
    p.PrecioVenta,
    (p.StockActual * p.PrecioCosto) AS ValorCosto,
    (p.StockActual * p.PrecioVenta) AS ValorVenta,
    (p.StockActual * (p.PrecioVenta - p.PrecioCosto)) AS GananciaPotencial
FROM Productos p
WHERE p.Activo = 1 AND p.StockActual > 0;
GO

-- =============================================
-- PROCEDIMIENTOS ALMACENADOS
-- =============================================

-- Procedimiento: Registrar una venta
CREATE PROCEDURE sp_RegistrarVenta
    @NumeroVenta NVARCHAR(50),
    @Subtotal DECIMAL(18,2),
    @Impuesto DECIMAL(18,2),
    @Descuento DECIMAL(18,2),
    @Total DECIMAL(18,2),
    @MetodoPago NVARCHAR(50),
    @UsuarioID INT = NULL,
    @Observaciones NVARCHAR(500) = NULL,
    @VentaID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO Ventas (NumeroVenta, Subtotal, Impuesto, Descuento, Total, MetodoPago, UsuarioID, Observaciones)
    VALUES (@NumeroVenta, @Subtotal, @Impuesto, @Descuento, @Total, @MetodoPago, @UsuarioID, @Observaciones);
    
    SET @VentaID = SCOPE_IDENTITY();
END;
GO

-- Procedimiento: Agregar detalle de venta y actualizar inventario
CREATE PROCEDURE sp_AgregarDetalleVenta
    @VentaID INT,
    @ProductoID INT,
    @Cantidad INT,
    @Descuento DECIMAL(18,2) = 0
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @NombreProducto NVARCHAR(200);
    DECLARE @PrecioVenta DECIMAL(18,2);
    DECLARE @PrecioCosto DECIMAL(18,2);
    DECLARE @Subtotal DECIMAL(18,2);
    DECLARE @Total DECIMAL(18,2);
    DECLARE @StockActual INT;
    DECLARE @StockNuevo INT;
    
    -- Obtener datos del producto
    SELECT 
        @NombreProducto = Nombre,
        @PrecioVenta = PrecioVenta,
        @PrecioCosto = PrecioCosto,
        @StockActual = StockActual
    FROM Productos
    WHERE ProductoID = @ProductoID;
    
    -- Validar stock suficiente
    IF @StockActual < @Cantidad
    BEGIN
        RAISERROR('Stock insuficiente para el producto', 16, 1);
        RETURN;
    END
    
    -- Calcular totales
    SET @Subtotal = @PrecioVenta * @Cantidad;
    SET @Total = @Subtotal - @Descuento;
    SET @StockNuevo = @StockActual - @Cantidad;
    
    -- Insertar detalle de venta
    INSERT INTO DetallesVenta (VentaID, ProductoID, NombreProducto, Cantidad, PrecioUnitario, PrecioCosto, Subtotal, Descuento, Total)
    VALUES (@VentaID, @ProductoID, @NombreProducto, @Cantidad, @PrecioVenta, @PrecioCosto, @Subtotal, @Descuento, @Total);
    
    -- Actualizar stock del producto
    UPDATE Productos
    SET StockActual = @StockNuevo,
        FechaModificacion = GETDATE()
    WHERE ProductoID = @ProductoID;
    
    -- Registrar movimiento de inventario
    INSERT INTO MovimientosInventario (ProductoID, TipoMovimiento, Cantidad, StockAnterior, StockNuevo, Motivo, VentaID)
    VALUES (@ProductoID, 'Salida', @Cantidad, @StockActual, @StockNuevo, 'Venta', @VentaID);
    
    -- Verificar si necesita alerta de stock bajo
    IF @StockNuevo <= (SELECT StockMinimo FROM Productos WHERE ProductoID = @ProductoID)
    BEGIN
        DECLARE @StockMinimo INT;
        SELECT @StockMinimo = StockMinimo FROM Productos WHERE ProductoID = @ProductoID;
        
        DECLARE @TipoAlerta NVARCHAR(50);
        IF @StockNuevo = 0
            SET @TipoAlerta = 'StockAgotado';
        ELSE IF @StockNuevo < @StockMinimo / 2
            SET @TipoAlerta = 'StockCritico';
        ELSE
            SET @TipoAlerta = 'StockBajo';
        
        INSERT INTO AlertasInventario (ProductoID, TipoAlerta, StockActual, StockMinimo)
        VALUES (@ProductoID, @TipoAlerta, @StockNuevo, @StockMinimo);
    END
END;
GO

-- Procedimiento: Generar número de venta automático
CREATE PROCEDURE sp_GenerarNumeroVenta
    @NumeroVenta NVARCHAR(50) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @Fecha NVARCHAR(8) = FORMAT(GETDATE(), 'yyyyMMdd');
    DECLARE @Consecutivo INT;
    
    SELECT @Consecutivo = ISNULL(MAX(CAST(RIGHT(NumeroVenta, 4) AS INT)), 0) + 1
    FROM Ventas
    WHERE NumeroVenta LIKE 'V' + @Fecha + '%';
    
    SET @NumeroVenta = 'V' + @Fecha + RIGHT('0000' + CAST(@Consecutivo AS NVARCHAR), 4);
END;
GO

-- Procedimiento: Obtener reporte de ventas por período
CREATE PROCEDURE sp_ReporteVentasPeriodo
    @FechaInicio DATETIME,
    @FechaFin DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        CAST(v.FechaVenta AS DATE) AS Fecha,
        COUNT(v.VentaID) AS NumeroVentas,
        SUM(v.Total) AS TotalRecaudado,
        SUM(v.Subtotal) AS Subtotal,
        SUM(v.Impuesto) AS TotalImpuestos,
        SUM(v.Descuento) AS TotalDescuentos,
        AVG(v.Total) AS PromedioVenta,
        SUM(dv.Total - (dv.PrecioCosto * dv.Cantidad)) AS GananciaTotal
    FROM Ventas v
    LEFT JOIN DetallesVenta dv ON v.VentaID = dv.VentaID
    WHERE v.FechaVenta BETWEEN @FechaInicio AND @FechaFin
        AND v.EstadoVenta = 'Completada'
    GROUP BY CAST(v.FechaVenta AS DATE)
    ORDER BY Fecha DESC;
END;
GO

-- Procedimiento: Actualizar stock de producto
CREATE PROCEDURE sp_ActualizarStock
    @ProductoID INT,
    @Cantidad INT,
    @TipoMovimiento NVARCHAR(50),
    @Motivo NVARCHAR(200) = NULL,
    @UsuarioID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @StockActual INT;
    DECLARE @StockNuevo INT;
    
    SELECT @StockActual = StockActual FROM Productos WHERE ProductoID = @ProductoID;
    
    IF @TipoMovimiento = 'Entrada'
        SET @StockNuevo = @StockActual + @Cantidad;
    ELSE IF @TipoMovimiento = 'Salida'
    BEGIN
        IF @StockActual < @Cantidad
        BEGIN
            RAISERROR('Stock insuficiente', 16, 1);
            RETURN;
        END
        SET @StockNuevo = @StockActual - @Cantidad;
    END
    ELSE IF @TipoMovimiento = 'Ajuste'
        SET @StockNuevo = @Cantidad;
    
    UPDATE Productos
    SET StockActual = @StockNuevo,
        FechaModificacion = GETDATE()
    WHERE ProductoID = @ProductoID;
    
    INSERT INTO MovimientosInventario (ProductoID, TipoMovimiento, Cantidad, StockAnterior, StockNuevo, Motivo, UsuarioID)
    VALUES (@ProductoID, @TipoMovimiento, @Cantidad, @StockActual, @StockNuevo, @Motivo, @UsuarioID);
END;
GO

-- =============================================
-- TRIGGERS
-- =============================================

-- Trigger: Actualizar fecha de modificación en productos
CREATE TRIGGER trg_Productos_ActualizarFecha
ON Productos
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE Productos
    SET FechaModificacion = GETDATE()
    FROM Productos p
    INNER JOIN inserted i ON p.ProductoID = i.ProductoID;
END;
GO

-- Trigger: Registrar cambios de precio
CREATE TRIGGER trg_Productos_HistorialPrecios
ON Productos
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO HistorialPrecios (ProductoID, PrecioCostoAnterior, PrecioCostoNuevo, PrecioVentaAnterior, PrecioVentaNuevo, FechaCambio)
    SELECT 
        i.ProductoID,
        d.PrecioCosto,
        i.PrecioCosto,
        d.PrecioVenta,
        i.PrecioVenta,
        GETDATE()
    FROM inserted i
    INNER JOIN deleted d ON i.ProductoID = d.ProductoID
    WHERE i.PrecioCosto <> d.PrecioCosto OR i.PrecioVenta <> d.PrecioVenta;
END;
GO

-- =============================================
-- DATOS INICIALES
-- =============================================

-- Insertar configuración inicial
INSERT INTO ConfiguracionNegocio (NombreNegocio, MonedaPredeterminada, PorcentajeImpuesto, AlertasHabilitadas)
VALUES ('Mi Negocio', 'CRC', 13.00, 1);
GO

-- Insertar usuario administrador inicial (contraseña: admin123 - debe cambiarla)
INSERT INTO Usuarios (NombreUsuario, Contrasena, NombreCompleto, Rol)
VALUES ('admin', 'admin123', 'Administrador del Sistema', 'Admin');
GO

-- Insertar categorías de ejemplo
INSERT INTO Categorias (Nombre, Descripcion) VALUES
('Electrónicos', 'Productos electrónicos y tecnología'),
('Alimentos', 'Productos alimenticios'),
('Bebidas', 'Bebidas y refrescos'),
('Limpieza', 'Productos de limpieza'),
('Papelería', 'Artículos de oficina y papelería'),
('Otros', 'Productos varios');
GO

-- =============================================
-- FIN DEL SCRIPT - BASE DE DATOS CREADA EXITOSAMENTE
-- =============================================

PRINT '====================================================';
PRINT 'BASE DE DATOS CREADA EXITOSAMENTE';
PRINT '====================================================';
PRINT 'Nombre: SistemaGestionNegocio';
PRINT 'Usuario inicial: admin';
PRINT 'Contraseña inicial: admin123';
PRINT '====================================================';
GO

*/