using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridFindPath {
    public const int MOVE_STRAIGHT_COST = 10;
    public const int MOVE_DIAGONAL_COST = 14;
    public static bool FindPath<Unit>(this GridMap<Unit> gridMap, Vector3 sPosition, Vector3 ePosition, out Queue<Vector3> vectorPath, bool isCornerWalkable = false) where Unit : GridUnit {
        vectorPath = new Queue<Vector3>();
        if (!gridMap.TryGetMapUnit(sPosition, out Unit startNode)) { return false; }
        if (!gridMap.TryGetMapUnit(ePosition, out Unit endUnit)) { return false; }
        if (endUnit == null || !endUnit.isWalkable) { return false; }
        List<Unit> path = gridMap.FindPath(startNode, endUnit, isCornerWalkable);
        if (path == null) { return false; }
        foreach (Unit pathNode in path) {
            Vector3 worldPosition = gridMap.GetWorldPosition(pathNode.x, pathNode.y);
            vectorPath.Enqueue(worldPosition);
        }
        return path.Count > 0;
    }
    public static List<Unit> FindPath<Unit>(this GridMap<Unit> gridMap, Unit startNode, Unit endNode, bool isCornerWalkable) where Unit : GridUnit {
        Unit[,] array = gridMap.array;
        List<Unit> openList = new List<Unit> { startNode };
        List<Unit> closedList = new List<Unit>();
        gridMap.Loop((x, y) => { array[x, y].InitializationCost(); });

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0) {
            //获得最小f成本节点
            Unit currentNode = GetLowestFCostNode(openList);
            //以达到最终目的地
            if (currentNode == endNode) { return CalculatePath(endNode); }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (Unit neighbourNode in gridMap.GetNeighbourList(currentNode)) {
                //如果临近节点在关闭列表则跳过
                if (closedList.Contains(neighbourNode)) continue;
                //如果节点不可通行则添加到关闭列表
                if (!neighbourNode.isWalkable && neighbourNode != endNode) {
                    closedList.Add(neighbourNode);
                    continue;
                }
                //计算阻挡
                if (!isCornerWalkable && CornerWalkable(currentNode, neighbourNode, array)) { continue; }
                //计算成本
                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost) {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode)) { openList.Add(neighbourNode); }
                }
            }
        }
        return null;
    }
    //计算距离h成本
    private static int CalculateDistanceCost<Unit>(Unit a, Unit b) where Unit : GridUnit {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }
    //获得最小f成本
    private static Unit GetLowestFCostNode<Unit>(List<Unit> pathNodeList) where Unit : GridUnit {
        Unit lowestFCostNode = pathNodeList[0];
        for (int i = 0; i < pathNodeList.Count; i++) {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost) {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }
    //返回最终路径
    private static List<Unit> CalculatePath<Unit>(Unit endNode) where Unit : GridUnit {
        List<Unit> path = new List<Unit>();
        Unit currentNode = endNode;
        while (currentNode.cameFromNode != null) {
            path.Add(currentNode);
            currentNode = (Unit)currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }
    //计算阻挡
    private static bool CornerWalkable<Unit>(Unit currentNode, Unit neighbourNode, Unit[,] array) where Unit : GridUnit {
        if (CalculateDistanceCost(currentNode, neighbourNode) == MOVE_DIAGONAL_COST) {
            int x = neighbourNode.x - currentNode.x;
            int y = neighbourNode.y - currentNode.y;
            Unit a = array[currentNode.x + x, currentNode.y];
            Unit b = array[currentNode.x, currentNode.y + y];
            if (!a.isWalkable || !b.isWalkable) { return true; }
        }
        return false;
    }
}
