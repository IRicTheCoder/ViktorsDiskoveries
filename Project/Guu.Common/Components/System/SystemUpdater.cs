using System;
using System.Collections;
using UnityEngine;

namespace Guu.Components.System
{
    public class SystemUpdater : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(TimedUpdate());
        }

        private IEnumerator TimedUpdate()
        {
            while (Levels.IsLevel(Levels.WORLD))
            {
                if (SceneContext.Instance.TimeDirector.HasPauser())
                    yield return new WaitWhile(SceneContext.Instance.TimeDirector.HasPauser);
                
                SRGuu.OnGameTimedUpdate();
                
                yield return new WaitForSeconds(5f);
            }
        }

        private void Update()
        {
            SRGuu.OnGameUpdate();
        }

        private void FixedUpdate()
        {
            SRGuu.OnGameFixedUpdate();
        }
        
        private void LateUpdate()
        {
            SRGuu.OnGameLateUpdate();
        }
    }
}