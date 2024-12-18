using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Connect.Core
{
    public class MainMenuManager : MonoBehaviour
    {
        public static MainMenuManager Instance;

        [SerializeField] private GameObject _titlePanel;
        [SerializeField] private GameObject _stagePanel;
        [SerializeField] private GameObject _levelPanel;

        public Sprite currentsprite;

        private void Awake()
        {
            Instance = this;

            _titlePanel.SetActive(true);
            _stagePanel.SetActive(false);
            _levelPanel.SetActive(false);
        }

        public void ClickedPlay()
        {
            _titlePanel.SetActive(false);
            _stagePanel.SetActive(true);
        }

        public void ClickedBackToTitle()
        {
            _titlePanel.SetActive(true);
            _stagePanel.SetActive(false);
        }

        public void ClickedBackToStage()
        {
            _stagePanel.SetActive(true);
            _levelPanel.SetActive(false);
        }

        public UnityAction LevelOpened;

        [HideInInspector]
        public Color CurrentColor;

        [SerializeField]
        private TMP_Text _levelTitleText;
        [SerializeField]
        private Image _levelTitleImage;

        public Image titlename;
        

        public void ClickedStage(Sprite stageName, Color stageColor)
        {
            _stagePanel.SetActive(false);
           // _levelPanel.SetActive(true);
            CurrentColor = Color.white;
            titlename.sprite = stageName;
            _levelTitleImage.color = CurrentColor;
            LevelOpened?.Invoke();
        }
    } 
}

