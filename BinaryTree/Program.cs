using BinaryTree;
using System.Diagnostics;
using System.Text;

var startNode = 15;
var node1 = 14354633;
var node2 = 14112123;

var sw1 = new Stopwatch();
sw1.Start();
var solution2 = FindMostRecentCommonAncestorDivision(startNode, node1, node2);
sw1.Stop();
Console.WriteLine($"Division Method: The most common ancestor of nodes '{node1}' and '{node2}' with start node '{startNode}' is '{solution2}'. This took {sw1.ElapsedMilliseconds}ms.");

sw1 = new Stopwatch();
sw1.Start();
var solution3 = FindMostRecentCommonAncestorBinary(startNode, node1, node2);
sw1.Stop();
Console.WriteLine($"Binary Method: The most common ancestor of nodes '{node1}' and '{node2}' with start node '{startNode}' is '{solution3}'. This took {sw1.ElapsedMilliseconds}ms.");

sw1 = new Stopwatch();
sw1.Start();
var solution1 = FindMostRecentCommonAncestorBasic(startNode, node1, node2);
sw1.Stop();
Console.WriteLine($"Basic Method: The most common ancestor of nodes '{node1}' and '{node2}' with start node '{startNode}' is '{solution1}'. This took {sw1.ElapsedMilliseconds}ms.");

long FindMostRecentCommonAncestorBinary(long startingNodeValue, long nodeValue1, long nodeValue2)
{
    nodeValue1 -= startingNodeValue - 1;
    nodeValue2 -= startingNodeValue - 1;

    var binaryString1 = Convert.ToString(nodeValue1, 2);
    var binaryString2 = Convert.ToString(nodeValue2, 2);

    var shortestBinaryLength = Math.Min(binaryString1.Length, binaryString2.Length);
    if (shortestBinaryLength < 2)
    {
        return 1 + startingNodeValue - 1;
    }
    var sb = new StringBuilder();
    for (var i = 1; i < shortestBinaryLength; i++)
    {
        if (binaryString1[i] == binaryString2[i])
        {
            sb.Append(binaryString1[i]);
        }
        else
        {
            break;
        }
    }
    var commonAncestor = 1;

    foreach (var digit in sb.ToString())
    {
        if (digit == '0')
        {
            commonAncestor *= 2;
        }
        else
        {
            commonAncestor = commonAncestor * 2 + 1;
        }
    }

    return commonAncestor + startingNodeValue - 1;
}

long FindMostRecentCommonAncestorDivision(long startingNodeValue, long nodeValue1, long nodeValue2)
{
    nodeValue1 -= startingNodeValue - 1;
    nodeValue2 -= startingNodeValue - 1;

    while(nodeValue1 != nodeValue2)
    {
        if (nodeValue1 > nodeValue2)
        {
            nodeValue1 /= 2;
        }
        else
        {
            nodeValue2 /= 2;
        }
    }

    return nodeValue1 + startingNodeValue - 1;
}

long FindMostRecentCommonAncestorBasic(long startingNodeValue, long nodeValue1, long nodeValue2)
{
    var tree = BuildTree(startingNodeValue, Math.Max(nodeValue1, nodeValue2));
    var nodes = new List<Node>() { tree.First(x => x.Value == nodeValue1), tree.First(x => x.Value == nodeValue2) };
    
    while (nodes.First().Value != nodes.Last().Value)
    {
        var highestValueNode = nodes.First(x => x.Value == nodes.Max(x => x.Value));
        nodes.Remove(highestValueNode);
        nodes.Add(highestValueNode.Parent!);
    }
    return nodes.First().Value;
}

List<Node> BuildTree(long startingNodeValue, long highestNodeValue)
{
    var currentNodeValue = startingNodeValue;
    var tree = new List<Node>() { new Node(currentNodeValue++, null) };
    var previousLevel = new List<Node>(tree);
    while (currentNodeValue <= highestNodeValue)
    {
        var currentLevel = new List<Node>();
        foreach (var node in previousLevel)
        {
            for (var i = 0; i < 2; i++)
            {
                currentLevel.Add(new Node(currentNodeValue++, node));
            }
            if (currentNodeValue > highestNodeValue)
            {
                break;
            }
        }
        previousLevel = new List<Node>(currentLevel);
        tree.AddRange(currentLevel);
    }
    return tree;
}