
/*
    Eliminamos la primer columna con el nombre de la localidad "Rosario, Argentina"
    y la ultima con el codigo de estacion "SAAR,87480099999"

    01  datetime          1/6/2022                              2/6/2022
    02  tempmax           11.7                                  13.8
    03  tempmin           6.3                                   5.4
    04  temp              8.5                                   8.7
    05  feelslikemax      11.7                                  13.8
    06  feelslikemin      4.9                                   5.2
    07  feelslike         8.3                                   8.3
    08  dew               3.4                                   3.2
    09  humidity          70.6                                  70.5
    10  precip            0                                     0
    11  precipprob        0                                     0
    12  precipcover       0                                     0
    13  preciptype
    14  snow              0                                     0
    15  snowdepth         0                                     0
    16  windgust          16.2                                  14.8
    17  windspeed         8.5                                   11.3
    18  winddir           131.3                                 114.4
    19  sealevelpressure  1021.9                                1024
    20  cloudcover        100                                   80.8
    21  visibility        11.7                                  11
    22  solarradiation    120.6                                 123.9
    23  solarenergy       10.5                                  10.6
    24  uvindex           5                                     5
    25  severerisk        10                                    10
    26  sunrise           2022-06-01T07:57:09                   2022-06-02T07:57:42
    27  sunset            2022-06-01T18:03:47                   2022-06-02T18:03:32
    28  moonphase         0.02                                  0.04
    29  conditions        Overcast                              Partially cloudy
    30  description       Cloudy skies throughout the day.      Partly cloudy throughout the day.
    31  icon              cloudy                                partly-cloudy-day
 */

using StreamReader rdr = new StreamReader(@"D:\CURSOS\2022\src\Datos-Clima.csv");

//  using string xx;  da error porque string no tiene Dispose()

//  DatoClima[] datos = new DatoClima[500];

List<DatoClima> datos = new List<DatoClima>();

while (!rdr.EndOfStream)
{
  string linea = rdr.ReadLine();
  //  DatoClima datosDia; --> a partir de C#8 podemos declarla en el mismo momento de usar el "out"

  //  procesar linea
  //
  if (TryParseDatoClima(linea, out DatoClima datosDia))
  {
    //  agregar datosDia al array de valores validos
    //
    datos.Add(datosDia);
  }
  else
  {
    Console.WriteLine("Marcar error y seguir...");
  }

}


Console.ReadLine();

//  rdr.Dispose();  

//  calcular estadisticas...

bool TryParseDatoClima(string linea, out DatoClima datosDia)
{
  //  obtener datos del clima
  datosDia = default;
  return true;
}

public record DatoClima(DateTime Fecha, float TempMinima, float TempMaxima, float Humedad);

