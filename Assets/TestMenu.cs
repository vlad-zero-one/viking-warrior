using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMenu : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _playerGameObject;
    private GameObject _enemiesParentGameObject;
    private List<GameObject> _spawnedEmenies;

    [SerializeField] private GameObject _bonfirePrefab;
    private GameObject _bonfireGameObject;

    private void Start()
    {
        _enemiesParentGameObject = GameObject.Find("Enemies");
        _spawnedEmenies = new List<GameObject>();
    }

    public void SpawnEnemy()
    {
        var pos = _playerGameObject.transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10));
        var spawnedEnemy = Instantiate(_enemyPrefab, pos, Quaternion.identity, _enemiesParentGameObject.transform);
        _spawnedEmenies.Add(spawnedEnemy);
    }

    public void DestroyEnemies()
    {
        Debug.Log(_spawnedEmenies.Count);
        foreach(var enemy in _spawnedEmenies)
        {
            Destroy(enemy);
        }
        _spawnedEmenies.Clear();
    }

    public void AddTenExperience()
    {
        _playerGameObject.GetComponent<PlayerUnit>().AddExperience(10);
    }

    public void PlaceBonfire()
    {
        if (_bonfireGameObject == null)
        {
            var pos = _playerGameObject.transform.position + new Vector3(Random.Range(-2, 2), Random.Range(-2, 2));
            _bonfireGameObject = Instantiate(_bonfirePrefab, pos, Quaternion.identity);
        }
        else
        {
            _bonfireGameObject.transform.position = _playerGameObject.transform.position + new Vector3(Random.Range(-2, 2), Random.Range(-2, 2));
        }
    }
}
