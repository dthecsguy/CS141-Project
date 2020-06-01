using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Runtime.Intrinsics;
using System.Reflection.Metadata.Ecma335;

namespace CS141
{
	//Node class 
	class Node
	{
		public string Name { get; set; }
		public int ID { get; set; }
		public List<Edge> outPaths = new List<Edge>();
		public List<Edge> inPaths = new List<Edge>();
		public int dist = 10000;
		public Node() { }
		public Node(int id, string name)
		{
			Name = name;
			ID = id;
		}
	}

	//Edge Class
	class Edge
	{
		public Node[] Nodes = new Node[2];
		public int weight { get; set; }

		public Edge(Node v1, Node v2, int dist)
		{
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
	class Graph
	{
		private List<string> connections = new List<string>();
		private List<Node> allNodes = new List<Node>();
		public void addNode(Node v) { allNodes.Add(v); }

		//makes relationships into graph
		public int collect(string file)
		{
			StreamReader F = new StreamReader(file);
			string line;

			F.ReadLine();

			while ((line = F.ReadLine()) != null)
			{
				connections.Add(line);
			}
			F.Close();
			return connections.Count;
		}

		//displays infformation ... changes often
		public void display()
		{
			/*foreach (string line in connections)
			{
				Console.WriteLine(line);
			}*/
			foreach (Node v in allNodes)
			{
				Console.WriteLine(v.Name + " : " + v.dist);
			}
		}

		//gets other ode on edge
		private Node other_node(Node v, Edge e)
        {
			return e.Nodes[0] == v ? e.Nodes[1] : e.Nodes[0];
        }

		//finds minimum distance
		public int findMinD(List<Node> V)
        {
			if (V.Count == 1)
				return 0;

			int lowest = int.MaxValue;
			int lowestI = int.MaxValue;

			for (int i = 0; i < V.Count; i++)
            {
				if (V[i].dist <= lowest)
                {
					lowest = V[i].dist;
					lowestI = i;
                }
            }

			return lowestI;
        }

		//Dijkstra
		public void calcShortestPath(string start) 
        {
			List<Node> done = new List<Node>(), temp = allNodes;
			Node v1 = new Node();
			Node v2 = new Node();
			Node v3 = new Node();

			Console.WriteLine("start: " + start);

			foreach (Node v in temp)
            {
				if (v.Name == start)
				{
					v.dist = 0;
					break;
				}
				else
					Console.WriteLine("Nothing was found");
            }

			while(temp.Count != 0)
            {

				done.Add(temp[findMinD(temp)]);
				temp.Remove(temp[findMinD(temp)]);

				foreach (Edge e in done[done.Count-1].outPaths)
                {
					foreach (Node vert in temp)
						if (vert.ID == other_node(done[done.Count - 1], e).ID)
							v3 = vert;

					if (v3.dist > done[done.Count - 1].dist + e.weight)
						v3.dist = done[done.Count - 1].dist + e.weight;
				}
            }

			allNodes = done;
		}
		
		//changes strings to relationships, look at .txt file for format
		public void parse()
		{
			StringBuilder buffer = new StringBuilder();
			List<int> discovered = new List<int>();

			foreach (string rep in connections)
			{
				List<string> info = new List<string>();

				if (allNodes.Count == 0)
				{
					foreach (char c in rep)
					{
						if (c != ',' && c.ToString() != Environment.NewLine)
							buffer.Append(c);
						else
						{
							info.Add(buffer.ToString());
							buffer.Clear();
						}
					}

					info.Add(buffer.ToString());
					buffer.Clear();

					int temp;

					int.TryParse(info[0], out temp);
					int tempID = temp;

					int.TryParse(info[2], out temp);
					int tempV2 = temp;

					int.TryParse(info[3], out temp);
					int tempDist = temp;

					Node v1 = new Node(tempID, info[1]);
					Node v2 = new Node();
					v2.ID = tempV2;
					Edge e = new Edge(v1, v2, tempDist);
					v1.outPaths.Add(e);
					allNodes.Add(v1);
					discovered.Add(v1.ID);
				}
				else
				{
					foreach (char c in rep)
					{
						if (c != ',' && c.ToString() != Environment.NewLine)
							buffer.Append(c);
						else
						{
							info.Add(buffer.ToString());
							buffer.Clear();
						}
					}

					info.Add(buffer.ToString());
					buffer.Clear();

					int temp;

					int.TryParse(info[0], out temp);
					int tempID = temp;

					int.TryParse(info[2], out temp);
					int tempV2 = temp;

					int.TryParse(info[3], out temp);
					int tempDist = temp;

					if (discovered.Contains(tempID))
					{
						foreach (Node n in allNodes)
						{
							if (n.ID == tempID)
							{
								Node v2 = new Node();
								v2.ID = tempV2;
								Edge e = new Edge(n, v2, tempDist);
								n.outPaths.Add(e);
							}
						}
					}
					else
					{
						Node v1 = new Node(tempID, info[1]);
						Node v2 = new Node();
						v2.ID = tempV2;
						Edge e = new Edge(v1, v2, tempDist);
						v1.outPaths.Add(e);
						allNodes.Add(v1);
						discovered.Add(v1.ID);
					}

				}
			}
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("starting");

			/*Node V1 = new Node();
			V1.Name = "Downtown";
			
			Node V2 = new Node();
			V2.Name = "Port";
			
			Edge E1 = new Edge(V1, V2, 14);*/

			Graph map = new Graph();
			map.collect("connections.txt");
			map.parse();
			map.calcShortestPath("Promenade West");
			map.display();


			/*Console.WriteLine(E1.Nodes[0].Name);
			Console.WriteLine(E1.Nodes[1].Name);
			Console.WriteLine(E1.Dist);*/
			Console.ReadKey();
		}
	}
}