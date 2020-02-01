﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComputerType {
	Mac,
	Windows,
	AppleIIe,
	Commodore64
};

public class Computer : MonoBehaviour
{
	public enum ComputerState {
		WaitingToCrash,
		Crashed,
		SafelyOccupied,
		Exploding
	}

	public int computerID;
	public ComputerType computerType;
	public ComputerState _state;
	private float _crashTimer;

    // Start is called before the first frame update
    void Start()
    {
		if (computerID == 0) {
			computerID = Random.Range (0, 100);
		}
		computerType =  (ComputerType)Random.Range (0, System.Enum.GetValues(typeof(ComputerType)).Length);
		_state = ComputerState.WaitingToCrash;
		_crashTimer = Random.Range (3f, 20f);
    }

	private void OnTriggerEnter (Collider other)
	{
		// If we've already clicked on the computer before being in range
		if (other.gameObject.layer == LayerMask.NameToLayer("Player") ) {
			if (other.gameObject.GetComponent<Player> ().targettedComputer == computerID) {
				rebootComputer ();
			}
		}
	}


	private void crashComputer ()
	{
		SetComputerColour (Color.magenta);
		_crashTimer = Random.Range (10f, 20f);

	}

	private void SetComputerColour (Color colour)
	{
		this.gameObject.GetComponent<Renderer> ().material.color = colour;

	}

	public void rebootComputer ()
	{
		if (_state == ComputerState.Crashed) {
			_state = ComputerState.WaitingToCrash;
			_crashTimer = Random.Range (3f, 20f);

			SetComputerColour (Color.white);
		}
	}

    // Update is called once per frame
    void Update()
    {
		_crashTimer -= Time.deltaTime;

		if (_crashTimer < 0) {
			if (_state == ComputerState.WaitingToCrash) {
				_state = ComputerState.Crashed;
				crashComputer ();
			} else if (_state == ComputerState.Crashed) {
				_state = ComputerState.Exploding;
				SetComputerColour (Color.black);
			}
		}
    }
}
