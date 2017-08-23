using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallColor : ScriptableObject {

	public int ID {get;};
	public Color Value {get;};
	public bool Unlocked {get; set;} = false;
	public bool Selected {get; set;} = false;

	public BallColor(int id, Color value) {
		ID = id;
		Value = value;
	}
}
