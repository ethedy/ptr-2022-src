using Servicios;

namespace consola
{
  internal class Program
  {
    static void Main(string[] args)
    {
      ServiciosClima sc = new ServiciosClima();

      var datos = sc.ObtenerDatosClimaticos();

      foreach (var item in datos)
        Console.WriteLine($"{item.Fecha} {item.Temperatura}");

      //  calcular estadisticas
      Console.ReadLine();
    }
  }
}