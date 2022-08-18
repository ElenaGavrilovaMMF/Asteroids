using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Asteroids
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private GameObject GameOverInf;

        public void Init(UnityAction startGame, string buttonText)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonStart.GetComponentInChildren<TMP_Text>().text = buttonText;
        }

        private void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
        }

        public void ActiveGameOverInf(bool isActive)
        {
            GameOverInf.SetActive(isActive);
        }

        public FinalUIView GetFinalView()
        {
            return GameOverInf.GetComponent<FinalUIView>();
        }
    }
}