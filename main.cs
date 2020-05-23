using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace CS141
{
	//Node class 
	class Node
	{
		public string Name { get; set; }
		public int ID { get; set; }
		public List<Edge> outPaths = new List<Edge>();
		public List<Edge> inPaths = new List<Edge>();
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
		public void display()
		{
			foreach (string line in connections)
			{
				Console.WriteLine(line);
			}
			foreach (Node v in allNodes)
			{
				Console.WriteLine(v.Name);
			}
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
			map.display();


			/*Console.WriteLine(E1.Nodes[0].Name);
			Console.WriteLine(E1.Nodes[1].Name);
			Console.WriteLine(E1.Dist);*/
			Console.ReadKey();
		}
	}
}