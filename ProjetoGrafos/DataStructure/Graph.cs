using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGrafos.DataStructure
{
    /// <summary>
    /// Classe que representa um grafo.
    /// </summary>
    public class Graph
    {

        #region Atributos

        /// <summary>
        /// Lista de nós que compõe o grafo.
        /// </summary>
        private List<Node> nodes;

        #endregion

        #region Propridades

        /// <summary>
        /// Mostra todos os nós do grafo.
        /// </summary>
        public Node[] Nodes
        {
            get { return this.nodes.ToArray(); }
        }

        #endregion

        #region Construtores

        /// <summary>
        /// Cria nova instância do grafo.
        /// </summary>
        public Graph()
        {
            this.nodes = new List<Node>();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Encontra o nó através do seu nome.
        /// </summary>
        /// <param name="name">O nome do nó.</param>
        /// <returns>O nó encontrado ou nulo caso não encontre nada.</returns>
        private Node Find(string name)
        {
            foreach (Node n in nodes)
            {
                if (n.Name == name)
                    return n;
            }
            return null;
        }

        /// <summary>
        /// Adiciona um nó ao grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser adicionado.</param>
        /// <param name="info">A informação a ser armazenada no nó.</param>
        public void AddNode(string name)
        {
            if (this.Find(name) != null)
                return;

            Node n = new Node();
            n.Name = name;

            nodes.Add(n);
        }

        /// <summary>
        /// Adiciona um nó ao grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser adicionado.</param>
        /// <param name="info">A informação a ser armazenada no nó.</param>
        public void AddNode(string name, object info)
        {
            if (Find(name) != null)
                return;

            Node n = new Node();
            n.Name = name;
            n.Info = info;

            nodes.Add(n);
        }

        /// <summary>
        /// Remove um nó do grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser removido.</param>
        public void RemoveNode(string name)
        {
            Node toRemove = Find(name);

            if (toRemove == null)
                return;

            foreach(Node n in nodes)
            {
                n.Edges.RemoveAll(ed => ed.To == toRemove);
            }

            nodes.Remove(toRemove);
        }

        /// <summary>
        /// Adiciona o arco entre dois nós associando determinado custo.
        /// </summary>
        /// <param name="from">O nó de origem.</param>
        /// <param name="to">O nó de destino.</param>
        /// <param name="cost">O cust associado.</param>
        public void AddEdge(string from, string to, double cost)
        {
            Node nodeFrom = this.Find(from);
            Node nodeTo = this.Find(to);

            if (nodeFrom == null || nodeTo == null)
                return;
            
            nodeFrom.AddEdge(nodeTo, cost);
        }

        /// <summary>
        /// Obtem todos os nós vizinhos de determinado nó.
        /// </summary>
        /// <param name="node">O nó origem.</param>
        /// <returns></returns>
        public Node[] GetNeighbours(string from)
        {
            Node n = Find(from);

            if (n == null)
                return null;

            int length = n.Edges.Count;

            Node[] nodes = new Node[length];

            int i = 0;
            foreach(Edge ed in n.Edges)
            {
                Node dest = ed.To;
                nodes[i++] = dest;
            }

            return nodes.Length > 0 ? nodes : null;
            
        }

        public Edge[] GetHamiltonPath()
        {
            foreach (Node node in Nodes)
            {
                Stack<Edge> pilha = new Stack<Edge>();
                Stack<Edge> path = new Stack<Edge>();
                node.Visited = true;
                foreach (Edge e in node.Edges)
                {
                    pilha.Push(e);
                }
                while (pilha.Count > 0)
                {
                    Edge e = pilha.Pop();
                    e.To.Visited = true;

                    Edge lastInPath = path.Count > 0 ? path.Peek() : null;
                    while(lastInPath != null && lastInPath.To != e.From)
                    {
                        lastInPath.To.Visited = false;
                        lastInPath = path.Count > 0 ? path.Pop() : null;
                    }

                    path.Push(e);
                    if (path.Count == nodes.Count - 1)
                    {
                        return path.ToArray();
                    }
                    List<Edge> notVisited = e.To.Edges.FindAll(ed => !ed.To.Visited);
                    if (notVisited.Count == 0)
                    {
                        path.Pop();
                        e.To.Visited = false;
                    }
                    foreach (Edge edge in notVisited)
                    {
                        pilha.Push(edge);
                    }
                }
                foreach (Node n in Nodes)
                {
                    n.Visited = false;
                }
                List<Node> all = nodes.FindAll(n => n.Visited);
                int a = all.Count;
            }
            return null;
        }

        /// <summary>
        /// Valida um caminho, retornando a lista de nós pelos quais ele passou.
        /// </summary>
        /// <param name="nodes">A lista de nós por onde passou.</param>
        /// <param name="path">O nome de cada nó na ordem que devem ser encontrados.</param>
        /// <returns></returns>
        public bool IsValidPath(ref Node[] nodes, params string[] path)
        {
            return false;
        }

        #endregion

    }
}
