using System.Collections.Generic;

using UnityEngine;

namespace Gameplay.SlotMachine
{
    public class FaceController : MonoBehaviour
    {
        public FaceType faceType;

        public bool _isSpinning;
        public bool _isStopping;

        public float _spinSpeed;

        private Slot _slot;
        private float _stopPoint = 0;

        public void InitFace(Slot slot)
        {
            _slot = slot;
        }

        private void Update()
        {
            _stopPoint = Mathf.Floor(transform.position.y);

            if (_isSpinning)
            {
                transform.Translate(Vector3.down * _spinSpeed * Time.deltaTime, Space.World);


                if (!_isStopping)
                {
                    if (transform.position.y < 0)
                        transform.position += new Vector3(0, Machine.Instance.Faces.Length, 0);
                }
            }

            if (_isStopping)
            {
                if (transform.position.y <= _stopPoint)
                {
                    transform.position = new Vector3(transform.position.x, _stopPoint, transform.position.z);

                    _isSpinning = false;
                    _isStopping = false;
                    _stopPoint = 0;
                }
            }
        }

        public void GetFaceType()
        {
            if (transform.position.y == 3)
            {
                _slot.currentFaceType = faceType;
                Machine.Instance.MidddleFaces.Add(this);
            }
        }
    }
}