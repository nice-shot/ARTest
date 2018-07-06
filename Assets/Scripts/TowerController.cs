using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

    private AudioSource audioSource;

	void Awake () {
        audioSource = GetComponent<AudioSource>();
        // Set random pitch to add variance when building rises
        audioSource.pitch = Random.Range(0.7f, 1.6f);
	}
}
