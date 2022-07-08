using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TMP_Text scoreText;
    public Button rebuildButton;
    public TMP_InputField gridSizeInputField;

    private int score = 0;

    public delegate void OnRebuildButtonClick(int count);
    public static event OnRebuildButtonClick onRebuildButtonClick;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        scoreText.text = "Score: " + score;

        rebuildButton.onClick.AddListener(() =>
        {
            if (int.TryParse(gridSizeInputField.text, out int size))
            {
                if(size > 50)
                    onRebuildButtonClick(50);
                else
                    onRebuildButtonClick(size);

                gridSizeInputField.text = "";
                score = 0;
                scoreText.text = "Score: " + score;

                Taptic.Light();
                SoundManager.Instance.PlaySound(SoundTrigger.Click);
            }
        });
    }

    public void IncreaseScoreCount(int _addition)
    {
        score += _addition;
        scoreText.text = "Score: " + score;
    }
}
