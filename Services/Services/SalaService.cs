using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Infrastructure.Repositories;

namespace Services
{
    public class SalaService : ISalaService
    {
        private readonly IRepository<Sala> _repoSalas;
        private readonly IRepository<Usuario> _repoUsuarios;
        private readonly IPrestamoSalaRepository _repoPrestamos;

        public SalaService(
            IRepository<Sala> repoSalas,
            IRepository<Usuario> repoUsuarios,
            IPrestamoSalaRepository repoPrestamos)
        {
            _repoSalas = repoSalas;
            _repoUsuarios = repoUsuarios;
            _repoPrestamos = repoPrestamos;
        }

        // --------------------------
        // ADMINISTRADOR
        // --------------------------
        public async Task<IEnumerable<Sala>> ObtenerTodasAsync()
        {
            return await _repoSalas.GetAllAsync();
        }

        public async Task<Sala> ObtenerPorIdAsync(int id)
        {
            return await _repoSalas.GetByIdAsync(id);
        }

        public async Task CrearAsync(Sala sala)
        {
            await _repoSalas.AddAsync(sala);
            await _repoSalas.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Sala sala)
        {
            _repoSalas.Update(sala);
            await _repoSalas.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var sala = await _repoSalas.GetByIdAsync(id);
            if (sala != null)
            {
                _repoSalas.Remove(sala);
                await _repoSalas.SaveChangesAsync();
            }
        }

        // --------------------------
        // USUARIO NORMAL
        // --------------------------
        public async Task SolicitarSalaAsync(int salaId, int usuarioId)
        {
            var solicitud = new PrestamoSala
            {
                SalaId = salaId,
                UsuarioId = usuarioId,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddHours(2),
                Estado = "Pendiente",
                AprobadoPor = null
            };

            await _repoPrestamos.CrearAsync(solicitud);
            await _repoPrestamos.GuardarAsync();
        }

        public async Task<IEnumerable<PrestamoSala>> ObtenerSolicitudesUsuarioAsync(int usuarioId)
        {
            return await _repoPrestamos.ObtenerPorUsuarioAsync(usuarioId);
        }

        // --------------------------
        // COORDINADOR
        // --------------------------
        public async Task<IEnumerable<PrestamoSala>> ObtenerSolicitudesAsync()
        {
            return await _repoPrestamos.ObtenerTodasAsync();
        }

        public async Task CambiarEstadoSolicitud(int solicitudId, string nuevoEstado)
        {
            var solicitud = await _repoPrestamos.GetByIdAsync(solicitudId);
            if (solicitud == null)
                return;

            solicitud.Estado = nuevoEstado;

            // Si fue aceptado → actualizar responsable de la sala
            if (nuevoEstado == "Aceptado")
            {
                var sala = await _repoSalas.GetByIdAsync(solicitud.SalaId);
                var usuario = await _repoUsuarios.GetByIdAsync(solicitud.UsuarioId);

                if (sala != null && usuario != null)
                {
                    sala.Responsable = usuario.Nombre;
                    _repoSalas.Update(sala);
                    await _repoSalas.SaveChangesAsync();
                }
            }

            await _repoPrestamos.GuardarAsync();
        }
    }
}


