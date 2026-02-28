using System.Collections.Generic;

namespace Organograma_SEI_SEE.Models
{
    public class Setor
    {
        public string Nome { get; set; }
        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public List<Setor> SubSetores { get; set; } = new List<Setor>();
    }
}