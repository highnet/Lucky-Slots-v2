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
        slotsGraph.AddEdge(0, 1);
        slotsGraph.AddEdge(0, 6);

        slotsGraph.AddEdge(1, 2);
        slotsGraph.AddEdge(1, 7);

        slotsGraph.AddEdge(2, 3);
        slotsGraph.AddEdge(2, 8);

        slotsGraph.AddEdge(3, 4);
        slotsGraph.AddEdge(3, 9);

        slotsGraph.AddEdge(5, 1);
        slotsGraph.AddEdge(5, 6);
        slotsGraph.AddEdge(5, 11);

        slotsGraph.AddEdge(6, 2);
        slotsGraph.AddEdge(6, 7);
        slotsGraph.AddEdge(6, 12);

        slotsGraph.AddEdge(7, 3);
        slotsGraph.AddEdge(7, 8);
        slotsGraph.AddEdge(7, 13);

        slotsGraph.AddEdge(8, 4);
        slotsGraph.AddEdge(8, 9);
        slotsGraph.AddEdge(8, 14);

        slotsGraph.AddEdge(10, 6);
        slotsGraph.AddEdge(10, 11);
        slotsGraph.AddEdge(10, 16);

        slotsGraph.AddEdge(11, 7);
        slotsGraph.AddEdge(11, 12);
        slotsGraph.AddEdge(11, 17);

        slotsGraph.AddEdge(12, 8);
        slotsGraph.AddEdge(12, 13);
        slotsGraph.AddEdge(12, 18);

        slotsGraph.AddEdge(13, 9);
        slotsGraph.AddEdge(13, 14);
        slotsGraph.AddEdge(13, 19);

        slotsGraph.AddEdge(15, 11);
        slotsGraph.AddEdge(15, 16);

        slotsGraph.AddEdge(16, 12);
        slotsGraph.AddEdge(16, 17);

        slotsGraph.AddEdge(17, 13);
        slotsGraph.AddEdge(17, 18);

        slotsGraph.AddEdge(18, 14);
        slotsGraph.AddEdge(18, 19);
    }

    private void GeneratePaths(int v1, int v2)
    {
        List<string> generatedPathStrings = slotsGraph.DFSGenerateAllPaths(v1, v2);

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

        private int numberOfVertices;

        private List<int>[] adjancencyList;

        private List<string> generatedPaths;

        public Graph(int vertices)
        {

            this.generatedPaths = new List<string>();
            this.numberOfVertices = vertices;
            InitializeAdjacencyList();
        }

        private void InitializeAdjacencyList()
        {
            adjancencyList = new List<int>[numberOfVertices];

            for (int i = 0; i < numberOfVertices; i++)
            {
                adjancencyList[i] = new List<int>();
            }
        }

        public void AddEdge(int u, int v)
        {
            adjancencyList[u].Add(v);
        }


        public List<string> DFSGenerateAllPaths(int source, int destination)
        {
            bool[] isVisited = new bool[numberOfVertices];
            List<int> pathList = new List<int>();

            pathList.Add(source);

            DFS(source, destination, isVisited, pathList);

            return generatedPaths;
        }

        private void DFS(int current, int destination,
                                       bool[] isVisited,
                                       List<int> localPathList)
        {

            if (current.Equals(destination))
            {
                string resultingPath = string.Join(" ", localPathList);
                generatedPaths.Add(resultingPath);
                return;
            }

            isVisited[current] = true;

            foreach (int neighbor in adjancencyList[current])
            {
                if (!isVisited[neighbor])
                {
                    localPathList.Add(neighbor);
                    DFS(neighbor, destination, isVisited,
                                      localPathList);
                    localPathList.Remove(neighbor);
                }
            }

            isVisited[current] = false;
        }


    }
}


