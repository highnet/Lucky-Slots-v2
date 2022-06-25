using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PaylinePathGenerator : MonoBehaviour
{

    private Dictionary<int, Vector2> slotsMapping = new Dictionary<int, Vector2>();
    private Graph slotsGraph = new Graph(20);
    private List<string> totalPathStrings = new List<string>();
    private List<List<int>> totalPathsint = new List<List<int>>();
    private List<List<Vector2>> totalPathsv2 = new List<List<Vector2>>();

    public List<List<Vector2>> GetTotalPathsv2()
    {
        return totalPathsv2;
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateSlotsGraph();
        GeneratePaylinePaths();

        foreach (string path in totalPathStrings)
        {
            Debug.Log("str: "+ path);
            Debug.Log("===");

        }

        foreach (string path in totalPathStrings)
        {
            List<int> result = new List<int>();
            string[] vertices = path.Split(" ");
            foreach (string vertex in vertices)
            {
                int num;
                int.TryParse(vertex, out num);
                result.Add(num);
            }
            totalPathsint.Add(result);

        }





        slotsMapping.Add(0, new Vector2(0, 0));
        slotsMapping.Add(1, new Vector2(0, 1));
        slotsMapping.Add(2, new Vector2(0, 2));
        slotsMapping.Add(3, new Vector2(0, 3));
        slotsMapping.Add(4, new Vector2(0, 4));
        slotsMapping.Add(5, new Vector2(1, 0));
        slotsMapping.Add(6, new Vector2(1, 1));
        slotsMapping.Add(7, new Vector2(1, 2));
        slotsMapping.Add(8, new Vector2(1, 3));
        slotsMapping.Add(9, new Vector2(1, 4));
        slotsMapping.Add(10, new Vector2(2, 0));
        slotsMapping.Add(11, new Vector2(2, 1));
        slotsMapping.Add(12, new Vector2(2, 2));
        slotsMapping.Add(13, new Vector2(2, 3));
        slotsMapping.Add(14, new Vector2(2, 4));
        slotsMapping.Add(15, new Vector2(3, 0));
        slotsMapping.Add(16, new Vector2(3, 1));
        slotsMapping.Add(17, new Vector2(3, 2));
        slotsMapping.Add(18, new Vector2(3, 3));
        slotsMapping.Add(19, new Vector2(3, 4));

        foreach (List<int> path in totalPathsint)
        {
            List<Vector2> pathv2 = new List<Vector2>();
            foreach (int vertex in path)
            {
                pathv2.Add(slotsMapping[vertex]);
            }
            totalPathsv2.Add(pathv2);
        }

        Debug.Log(totalPathStrings.Count);
        Debug.Log(totalPathsint.Count);
        Debug.Log(totalPathsv2.Count);

    }

    private void GeneratePaylinePaths()
    {

        GeneratePaths(0, 2);
        GeneratePaths(0, 3);
        GeneratePaths(0, 4);
        GeneratePaths(0, 7);
        GeneratePaths(0, 8);
        GeneratePaths(0, 9);
        GeneratePaths(0, 12);
        GeneratePaths(0, 13);
        GeneratePaths(0, 14);
        GeneratePaths(0, 18);
        GeneratePaths(0, 19);

        GeneratePaths(1, 3);
        GeneratePaths(1, 4);
        GeneratePaths(1, 8);
        GeneratePaths(1, 8);
        GeneratePaths(1, 9);
        GeneratePaths(1, 13);
        GeneratePaths(1, 14);
        GeneratePaths(1, 19);

        GeneratePaths(2, 4);
        GeneratePaths(2, 9);
        GeneratePaths(2, 14);

        GeneratePaths(5, 2);
        GeneratePaths(5, 3);
        GeneratePaths(5, 4);
        GeneratePaths(5, 7);
        GeneratePaths(5, 8);
        GeneratePaths(5, 9);
        GeneratePaths(5, 12);
        GeneratePaths(5, 13);
        GeneratePaths(5, 14);
        GeneratePaths(5, 17);
        GeneratePaths(5, 18);
        GeneratePaths(5, 19);

        GeneratePaths(6, 3);
        GeneratePaths(6, 4);
        GeneratePaths(6, 8);
        GeneratePaths(6, 9);
        GeneratePaths(6, 13);
        GeneratePaths(6, 14);
        GeneratePaths(6, 18);
        GeneratePaths(6, 19);

        GeneratePaths(7, 4);
        GeneratePaths(7, 9);
        GeneratePaths(7, 14);

        GeneratePaths(10, 2);
        GeneratePaths(10, 3);
        GeneratePaths(10, 4);
        GeneratePaths(10, 7);
        GeneratePaths(10, 8);
        GeneratePaths(10, 9);
        GeneratePaths(10, 12);
        GeneratePaths(10, 13);
        GeneratePaths(10, 14);
        GeneratePaths(10, 17);
        GeneratePaths(10, 18);
        GeneratePaths(10, 17);

        GeneratePaths(11, 3);
        GeneratePaths(11, 4);
        GeneratePaths(11, 8);
        GeneratePaths(11, 9);
        GeneratePaths(11, 13);
        GeneratePaths(11, 14);
        GeneratePaths(11, 18);
        GeneratePaths(11, 19);

        GeneratePaths(12, 4);
        GeneratePaths(12, 9);
        GeneratePaths(12, 14);
        GeneratePaths(12, 19);

        GeneratePaths(15, 7);
        GeneratePaths(15, 3);
        GeneratePaths(15, 4);
        GeneratePaths(15, 8);
        GeneratePaths(15, 9);
        GeneratePaths(15, 12);
        GeneratePaths(15, 13);
        GeneratePaths(15, 14);
        GeneratePaths(15, 17);
        GeneratePaths(15, 18);
        GeneratePaths(15, 19);

        GeneratePaths(16, 8);
        GeneratePaths(16, 4);
        GeneratePaths(16, 9);
        GeneratePaths(16, 13);
        GeneratePaths(16, 14);
        GeneratePaths(16, 18);
        GeneratePaths(16, 19);

        GeneratePaths(17, 9);
        GeneratePaths(17, 14);
        GeneratePaths(17, 19);

    }

    private void GenerateSlotsGraph()
    {
        slotsGraph.addEdge(0, 1);
        slotsGraph.addEdge(0, 6);

        slotsGraph.addEdge(1, 2);
        slotsGraph.addEdge(1, 7);

        slotsGraph.addEdge(2, 3);
        slotsGraph.addEdge(2, 8);

        slotsGraph.addEdge(3, 4);
        slotsGraph.addEdge(3, 9);

        slotsGraph.addEdge(5, 1);
        slotsGraph.addEdge(5, 6);
        slotsGraph.addEdge(5, 11);

        slotsGraph.addEdge(6, 2);
        slotsGraph.addEdge(6, 7);
        slotsGraph.addEdge(6, 12);

        slotsGraph.addEdge(7, 3);
        slotsGraph.addEdge(7, 8);
        slotsGraph.addEdge(7, 13);

        slotsGraph.addEdge(8, 4);
        slotsGraph.addEdge(8, 9);
        slotsGraph.addEdge(8, 14);

        slotsGraph.addEdge(10, 6);
        slotsGraph.addEdge(10, 11);
        slotsGraph.addEdge(10, 16);

        slotsGraph.addEdge(11, 7);
        slotsGraph.addEdge(11, 12);
        slotsGraph.addEdge(11, 17);

        slotsGraph.addEdge(12, 8);
        slotsGraph.addEdge(12, 13);
        slotsGraph.addEdge(12, 18);

        slotsGraph.addEdge(13, 9);
        slotsGraph.addEdge(13, 14);
        slotsGraph.addEdge(13, 19);

        slotsGraph.addEdge(15, 11);
        slotsGraph.addEdge(15, 16);

        slotsGraph.addEdge(16, 12);
        slotsGraph.addEdge(16, 17);

        slotsGraph.addEdge(17, 13);
        slotsGraph.addEdge(17, 18);

        slotsGraph.addEdge(18, 14);
        slotsGraph.addEdge(18, 19);
    }

    private void GeneratePaths(int v1, int v2)
    {
        List<string> generatedPathStrings = slotsGraph.dfsGenerateAllPaths(v1, v2);

        foreach (string path in generatedPathStrings)
        {
            if (IsUnique(totalPathStrings, path))
            {
                totalPathStrings.Add(path);

            }
        }


    }

    private bool IsUnique(List<string> generatedPathStrings, string path)
    {
        if (generatedPathStrings.Contains(path))
        {
            return false;
        }
        return true;
    }

    public class Graph
    {

        // No. of vertices in graph
        private int v;

        // adjacency list
        private List<int>[] adjList;

        private List<string> generatedPaths;

        // Constructor
        public Graph(int vertices)
        {

            this.generatedPaths = new List<string>();
            // initialise vertex count
            this.v = vertices;

            // initialise adjacency list
            initAdjList();
        }

        // utility method to initialise
        // adjacency list
        private void initAdjList()
        {
            adjList = new List<int>[v];

            for (int i = 0; i < v; i++)
            {
                adjList[i] = new List<int>();
            }
        }

        // add edge from u to v
        public void addEdge(int u, int v)
        {
            // Add v to u's list.
            adjList[u].Add(v);
        }

        // Prints all paths from
        // 's' to 'd'
        public List<string> dfsGenerateAllPaths(int s, int d)
        {
            bool[] isVisited = new bool[v];
            List<int> pathList = new List<int>();

            // add source to path[]
            pathList.Add(s);

            // Call recursive utility
            dfs(s, d, isVisited, pathList);

            return generatedPaths;
        }

        // A recursive function to print
        // all paths from 'u' to 'd'.
        // isVisited[] keeps track of
        // vertices in current path.
        // localPathList<> stores actual
        // vertices in the current path
        private void dfs(int u, int d,
                                       bool[] isVisited,
                                       List<int> localPathList)
        {

            if (u.Equals(d))
            {
                string resultingPath = string.Join(" ", localPathList);
                generatedPaths.Add(resultingPath);
                // if match found then no need
                // to traverse more till depth
                return;
            }

            // Mark the current node
            isVisited[u] = true;

            // Recur for all the vertices
            // adjacent to current vertex
            foreach (int i in adjList[u])
            {
                if (!isVisited[i])
                {
                    // store current node
                    // in path[]
                    localPathList.Add(i);
                    dfs(i, d, isVisited,
                                      localPathList);

                    // remove current node
                    // in path[]
                    localPathList.Remove(i);
                }
            }

            // Mark the current node
            isVisited[u] = false;
        }


    }
}


