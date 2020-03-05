using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFBanco.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Iban { get; set; }
        public double Balance { get; set; }
        public int SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }

    }
}
