using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Servicios
{
  public class ServiciosClima
  {
    public IEnumerable<DatoClima> ObtenerDatosClimaticos()
    {
      return new List<DatoClima>()
      {
        new DatoClima { Fecha = DateTime.Now, Temperatura = 10.2f},
        new DatoClima { Fecha = DateTime.Now, Temperatura = 1.2f},
        new DatoClima { Fecha = DateTime.Now, Temperatura = -10.2f}
      };
    }
  }
}
