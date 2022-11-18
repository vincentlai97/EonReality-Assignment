using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;
using InputField = TMPro.TMP_InputField;
using System.Linq;

public class PreviewQuestionDialogController : MonoBehaviour
{
    public GameObject go_question;
    public Text tx_title;
    public InputField if_question;
    public Toggle[] tg_options;
    public Text[] tx_options;
    public Button bt_next;

    public GameObject go_result;
    public Text tx_result;
    public Button bt_return;

    public QuizJson quizJson;
    int questionIndex;
    int total;
    int score;

    void Start()
    {
        bt_next.onClick.AddListener(() =>
        {
            int selectedOption = tg_options.Select((value, index) => (value, index)).First(x => x.value.isOn).index;
            if (selectedOption == quizJson.questions[questionIndex].correctOption)
            {
                score++;
            }

            questionIndex++;
            UpdateUI();
        });

        questionIndex = 0;
        total = quizJson.questions.Count;
        score = 0;

        UpdateUI();

        bt_return.onClick.AddListener(() => Destroy(gameObject));
    }


    void Update()
    {
        bt_next.interactable = tg_options.Where(x => x.isOn).Count() > 0;
    }

    void UpdateUI()
    {
        if (questionIndex < total)
        {
            tx_title.text = $"Question ({questionIndex + 1}/{total})";
            if_question.text = quizJson.questions[questionIndex].question;
            for (int i = 0; i < tx_options.Length; i++)
            {
                tx_options[i].text = quizJson.questions[questionIndex].options[i];
            }
            foreach (Toggle tg in tg_options)
            {
                tg.isOn = false;
            }
            bt_next.GetComponentInChildren<Text>().text = questionIndex == total - 1 ? "Submit" : "Next";
            bt_next.GetComponent<ButtonContentSizeFitter>().forceUpdateCount = 2;
        }
        else
        {
            go_question.SetActive(false);
            go_result.SetActive(true);
            tx_result.text = $"Your score is: {score}/{total}";
        }
    }
}
