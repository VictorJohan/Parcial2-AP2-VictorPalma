using Microsoft.EntityFrameworkCore;
using Parcial2_AP2_VictorPalma.DAL;
using Parcial2_AP2_VictorPalma.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Parcial2_AP2_VictorPalma.BLL
{
    public class VentasBLL
    {
        public Contexto _contexto { get; set; }

        public VentasBLL(Contexto contexto)
        {
            this._contexto = contexto;
        }

        public async Task<bool> Guardar(Ventas venta)
        {
            if (!await Existe(venta.VentaId))
                return await Insertar(venta);
            else
                return await Modificar(venta);
        }

        private async Task<bool> Existe(int id)
        {
            bool ok = false;

            try
            {
                ok = await _contexto.Venta.AnyAsync(v => v.VentaId == id);
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Insertar(Ventas venta)
        {
            bool ok = false;

            try
            {
                _contexto.Venta.Add(venta);
                ok = await _contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Modificar(Ventas venta)
        {
            bool ok = false;

            try
            {
                _contexto.Entry(venta.Cliente).State = EntityState.Modified;
                _contexto.Entry(venta).State = EntityState.Modified;

                ok = await _contexto.SaveChangesAsync() > 0;

            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<Ventas> Buscar(int id)
        {
            Ventas venta;

            try
            {
                venta = await _contexto.Venta
                   .Where(v => v.VentaId == id)
                   .Include(v => v.Cliente)
                   .AsNoTracking()
                   .SingleOrDefaultAsync();

                var entidad = _contexto
                .Set<Ventas>()
                .Local.SingleOrDefault(v => v.VentaId == id);

                if (entidad != null)
                {
                    _contexto.Entry(entidad).State = EntityState.Detached;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return venta;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool ok = false;
            try
            {
                var registro = await Buscar(id);
                if(registro != null)
                {
                    _contexto.Venta.Remove(registro);
                    ok = await _contexto.SaveChangesAsync() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<List<Ventas>> GetList(Expression<Func<Ventas, bool>> criterio)
        {
            List<Ventas> lista = new List<Ventas>();

            try
            {
                lista = await _contexto.Venta.Where(criterio).Include(c => c.Cliente).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }

    }
}
