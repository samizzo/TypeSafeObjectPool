﻿using UnityEngine;
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

    private List<GameObject> m_freeList;
    private List<GameObject> m_usedList;

    public void Awake()
    {
        m_freeList = new List<GameObject>(m_size);
        m_usedList = new List<GameObject>(m_size);

        // Instantiate the pooled objects and disable them.
        for (var i = 0; i < m_size; i++)
        {
            var pooledObject = Instantiate(m_prefab, transform);
            pooledObject.gameObject.SetActive(false);
            m_freeList.Add(pooledObject);
        }
    }

    /// <summary>
    /// Returns an object from the pool. Returns null if there are no more objects free in the pool.
    /// </summary>
    /// <returns>Object of type T from the pool.</returns>
    public GameObject Get()
    {
        var numFree = m_freeList.Count;
        if (numFree == 0)
            return null;

        // Pull an object from the end of the free list.
        var pooledObject = m_freeList[numFree - 1];
        m_freeList.RemoveAt(numFree - 1);
        m_usedList.Add(pooledObject);
        return pooledObject;
    }

    /// <summary>
    /// Returns an object to the pool. The object must have been created by this ObjectPool.
    /// </summary>
    /// <param name="pooledObject">Object previously obtained from this ObjectPool</param>
    public void ReturnObject(GameObject pooledObject)
    {
        Debug.Assert(m_usedList.Contains(pooledObject));

        // Put the pooled object back in the free list.
        m_usedList.Remove(pooledObject);
        m_freeList.Add(pooledObject);

        // Reparent the pooled object to us, and disable it.
        var pooledObjectTransform = pooledObject.transform;
        pooledObjectTransform.parent = transform;
        pooledObjectTransform.localPosition = Vector3.zero;
        pooledObject.gameObject.SetActive(false);
    }
}
