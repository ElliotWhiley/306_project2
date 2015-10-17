using UnityEngine;
using System;

public class ReSkinAnimation : MonoBehaviour {

	public Animation anim;
	void Start() {
		anim = GetComponent<Animation>();
		foreach (AnimationState state in anim) {
			state.speed = 0.5F;
		}
	}

	
}
