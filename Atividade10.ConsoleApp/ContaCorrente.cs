using System;

namespace Atividade10.ConsoleApp
{
    public class ContaCorrente
    {
        #region Atrbutos
        public int numeroDaConta;
        public decimal saldo;
        public string status;
        public decimal limite;
        public Movimentacao[] movimentacoes = new Movimentacao[100];
        #endregion

        #region Métodos
        public void Sacar(decimal valor)
        {
            if (DecrementarSaldo(valor) == true)
            {
                Movimentacao movimentacaoAtual = new Movimentacao();

                movimentacaoAtual.valor = valor;

                movimentacaoAtual.tipo = "debito";

                AdicionarMovimentacao(movimentacaoAtual);

                ApresentarMensagem("\nO valor de R$: " + valor.ToString("F2") + " foi sacado com sucesso da conta " + numeroDaConta + "!!", ConsoleColor.Green);
            }
            else
            {
                ApresentarMensagem("\nNão foi possível efetuar o saque!\nSaldo insuficiente!!", ConsoleColor.Red);
            }
        }

        private void IncrementarSaldo(decimal valor)
        {
            saldo += valor;
        }

        private bool DecrementarSaldo(decimal valor)
        {
            if ((saldo + limite) > valor)
            {
                saldo -= valor;

                return true;
            }
            else
            {
                return false;
            }
        }

        public void Depositar(decimal valor)
        {
            IncrementarSaldo(valor);

            Movimentacao movimentacaoAtual = new Movimentacao();

            movimentacaoAtual.valor = valor;

            movimentacaoAtual.tipo = "credito";

            AdicionarMovimentacao(movimentacaoAtual);

            ApresentarMensagem("\nO valor de R$: " + valor.ToString("F2") + " foi depositado com sucesso na conta " + numeroDaConta + "!!", ConsoleColor.Green);
        }

        public void Transferir(decimal valor, ContaCorrente contaDeDestino)
        {
            if (DecrementarSaldo(valor) == true)
            {
                contaDeDestino.IncrementarSaldo(valor);

                Movimentacao movimentacaoAtualEmitente = new Movimentacao();

                movimentacaoAtualEmitente.valor = valor;

                movimentacaoAtualEmitente.tipo = "transferenciaEfetuada";

                movimentacaoAtualEmitente.contaDestino = contaDeDestino.numeroDaConta;

                AdicionarMovimentacao(movimentacaoAtualEmitente);

                Movimentacao movimentacaoAtualDestino = new Movimentacao();

                movimentacaoAtualDestino.valor = valor;

                movimentacaoAtualDestino.tipo = "transferenciaRecebida";

                movimentacaoAtualDestino.contaOrigem = numeroDaConta;

                contaDeDestino.AdicionarMovimentacao(movimentacaoAtualDestino);

                ApresentarMensagem("\nTransferência de R$ " + valor.ToString("F2") + " efetuada com suceso para a conta " + contaDeDestino.numeroDaConta + "!!", ConsoleColor.Green);
            }
            else
            {
                ApresentarMensagem("\nTransferência não efetuada saldo insuficiente!!", ConsoleColor.Red);
            }
        }

        public void EmitirSaldo()
        {
            ConsoleColor cor;

            if (saldo < 0)
                cor = ConsoleColor.Red;
            else
                cor = ConsoleColor.Green;

            ApresentarMensagem(("\nEmissão de saldo\nAtual: " + saldo.ToString("F2")), cor);
        }

        public void EmitirExtrato()
        {
            Console.WriteLine("\n=== EXTRATO DA CONTA ===\n");

            ApresentarDadosDaConta();

            if (movimentacoes[0] == null)
            {
                ApresentarMensagem("\nEsta conta não possui movimentações!!", ConsoleColor.Yellow);
            }
            else
            {
                for (int i = 0; i < movimentacoes.Length; i++)
                {
                    if (movimentacoes[i] == null)
                    {
                        break;
                    }
                    else
                    {
                        switch (movimentacoes[i].tipo)
                        {
                            case "debito":
                                ApresentarMensagem(("\nSaque realizado da conta " + numeroDaConta + " no seguinte valor: " + movimentacoes[i].valor.ToString("F2")), ConsoleColor.Red);
                                break;
                            case "credito":
                                ApresentarMensagem(("\nDepósito realizado na conta " + numeroDaConta + " no seguinte valor: " + movimentacoes[i].valor.ToString("F2")), ConsoleColor.Green);
                                break;
                            case "transferenciaEfetuada":
                                ApresentarMensagem(("\nTransferência efetuada da conta " + numeroDaConta + " no valor de R$: " + movimentacoes[i].valor.ToString("F2") + " para a conta " + movimentacoes[i].contaDestino), ConsoleColor.Yellow);
                                break;
                            case "transferenciaRecebida":
                                ApresentarMensagem(("\nTransferência recebida da conta " + movimentacoes[i].contaOrigem + " no valor de R$ " + movimentacoes[i].valor.ToString("F2") + "."), ConsoleColor.Yellow);
                                break;
                        }
                    }
                }
            }
        }

        public void AdicionarMovimentacao(Movimentacao movimentacaoExecutada)
        {
            for (int i = 0; i < movimentacoes.Length; i++)
            {
                if (movimentacoes[i] == null)
                {
                    movimentacoes[i] = movimentacaoExecutada;

                    break;
                }
            }
        }

        public void ApresentarMensagem(string mensagem, ConsoleColor cor)
        {
            Console.ForegroundColor = cor;

            Console.WriteLine(mensagem);

            Console.ResetColor();
        }

        public void ApresentarDadosDaConta()
        {
            Console.WriteLine("\nNúmero da conta: {0}", numeroDaConta);
            Console.WriteLine("\nSaldo atual da conta: R$ {0}", saldo.ToString("F2"));
            Console.WriteLine("\nStatus da conta: {0}", status);
            Console.WriteLine("\nLimite da conta: R$ {0}", limite.ToString("F2"));
            Console.WriteLine();
        }
        #endregion
    }
}