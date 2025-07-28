using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // List of values dictating the -space- between spawning elements, repeats
    public float[] distancePattern = new float[] { 1f,1f,1f };
    // List of Entities to spawn, in order, repeats
    public Entity[] entityPattern;

    // Init value is offset
    public float nextTimeToSpawn = 0f;
    public float speed = 5;

    private int distanceIndex = 0;
    private int entityIndex = 0;

    private void Start()
    {
        if (transform.position.x > 0)
        {
            speed *= -1;
        }
    }

    public Entity GetNextEntity()
    {
        if (distanceIndex >= distancePattern.Length)
        {
            distanceIndex = 0;
        }
        if (entityIndex >= entityPattern.Length)
        {
            entityIndex = 0;
        }

        Entity nextEntity = entityPattern[entityIndex];
        float width = nextEntity.GetComponent<BoxCollider2D>().size.x;

        nextTimeToSpawn = Time.time + ((distancePattern[distanceIndex] + width) / Mathf.Abs(speed));

        distanceIndex++;
        entityIndex++;

        return nextEntity;
    }

}
