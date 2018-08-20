using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public static class GeneratorPoints
    {
        public static float MinX = -9;
        public static float MaxX = 9;
        public static float MinZ = -9;
        public static float MaxZ = 9;

        public static List<Vector3> GetPointsCoords(int count, Vector3 playerPos)
        {
            int[] borders = { Convert.ToInt32(Math.Ceiling(MinX + 0.5f)), Convert.ToInt32(Math.Floor(MaxX - 0.5f)),
                Convert.ToInt32(Math.Ceiling(MinZ + 0.5f)), Convert.ToInt32(Math.Floor(MaxZ - 0.5f)) };

            List<Vector3> result = new List<Vector3>();
            for (int i = 0; i < count; i++)
            {
                int randX = UnityEngine.Random.Range(borders[0], borders[1]);
                int randZ = UnityEngine.Random.Range(borders[2], borders[3]);
                Vector3 newVector = new Vector3(randX, 0, randZ);
                if (!result.Contains(newVector) && !AreCrossing(playerPos, newVector))
                {
                    result.Add(newVector);
                }
                else i--;
            }
            return result;
        }

        private static bool AreCrossing(Vector3 first, Vector3 second)
        {
            return ((first.x < second.x + 2) && (first.x > second.x - 2)) && 
                ((first.z < second.z + 2) && (first.z > second.z - 2));
        }
    }

    public static class PoolPoints
    {
        public static Stack<GameObject> NonActive = new Stack<GameObject>();
        public static GameObject Prefab = Resources.Load("Prefabs/Point") as GameObject;
        private static int baseAmount = 10;

        public static GameObject Pop()
        {
            GameObject newObj;
            if (NonActive.Count == 0)
                newObj = UnityEngine.Object.Instantiate(Prefab);
            else newObj = NonActive.Pop();
            newObj.SetActive(true);
            return newObj;
        }
        public static void Push(GameObject newObj)
        {
            newObj.SetActive(false);
            NonActive.Push(newObj);
        }

        public static void PoolInitialization()
        {
            for (int i = 0; i < baseAmount; i++)
            {
                Push(UnityEngine.Object.Instantiate(Prefab));
            }
        }
    }
}
