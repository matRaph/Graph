using System;
using System.Numerics;
using System.Collections.Generic;
namespace Graph
{
    public class Graph
    {
        //Matriz de adjacencia
        List<List<List<Edge>>> Matrix { get; set; }
        //Lista de vertices
        internal List<Vertex> Vertices { get; set; }

        //Construtor
        public Graph()
        {
            Matrix = new List<List<List<Edge>>>(); //new List<Edge>[0, 0]; 
            Vertices = new List<Vertex>();
        }

        //Adiciona um vertice
        internal void AddVertex(object label)
        {
            Vertices.Add(new Vertex(label));
            //Aumenta a matriz
            for (int i = 0; i < Matrix.Count; i++)
            {
                Matrix[i].Add(new List<Edge>());
            }
            Matrix.Add(new List<List<Edge>>());
            for (int i = 0; i < Matrix.Count; i++)
            {
                Matrix[Matrix.Count - 1].Add(new List<Edge>());
            }
        }

        //Adiciona uma aresta
        internal void AddEdge(object labelVertex1, object labelVertex2, object edge)
        {
            int indexVertex1 = FindVertice(labelVertex1);
            int indexVertex2 = FindVertice(labelVertex2);

            //Verifica se os vertices existem
            if (indexVertex1 == -1 || indexVertex2 == -1)
            {
                throw new Exception("Vertice nao encontrado");
            }

            //Adiciona a aresta na matriz
            Matrix[indexVertex1][indexVertex2].Add(new Edge(edge));

        }

        //Encontra um vertice
        private int FindVertice(object label){
            for (int i = 0; i < Vertices.Count; i++)
            {
                if (Vertices[i].Label.Equals(label))
                {
                    return i;
                }
            }
            return -1;
        }

        // public void PrintMatrix()
        // {
        //     for (int i = 0; i < Matrix.GetLength(0); i++)
        //     {
        //         for (int j = 0; j < Matrix.GetLength(1); j++)
        //         {
        //             Console.Write(Matrix[i, j] + " ");
        //         }
        //         Console.WriteLine();
        //     }
        // }
    }
}