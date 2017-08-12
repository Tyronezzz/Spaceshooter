using UnityEngine;
using System.Collections;

public class Done_DestroyByTime : MonoBehaviour
{
	public float lifetime;           // VFX Explosion

	void Start ()
	{
        Destroy (gameObject, lifetime);
	}
}
