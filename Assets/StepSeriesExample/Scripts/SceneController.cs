using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    OSC osc;
    [SerializeField]
    InputField _ipInput;
    [SerializeField]
    InputField _sendPortInput;
    [SerializeField]
    InputField _receivePortInput;
    [SerializeField]
    Button _openButton;
    [SerializeField]
    Button _closeButton;
    [SerializeField]
    Button _setDestIpButton;

    [SerializeField]
    Dropdown _kvalTargetDropdown;
    [SerializeField]
    InputField _kvalHoldInput;
    [SerializeField]
    InputField _kvalRunInput;
    [SerializeField]
    InputField _kvalAccInput;
    [SerializeField]
    InputField _kvalDecInput;
    [SerializeField]
    Button _getKvalButton;
    [SerializeField]
    Button _setKvalButton;

    [SerializeField]
    Dropdown _speedTargetDropdown;
    [SerializeField]
    InputField _speedAccInput;
    [SerializeField]
    InputField _speedDecInput;
    [SerializeField]
    InputField _speedMaxInput;
    [SerializeField]
    Button _getSpeedButton;
    [SerializeField]
    Button _setSpeedButton;

    [SerializeField]
    Dropdown _runTargetDropdown;
    [SerializeField]
    InputField _runInput;
    [SerializeField]
    Button _runButton;
    [SerializeField]
    Button _softHiZButton;

    [SerializeField]
    Dropdown _goToTargetDropdown;
    [SerializeField]
    InputField _goToInput;
    [SerializeField]
    Button _goToButton;


    [SerializeField]
    Text _sendMessageText;
    [SerializeField]
    Text _receiveMessageText;

    const int TARGET_ALL = 255;

    // Start is called before the first frame update
    void Start()
    {
        _openButton.onClick.AddListener(Open);
        _closeButton.onClick.AddListener(Close);
        _setDestIpButton.onClick.AddListener(SendSetDestIp);
        _setKvalButton.onClick.AddListener(SetKval);
        _getKvalButton.onClick.AddListener(GetKval);
        _setSpeedButton.onClick.AddListener(SetSpeedProfile);
        _getSpeedButton.onClick.AddListener(GetSpeedProfile);
        _runButton.onClick.AddListener(Run);
        _softHiZButton.onClick.AddListener(SoftHiZ);
        _goToButton.onClick.AddListener(GoTo);

        osc.SetAllMessageHandler(ReceiveOsc);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Open() {
        osc.outIP = _ipInput.text;
        osc.outPort = int.Parse(_sendPortInput.text);
        osc.inPort = int.Parse(_receivePortInput.text);
        osc.Open();
    }

    void Close() {
        osc.Close();
    }

    void SendSetDestIp() {
        OscMessage oscMessage = new OscMessage();
        oscMessage.address = "/setDestIp";
        SendOsc(oscMessage);
    }

    void SetKval() {
        OscMessage oscMessage = new OscMessage();
        oscMessage.address = "/setKval";
        int target = _kvalTargetDropdown.value;
        if (target == 0) {
            target = TARGET_ALL;
        }
        oscMessage.values.Add(target);
        oscMessage.values.Add(int.Parse(_kvalHoldInput.text));
        oscMessage.values.Add(int.Parse(_kvalRunInput.text));
        oscMessage.values.Add(int.Parse(_kvalAccInput.text));
        oscMessage.values.Add(int.Parse(_kvalDecInput.text));
        SendOsc(oscMessage);
    }

    void GetKval() {
        OscMessage oscMessage = new OscMessage();
        oscMessage.address = "/getKval";

        int target = _kvalTargetDropdown.value;
        if (target == 0) {
            target = TARGET_ALL;
        }
        oscMessage.values.Add(target);
        SendOsc(oscMessage);
    }

    void SetSpeedProfile() {
        OscMessage oscMessage = new OscMessage();
        oscMessage.address = "/setSpeedProfile";
        int target = _speedTargetDropdown.value;
        if (target == 0) {
            target = TARGET_ALL;
        }
        oscMessage.values.Add(target);
        oscMessage.values.Add(float.Parse(_speedAccInput.text));
        oscMessage.values.Add(float.Parse(_speedDecInput.text));
        oscMessage.values.Add(float.Parse(_speedMaxInput.text));
        SendOsc(oscMessage);
    }

    void GetSpeedProfile() {
        OscMessage oscMessage = new OscMessage();
        oscMessage.address = "/getSpeedProfile";

        int target = _speedTargetDropdown.value;
        if (target == 0) {
            target = TARGET_ALL;
        }
        oscMessage.values.Add(target);
        SendOsc(oscMessage);
    }

    void Run() {
        OscMessage oscMessage = new OscMessage();
        oscMessage.address = "/run";
        int target = _runTargetDropdown.value;
        if (target == 0) {
            target = TARGET_ALL;
        }
        oscMessage.values.Add(target);
        oscMessage.values.Add(float.Parse(_runInput.text));
        SendOsc(oscMessage);
    }

    void SoftHiZ() {
        OscMessage oscMessage = new OscMessage();
        oscMessage.address = "/softHiZ";
        int target = _runTargetDropdown.value;
        if (target == 0) {
            target = TARGET_ALL;
        }
        oscMessage.values.Add(target);
        SendOsc(oscMessage);
    }

    void GoTo() {
        OscMessage oscMessage = new OscMessage();
        oscMessage.address = "/goTo";
        int target = _goToTargetDropdown.value;
        if (target == 0) {
            target = TARGET_ALL;
        }
        oscMessage.values.Add(target);
        oscMessage.values.Add(int.Parse(_goToInput.text));
        SendOsc(oscMessage);
    }

    void SendOsc(OscMessage oscMessage) {
        PrintMessage(_sendMessageText, oscMessage);
        osc.Send(oscMessage);
    }

    void ReceiveOsc(OscMessage oscMessage) {
        PrintMessage(_receiveMessageText, oscMessage);
    }

    void PrintMessage(Text text, OscMessage oscMessage) {
        string newText = oscMessage.ToString();
        string[] texts = text.text.Split(Environment.NewLine.ToCharArray());
        
        text.text = newText + "\n";
        for (int i = 0; i < 15 && i < texts.Length; i ++) {
            text.text += texts[i] + "\n";
        }
    }
}
