using UnityEngine;
using System.Collections;
using System.Net.NetworkInformation;

public class UnitEnergy : MonoBehaviour
{
    public int startEnergy = 1;
    public int maxEnergy = 4;
    public GameObject explosionPrefab;
    public GameObject minePrefab;
    public Camera gameCam;

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
        _currenEnergy = startEnergy;
        //StartCoroutine(TestDeath());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Plane plane = new Plane(Vector3.up, transform.position);
            Ray rayToMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
            float dist;
            plane.Raycast(rayToMouse, out dist);
            Vector3 targetPos = rayToMouse.GetPoint(dist);
            var mine = Instantiate(minePrefab, targetPos, Quaternion.identity);
        }
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
        else if (_currenEnergy > maxEnergy)
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
    }

    private IEnumerator TestDeath()
    {
        yield return new WaitForSeconds(2.0f);
        TakeEnergy(-100);
    }
}
