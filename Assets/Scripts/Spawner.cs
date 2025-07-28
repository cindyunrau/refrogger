using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    void Update()
    {
        foreach(Transform childTs in GetComponentInChildren<Transform>())
        {
            SpawnPoint child = childTs.GetComponent<SpawnPoint>();

            if (child.nextTimeToSpawn <= Time.time)
            {
                SpawnEntity(child);
            }
        }
    }

    void SpawnEntity(SpawnPoint spawnPoint)
    {
        Entity entity = Instantiate(spawnPoint.GetNextEntity(), spawnPoint.transform.position, spawnPoint.transform.rotation).GetComponent<Entity>();

        entity.speed = spawnPoint.speed;
    }
}
