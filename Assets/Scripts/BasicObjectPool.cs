using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// An example of a basic object pool without type-safety.
/// </summary>
public class BasicObjectPool : MonoBehaviour
{
    [Tooltip("Prefab for this object pool")]
    public GameObject m_prefab;

    [Tooltip("Size of this object pool")]
    public int m_size;

    public void Awake()
    {
        // Instantiate the pooled objects and disable them.
        for (var i = 0; i < m_size; i++)
        {
            var pooledObject = Instantiate(m_prefab, transform);
            pooledObject.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Returns an object from the pool. Returns null if there are no more objects free in the pool.
    /// </summary>
    /// <returns>GameObject from the pool.</returns>
    public GameObject Get()
    {
        if (transform.childCount == 0)
            return null;

        return transform.GetChild(0).gameObject;
    }

    /// <summary>
    /// Returns an object to the pool.
    /// </summary>
    /// <param name="pooledObject">Object previously obtained from this ObjectPool</param>
    public void ReturnObject(GameObject pooledObject)
    {
        // Reparent the pooled object to us and disable it.
        var pooledObjectTransform = pooledObject.transform;
        pooledObjectTransform.parent = transform;
        pooledObjectTransform.localPosition = Vector3.zero;
        pooledObject.gameObject.SetActive(false);
    }
}
