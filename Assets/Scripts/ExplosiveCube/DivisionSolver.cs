using System;
using UnityEngine;
using Random = System.Random;

public class DivisionSolver : MonoBehaviour
{
    private const int TotalPercentsCount = 100;

    [SerializeField]private int _divisionChanceInPercents = 100;
    private Random _random = new Random();

    public event Action<int, Vector3> Divided;

    private void OnMouseDown()
    {
        TryDivide();
    }
    
    public void SetDivisionChance(int divisionChanceInPercents)
    {
        _divisionChanceInPercents = divisionChanceInPercents;
    }

    private void TryDivide()
    {
        int divisionChance = _random.Next(0, TotalPercentsCount);

        if (divisionChance <= _divisionChanceInPercents)
            Divided?.Invoke(_divisionChanceInPercents, transform.localScale);
        else Destroy(gameObject);
    }
}