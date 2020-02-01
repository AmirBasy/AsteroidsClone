using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICreation
{
    void Create(Vector3 position, Vector3 size, Vector3 direction, float speed, System.Action<AudioClip> soundFunction);
}
