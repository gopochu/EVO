using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [Header("Oil")]
    [SerializeField] private int _oilCount = 0;
    [SerializeField] public int OilMax = 100;

    [Header("Metal")]
    [SerializeField] private int _metalCount = 0;
    [SerializeField] public int MetalMax = 100;

    public int OilCount
    {
        get => _oilCount;
    }
    public int MetalCount
    {
        get => _metalCount;
    }

    /// <summary>
    /// Вычитает из количества металла value. Если value больше количества металла, то количество приравнивается к 0.
    /// </summary>
    /// <return>
    /// Если value больше количества металла, то возвращает разницу между ними, иначе - 0.
    /// </return>
    public int DecreaseMetal(int value)
    {
        var result = value - _metalCount;
        _metalCount = Mathf.Max(0, _metalCount - value);
        return Mathf.Max(result, 0);
    }

    /// <summary>
    ///Вычитает из количества нефти value. Если value больше количества нефти, то количество приравнивается к 0.
    /// </summary>
    /// <return>
    ///Если value больше количества нефти, то возвращает разницу между ними, иначе - 0.
    /// </return>
    public int DecreaseOil(int value)
    {
        var result = value - _oilCount;
        _oilCount = Mathf.Max(0, _oilCount - value);
        return Mathf.Max(result, 0);
    }

    /// <summary>
    /// Увеличивает количество металла на value. Если количество больше максимума, то количество приравнивается к максимуму.
    /// </summary>
    /// <return>
    /// Возвращает количество металла, которое не удалось положить на склад.
    /// </return>
    public int IncreaseMetal(int value)
    {
        var result = _metalCount + value - MetalMax;
        _metalCount = Mathf.Min(MetalMax, _metalCount + value);
        return Mathf.Max(0, result);
    }

    /// <summary>
    /// Увеличивает количество нефти на value. Если количество больше максимума, то количество приравнивается к максимуму.
    /// </summary>
    /// <return>
    /// Возвращает количество нефти, которое не удалось положить на склад.
    /// </return>
    public int IncreaseOil(int value)
    {
        var result = _oilCount + value -OilMax;
        _oilCount = Mathf.Min(OilMax, _oilCount + value);
        return Mathf.Max(0, result);
    }
}
