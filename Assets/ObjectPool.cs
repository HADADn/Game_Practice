using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject squarePrefab;
    public int poolSize = 5;
    private Queue<GameObject> pool = new Queue<GameObject>();
    private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(squarePrefab);
            obj.SetActive(false);
            originalPositions[obj] = transform.position;
            obj.transform.position = transform.position;
            pool.Enqueue(obj);
        }

        StartCoroutine(LaunchAllSquares());
    }

    IEnumerator LaunchAllSquares()
    {
        while (true)
        {
            int launchCount = Mathf.Min(poolSize, pool.Count);
            for (int i = 0; i < launchCount; i++)
            {
                GameObject square = pool.Dequeue();
                LaunchSquare(square);
            }

            yield return new WaitForSeconds(3f);
        }
    }

    void LaunchSquare(GameObject obj)
    {
        Vector3 startPosition = originalPositions[obj];
        obj.transform.position = startPosition;
        Vector2 direction = Random.insideUnitCircle.normalized;
        float distance = Random.Range(2f, 5f);
        Vector3 targetPosition = startPosition + (Vector3)(direction * distance);
        obj.SetActive(true);
        obj.transform.position = targetPosition;
        StartCoroutine(ReturnToPoolAfterDelay(obj, 2f));
    }

    IEnumerator ReturnToPoolAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.transform.position = originalPositions[obj];
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
