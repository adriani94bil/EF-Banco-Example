using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFBanco.Models
{
    public class Sucursal
    {
        public int Id { get; set; }
        [Required]
        public string Direccion { get; set; }
        public int BancoId { get; set; }
        public Banco Banco { get; set; }
        public List<Cliente> Cliente { get; set; }
    }
}
