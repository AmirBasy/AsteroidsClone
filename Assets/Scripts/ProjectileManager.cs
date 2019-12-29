using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour 
{

    public GameObject _projectilePrefab;

    public void SpawnProjectile(GameObject offset)
    {

        GameObject _projectile = CreateProjectile();

        _projectile.transform.position = offset.transform.position;

    }

    private GameObject CreateProjectile()
    {
        GameObject _item = Instantiate(_projectilePrefab);

        _item.gameObject.AddComponent<Projectile>();

        _item.gameObject.GetComponent<Projectile>().speed = 1f;

        return _item;
    }


}

