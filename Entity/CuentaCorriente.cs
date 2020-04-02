using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CuentaCorriente:Cuenta
    {
        public const decimal Cupo = 1000000;
        public decimal Debe { get; set; }

        public CuentaCorriente(decimal saldo):base(saldo)
        {
            Saldo = saldo;
            Debe = Cupo;
        }

        public override string Consignar(decimal valor)
        {
            if (valor > 0 && Saldo >= valor)
            {
                Saldo -= valor;
                Debe += valor;
                RegistrarMovimientos(valor, "Consignacion", Cupo);
                return $"\n Se consigno: {valor}, nuevo saldo es: {Saldo}, y su cupo es de: {Debe}";
            }
            return $"\n No puede consginar un valor mayor al cupo: {Saldo}";
        }
        public override string ConsultarSaldo()
        {
            return $"Su cupo con el banco es: {Saldo}";
        }

        public override string Retirar(decimal valor)
        {
            if (valor <= Cupo)
            {
                Saldo += valor;

                Debe -= valor;
                RegistrarMovimientos(valor, "Retiro", Cupo);
                return $"\n Se retiro: {valor}, su nuevo saldo es: {Saldo}, y su cupo es de {Debe}";
            }
            return $"\n No es posible realizar el retiro excede su cupo: {Saldo}";
        }
    }
    
}
