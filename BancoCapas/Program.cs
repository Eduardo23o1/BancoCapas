using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using Entity;
namespace BancoCapas
{
    class Program
    {
        static Cliente cliente;
        static Cuenta cuenta;
        static CuentaAhorroService cuentaService = new CuentaAhorroService();
        static Movimiento movimiento;

        static void Main(string[] args)
        {

            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("\n Banco AHORRATODO\n");
                Console.WriteLine(" 1. Ingresar");
                Console.WriteLine(" 2. Registrar usuario");
                Console.WriteLine(" 3. Consultar");
                Console.WriteLine(" 4. Eliminar cuenta");
                Console.WriteLine(" 5. Salir");
                Console.Write("\n DIGITE UNA OPCION >>> "); opcion = Int32.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1: Ingresar(); break;
                    case 2: RegistrarUsuario(); break;
                    case 3: Consultar(); break;
                    case 4: EliminarCuenta(); break;
                    case 5: Console.Write("\n Pulse enter para salir..."); Console.ReadKey(); break;
                    default:
                        Console.Write("\n Numero fuera de rango intente de nuevo..."); Console.ReadKey(); break;
                }
            } while (opcion != 5);
        }

        public static void Ingresar()
        {
            Console.Clear();
            string numeroCuenta, clave;
            Console.WriteLine("\n 1. Ingresar\n");
            Console.Write(" Digite numero de cuenta: "); numeroCuenta = Console.ReadLine();
            Console.Write(" Digite contraseña: "); clave = Console.ReadLine();
            cuenta = cuentaService.BuscarNumeroCuenta(numeroCuenta);

            if (cliente == null)
            {
                Console.Write("\n Numero de cuenta o contraseña incorrectos..."); Console.ReadKey();
            }
            else
            {
                IngresarMenu(cuenta);
            }
        }
        static public void IngresarMenu(Cuenta cuenta)
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("\n -------- MENU DE CUENTA --------");
                Console.WriteLine(" 1. Retirar");
                Console.WriteLine(" 2. Consignar");
                Console.WriteLine(" 3. Consultar saldo");
                Console.WriteLine(" 4. Consultar movimientos");
                Console.WriteLine(" 5. Salir");
                Console.Write(" --------------------------------");
                Console.Write("\n DIGITE UNA OPCION >>> "); opcion = Int32.Parse(Console.ReadLine());
                switch (opcion)
                {
                    case 1: RetirarSaldo(cuenta); break;
                    case 2: ConsignarSaldo(cuenta); break;
                    case 3: ConsultarSaldo(cuenta); break;
                    case 4: ConsultarMoviminetos(cuenta); break;
                    case 5: Console.Write("\n Pulse enter para salir..."); Console.ReadKey(); break;
                    default:
                        Console.Write("\n Numero fuera de rango intente de nuevo..."); Console.ReadKey(); break;
                }
            } while (opcion != 5);
        }
        static public void RetirarSaldo(Cuenta cuenta)
        {
            decimal valor, cupo = 0;
            Console.Write("\n Digite valor a retirar: ");
            valor = decimal.Parse(Console.ReadLine());
            Console.WriteLine(cuenta.Retirar(valor));
            Console.Write("\n Pulse enter para continar..."); Console.ReadKey();
        }
        static public void ConsignarSaldo(Cuenta cuenta)
        {
            decimal valor, cupo = 0;
            Console.Write("\n Digite valor a consignar: "); valor = decimal.Parse(Console.ReadLine());
            Console.WriteLine(cuenta.Consignar(valor));
            Console.Write("\n Pulse enter para continar..."); Console.ReadKey();
        }
        static public void ConsultarSaldo(Cuenta cuenta)
        {
            Console.WriteLine($"\n Su saldo actual es: {cuenta.Saldo}");
            Console.Write("\n Pulse enter para continar..."); Console.ReadKey();
        }
        static public void ConsultarMoviminetos(Cuenta cuenta)
        {
            if (cuenta.Movimientos.Count == 0)
            {
                Console.WriteLine("\n No se han realizado movimientos");
                Console.Write("\n Pulse enter para continar..."); Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\n Movimientos de la cuenta\n");
                foreach (var itemMovimientos in cuenta.Movimientos)
                {
                    Console.WriteLine(" --------------------");
                    Console.WriteLine($" Tipo: {itemMovimientos.Tipo}");
                    Console.WriteLine($" Valor: {itemMovimientos.Valor}");
                    Console.WriteLine($" Saldo: {itemMovimientos.Saldo}");
                    if (itemMovimientos.Cupo != 0)
                    {
                        Console.WriteLine($" Cupo: {itemMovimientos.Cupo}");
                    }
                    Console.WriteLine($" Fecha: {String.Format("{0: MM/dd/yyyy - HH: mm: ss}", itemMovimientos.Fecha)}");
                    Console.WriteLine(" --------------------\n");
                }
                Console.Write("\n Pulse enter para continar..."); Console.ReadKey();
            }
        }

        public static void RegistrarUsuario()
        {
            Console.Clear();
            string mensaje;
            cliente = new Cliente();
            cliente.Cuentas = new List<Cuenta>();
            Console.WriteLine("\n 2. Registrar usuario\n");
            Console.WriteLine(" ----------------------------------");
            Console.WriteLine(" Ingresar informacion de usuario\n");
            Console.Write(" Identificacion: "); cliente.Identificacion = Console.ReadLine();
            Console.Write(" Nombre: "); cliente.Nombre = Console.ReadLine();
            Console.Write(" Apellido: "); cliente.Apellido = Console.ReadLine();
            Console.Write(" Edad: "); cliente.Edad = Int32.Parse(Console.ReadLine());
            Console.Write(" Genero: "); cliente.Genero = Console.ReadLine();
            Console.WriteLine(" ----------------------------------");
            cliente.Cuentas.Add(CrearCuenta(cliente));

            mensaje = cuentaService.Guardar(cliente);
            Console.Write($"\n{mensaje}");
            Console.Write("\n Presione enter para volver al menu..."); Console.ReadKey();
        }
        public static Cuenta CrearCuenta(Cliente cliente)
        {
            byte opcion;
            decimal saldo = 0;
            bool comprobar = false;
            do
            {
                Console.WriteLine("\n ----------------------------------");
                Console.WriteLine(" Crear cuenta\n");
                Console.WriteLine(" 1. Cuenta de ahorro");
                Console.WriteLine(" 2. Cuenta corriente");
                Console.WriteLine(" ----------------------------------");
                Console.Write("\n DIGITE UNA OPCION >>> "); opcion = byte.Parse(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        Console.Write("\n Digite saldo con el que desea crear su cuenta: "); saldo = decimal.Parse(Console.ReadLine());
                        cuenta = AgregarCuenta(saldo, "Ahorro", cliente); comprobar = true; break;
                    case 2:
                        cuenta = AgregarCuenta(saldo, "Corriente", cliente); comprobar = true; break;
                    default: Console.Write("\n Numero fuera de rango intente de nuevo..."); Console.ReadKey(); Console.Clear(); break;
                }
            } while (comprobar != true);
            return cuenta;
        }
        public static Cuenta AgregarCuenta(decimal saldo, string tipo, Cliente cliente)
        {
            if (tipo.Equals("Ahorro"))
            {
                cuenta = new CuentaAhorro(saldo);
                cuenta.Tipo = tipo;
                cuenta.Numero = $"{cliente.Identificacion}";

            }
            else if (tipo.Equals("Corriente"))
            {
                cuenta = new CuentaCorriente(saldo);
                cuenta.Tipo = tipo;
                cuenta.Numero = $"{cliente.Identificacion}";
            }
            cuenta.Movimientos = new List<Movimiento>();
            return cuenta;
        }

        static public void Consultar()
        {
            Console.Clear();
            Console.WriteLine("\n 3. Consultar\n");

            foreach (var itemCuenta in cuentaService.Consultar())
            {
                Console.WriteLine(" -------------------------------------");
                Console.WriteLine($" Identificacion: {itemCuenta.Identificacion}");
                Console.WriteLine($" Apellido: {itemCuenta.Apellido}");
                Console.WriteLine($" Nombre: {itemCuenta.Nombre}");
                Console.WriteLine($" Edad: {itemCuenta.Edad}");
                Console.WriteLine($" Genero: {itemCuenta.Genero}\n");
                foreach (var item in itemCuenta.Cuentas)
                {
                    Console.WriteLine($" Tipo de cuenta: {item.Tipo}");
                    Console.WriteLine($" Numero de cuenta: {item.Numero}");
                    Console.WriteLine($" Saldo: {item.Saldo}");
                }
                Console.WriteLine(" -------------------------------------");
            }
            Console.WriteLine("\n Presione enter para continuar..."); Console.ReadKey();
        }

        static public void EliminarCuenta()
        {
            Console.Clear();
            string identificacion, numeroCuenta;
            Console.WriteLine("\n 4. Eliminar cuenta\n");
            Console.Write(" Digite su identificacion: "); identificacion = Console.ReadLine();
            cliente = cuentaService.BuscarIdentificacion(identificacion);
            if (cliente == null)
            {
                Console.WriteLine($"\n Numero de identifiacion [{identificacion}] no encontrada");
            }
            else
            {
                Console.Write(" Digite numero de cuenta a eliminar: "); numeroCuenta = Console.ReadLine();
                cuenta = cuentaService.BuscarNumeroCuenta(numeroCuenta);
                if (cuenta == null)
                {
                    Console.WriteLine($"\n Numero de cuenta [{numeroCuenta}] no encontrada");
                }
                else
                {
                    cliente.Cuentas.Remove(cuenta);
                    Console.WriteLine($"\n Cuenta eliminada correctamente");
                }
            }
            Console.Write("\n Pulse enter para continar..."); Console.ReadKey();
        }
    }
}
