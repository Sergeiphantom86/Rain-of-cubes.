using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class RandomColorAssigner : MonoBehaviour
{
    [SerializeField] private Color _default;
    private readonly List<Color> _colors = new();
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Replace()
    {
        AddColors();

        _renderer.material.color = _colors[Random.Range(0, _colors.Count)];
    }

    public void Default()
    {
        _renderer.material.color = _default;
    }

    private void AddColors()
    {
        _colors.Add(Color.green);
        _colors.Add(Color.blue);
        _colors.Add(Color.magenta);
        _colors.Add(Color.yellow);
        _colors.Add(Color.white);
        _colors.Add(Color.cyan);
        _colors.Add(Color.red);
    }
}