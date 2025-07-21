using DiContainer;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sorter.UI
{
    public class LosePanel : BasePanel
    {
        [field: SerializeField] public Button RestartButton { get; private set; }
        
        [MonoConstructor]
        public void Construct()
        {
            RestartButton.onClick.AddListener(RestartPressed);
        }
        
        private void RestartPressed()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}