using UnityEngine;
using UnityEngine.UI;
using System;
using Connect.Common;
using System.Collections.Generic;

namespace Connect.Core
{
    public class StageButton : MonoBehaviour
    {
       // [SerializeField] private string _stageName;
        [SerializeField] private Color _stageColor;
        [SerializeField] private int _stageNumber;
        [SerializeField] private Button _button;
       // public LevelData[] level;
       // [SerializeField]
       // private LevelData DefaultLevel;

        private int currentLevel;

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
            GameManager.Instance.GoToGameplay();
           // DefaultLevel = level[0];


        }

    } 
}
