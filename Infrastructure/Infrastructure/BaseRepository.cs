using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    // 🔹 AJUSTE: se renombra para evitar conflicto con Repositories.BaseRepository<T>
    public class UnitOfWork
    {
        public readonly AppDbContext context;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public async Task Beguin()
        {
            await context.Database.BeginTransactionAsync();
        }

        public async Task Comit()
        {
            await context.Database.CommitTransactionAsync();
        }

        public async Task RollBack()
        {
            await context.Database.RollbackTransactionAsync();
        }
    }
}
