using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class EventosPersist : IEventoPersist
    {
        private readonly ProEventosContext _context;
        public EventosPersist(ProEventosContext context)
        {
            _context = context;
            // _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(p => p.Lotes)
                                                       .Include(p => p.RedesSociais);

            if (includePalestrantes)
            {
                query = query.Include(p => p.PalestrantesEventos)
                             .ThenInclude(p => p.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }
        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(p => p.Lotes)
                                                      .Include(p => p.RedesSociais);

            if (includePalestrantes)
            {
                query = query.Include(p => p.PalestrantesEventos)
                             .ThenInclude(p => p.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id)
                         .Where(p => p.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(p => p.Lotes)
                                                       .Include(p => p.RedesSociais);

            if (includePalestrantes)
            {
                query = query.Include(p => p.PalestrantesEventos)
                             .ThenInclude(p => p.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id).Where(p => p.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}