using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private int _poolCapacity;
    [SerializeField, Min(0.1f)] private float _spawnDelay;

    private ObjectPool<Cube> _pool;

    private void Start()
    {
        StartCoroutine(StartSpawning());
    }

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cube),
            actionOnGet:(cube) => UseOnGet(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void UseOnGet(Cube cube)
    {
        int dropHeight = 30;
        
        cube.transform.position = new Vector3(GetRandomPosition(), dropHeight, GetRandomPosition());
        cube.LifeSpanEnded += ReleaseObject;
        gameObject.SetActive(true);
    }

    private float GetRandomPosition(float positionX = 5, float positionY = 35)
    {
        return Random.Range(positionX, positionY);
    }

    private void ReleaseObject(Cube cube)
    {
        _pool.Release(cube);

        cube.LifeSpanEnded -= ReleaseObject;
    }

    private IEnumerator StartSpawning()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(_spawnDelay);

            _pool.Get();
        }
    }
}