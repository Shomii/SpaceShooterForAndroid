using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GranicaIgranja {

	public float xMin, xMax, zMin, zMax;
}

public class KontrolerIgraca : MonoBehaviour {

	public float speed;
	public float tilt;
	public GranicaIgranja granicaIgranja;
	public TouchPad touchPad; 

	public GameObject pucanj;
	//public Transform kreiranjeMetka;
	public Transform[] kreiranjeMetaka;
	public float brzinaPucanja;

	private Rigidbody rb;
	private AudioSource zvuk;
	private float sledeciPucanj;
	private Quaternion izracunajKvaternion;

	void Start () {

		rb = GetComponent<Rigidbody>();
		zvuk = GetComponent<AudioSource> ();
		OdrediUbrzanje ();
	}

	void Update(){
		if (Input.GetButton ("Fire1") && Time.time > sledeciPucanj) {
			sledeciPucanj = Time.time + brzinaPucanja;
			foreach (var kreiranjeMetka in kreiranjeMetaka){
			Instantiate (pucanj, kreiranjeMetka.position, kreiranjeMetka.rotation); 
			}
				zvuk.Play ();

		}
	}

	void OdrediUbrzanje () {

		Vector3 pocetnoUbrzanje = Input.acceleration;
		Quaternion kvaternionZaRotiranje = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), pocetnoUbrzanje);
		izracunajKvaternion = Quaternion.Inverse (kvaternionZaRotiranje);
	}

	Vector3 FiksiranoUbrzanje (Vector3 ubrzanje){

		Vector3 fiksiranoUbrzanje = izracunajKvaternion * ubrzanje;
		return fiksiranoUbrzanje;
	}

	void FixedUpdate (){
	
		Vector2 pravac = touchPad.PravacKretanja();
		Vector3 kretanjeBroda = new Vector3 (pravac.x, 0.0f, pravac.y);
		rb.velocity = kretanjeBroda * speed;

		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, granicaIgranja.xMin, granicaIgranja.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, granicaIgranja.zMin, granicaIgranja.zMax)
		);

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}

}

