using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

	public float neznoKretanje;

	private Vector2 pocetak;
	private Vector2 pravac;
	private Vector2 pravacNeznogKretanja;
	private bool prstNaEkranu;
	private int idPrstaNaEkranu;

	void Awake () {
	
		pravac = Vector2.zero;
		prstNaEkranu = false;
	}

public void OnPointerDown (PointerEventData datoteka){
	
	if (!prstNaEkranu){
		prstNaEkranu = true;
		idPrstaNaEkranu = datoteka.pointerId;
		pocetak = datoteka.position;
	}
}

	public void OnDrag (PointerEventData datoteka){
		if (datoteka.pointerId == idPrstaNaEkranu){
			Vector2 trenutnaPozicija = datoteka.position;
			Vector2 pocetniPravac = trenutnaPozicija - pocetak;
			pravac = pocetniPravac.normalized;
				Debug.Log (pravac);
		}
}

	public void OnPointerUp (PointerEventData datoteka){
		if (datoteka.pointerId == idPrstaNaEkranu){
			pravac = Vector2.zero;
			prstNaEkranu = false;
		}
}
	
	public Vector2 PravacKretanja (){

		pravacNeznogKretanja = Vector2.MoveTowards(pravacNeznogKretanja, pravac, neznoKretanje);
		return pravacNeznogKretanja;
	}

}
