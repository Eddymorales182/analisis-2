using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Apiempresa.Models;

    public class Conexiones : DbContext
    {
        public Conexiones (DbContextOptions<Conexiones> options)
            : base(options)
        {
        }

        public DbSet<Apiempresa.Models.Clientes> Clientes { get; set; } = default!;
    }
