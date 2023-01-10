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
        public void AddVertex(object label)
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
        public void AddEdge(object initial, object final, object edge)
        {
            int indexVertex1 = FindVertex(initial);
            int indexVertex2 = FindVertex(final);
            //Adiciona a aresta na matriz
            Matrix[indexVertex1][indexVertex2].Add(new Edge(edge));

        }

        //Encontra um vertice
        private int FindVertex(object label){
            for (int i = 0; i < Vertices.Count; i++)
            {
                if (Vertices[i].Label.Equals(label))
                {
                    return i;
                }
            }
            throw new Exception("Vertice nao encontrado");
        }
        public void RemoveVertex(object label){
            int index = FindVertex(label);

            Vertices.RemoveAt(index);

            Matrix.RemoveAt(index);

            for (int i = 0; i < Matrix.Count; i++)
            {
                Matrix[i].RemoveAt(index);
            }
        }

        public void RemoveEdge(object initial, object final, object edge){
            int from = FindVertex(initial);
            int to = FindVertex(final);

            Matrix[from][to].Remove(Matrix[from][to].Find(x => x.Value.Equals(edge)));
        }

    }
}