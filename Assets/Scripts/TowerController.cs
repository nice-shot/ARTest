using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

    public AudioClip dissappearClip;
    public float maxTimeToDissappear;
    private AudioSource audioSource;
    private MeshRenderer mesh;

    private const float DISSAPEAR_SOUND_WAIT = 2f;

	void Awake () {
        mesh = GetComponentInChildren<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
        // Set random pitch to add variance when building rises
        audioSource.pitch = Random.Range(0.7f, 1.6f);
	}

    public void Disappear() {
        // Each tower will dissappear at a slightly different time
        float wait = Random.Range(0f, maxTimeToDissappear);
        StartCoroutine(Remove(wait));
    }

    private IEnumerator Remove(float wait) {
        yield return new WaitForSeconds(wait);
        audioSource.clip = dissappearClip;
        audioSource.Play();
        mesh.enabled = false;
        // Wait a few seconds for the sound to finish playing
        Destroy(this.gameObject, DISSAPEAR_SOUND_WAIT);
    }
}
