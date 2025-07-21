using UnityEngine;

namespace Sorter.Infrastructure
{
    public class MonoInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private bool _installInAwake;

        private void Awake()
        {
            if(_installInAwake)
                Install();
        }

        public virtual void Install()
        {
            
        }
    }
}