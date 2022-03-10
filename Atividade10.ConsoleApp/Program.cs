using System;

namespace Atividade10.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region Criando as conta e inicializando as mesmas
            ContaCorrente conta1 = new ContaCorrente();
            conta1.numeroDaConta = 1234;
            conta1.saldo = 500;
            conta1.status = "comum";
            conta1.limite = 200;

            ContaCorrente conta2 = new ContaCorrente();
            conta2.numeroDaConta = 1235;
            conta2.saldo = 1000;
            conta2.status = "especial";
            conta2.limite = 300;
            #endregion

            #region Realizando as operações de teste
            //realizando operações com a conta 1
            conta1.EmitirExtrato();

            conta1.EmitirSaldo();

            conta1.Depositar(15);

            conta1.EmitirSaldo();

            //Saque inválido
            conta1.Sacar(18000);

            //Saque válido
            conta1.Sacar(15);

            conta1.EmitirExtrato();

            Console.WriteLine("\n=========================");

            //realizando operações com a conta 2
            conta2.EmitirExtrato();

            conta2.EmitirSaldo();

            conta2.Depositar(15);

            conta2.EmitirSaldo();

            //Saque inválido
            conta2.Sacar(18000);

            //Saque válido
            conta2.Sacar(15);

            conta2.EmitirExtrato();

            //realizando operações transfer~encias entre as contas
            Console.WriteLine("\n=========================");

            //valor excedido
            conta1.Transferir(15000, conta2);

            //valor válido
            conta1.Transferir(15, conta2);

            //Vendo a transferência na perspectiva da conta1
            conta1.EmitirExtrato();

            //Vendo a transferência na perspectiva da conta2
            conta2.EmitirExtrato();

            //valor excedido
            conta2.Transferir(15000, conta1);

            //valor válido
            conta2.Transferir(15, conta1);

            //Vendo a transferência na perspectiva da conta2
            conta2.EmitirExtrato();

            //Vendo a transferência na perspectiva da conta1
            conta1.EmitirExtrato();
            #endregion
        }
    }
}
