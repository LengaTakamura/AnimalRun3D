using System.Collections.Generic;
using UnityEngine;

public class CaraGenerator : MonoBehaviour
{
    [SerializeField] GameObject horse;

    [SerializeField] GameObject kangaroo;

    List<(Vector3 tage1, Vector3 tage2)> Spawns = new List<(Vector3, Vector3)>();

    void Awake()
    {

        Spawns.Add((new Vector3(390, 20, -120), new Vector3(160, 20, 110)));
        Spawns.Add((new Vector3(160, 20, -120), new Vector3(390, 20, 110)));
        Spawns.Add((new Vector3(170, 30, 15), new Vector3(330, 25, 15)));
        Spawns.Add((new Vector3(330, 20, -110), new Vector3(230, 20, 75)));
        Spawns.Add((new Vector3(275, 20, -3), new Vector3(355, 20, 85)));
        Spawns.Add((new Vector3(275, 20, -3), new Vector3(365, 20, -96)));


        RandomSpawn();


    }

    void RandomSpawn()
    {
        int index = (int)Random.Range(0f, Spawns.Count + 1);

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
