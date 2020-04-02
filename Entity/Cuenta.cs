using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    abstract public class Cuenta
    { 
        public Cuenta(decimal saldo)
        {
            Saldo = saldo;
            //Movimientos = new List<Movimiento>();
        }
        
        public List<Movimiento> Movimientos { get; set; }
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public decimal Saldo { get; set; }


        public abstract string Consignar(decimal valor);
        public abstract string Retirar(decimal valor);
        public Movimiento RegistrarMovimientos(decimal valor, string tipo, decimal cupo = 0)
        {
            Movimiento movimiento = new Movimiento();
            movimiento.Tipo = tipo;
            movimiento.Fecha = DateTime.Now;
            movimiento.Cupo = cupo;
            movimiento.Saldo = Saldo;
            movimiento.Valor = valor;
            Movimientos.Add(movimiento);
            return movimiento;
        }
        public string VisualizarMovimiento()
        {
            string listaCuenta="";
            foreach (var item in Movimientos)
            {
                listaCuenta += ("fecha de movimiento:"+item.Fecha+" --tipo de movimiento: "+item.Tipo+" --con valor: "+item.Valor+" -- y el saldo quedo en: "+ item.Saldo +" \n");
                
            }
            return listaCuenta;
        }


        public abstract string ConsultarSaldo();



        public void ConsultarMovimiento()
        {

        }
    }
    }

