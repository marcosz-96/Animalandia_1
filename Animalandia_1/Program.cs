using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Animalandia_1
{
    public struct Productos
    {
        public string Nombre;
        public char Categoria; //AE = areo, T = terrestre, AC = acuatico
        public int Stock;
        public double Precio;
        public bool Disponible;

        public Productos(string nombre, char categoria, int sotck, double precio, bool disponible)
        {
            Nombre = nombre;
            Categoria = categoria;
            Stock = sotck;  
            Precio = precio;
            Disponible = disponible;
        }
    }
    internal class Program
    {
        //creamos una lista para guardar los datos de los animales
        static List<Productos> inventario = new List<Productos>();
        //creamo un arreglo para guardar el tipo de animal
        static string[] locales = { "Local 1", "Local 2", "Local 3" };
        //cramos una matriz para definir a que categoria pertenece cada animal
        static int[,] ventasEnLocales = new int[locales.Length, 12]; //12 meses del año

        static bool existe = false;
        static string palabra, nombre;
        static int opcion, stock, numero, contador;
        static double sumarPrecio, precio;
        static void Main(string[] args)
        {
            do
            {
                Mensaje();
                opcion = int.Parse(Console.ReadLine());
                Menu();
            } while (opcion != 0);

        }
        static void Mensaje()
        {
            Console.Clear();
            Console.WriteLine("==>¡¡¡Bienvenido al Menú!!!<==");
            Console.WriteLine("Seleccione una opcion:");
            Console.WriteLine("1: Agregar Producto.");
            Console.WriteLine("2: Ver Lista de Productos.");
            Console.WriteLine("3: Ordenar productos por Stock.");
            Console.WriteLine("4: Filtar productos por Precio.");
            Console.WriteLine("5: Registrar Ventas.");
            Console.WriteLine("0: Para Salir.");
        }
        static void Menu()
        {
            Console.Clear();
            switch (opcion)
            {
                case 1:
                    AgregarProductos(inventario, palabra);
                    break;
                case 2:
                    ListaDeProductos(inventario);
                    break;
                case 3:
                    OrdenarProductos(inventario);
                    break;
                case 4:
                    FiltrarProductos(inventario, precio);
                    break;
                case 5:
                    RegistroDeVentas(inventario, locales, ventasEnLocales, numero);
                    break;
                case 0:
                    Console.WriteLine("Ha salido del programa.");
                    break;
                default:
                    Console.WriteLine("Valor invalido. Ingrese una opcion del menú");
                    break;
            }
        }
        static void AgregarProductos(List<Productos> inventario, string palabra)
        {
            Console.Clear();
            bool continuarIngresando = true;
            while (continuarIngresando)
            {
                Console.WriteLine("Ingrese 'nuevo' para agregar datos. \nO 'menu' para salir.");
                palabra = Console.ReadLine().ToLower();

                if (palabra == "menu")
                {
                    Console.WriteLine("Ha salido de la opcion para agregar datos.");
                    continuarIngresando = false;
                }
                else if (palabra == "nuevo")
                {
                    Console.Write("Ingrese el nombre del producto: ");
                    string nombre = Console.ReadLine();
                    //validamos si existe el nombre
                    foreach (var dato in inventario)
                    {
                        if (dato.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("El nombre del producto ya existe. Ingrese otro nombre");
                        }
                    }

                    Console.Write("Ingrese la categoria del producto (A - alimentos, B - bebidas, L - limpieza): ");
                    char categoria = char.ToUpper(Console.ReadLine()[0]);
                    //validamos ingrese del caracter correcto
                    if (!ValidarCategoria(categoria))
                    {
                        Console.WriteLine("Valor invalido. Ingrese 'A', 'B' o 'L'");
                        return;
                    }
                    else if (categoria == 'A')
                    {
                        Console.WriteLine("Pertenece a 'Alimentos'");
                    }
                    else if (categoria == 'B')
                    {
                        Console.WriteLine("Pertence a 'Bebidas'");
                    }
                    else if (categoria == 'L')
                    {
                        Console.WriteLine("Pertenece a 'Limpieza'");
                    }

                    Console.Write("Ingrese el stock del producto: ");
                    int stock;
                    //validamos el valor ingresado
                    while (!int.TryParse(Console.ReadLine(), out stock) || stock < 0)
                    {
                        Console.WriteLine("Valor invalido. Ingrese un numero igual o mayor a 0");
                    }

                    Console.Write("Ingrese el precio del producto: ");
                    double precio;
                    //validamos el valor tipo precio
                    while (!double.TryParse(Console.ReadLine(), out precio) || precio < 0)
                    {
                        Console.WriteLine("Valor invalido. Ingrese un numero igual a mayor a 0");
                    }

                    Console.Write("Ingrese disponibilidad del producto (true/false): ");
                    bool disponible;
                    //validamos el dato de entrada
                    while (!bool.TryParse(Console.ReadLine(), out disponible))
                    {
                        Console.WriteLine("Valor invalido. Ingrese 'true' o 'false'");
                    }
                    //añadimos el producto a la lista
                    Productos nuevoProducto = new Productos(nombre, categoria, stock, precio, disponible);
                    inventario.Add(nuevoProducto);
                    //mensaje final
                    Console.WriteLine("Producto agregado exitosamnete!");
                    Console.WriteLine();
                }
            }

        }
        static bool ValidarCategoria(char categoria)
        {
            return categoria == 'A' || categoria == 'B' || categoria == 'L';
        }
        static void ListaDeProductos(List<Productos> inventario)
        {
            Console.Clear();
            inventario.Add(new Productos
            {
                Nombre = "Jabon",
                Categoria = 'L',
                Stock = 189,
                Precio = 1230.67,
                Disponible = true
            });

            inventario.Add(new Productos
            {
                Nombre = "Pan",
                Categoria = 'A',
                Stock = 59,
                Precio = 670.89,
                Disponible = true
            });

            inventario.Add(new Productos
            {
                Nombre = "Brahama",
                Categoria = 'B',
                Stock = 267,
                Precio = 2390.53,
                Disponible = true
            });

            inventario.Add(new Productos
            {
                Nombre = "Coca-Cola",
                Categoria = 'B',
                Stock = 0,
                Precio = 1350.00,
                Disponible = false
            });

            Console.WriteLine("Los productos son:");
            foreach (var producto in inventario)
            {
                Console.WriteLine($"Nombre: {producto.Nombre}, Categoria: {producto.Categoria}, " +
                    $"Stock: {producto.Stock}, Precio: {producto.Precio}, Disponible: {producto.Disponible}");
            }
            Console.WriteLine("Presione 'enter' par avolve al menú");
            Console.ReadKey();
        }
        static void OrdenarProductos(List<Productos> inventario)
        {
            Console.Clear();
            //Ordenamos de Mayor a Menor con Bubble Sort
            for (int i = 0; i < inventario.Count; i++)
            {
                for (int j = 0; j < inventario.Count - 1 - i; j++)
                {
                    if (inventario[j].Stock < inventario[j + 1].Stock)
                    {
                        var temp = inventario[j];
                        inventario[j] = inventario[j + 1];
                        inventario[j + 1] = temp;
                    }
                }
            }
            Console.WriteLine("Lista ordenada por Stock");
            foreach (var producto in inventario)
            {
                Console.WriteLine($"Nombre: {producto.Nombre}, Categoria: {producto.Categoria}, " +
                    $"Stock: {producto.Stock}, Precio: {producto.Precio}, Disponible: {producto.Disponible}");
            }
            Console.WriteLine("Presione 'enter' par avolve al menú");
            Console.ReadKey();
        }
        static void FiltrarProductos(List<Productos> inventario, double precio)
        {
            Console.Clear();
            Console.Write("Ingrese un valor: ");
            precio = double.Parse(Console.ReadLine());
            //filtramos la busqueda
            Console.WriteLine("Los productos son:");
            foreach (var producto in inventario)
            {
                if(precio <= producto.Precio)
                {
                    Console.WriteLine($"Producto: {producto.Nombre}, Precio: {producto.Precio}");
                }
            }
            Console.WriteLine("Presione 'enter' para volver al menu");
            Console.ReadKey();
        }
        static void RegistroDeVentas(List<Productos> inventario, string[] locales, int[,] ventasEnLocales, int numero)
        {
            Console.Clear();
            
            Console.WriteLine("Registrando Ventas");
            //mostramos los locales disponibles (arreglo locales)
            for (int i = 0; i < locales.Length; i++) 
            {
                Console.WriteLine($"{i + 1} {locales[i]}");
            }
            //luego elegimos el local al cual cargaremos el registro de ventas
            Console.Write("Seleccion el local para registrar ventas (entre 1-3): ");
            numero = int.Parse(Console.ReadLine()) -1;
            //verificamos si el indice es válido
            if (numero < 0 || numero > locales.Length)
            {
                Console.WriteLine("Valor incorrecto. Ingrese entre '1-3'");
            }
            //luego recorremos la matriz ventasEnLocales
            for (int mes = 0; mes < 12; mes++) 
            {
                Console.Write($"Ingrese la cantidad de productos vendidos en mes {mes +1}: ");
                ventasEnLocales[numero, mes] = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("Ventas registradas exitosamente!");
            Console.WriteLine("Presione 'enter' para volver al menú");
            Console.ReadKey();
        }
    }
}
