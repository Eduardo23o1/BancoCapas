using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;

namespace BLL
{
    public class CuentaAhorroService
    {
        CuentaAhorroRepository AhorroRepositorio;
        public CuentaAhorroService()
        {
            AhorroRepositorio = new CuentaAhorroRepository();
        }

        public string Guardar(Cliente cliente)
        {
            if (BuscarIdentificacion(cliente.Identificacion) == null)
            {
                AhorroRepositorio.Guardar(cliente);
                return $" Cliente [{cliente.Nombre} {cliente.Apellido}] registrado correctamente";
            }
            return $" El numero de identificacion: {cliente.Identificacion} ya existe, no se aceptan duplicados";
        }
        public Cuenta BuscarNumeroCuenta(string numeroCuenta)
        {
            return AhorroRepositorio.BuscarNumeroCuenta(numeroCuenta);
        }
        public Cliente BuscarIdentificacion(string identificacion)
        {
            return AhorroRepositorio.BuscarIdentificacion(identificacion);
        }

        public List<Cliente>Consultar()
        {
            return AhorroRepositorio.Consultar();
        }
    }
}
