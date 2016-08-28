using UnityEngine;
using System.Collections;

public class EnemyEnergy : MonoBehaviour
{
    public int startEnergy = 1;
    public GameObject explosionPrefab;
    public AudioClip explosionAudio;

    private float _currenEnergy;
    private bool _isDead;
    private ParticleSystem _explosionParticels;
    private AudioSource _audioSource;
    private MeshRenderer _enemyMesh;

    #region UNITY_EVENTS

    private void Awake()
    {
        _explosionParticels = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
        _explosionParticels.gameObject.SetActive(false);
        _audioSource = GetComponent<AudioSource>();
        _enemyMesh = GetComponentInChildren<MeshRenderer>();
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
            StartCoroutine(OnDeath());
        }
    }

    private IEnumerator OnDeath(float delay = 0.5f)
    {
        _isDead = true;
        _audioSource.PlayOneShot(explosionAudio);
        _explosionParticels.transform.position = transform.position;
        _explosionParticels.gameObject.SetActive(true);
        _enemyMesh.enabled = false;
        var enemyCom = GetComponent<Enemy>();
        GameThreadManager.Instance.availableEnemies.Remove(enemyCom);
        if (GameThreadManager.Instance.availableEnemies.Count == 1)
        {
            for (var i = 0; i < 2; i++)
            {
                GameThreadManager.Instance.SpawnEnemyRandom();
            }
        }
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
