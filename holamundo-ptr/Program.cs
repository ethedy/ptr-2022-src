Console.WriteLine("Ingresar el nombre");

string nombre = Console.ReadLine();

string mensaje = @$"Hola, {nombre}
Me quiero ir";

Console.WriteLine(mensaje);

object nombre1 = 15;

string texto = (string)nombre1;

Console.WriteLine(texto);

