﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GUISkin UISkin;
    public GameObject box;

    List<GameObject> _boxes = new List<GameObject>();

    IEnumerator AnimateCellularRow()
    {
        var cellular = new CellularAutomata1d();
        var length = cellular.Cells.Length;
        int genCount = cellular.Cells.Length;

        for (int i = 0; i < genCount; i++)
        {
            

            for (int j = 0; j < length; j++)
            {
                if (cellular.Cells[j])
                {
                    
                    AddBoxToGrid(i, j, length);
                    
                }
            }

            cellular.NextGeneration();
            yield return new WaitForSeconds(0.25f);
        }
    }

    void AddBoxToGrid(int layer, int row, int rowCount)
    {
        float size = 1.02f;
        float halfRow = rowCount * 0.5f;
        

        var rotation = Quaternion.Euler(0, layer * 0, 0);
        var pos = new Vector3(
            (row - halfRow) * size,
            0.5f + layer* size,
            0
            );
        pos = rotation * pos;
        var instance = Instantiate(box, pos, rotation);
        _boxes.Add(instance);
        layer++;
    }

    private void OnGUI()
    {
        GUI.skin = UISkin;

        GUILayout.BeginArea(new Rect(20, 20, 200, 200));

        if (GUILayout.Button("Animate Cellular Row"))
        {
            //foreach (var instance in _boxes)
            //    Destroy(instance);
            StartCoroutine(AnimateCellularRow());
        }

        GUILayout.EndArea();
    }
}
