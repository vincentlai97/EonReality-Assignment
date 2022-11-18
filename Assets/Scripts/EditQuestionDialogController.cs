using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;
using InputField = TMPro.TMP_InputField;
using System.Linq;

public class EditQuestionDialogController : MonoBehaviour
{
    public Text tx_title;
    public InputField if_question;
    public InputField[] if_options;
    public Toggle[] tg_options;
    public Button bt_save;
    public Button bt_discard;

    public void Initialize(string title, QuestionJson qj, UnityEngine.Events.UnityAction<QuestionJson> onSave)
    {
        tx_title.text = title;
        if_question.text = qj.question;
        for (int i = 0; i < if_options.Length; i++)
        {
            if_options[i].text = qj.options[i];
            tg_options[i].SetIsOnWithoutNotify(qj.correctOption == i);
        }
        bt_save.onClick.AddListener(() =>
        {
            onSave.Invoke(GetQuestionJson());
            Destroy(gameObject);
        });
        bt_discard.onClick.AddListener(() => Destroy(gameObject));
    }

    public QuestionJson GetQuestionJson()
    {
        return new QuestionJson
        {
            question = if_question.text,
            options = if_options.Select(x => x.text).ToArray(),
            correctOption = tg_options.Select((value, index) => (value, index)).Where(x => x.value.isOn).Select(x => x.index).First()
        };
    }
}
