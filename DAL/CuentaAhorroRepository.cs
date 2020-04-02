using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace DAL
{
    public class CuentaAhorroRepository
    {
        private string ruta = @"Cuenta.txt";
        private List<Cliente> clientes;
        public CuentaAhorroRepository()
        {
            clientes = new List<Cliente>();
        }

        public void Guardar(Cliente cliente)
        {
            clientes.Add(cliente);
            FileStream fileStream = new FileStream(ruta, FileMode.Append);
            StreamWriter writer = new StreamWriter(fileStream);
            writer.WriteLine($"{ cliente.Identificacion}, { cliente.Nombre}, { cliente.Apellido}, { cliente.Edad}, { cliente.Genero}");
            writer.Close();
            fileStream.Close();

        }
        public void Eliminar(Cliente cliente)
        {
            clientes.Remove(cliente);
        }
        public Cuenta BuscarNumeroCuenta(string numeroCuenta)
        {
            foreach (var itemCliente in clientes)
            {
                foreach (var itemCuenta in itemCliente.Cuentas)
                {
                    if (itemCuenta.Numero.Equals(numeroCuenta))
                    {
                        return itemCuenta;
                    }
                }
            }
            return null;
        }
        public Cliente BuscarIdentificacion(string identificacion)
        {
            foreach (var itemCliente in clientes)
            {
                if (itemCliente.Identificacion.Equals(identificacion))
                {
                    return itemCliente;
                }
            }
            return null;
        }
        public List<Cliente> Consultar()
        {
            clientes.Clear();
            FileStream fileStream = new FileStream(ruta, FileMode.OpenOrCreate);
            StreamReader lector = new StreamReader(fileStream);
            String linea = string.Empty;
            while ((linea = lector.ReadLine()) != null)
            {
                Cliente cliete = Mapearpersona(linea);
                clientes.Add(cliete);

            }
            fileStream.Close();
            lector.Close();
            return clientes;
        }

        private static Cliente Mapearpersona(string linea)
        {
            Cliente cliente = new Cliente();
            string[] datos = linea.Split(';');
            cliente.Identificacion = datos[0];
            cliente.Nombre = datos[1];
            cliente.Edad = int.Parse(datos[2]);
            cliente.Genero = datos[3];
            return cliente;
        }
    }
}
