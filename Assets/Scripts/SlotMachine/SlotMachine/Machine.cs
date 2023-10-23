using Gameplay.Audio;
using Gameplay.Input;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.SlotMachine
{
    public class Machine : Singleton<Machine>
    {
        public bool IsSpinning { get; set; }
        public int ReelsSpining { get; set; }
        [field: Header("faces"), SerializeField]
        public List<FaceController> MidddleFaces { get; set; } = new();

        [field: Header("Slots faces"), SerializeField]
        public Face[] Faces { get; private set; }

        [field: SerializeField] public Slot[] Slots { get; private set; }

        [field: Header("Slots Speed Propertis"), SerializeField]
        public float MinSlotSpeed { get; private set; }
        [field: SerializeField] public float MaxSlotSpeed { get; private set; }
        [field: SerializeField] public float SlotSlowdown { get; private set; }
        [field: SerializeField] public float NextSlotSlowdown { get; private set; }

        [Header("Slots Properties")]
        [SerializeField] private int slotsNumber;
        [SerializeField] private Slot slotPrefab;

        private Inputs _inputs;

        protected override void Awake()
        {
            base.Awake();

            _inputs = new Inputs();

            _inputs.Actions.Spin.canceled += _ => StartSlotsSpin();
        }

        private void OnEnable()
        {
            _inputs.Enable();
        }

        public void Init()
        {
            SpawnSlots();
        }
                
        private void SpawnSlots()
        {
            Slots = new Slot[slotsNumber];

            for (int i = 0; i < slotsNumber; i++)
            {
                Slots[i] = Instantiate(slotPrefab);
                Slots[i].InitSlot(i);
            }
        }

        public void FindMatches()
        {
            print("Lookin...");
            if (Slots[0].currentFaceType == FaceType.Apple && Slots[1].currentFaceType == FaceType.Apple && Slots[2].currentFaceType == FaceType.Apple)
            {
                StartCoroutine(ChangeColorWhite());
                AudioManager.Instance.PlayWinSound();
                GameManager.Instance.WebBridge.scoreTotal += 1000;
                print("Apples");
            }
            else if (Slots[1].currentFaceType == FaceType.Bar && Slots[0].currentFaceType == FaceType.Bar && Slots[2].currentFaceType == FaceType.Bar)
            {
                StartCoroutine(ChangeColorWhite());
                AudioManager.Instance.PlayWinSound();
                GameManager.Instance.WebBridge.scoreTotal += 1000;
                print("Bar");
            }
            else if (Slots[0].currentFaceType == FaceType.Bell && Slots[1].currentFaceType == FaceType.Bell && Slots[2].currentFaceType == FaceType.Bell)
            {
                StartCoroutine(ChangeColorWhite());
                AudioManager.Instance.PlayWinSound();
                GameManager.Instance.WebBridge.scoreTotal += 1000;
                print("Bell");
            }
            else if (Slots[1].currentFaceType == FaceType.Bar && Slots[2].currentFaceType == FaceType.Bar)
            {
                GameManager.Instance.WebBridge.scoreTotal += 400;
                print("Small Bar");
            }
            else if (Slots[1].currentFaceType == FaceType.Bell && Slots[2].currentFaceType == FaceType.Bell)
            {
                GameManager.Instance.WebBridge.scoreTotal += 300;
                print("Small Bell");
            }
            else if (Slots[1].currentFaceType == FaceType.Apple && Slots[2].currentFaceType == FaceType.Apple)
            {
                GameManager.Instance.WebBridge.scoreTotal += 200;
                print("Small Apple");
            }

            IsSpinning = false;
            ScoreToWebPage.Instance.SendScoreToWeb();
        }

        private IEnumerator ChangeColorWhite()
        {
            for (int i = 0; i < MidddleFaces.Count; i++)
            {
                MidddleFaces[i].GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            }

            yield return new WaitForSeconds(2);

            for (int i = 0; i < MidddleFaces.Count; i++)
            {
                MidddleFaces[i].GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            }
        }

        public void StartSlotsSpin()
        {
            if (IsSpinning)
                return;

            MidddleFaces.Clear();

            AudioManager.Instance.PlayStartSound();
            IsSpinning = true;
            ReelsSpining = 3;

            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[i].StartSpin();
            }
            StartCoroutine(SlotSlowdownTimer(SlotSlowdown, NextSlotSlowdown));
        }

        private IEnumerator SlotSlowdownTimer(float slowdownStart, float slowdownNext)
        {
            yield return new WaitForSeconds(slowdownStart);

            for (int i = 0; i < slotsNumber; i++)
            {
                Slots[i].StopSpinning();
                yield return new WaitForSeconds(slowdownNext);
            }

            yield break;
        }

        private void OnDisable()
        {
            _inputs.Disable();
        }
    }
}