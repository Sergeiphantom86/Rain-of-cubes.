using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RandomColorAssigner), typeof(Rigidbody))]

public class Cube: MonoBehaviour
{
    private bool _hasTouched;
    private RandomColorAssigner _randomColorAssigner;

    public event Action<Cube> LifeSpanEnded;

    private void Start()
    {
        _randomColorAssigner = GetComponent<RandomColorAssigner>();
        
        _hasTouched = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Platform _) && _hasTouched)
        {
            _randomColorAssigner.Replace();

            StartCoroutine(TurnOffElement());

            _hasTouched = false;
        }
    }

    private IEnumerator TurnOffElement()
    {
        int min = 2;
        int max = 6;

        float delay = UnityEngine.Random.Range(min, max);

        yield return new WaitForSeconds(delay);

        DisableItem();
    }

    private void DisableItem()
    {
        gameObject.SetActive(false);

        ReturnDefaultValues();
        
        LifeSpanEnded?.Invoke(this);
    }

    private void ReturnDefaultValues()
    {
        _randomColorAssigner.Default();
        transform.rotation = new Quaternion();
        _hasTouched = true;
    }
}