using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandle : MonoBehaviour
{

    public PlayerData playerdata;

    public void Start()
    {
        if(string.IsNullOrEmpty(playerdata.id))
        {
            playerdata.id = System.DateTime.Now.ToLongDateString() + System.DateTime.Now.ToLongTimeString() + Random.Range(0, int.MaxValue).ToString();

            SaveData.current.thislevel.player.id = playerdata.id;
        }

        //GameManager.current.onloadevent += DestroyMe;
    }



    void DestroyMe()
    {
        //GameManager.current.onLoadEvent -= DestroyMe;
        Destroy(gameObject);
    }

}
