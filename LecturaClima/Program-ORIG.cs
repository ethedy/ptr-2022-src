
/*
    Eliminamos la primer columna con el nombre de la localidad "Rosario, Argentina"
    y la ultima con el codigo de estacion "SAAR,87480099999"

 X  00  datetime          1/6/2022                              2/6/2022
 X  01  tempmax           11.7                                  13.8
 X  02  tempmin           6.3                                   5.4
    03  temp              8.5                                   8.7
    04  feelslikemax      11.7                                  13.8
    05  feelslikemin      4.9                                   5.2
    06  feelslike         8.3                                   8.3
    07  dew               3.4                                   3.2
 X  08  humidity          70.6                                  70.5
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

using System.Globalization;

const int NUMERO_CAMPOS = 31;

// ReSharper disable StringIndexOfIsCultureSpecific.1

using StreamReader rdr = new StreamReader(@"Datos-Clima.csv");

//  using string xx;  da error porque string no tiene Dispose()

//  DatoClima[] datos = new DatoClima[50000];

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
    Console.WriteLine($"Marcar error y seguir...{linea}");
  }
}

//float sumaTotal = 0.0f;

//foreach (var item in datos)
//  sumaTotal += item.TempMinima;
//Console.WriteLine($"Promedio de minimas: {sumaTotal / datos.Count:N1}");

float[] d = new float[datos.Count];
int idx = 0;
foreach (var x in datos)
  d[idx++] = x.TempMinima;

//  en d[] voy a tener el array de todos los valores de ....
//    temp minima

Calculo(Promedio, d, "Promedio de temperaturas minimas");
Calculo(Minimo, d, "Minimo de temp minimas");
Calculo(Maximo, d, "Maximo de temp minimas");


Console.ReadLine();

//  rdr.Close();    ==> OK, pero no estoy liberando recursos del OS, mejor Dispose()
//  rdr.Dispose();  ==> es automatico si uso la sentencia using

//  calcular estadisticas...

void Calculo(Func<float[], float> delegado, float[] valores, string texto)
{
  float resultado = delegado(valores);

  Console.WriteLine($"{texto}: {resultado}");
}


float Promedio(float[] items)
{
  float total = 0f;

  foreach (var i in items)
    total += i;
  return total / items.Length;
}

float Maximo(float[] items)
{
  float maximo = float.MinValue;

  foreach (var i in items)
    if (i > maximo)
      maximo = i;
  return maximo;
}

float Minimo(float[] items)
{
  float minimo = float.MaxValue;

  foreach (var i in items)
    if (i < minimo)
      minimo = i;
  return minimo;
}



//   name,datetime,tempmax,tempmin,temp,feelslikemax,feelslikemin,feelslike,dew,humidity,precip,precipprob,precipcover,preciptype,snow,snowdepth,windgust,windspeed,winddir,sealevelpressure,cloudcover,visibility,solarradiation,solarenergy,uvindex,severerisk,sunrise,sunset,moonphase,conditions,description,icon,stations
//   "Rosario, Argentina",2022-06-01,11.7,6.3,8.5,11.7,4.9,8.3,3.4,70.6,0,0,0,,0,0,16.2,8.5,131.3,1021.9,100,11.7,120.6,10.5,5,10,2022-06-01T07:57:09,2022-06-01T18:03:47,0.02,Overcast,Cloudy skies throughout the day.,cloudy,"SAAR,87480099999"
//                      ^                                                                                                                                                                                                      ^                   
//                      primera                                                                                                                                                                                                ultima
//
bool TryParseDatoClima(string linea, out DatoClima datosDia)
{
  datosDia = default;

  int primera = linea.IndexOf("\",");
  int ultima = linea.IndexOf(",\"");

  if (primera < 0 || ultima < 0 || ultima < primera)
    return false;

  string lineaValida = linea.Substring(primera + 2, ultima - (primera + 2));
  string[] items = lineaValida.Split(new char[] { ',' }, StringSplitOptions.TrimEntries);

  if (items.Length != NUMERO_CAMPOS)
    return false;

  if (!DateTime.TryParseExact(items[0], new[] { "yyyy-MM-dd" }, null, DateTimeStyles.None,
        out DateTime fecha))
  {
    Console.WriteLine($"Fecha incorrecta ==> {items[0]}");
    return false;
  }

  //  para generar un error y probar try..catch, editar el .csv y cambiar alguno de los
  //  valores de temperatura para que no sea un float (ej 16xx.8)
  //
  try
  {
    float tempMaxima = ConvertirFloat(items[1], "Temperatura Maxima");
    float tempMinima = ConvertirFloat(items[2], "Temperatura Minima");
    float humedad = ConvertirFloat(items[8], "Humedad");

    //  datosDia.Fecha = fecha; NO!!!
    //
    datosDia = new DatoClima(fecha, tempMinima, tempMaxima, humedad);
    return true;
  }
  catch (LecturaClimaException ex)
  {
    Console.WriteLine(ex.Message);
    return false;
  }

  //  funcion local --> puede lanzar LecturaClimaException
  //
  float ConvertirFloat(string s, string nombreCampo)
  {
    if (!float.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out float resultado))
      throw new LecturaClimaException(nombreCampo, s);

    return resultado;
  }
}

/// <summary>
/// Tipo inmutable que contiene los datos climaticos para una fecha particular
/// </summary>
/// <param name="Fecha"></param>
/// <param name="TempMinima"></param>
/// <param name="TempMaxima"></param>
/// <param name="Humedad"></param>
public record DatoClima(DateTime Fecha, float TempMinima, float TempMaxima, float Humedad);

/// <summary>
/// Clase de excepcion customizada para nuestra aplicacion
/// Contiene el mensaje producido desde la conversion a DatoClima
/// </summary>
class LecturaClimaException : Exception
{
  public LecturaClimaException(string mensaje, string origen) : 
    base($"Error leyendo {mensaje} ==> valor original [{origen}]")
  {

  }
}


