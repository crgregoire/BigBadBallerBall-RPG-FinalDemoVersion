using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTransfer : MonoBehaviour
{
	public Health hs;
	public float percentage = 1;

	public void Start ()
	{
		hs = GetComponentInParent<Health> ();
	}

	public void Transfer (int amount)
	{
		SwordNoises ();
		hs.TakeDamage (Mathf.FloorToInt (amount * percentage));
	}

	public void SwordNoises ()
	{
		float noisePicker = Random.value;

		if (noisePicker <= .1) {
			Create.Sound ("Sword 1", gameObject.transform.position);
		} else if (noisePicker > .1 && noisePicker <= .3) {
			Create.Sound ("Sword 2", gameObject.transform.position);
		} else if (noisePicker > .3 && noisePicker <= .5) {
			Create.Sound ("Sword 3", gameObject.transform.position);
		} else if (noisePicker > .5 && noisePicker <= .6) {
			Create.Sound ("Sword 4", gameObject.transform.position);
		} else if (noisePicker > .6 && noisePicker <= .7) {
			Create.Sound ("Sword 5", gameObject.transform.position);
		} else if (noisePicker > .7 && noisePicker <= .9) {
			Create.Sound ("Sword 6", gameObject.transform.position);
		} else {
			Create.Sound ("Sword 7", gameObject.transform.position);
		}
	}

}
