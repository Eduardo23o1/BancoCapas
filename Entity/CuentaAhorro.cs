using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   public class CuentaAhorro:Cuenta
    {
        public CuentaAhorro(decimal saldo):base (saldo)
        {

        }
        public override string Consignar(decimal valor)
        {

            if (valor > 0)
            {
                Saldo = Saldo + valor;
                RegistrarMovimientos(valor, "Ahorro");
                return $"Se cosigno: {valor} su nuevo saldo es {Saldo}";
            }

            return $"NO es posible consignar valores negativos";
        }

        public override string Retirar(decimal valor)
        {
            if (Saldo >= valor)
            {
                Saldo = Saldo - valor;
                RegistrarMovimientos(valor, "retirar");
                return $"Se retiró: {valor} su nuevo saldo es {Saldo}";
            }
            return $"Saldo insuficiente su saldo es:  {Saldo}";
        }
        public override string ConsultarSaldo()
        {
            return $"Su saldo es: {Saldo}";
        }

        public string verMovimiento()
        {
            
                return   VisualizarMovimiento();
            
            
        }


    }
}
