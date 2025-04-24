using System;
using System.Collections.Generic;

namespace EstruturasDeDados
{
    class Program
    {
        static void Main(string[] args)
        {
            // Lista
            List<int> lista = new List<int> { 1, 2, 3 };
            Console.WriteLine("Lista:");
            lista.ForEach(item => Console.WriteLine(item));

            // Pilha
            Stack<int> pilha = new Stack<int>();
            pilha.Push(1);
            pilha.Push(2);
            pilha.Push(3);
            Console.WriteLine("\nPilha:");
            while (pilha.Count > 0)
            {
                Console.WriteLine(pilha.Pop());
            }

            // Fila
            Queue<int> fila = new Queue<int>();
            fila.Enqueue(1);
            fila.Enqueue(2);
            fila.Enqueue(3);
            Console.WriteLine("\nFila:");
            while (fila.Count > 0)
            {
                Console.WriteLine(fila.Dequeue());
            }

            // Árvore Binária
            ArvoreBinaria arvore = new ArvoreBinaria();
            arvore.Inserir(2);
            arvore.Inserir(1);
            arvore.Inserir(3);
            Console.WriteLine("\nÁrvore Binária:");
            arvore.EmOrdem(arvore.Raiz);

            // Tabela Hash
            TabelaHash tabelaHash = new TabelaHash();
            tabelaHash.Inserir(1, "Valor 1");
            tabelaHash.Inserir(2, "Valor 2");
            tabelaHash.Inserir(3, "Valor 3");
            Console.WriteLine("\nTabela Hash:");
            Console.WriteLine(tabelaHash.Buscar(1));
            Console.WriteLine(tabelaHash.Buscar(2));
            Console.WriteLine(tabelaHash.Buscar(3));
        }
    }

    class Nodo
    {
        public int Valor;
        public Nodo Esquerda, Direita;

        public Nodo(int valor)
        {
            Valor = valor;
            Esquerda = null;
            Direita = null;
        }
    }

    class ArvoreBinaria
    {
        public Nodo Raiz;

        public void Inserir(int valor)
        {
            if (Raiz == null)
            {
                Raiz = new Nodo(valor);
            }
            else
            {
                InserirNo(Raiz, valor);
            }
        }

        private void InserirNo(Nodo nodo, int valor)
        {
            if (valor < nodo.Valor)
            {
                if (nodo.Esquerda == null)
                {
                    nodo.Esquerda = new Nodo(valor);
                }
                else
                {
                    InserirNo(nodo.Esquerda, valor);
                }
            }
            else
            {
                if (nodo.Direita == null)
                {
                    nodo.Direita = new Nodo(valor);
                }
                else
                {
                    InserirNo(nodo.Direita, valor);
                }
            }
        }

        public void EmOrdem(Nodo nodo)
        {
            if (nodo != null)
            {
                EmOrdem(nodo.Esquerda);
                Console.WriteLine(nodo.Valor);
                EmOrdem(nodo.Direita);
            }
        }
    }

    class TabelaHash
    {
        private Dictionary<int, string> tabela = new Dictionary<int, string>();

        public void Inserir(int chave, string valor)
        {
            tabela[chave] = valor;
        }

        public string Buscar(int chave)
        {
            tabela.TryGetValue(chave, out string valor);
            return valor;
        }
    }
}