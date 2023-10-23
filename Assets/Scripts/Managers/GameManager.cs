using Gameplay.Audio;
using UnityEngine;

namespace Gameplay.SlotMachine
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private WebBridge webBridge;
        public WebBridge WebBridge => webBridge;

        [SerializeField] private Machine slotMachinePrefab;
        
        private Machine slotMachine;

        protected override void Awake()
        {
            base.Awake();

            SlotMachine();
        }

        private void SlotMachine()
        {
            slotMachine = Instantiate(slotMachinePrefab);

            Machine.Instance.Init();
        }

        public void SpinSlots()
        {
            slotMachine.StartSlotsSpin();
        }
    }
}