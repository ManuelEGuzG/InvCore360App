using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvCore360_App.Model.Models;
using InvCore360_App.DA.Data;
using System.Linq;
using System;

namespace InvCore360_App.DA.Repositories
{
    public class VentaRepository
    {
        private readonly DataContext _context;
        public VentaRepository(DataContext context) => _context = context;

        public async Task<IEnumerable<Venta>> GetAllAsync() => await _context.Ventas.Include(v=>v.DetallesVenta).ToListAsync();

        public async Task<Venta?> GetByIdAsync(int id) => await _context.Ventas.FindAsync(id);

        public async Task<Venta> AddAsync(Venta entity)
        {
            _context.Ventas.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Venta?> UpdateAsync(Venta entity)
        {
            var existing = await _context.Ventas.FindAsync(entity.VentaID);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Ventas.FindAsync(id);
            if (existing == null) return false;
            _context.Ventas.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        // Complex operation: register a sale with details, update stock, create inventory movements and alerts in a transaction
        public async Task<Venta> RegisterSaleAsync(Venta venta, IEnumerable<DetalleVenta> detalles)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Generate NumeroVenta based on date and a 4-digit consecutive
                var fecha = DateTime.Now.ToString("yyyyMMdd");
                var prefix = "V" + fecha;

                var maxConsec = await _context.Ventas
                    .Where(v => EF.Functions.Like(v.NumeroVenta, prefix + "%"))
                    .Select(v => v.NumeroVenta)
                    .ToListAsync();

                int consecutivo = 0;
                if (maxConsec.Any())
                {
                    foreach (var s in maxConsec)
                    {
                        if (s.Length >= prefix.Length + 4)
                        {
                            var tail = s.Substring(s.Length - 4);
                            if (int.TryParse(tail, out var n) && n > consecutivo) consecutivo = n;
                        }
                    }
                }
                consecutivo++;
                venta.NumeroVenta = prefix + consecutivo.ToString("0000");
                venta.FechaVenta = DateTime.Now;

                _context.Ventas.Add(venta);
                await _context.SaveChangesAsync();

                foreach (var d in detalles)
                {
                    var producto = await _context.Productos.FindAsync(d.ProductoID);
                    if (producto == null)
                        throw new InvalidOperationException($"Producto con ID {d.ProductoID} no encontrado.");

                    if (producto.StockActual < d.Cantidad)
                        throw new InvalidOperationException($"Stock insuficiente para el producto {producto.Nombre}.");

                    d.VentaID = venta.VentaID;
                    d.NombreProducto = producto.Nombre;
                    d.PrecioUnitario = producto.PrecioVenta;
                    d.PrecioCosto = producto.PrecioCosto;
                    d.Subtotal = d.PrecioUnitario * d.Cantidad;
                    d.Total = d.Subtotal - d.Descuento;

                    _context.DetallesVenta.Add(d);

                    var stockAnterior = producto.StockActual;
                    producto.StockActual = producto.StockActual - d.Cantidad;
                    producto.FechaModificacion = DateTime.Now;
                    _context.Productos.Update(producto);

                    var movimiento = new MovimientoInventario
                    {
                        ProductoID = producto.ProductoID,
                        TipoMovimiento = "Salida",
                        Cantidad = d.Cantidad,
                        StockAnterior = stockAnterior,
                        StockNuevo = producto.StockActual,
                        Motivo = "Venta",
                        VentaID = venta.VentaID
                    };
                    _context.MovimientosInventario.Add(movimiento);

                    if (producto.StockActual <= producto.StockMinimo)
                    {
                        string tipoAlerta;
                        if (producto.StockActual == 0) tipoAlerta = "StockAgotado";
                        else if (producto.StockActual < producto.StockMinimo / 2) tipoAlerta = "StockCritico";
                        else tipoAlerta = "StockBajo";

                        var alerta = new AlertaInventario
                        {
                            ProductoID = producto.ProductoID,
                            TipoAlerta = tipoAlerta,
                            StockActual = producto.StockActual,
                            StockMinimo = producto.StockMinimo
                        };
                        _context.AlertasInventario.Add(alerta);
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return venta;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}