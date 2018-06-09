using UnityEngine;
using System.Collections.Generic;

public class BasicInputManager : MonoBehaviour
{
    public BasicObjectPool m_explosionPool;

    private List<Explosion> m_activeExplosions = new List<Explosion>();

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnExplosion(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));

        if (Input.GetMouseButtonDown(0))
            SpawnExplosion(Input.mousePosition);

        // When any explosions have finished, return them to the pool.
        for (var i = m_activeExplosions.Count - 1; i >= 0; i--)
        {
            var explosion = m_activeExplosions[i];
            if (!explosion.IsAlive)
            {
                m_activeExplosions.RemoveAt(i);
                m_explosionPool.ReturnObject(explosion.gameObject);
            }
        }
    }

    private void SpawnExplosion(Vector2 mousePosition)
    {
        var gob = m_explosionPool.Get();
        if (gob == null)
        {
            Debug.LogWarning("Explosion pool is empty!");
            return;
        }

        var explosion = gob.GetComponent<Explosion>();

        // Track the explosion and spawn it at the mouse position.
        m_activeExplosions.Add(explosion);
        var position = Camera.main.ScreenToWorldPoint(mousePosition);
        explosion.Spawn(position);
        explosion.transform.parent = transform;
    }
}
