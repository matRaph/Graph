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

        //Adiciona uma aresta direcional
        public void AddDirectEdge(object initial, object final, object edge)
        {
            int indexVertex1 = FindVertex(initial);
            int indexVertex2 = FindVertex(final);
            //Adiciona a aresta na matriz
            Matrix[indexVertex1][indexVertex2].Add(new Edge(edge));

        }

        //Adiciona uma aresta não direcional
        public void AddNonDirectEdge(object initial, object final, object edge){
            int indexVertex1 = FindVertex(initial);
            int indexVertex2 = FindVertex(final);
            //Adiciona as arestas na matriz
            Matrix[indexVertex1][indexVertex2].Add(new Edge(edge));
            Matrix[indexVertex2][indexVertex1].Add(new Edge(edge));
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
        //Remove um vertice
        public void RemoveVertex(object label){
            int index = FindVertex(label);

            Vertices.RemoveAt(index);

            Matrix.RemoveAt(index);

            for (int i = 0; i < Matrix.Count; i++)
            {
                Matrix[i].RemoveAt(index);
            }
        }

        //Remove uma aresta
        public void RemoveEdge(object initial, object final, object edge){
            int from = FindVertex(initial);
            int to = FindVertex(final);

            Matrix[from][to].Remove(Matrix[from][to].Find(x => x.Value.Equals(edge)));
        }

        //Retorna uma lista com todas as arestas
        public List<Edge> Edges(){
            List<Edge> edges = new List<Edge>();
            for (int i = 0; i < Matrix.Count; i++)
            {
                for (int j = 0; j < Matrix[i].Count; j++)
                {
                    for (int k = 0; k < Matrix[i][j].Count; k++)
                    {
                        edges.Add(Matrix[i][j][k]);
                    }
                }
            }
            return edges;
        }
        
        //Retorna um arry com os vértices que compõem a aresta
        internal Vertex[] FinalVertices(Edge edge){
            for (int i = 0; i < Matrix.Count; i++)
            {
                for (int j = 0; j < Matrix[i].Count; j++)
                {
                    for (int k = 0; k < Matrix[i][j].Count; k++)
                    {
                        if (Matrix[i][j][k].Equals(edge))
                        {
                            return new Vertex[]{Vertices[i], Vertices[j]};
                        }
                    }
                }
            }
            throw new Exception("Aresta nao encontrada");
        }


        //Retorna o vértice oposto ao vertex na aresta edge
        internal Vertex Oposite(Vertex vertex, Edge edge){
            Vertex[] vertices = FinalVertices(edge);
            if (vertices[0].Equals(vertex))
            {
                return vertices[1];
            }
            else if (vertices[1].Equals(vertex))
            {
                return vertices[0];
            }
            else
            {
                throw new Exception("Não é incidente a V");
            }
        }

        //Retorna se o os vertices são adjacentes
        public bool AreAdjacent(object initial, object final){
            int from = FindVertex(initial);
            int to = FindVertex(final);

            return Matrix[from][to].Count > 0 || Matrix[to][from].Count > 0;
        }

        //Retorna uma lista com todas as arestas chegando ao vértice
        public List<Edge> EdgesComing(object label){
            List<Edge> edges = new List<Edge>();
            int index = FindVertex(label);           

            for (int i = 0; i < Matrix.Count; i++)
            {
                for (int j = 0; j < Matrix[i][index].Count; j++)
                {
                    edges.Add(Matrix[i][index][j]);
                }
            }

            return edges;
        }

        //Retorna se a aresta é direcionada
        public bool IsDirected(Edge edge){
            Vertex[] vertices = FinalVertices(edge);
            int from = FindVertex(vertices[0].Label);
            int to = FindVertex(vertices[1].Label);

            bool HasComing = Matrix[to][from].Exists(x => x.Value.Equals(edge.Value));
            bool HasGoing = Matrix[from][to].Exists(x => x.Value.Equals(edge.Value));

            return HasComing && HasGoing;
        }
    }
}