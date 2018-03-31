using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace WhatsMyLogic
{
    [Serializable]
    public class Graph
    {
        [XmlElement(ElementName = "id")]
        public string id;
        //read https://msdn.microsoft.com/ru-ru/library/system.xml.serialization.xmlelementattribute(v=vs.110).aspx
        [XmlElement(ElementName ="Node")]
        public Node[] nodes;
       [XmlElement(ElementName="Edge")]
      public  Edge[] edges;

        public Graph(string id, Node[] nodes, Edge[] edges)
        {
            this.id = id;
            this.nodes = nodes;
            this.edges = edges;
        }

        public Graph(Node[] nodes, Edge[] edges)
        {
            this.nodes = nodes;
            this.edges = edges;
        }
        public Graph() { }
    }
    public class GraphList
    {
        List<GraphItem> Items;// = new List<GraphItem>();

        public GraphList(List<GraphItem> items)
        {
            Items = items;
        }
    }
    public class GraphItem
    {
        public string name;
        public int edgesCount;

        public GraphItem(string name, int edgesCount)
        {
            this.name = name;
            this.edgesCount = edgesCount;
        }
    }
    public class Node
    {
        [XmlAttribute]
        public string id;
        [XmlAttribute]
        public string value;

        public Node(string id, string value)
        {
            this.id = id;
            this.value = value;
        }
        public Node() { }
    }
    [Serializable]
    public class Edge
    {
        [XmlAttribute]
        public string id;
        [XmlAttribute]
       public bool directed;
       [XmlAttribute]
       public string source;
       [XmlAttribute]
       public string target;
        public void Reverce() { var t = source; source = target; target = t; }

        public Edge(string id, bool directed, string source, string target)
        {
            this.id = id;
            this.directed = directed;
            this.source = source;
            this.target = target;
        }
        public Edge() { }
    }
}
