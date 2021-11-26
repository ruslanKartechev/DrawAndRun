using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DrawAndRun
{
    public class ClonesAlignment : Singleton<ClonesAlignment>
    {
        public float defaultSpacing = 10;
        public float defaultHeaight = 3;
        public List<Vector3> GetDefaultAlignment(int num)
        {
            List<Vector3> defAlignment = new List<Vector3>();
            float offset = defaultSpacing * num/4;
            if (num % 4 == 0)
                offset += -defaultSpacing / 2;
            for(int i=0; i < num; i++)
            {
                float z = defaultSpacing;
                float y = defaultHeaight;
                float x = i * defaultSpacing - offset;
                if (i >= num / 2)
                {
                    z = -z;
                    x = (i - num/2) * defaultSpacing - offset;
                }
                Vector3 temp = new Vector3(x,y,z);
                defAlignment.Add(temp);
            }
            return defAlignment;
        }

        public List<Vector3> CreateAlignment(List<Vector3> points)
        {
            List<Vector3> newAlignment = new List<Vector3>();
            int posCount = GameManager.instance.clonesSpawner.currentClonesCount;
            if (posCount <= 0)
                return null; 
            if (points.Count >= posCount)
            {
                int spacing = points.Count / posCount;
                for(int i =0;i< posCount; i++)
                {
                    newAlignment.Add(new Vector3(points[i * spacing].x, defaultHeaight, points[i * spacing].z));
                }
            } else
            {
                for (int i = 0; i < posCount; i++)
                {
                    if (i < points.Count)
                        newAlignment.Add(points[i]);
                    else
                        newAlignment.Add(points[points.Count-1]);
                }
            }
            return newAlignment;
        }


    }

}
