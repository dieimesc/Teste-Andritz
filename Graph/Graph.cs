using Graph;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graph
{
    public interface IGraph<T>
    {
        IEnumerable<T> RoutesBetween(T source, T target);
    }
   

    public class Graph<T> : IGraph<T>
    {
        private IEnumerable<ILink<T>> links;

        public Graph(IEnumerable<ILink<T>> links)
        {
            this.links = links;
        }

        public IEnumerable<T> RoutesBetween(T source, T target)
        {

            
            List<Node> nodes = new List<Node>();
            List<string> points = new List<string>();
            List<List<string>> routesBetween = new List<List<string>>();

            List<T> rotas = new List<T>();
         
                     
            string caminho;
            foreach (ILink<T> l in links)
            {
                if (!points.Contains(l.Source.ToString()))
                {
                    points.Add(l.Source.ToString());
                                       
                }
                if (!points.Contains(l.Target.ToString()))
                {
                    points.Add(l.Target.ToString());
                                     
                }
                #region desativado

                int indexNode = nodes.FindIndex(p => p.ToString().Equals(l.Source.ToString()));
                if (indexNode < 0)
                    nodes.Add(new Node(l.Source.ToString(), l.Target.ToString()));
                                
                indexNode = nodes.FindIndex(p => p.ToString().Equals(l.Target.ToString()));
                if (indexNode < 0)
                   nodes.Add(new Node(l.Target.ToString(), l.Source.ToString()));
                                     

                
                #endregion

            }

            //Cria as conexões
            foreach(Node node in nodes)
            {
                List<ILink<T>> listaLinks = links.Where(p => p.Target.Equals(node.ToString()) || p.Source.Equals(node.ToString())).ToList();
                foreach(ILink<T> l in listaLinks)
                {
                    if (node.Conexoes.FindIndex(p => p.ToString().Equals(l.Source)) < 0 && !l.Source.Equals(node.ToString()))
                        node.Conexoes.Add(new Node(l.Source.ToString()));

                    if (node.Conexoes.FindIndex(p => p.ToString().Equals(l.Target)) < 0 && !l.Target.Equals(node.ToString()))
                        node.Conexoes.Add(new Node(l.Target.ToString()));


                }
                
            }
            //Cria as rotas

            routesBetween = getCaminhos(source.ToString(), target.ToString(), nodes);


            string str = "";

        

            //caminho = getCaminhos(source.ToString(), target.ToString());



            return (IEnumerable<T>)nodes.AsEnumerable();


        }
        protected List<List<string>> getCaminhos(string source, string target, List<Node> nodes)
        {
            List<List<string>> paths = new List<List<string>>();
            List<List<string>> rotas = new List<List<string>>();
            Node node = nodes.Where(p => p.ToString().Equals(source)).ToList()[0];
            if (node != null)
            {
                foreach (Node conexao in node.Conexoes)
                {
                    paths.Add(new List<string>());
                    paths[node.Conexoes.IndexOf(conexao)].Add(conexao.ToString());

                    Node n1 = nodes.First(p => p.ToString().Equals(conexao.ToString()));
                    
                    foreach (Node n in n1.Conexoes)
                    {
                        paths[node.Conexoes.IndexOf(conexao)].Add(n.ToString());
                    }
                    foreach(List<string> path in paths)
                    {
                        if (path.Contains(target))
                        {
                            rotas.Add(path);
                        }
                    }
                }

            }

            return rotas;

        }
        protected List<string> getNodes(List<string> points)
        {

            string path = "";
            List<string> paths = new List<string>();

            foreach (string point in points)
            {
                path = point;

                foreach (ILink<T> l in this.links)
                {

                    if (point.ToString().Contains(l.Source.ToString()))
                    {
                        if (!path.ToString().Contains(l.Target.ToString()))
                        {
                            path += "-" + l.Target.ToString();

                        }
                    }
                    else if (point.ToString().Contains(l.Target.ToString()))
                    {
                        if (!path.ToString().Contains(l.Source.ToString()))
                        {
                            path += "-" + l.Source.ToString();

                        }
                    }

                }
                paths.Add(path);
            }

            return paths;

        }


    }

    public class Node
    {
        string node;
        List<List<Node>> rotas = new List<List<Node>>();
        List<Node> conexoes = new List<Node>();

        public List<Node> Conexoes
        {
            get { return conexoes; }
            set { conexoes = value; }
        }
        public Node(string node)
        {
            this.node = node;

        }

        public Node(string node, List<Node> nodes)
        {
            this.node = node;
            this.conexoes = nodes;

        }
        public Node(string node, string node1)
        {
            this.node = node;
            this.conexoes.Add(new Node(node1));

        }
        public Node(string node, string node1, string node2)
        {
            this.node = node;
            this.conexoes.Add(new Node(node1));
            this.conexoes.Add(new Node(node2));

        }
        public List<string> GetConexoes()
        {
            return conexoes.Select(p => p.ToString()).ToList();

        }

        public List<List<Node>> Rotas
        {
            get { return rotas; }
            set { rotas = value; }
        }
        public override string ToString()
        {
            return node.ToString();

        }


    }
}

