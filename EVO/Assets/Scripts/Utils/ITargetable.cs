using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetable
{
    public List<Order> GetOrderPriority();
    public GameObject GetGameObject();
}
