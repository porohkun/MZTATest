using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Test : MonoBehaviour
    {
        IEnumerator Start()
        {
            //Debug.Log(Time.realtimeSinceStartup);
            yield return null;
            //Debug.Log(Time.realtimeSinceStartup);
            //yield return null;
            //Debug.Log(Time.realtimeSinceStartup);
            //yield return new WaitForSeconds(3);
            //Debug.Log(Time.realtimeSinceStartup);
        }
    }
}
