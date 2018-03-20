using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KontrolerIgre : MonoBehaviour {

	public GameObject[] opasnostiZaIgraca;
	public Vector3 parametriAsteroida;
	public int brojNaleta;
	public float pauzaUKreiranjuNaleta;
	public float pocetakPauze;
	public float pauzaUNaletima;
	public Text rezultatTekst;
	public Text krajIgreTekst;
	public GameObject dugmeZaRestart;

	private int rezultat;
	private bool krajIgre;
	private bool restartujIgru;
	


	void Start () {
		krajIgre = false;
		restartujIgru = false;
		dugmeZaRestart.SetActive (false);
		krajIgreTekst.text = "";
		rezultat = 0;
		AzuriranjeRezultata ();
		StartCoroutine (NaletiOpasnosti ());
	}

	IEnumerator NaletiOpasnosti () {
		yield return new WaitForSeconds (pocetakPauze);
		while (true) {
			for (int i = 0; i < brojNaleta; i++) {
				GameObject opanostZaIgraca = opasnostiZaIgraca[Random.Range(0,opasnostiZaIgraca.Length)];
				Vector3 pozicijaStvaranja = new Vector3 (Random.Range (-parametriAsteroida.x, parametriAsteroida.x), parametriAsteroida.y, parametriAsteroida.z);
				Quaternion pozicijaRotiranja = Quaternion.identity;
				Instantiate (opanostZaIgraca, pozicijaStvaranja, pozicijaRotiranja);
				yield return new WaitForSeconds (pauzaUKreiranjuNaleta);
			}
			yield return new WaitForSeconds (pauzaUNaletima);

			if (krajIgre){
				dugmeZaRestart.SetActive (true);
				restartujIgru = true;
				break;
			}
		} 
	}

	public void DodavanjeRezultata (int noviRezultat){
	
		rezultat += noviRezultat;
		AzuriranjeRezultata ();
	}

	void AzuriranjeRezultata (){
	
		rezultatTekst.text = "Rezultat: " + rezultat;
	}

	public void KrajIgre () {
	
		krajIgreTekst.text = "Kraj igre !";
		krajIgre = true;
	}

	public void RestartujIgru (){

		Application.LoadLevel (Application.loadedLevel);
	}
}
