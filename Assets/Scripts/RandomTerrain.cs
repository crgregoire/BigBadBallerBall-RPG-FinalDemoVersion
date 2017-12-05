using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTerrain : MonoBehaviour {

    Terrain selfTerrain; 
 
    private void Awake()
    {
        selfTerrain = gameObject.GetComponent<Terrain>();
        GenerateTerrain(100f);
    }

    private void GenerateTerrain(float tileSize)
    {

        //The higher the numbers, the more hills/mountains there are
        float HM = Random.Range(0, 100);
        //The lower the numbers in the number range, the higher the hills/mountains will be...
        float divRange = Random.Range(1, 10);

        var height = selfTerrain.terrainData.heightmapHeight;
        var width = selfTerrain.terrainData.heightmapWidth;

        //Heights For Our Hills/Mountains
        float[,] hts  = new float[width, height];
        for (int i = 0; i < width; i++) {
            for (int k = 0; k < height; k++) {
                hts[i, k] = Mathf.PerlinNoise((i / width) * tileSize, (k / height) * tileSize) / divRange;
            }
        }
        Debug.LogWarning("DivRange: " + divRange + " , " + "HTiling: " + HM);
        selfTerrain.terrainData.SetHeights(0, 0, hts);
    }
}
