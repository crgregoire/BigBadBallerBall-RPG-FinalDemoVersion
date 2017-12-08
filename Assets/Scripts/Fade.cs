using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

        public float fadePerSecond = 2.5f;

        private void Update()
        {
            var material = GetComponent<Renderer>().material;
            var color = material.color;
        var alpha = color.a - (fadePerSecond * Time.deltaTime);
        if (alpha < 0.01f)
        {
            alpha = 0;
        }

            material.color = new Color(color.r, color.g, color.b, alpha);
        }

}
