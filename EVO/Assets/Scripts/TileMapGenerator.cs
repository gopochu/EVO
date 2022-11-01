using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapGenerator : MonoBehaviour
{
    public Tilemap grid;
    public Tile[] tiles;

    public int width;
    public int height;
    float[] noiseSeed;
    float[] noiseMap;

    void Start()
    {
        noiseSeed = new float[width * height];
        noiseMap = new float[width * height];

        System.Random rnd = new System.Random();
        for (int i = 0; i < width * height; i++) noiseSeed[i] = (float)rnd.NextDouble();

        PerlinNoise(noiseSeed, noiseMap);
        
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                if (noiseMap[y*width+x] >= 0f && noiseMap[y * width + x] <= 0.3f)
                    grid.SetTile(new Vector3Int(x-width/2, y-height/2, 0), tiles[0]);

                else if (noiseMap[y * width + x] > 0.3f && noiseMap[y * width + x] < 0.6f)
                    grid.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), tiles[1]);

                else if (noiseMap[y * width + x] >= 0.6f && noiseMap[y * width + x] <= 1f)
                    grid.SetTile(new Vector3Int(x - width / 2, y - height / 2, 0), tiles[2]);
            }
    }


    float[] PerlinNoise(float[] seed, float[] map)
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                float fNoise = 0.0f;
                float fScaleAcc = 0.0f;
                float fScale = 1.0f;

                for (int oct = 0; oct <5; oct++)
                {
                    int pitch = (int)(width / Math.Pow(2, oct));

                    int sampleX1 = (x / pitch) * pitch;
                    int sampleY1 = (y / pitch) * pitch;

                    int sampleX2 = (sampleX1 + pitch) % width;
                    int sampleY2 = (sampleY1 + pitch) % width;

                    float blendX = (float)(x - sampleX1) / (float)pitch;
                    float blendY = (float)(y - sampleY1) / (float)pitch;

                    float sample1 = (1.0f - blendX) * seed[sampleY1 * width + sampleX1] + blendX * seed[sampleY1 * width + sampleX2];
                    float sample2 = (1.0f - blendX) * seed[sampleY2 * width + sampleX1] + blendX * seed[sampleY2 * width + sampleX2];

                    fScaleAcc += fScale;
                    fNoise += (blendY * (sample2 - sample1) + sample1) * fScale;
                    fScale = fScale / 0.4f;
                }
                map[y * width + x] = fNoise / fScaleAcc;
            }
        return map;
    }
}
