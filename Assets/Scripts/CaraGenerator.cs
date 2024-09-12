using System.Collections.Generic;
using UnityEngine;

public class CaraGenerator : MonoBehaviour
{
    [SerializeField] GameObject horse;

    [SerializeField] GameObject kangaroo;

    List<(Vector3 tage1, Vector3 tage2)> Spawns = new List<(Vector3, Vector3)>();

    void Awake()
    {

        Spawns.Add((new Vector3(380, 25, -110), new Vector3(160, 20, 110)));
        Spawns.Add((new Vector3(160, 25, -120), new Vector3(390,25, 110)));
        Spawns.Add((new Vector3(170, 25, 15), new Vector3(330, 25, 20)));
        Spawns.Add((new Vector3(330, 25, -110), new Vector3(235, 25, 75)));
        Spawns.Add((new Vector3(275, 25, -3), new Vector3(343, 25, 67)));
        Spawns.Add((new Vector3(275, 25, -3), new Vector3(366, 25, -97)));

        RandomSpawn();

    }

    void RandomSpawn()
    {
        int index = (int)Random.Range(0, Spawns.Count);

        int or = (int)Random.Range(0, 2);

        if(or == 0)
        {
            horse.transform.position = Spawns[index].tage1;
            kangaroo.transform.position = Spawns[index].tage2;

        }
        else
        {
            horse.transform.position = Spawns[index].tage2;
            kangaroo.transform.position = Spawns[index].tage1;

        }

    }
}
