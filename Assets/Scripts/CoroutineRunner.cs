using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class CoroutineRunner : MonoBehaviour
    {
        private List<Coroutine> _coroutines = new List<Coroutine>();
        public Coroutine StartMyCoroutine(IEnumerator coroutine)
        {
            var routine = StartCoroutine(coroutine);
            _coroutines.Add(routine);
            return routine;
        }
    }
}