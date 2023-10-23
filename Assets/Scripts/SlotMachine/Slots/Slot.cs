using Gameplay.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.SlotMachine
{
    public class Slot : MonoBehaviour
    {
        public FaceType currentFaceType;

        private FaceController[] _faces;
        private int _slotIndex;

        public void InitSlot(int slotIndex)
        {
            _slotIndex = slotIndex;

            _faces = new FaceController[Machine.Instance.Faces.Length];

            for (int i = 0; i < Machine.Instance.Faces.Length; i++)
            {
                _faces[i] = Instantiate(Machine.Instance.Faces[i].facePrefab);
                _faces[i].transform.position = new Vector3((float)_slotIndex,i,0);
                _faces[i].transform.parent = this.transform;
                _faces[i].InitFace(this);
            }
        }

        public void StartSpin()
        {
            float speed = Random.Range(Machine.Instance.MinSlotSpeed, Machine.Instance.MaxSlotSpeed);            

            for (int i = 0; i < _faces.Length; i++)
            {
                _faces[i]._spinSpeed = speed;
                _faces[i]._isSpinning = true;
            }
        }

        public void StopSpinning()
        {
            Machine.Instance.ReelsSpining -= 1;
            AudioManager.Instance.PlayStopSound();

            for (int i = 0; i < _faces.Length; i++)
            {
                _faces[i]._isSpinning = true;
                _faces[i]._isStopping = true;
            }

            StartCoroutine(GetFaceAfter(.3f));
        }

        public IEnumerator GetFaceAfter(float t)
        {
            yield return new WaitForSeconds(t);

            for (int i = 0; i < _faces.Length; i++)
            {
                _faces[i].GetFaceType();
            }

            if(Machine.Instance.ReelsSpining == 0)
            {
                Machine.Instance.FindMatches();
            }
        }
    }
}