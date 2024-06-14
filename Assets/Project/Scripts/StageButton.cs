using UnityEngine;
using UnityEngine.UI;

namespace Connect.Core
{
    public class StageButton : MonoBehaviour
    {
       // [SerializeField] private string _stageName;
        [SerializeField] private Color _stageColor;
        [SerializeField] private int _stageNumber;
        [SerializeField] private Button _button;

        public Sprite stagename;

        private void Awake()
        {
            _button.onClick.AddListener(ClickedButton);
        }

        private void ClickedButton()
        {
            GameManager.Instance.CurrentStage = _stageNumber;
            GameManager.Instance.StageName = stagename;
            MainMenuManager.Instance.ClickedStage(stagename, _stageColor);
        }

    } 
}
