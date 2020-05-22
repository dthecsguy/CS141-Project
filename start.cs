using System;
using System.Collections.Generic;
using System.IO;

namespace CS141{
	
	//Node class 
	class Node{
		public string Name {get; set;}
		public int ID {get; set;}
		public List<Edge> outPaths = new List<Edge>();
		public List<Edge> inPaths = new List<Edge>();
	}
	
	//Edge Class
	class Edge{
		public Node[] Nodes = new Node[2];
		public int weight {get; set;}
		
		public Edge(Node v1, Node v2, int dist){
			Nodes[0] = v1;
			Nodes[1] = v2;
			weight = dist;
		}
	}
	
	/*To make dealing with edges easier
	class EdgePair{
		public Edge[] pair = new Edge[2];
		public Node[] Nodes = new Node[2];
		public EdgePair(Edge e1, Edge e2){
			pair[0] = e1; pair[1] = e2;
			Nodes[0] = e1.Nodes[0]; Nodes[0] = e1.Nodes[0];
		}
	}*/
	
	//Implementation of the map
	class Graph{
		private List<string> connections = new List<string>();
		private List<Edge> allEdges = new List<Edge>();
		private List<Node> allNodes = new List<Node>();
		public void addNode(Node v){allNodes.Add(v);}
		public void addEdge(Edge e){allEdges.Add(e);}
		public int collect(string file){
			StreamReader F = new StreamReader(file);
			string line;
			
			while ((line = F.ReadLine()) != null){
				connections.Add(line);
			}
			F.Close();
			return connections.Count;
		}
		public void display(){
			foreach (string line in connections){
				Console.WriteLine(line);
			}
		}
	}
	
	class Program{
		static void Main(string[] args){
			Console.WriteLine("starting");
			
			/*Node V1 = new Node();
			V1.Name = "Downtown";
			
			Node V2 = new Node();
			V2.Name = "Port";
			
			Edge E1 = new Edge(V1, V2, 14);*/
			
			Graph map = new Graph();
			map.collect("connections.txt");
			map.display();
			
			
			/*Console.WriteLine(E1.Nodes[0].Name);
			Console.WriteLine(E1.Nodes[1].Name);
			Console.WriteLine(E1.Dist);*/
			Console.ReadKey();
		}
	}
}