using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFBanco.Models
{
    public class Banco
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public List<Sucursal> Sucursal { get; set; }
    }
}
