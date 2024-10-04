using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwYO.Models.Interfaces
{
    public interface IDBAccess
    {
        public Cliente ComprobarLogin(String Nickname, String Password);
        public List<Heroe> RecuperarListaHeroes();

        public Heroe RecuperarHeroe(int IdHeroe);


        public bool BorrarHeroe(int idHeroe);

        public bool CrearHeroe(Heroe newHero);
        public bool ActualizarHeroe(Heroe Heroe);
    }
}
