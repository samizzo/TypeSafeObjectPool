using UnityEngine;

public class Explosion : MonoBehaviour
{
    private ParticleSystem m_particleSystem;

    public bool IsAlive { get { return m_particleSystem.IsAlive(); } }

    public void Awake()
    {
        m_particleSystem = GetComponent<ParticleSystem>();
    }

    public void Spawn(Vector3 position)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = position;
        m_particleSystem.Play();
    }
}
