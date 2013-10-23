using System;
using System.Collections.Generic;
using DataStructure.Common;
using DataStructure.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructureTest
{
    [TestClass]
    public class GraphTest
    {
        [TestMethod]
        public void CreateGraph()
        {
            var graph = new GraphAdjMatrix<string>(5);
            var vertex = new List<Node<string>>
                {
                    new Node<string>("A"),
                    new Node<string>("B"),
                    new Node<string>("C"),
                    new Node<string>("D"),
                    new Node<string>("E"),
                };
            graph.Nodes.AddRange(vertex);

            graph.SetEdge("A", "B", 60);
            graph.SetEdge("A", "C", 100);
            graph.SetEdge("A", "D", 20);
            graph.SetEdge("B", "C", 80);
            graph.SetEdge("B", "D", 95);
            graph.SetEdge("C", "E", 70);
            graph.SetEdge("D", "E", 10);

            graph.Print();
        }

    }
}
