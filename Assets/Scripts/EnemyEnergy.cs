using UnityEngine;
using System.Collections;

public class EnemyEnergy : MonoBehaviour
{
    public int startEnergy = 1;
    public GameObject explosionPrefab;

    private float _currenEnergy;
    private bool _isDead;
    private ParticleSystem _explosionParticels;

    #region UNITY_EVENTS

    private void Awake()
    {
        _explosionParticels = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
        _explosionParticels.gameObject.SetActive(false);
    }

    private void Start()
    {
        TakeEnergy(startEnergy);
    }

    #endregion

    public void TakeEnergy(int amount)
    {
        if (_isDead)
            return;
        _currenEnergy += amount;
        if (_currenEnergy <= 0f)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        _isDead = true;
        _explosionParticels.transform.position = transform.position;
        _explosionParticels.gameObject.SetActive(true);
        gameObject.SetActive(false);
        var enemyCom = GetComponent<Enemy>();
        GameThreadManager.Instance.availableEnemies.Remove(enemyCom);
    }
}
