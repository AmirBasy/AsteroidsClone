using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitController : MonoBehaviour
{
    public bool _enableLimitController;

    #region UnityCallbacks

    private void Update()
    {
        if(_enableLimitController)
        {
            CheckEntityPosition();
        }
    }

    #endregion

    private void CheckEntityPosition()
    {
        if(EntityOverTheLimit())
        {
            if(this.gameObject.transform.position.x > 10)
            {
                this.gameObject.transform.position -= new Vector3(20, 0, 0);
            }
            if(this.gameObject.transform.position.x < -10)
            {
                this.gameObject.transform.position += new Vector3(20, 0, 0);
            }
            if(this.gameObject.transform.position.y > 10)
            {
                this.gameObject.transform.position -= new Vector3(0, 20, 0);
            }
            if(this.gameObject.transform.position.y < -10)
            {
                this.gameObject.transform.position += new Vector3(0, 20, 0);
            }
        }
    }

    private bool EntityOverTheLimit()
    {
        if (this.gameObject.transform.position.x > 10 || this.gameObject.transform.position.x < -10)
        {
            return true;
        }
        else if (this.gameObject.transform.position.y > 10 || this.gameObject.transform.position.y < -10)
        {
            return true;
        }
        else return false;
    }
}

