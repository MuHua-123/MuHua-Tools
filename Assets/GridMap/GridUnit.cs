using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUnit {
    public int x, y;
    public GridUnit(int x, int y) {
        this.x = x;
        this.y = y;
    }
    //寻路属性
    public int gCost, hCost, fCost;
    public GridUnit cameFromNode;
    public virtual bool isWalkable => true;
    public void InitializationCost() {
        gCost = int.MaxValue;
        CalculateFCost();
        cameFromNode = null;
    }
    public void CalculateFCost() {
        fCost = gCost + hCost;
    }
}
