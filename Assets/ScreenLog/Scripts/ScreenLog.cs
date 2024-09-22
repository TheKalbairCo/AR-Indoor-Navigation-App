using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NeptunDigital
{
    public class ScreenLog : MonoBehaviour
    {
        public static ScreenLog Instance { get; set; }

        [Header("Log Settings")]
        public LogPosition logPosition = LogPosition.Top;
        float logSize = .35f;
        int logLength = 30;
        int logFontSize = 15;
        float logOpacity = .3f;
        public Font logFont;
        public bool buttonAlwaysOnScreen = false;
        [Space(15)]
        public float minLogSize = .1f;
        public float maxLogSize = .7f;
        [Space(15)]
        public int minLogLength = 10;
        public int maxLogLength = 500;
        [Space(15)]
        public int minFontSize = 10;
        public int maxFontSize = 20;
        [Space(15)]
        public bool checkRotation;

        [Header("Color Settings")]
        [Space(10)]
        public Color logBackgroundColor = new Color(1, 1, 1, .3f);
        [Space(10)]
        public Color debugTextColor;
        [Space(10)]
        public Color warningTextColor;
        [Space(10)]
        public Color errorTextColor;

        [Header("Swipe Settings")]
        public float SWIPE_THRESHOLD = 20f;
        public Swipe swipe = Swipe.UP;
        Vector2 fingerDown;
        Vector2 fingerUp;

        [Header("Log Line Prefab")]
        public GameObject logPrefab;

        [Header("Other")]
        public Transform logParent;
        public Image logBackground;
        public Image buttonsBackground;
        public Transform log;
        public Transform canvasTransform;
        public GameObject buttons;
        public GameObject settings;
        public Image logButtonImage;
        public Image buttonPlayImage;
        public Image settingsButtonImage;
        public Scrollbar scrollbar;
        public ScrollRect scrollRect;
        public Text logSizeValue, logLengthValue, logFontValue, logOpacityValue;
        public Slider fontSizeSlider, logLengthSlider, logSizeSlider, logOpacitySlider;
        public Dropdown dropdownPosition;
        public Toggle toggle;
        public List<GameObject> textList;

        GameObject tempObject;
        bool logging = true;
        bool buttonsActive;
        bool buttonsAnimation;
        int currentLogIndex = 0;
        ScreenOrientation orientation;
        ScreenOrientation tempOrientation = ScreenOrientation.PORTRAIT;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            if (checkRotation)
                StartCoroutine(CheckRotation(.3f));
            else
                CheckOrientation();

            SetLogFont();
            LoadPreviousValues();
            InitLog();
        }

        void Update()
        {
            if (!buttonAlwaysOnScreen)
            {
                CheckSwipe();

                if (!buttonsAnimation && !settings.activeSelf)
                {
                    buttonsAnimation = true;
                    StartCoroutine(ChangeButtonState());
                }
            }
        }

        //set the font for all text components on the gameobject's childrens
        void SetLogFont()
        {
            foreach (var item in gameObject.GetComponentsInChildren<Text>())
            {
                item.font = logFont;
            }
        }

        //check the screen orientation depends on the screen height and width
        void CheckOrientation()
        {
            if (Screen.width > Screen.height)
            {
                orientation = ScreenOrientation.LANDSCAPE;
            }
            else
            {
                orientation = ScreenOrientation.PORTRAIT;
            }

            SwitchButtonsPosition();
        }

        //before setting log position, set the enum from dropdown value
        public void ChangePosition()
        {
            int tmp = dropdownPosition.value;

            if (tmp == 0)
                logPosition = LogPosition.Top;
            else if (tmp == 1)
                logPosition = LogPosition.Bottom;
            else if (tmp == 2)
                logPosition = LogPosition.Left;
            else if (tmp == 3)
                logPosition = LogPosition.Right;

            SwitchPosition();
        }

        //log initialization, set colors, texts, sliders, etc...
        void InitLog()
        {
            buttonsAnimation = false;
            swipe = Swipe.UP;
            buttonsActive = true;
            logBackground.color = logBackgroundColor;
            buttonsBackground.color = logBackgroundColor;
            buttonPlayImage.color = Color.red;

            SwitchPosition();
  
            logSizeSlider.value = logSize;
            fontSizeSlider.value = logFontSize;
            logLengthSlider.value = logLength;
            logSizeValue.text = Mathf.RoundToInt(logSize * 100) + "%";
            logFontValue.text = logFontSize.ToString();
            logLengthValue.text = logLength.ToString();
            logOpacityValue.text = System.Math.Round(logOpacity, 2).ToString();

            logSizeSlider.minValue = minLogSize;
            logSizeSlider.maxValue = maxLogSize;
            logLengthSlider.minValue = minLogLength;
            logLengthSlider.maxValue = maxLogLength;
            fontSizeSlider.minValue = minFontSize;
            fontSizeSlider.maxValue = maxFontSize;
        }

        //load previous log values or set the default ones
        void LoadPreviousValues()
        {
            dropdownPosition.value = PlayerPrefs.GetInt("LogPosition", 0);
            logLength = PlayerPrefs.GetInt("LogLength", (maxLogLength + minLogLength) / 2);
            logFontSize = PlayerPrefs.GetInt("FontSize", (maxFontSize + minFontSize) / 2);
            logSize = PlayerPrefs.GetFloat("LogSize", (maxLogSize + minLogSize) / 2);
            logSizeSlider.value = logSize;
            fontSizeSlider.value = logFontSize;
            logLengthSlider.value = logLength;
            logOpacity = PlayerPrefs.GetFloat("LogOpacity", .3f);
            logBackground.color = new Color(logBackground.color.r, logBackground.color.g, logBackground.color.b, logOpacity);
            buttonsBackground.color = new Color(buttonsBackground.color.r, buttonsBackground.color.g, buttonsBackground.color.b, logOpacity);
            buttonAlwaysOnScreen = PlayerPrefs.GetInt("LogButtons", 0) == 1;
            toggle.isOn = buttonAlwaysOnScreen;
        }

        //switch buttons position depends on the screen orientation
        void SwitchButtonsPosition()
        {
            if (orientation == tempOrientation)
                return;
            else
                tempOrientation = orientation;

            //update the log size, because sometimes when you rotate device, log disapear from the screen (bug with autorotation)
            ChangeLogSize();

            switch (orientation)
            {
                case ScreenOrientation.PORTRAIT:
                    buttons.GetComponent<RectTransform>().anchorMin = new Vector2(.5f, 0);
                    buttons.GetComponent<RectTransform>().anchorMax = new Vector2(.5f, 0);
                    buttons.GetComponent<RectTransform>().pivot = new Vector2(.5f, 0);
                    buttons.GetComponent<RectTransform>().offsetMax = new Vector2(105, 70);
                    buttons.GetComponent<RectTransform>().offsetMin = new Vector2(-105, 0);
                    buttonsActive = true;
                    break;
                case ScreenOrientation.LANDSCAPE:
                    buttons.GetComponent<RectTransform>().anchorMin = new Vector2(0, .5f);
                    buttons.GetComponent<RectTransform>().anchorMax = new Vector2(0, .5f);
                    buttons.GetComponent<RectTransform>().pivot = new Vector2(0, .5f);
                    buttons.GetComponent<RectTransform>().offsetMax = new Vector2(70, 105);
                    buttons.GetComponent<RectTransform>().offsetMin = new Vector2(0, -105);
                    buttonsActive = true;
                    break;
            }
        }

        //switch the log position on the screen
        void SwitchPosition()
        {
            switch (logPosition)
            {
                case LogPosition.Top:
                    log.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
                    log.GetComponent<RectTransform>().offsetMin = new Vector2(0, canvasTransform.GetComponent<RectTransform>().rect.height - (canvasTransform.GetComponent<RectTransform>().rect.height * logSize));
                    break;
                case LogPosition.Bottom:
                    log.GetComponent<RectTransform>().offsetMax = new Vector2(0, (canvasTransform.GetComponent<RectTransform>().rect.height * logSize) - canvasTransform.GetComponent<RectTransform>().rect.height);
                    log.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
                    break;
                case LogPosition.Left:
                    log.GetComponent<RectTransform>().offsetMax = new Vector2((canvasTransform.GetComponent<RectTransform>().rect.width * logSize) - canvasTransform.GetComponent<RectTransform>().rect.width, 0);
                    log.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
                    break;
                case LogPosition.Right:
                    log.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
                    log.GetComponent<RectTransform>().offsetMin = new Vector2(canvasTransform.GetComponent<RectTransform>().rect.width - (canvasTransform.GetComponent<RectTransform>().rect.width * logSize), 0);
                    break;
            }

            PlayerPrefs.SetInt("LogPosition", dropdownPosition.value);
            StartCoroutine(ScrollToBottom());
        }

        //hide or show the log
        public void ShowHideLog()
        {
            if (log.gameObject.activeSelf)
            {
                log.gameObject.SetActive(false);
                logButtonImage.color = Color.green;
            }
            else
            {
                log.gameObject.SetActive(true);
                logButtonImage.color = Color.red;
            }
        }

        //change the log size depend on the slider value
        public void ChangeLogSize()
        {
            logSize = logSizeSlider.value;
            logSizeValue.text = Mathf.RoundToInt(logSize * 100) + "%";
            PlayerPrefs.SetFloat("LogSize", logSize);
            SwitchPosition();
        }

        //change number of the text objects in the texts list 
        public void ChangeLogLength()
        {
            logLength = (int)logLengthSlider.value;
            logLengthValue.text = logLength.ToString();
            PlayerPrefs.SetInt("LogLength", logLength);
            UpdateLogLength();
        }

        //change the log font size
        public void ChangeLogFontSize()
        {
            logFontSize = (int)fontSizeSlider.value;
            logFontValue.text = logFontSize.ToString();
            PlayerPrefs.SetInt("FontSize", logFontSize);
            UpdateFontSize();
        }

        //change the log opacity
        public void ChangeLogOpacity()
        {
            logOpacity = logOpacitySlider.value;
            logBackground.color = new Color(logBackground.color.r, logBackground.color.g, logBackground.color.b, logOpacity);
            buttonsBackground.color = new Color(buttonsBackground.color.r, buttonsBackground.color.g, buttonsBackground.color.b, logOpacity);
            logOpacityValue.text = System.Math.Round(logOpacity, 2).ToString();
            PlayerPrefs.SetFloat("LogOpacity", logOpacity);
        }

        //show or hide the sittings popup
        public void ShowHideSettings()
        {
            if (settings.activeSelf)
            {
                settings.SetActive(false);
                settingsButtonImage.color = Color.green;
            }
            else
            {
                settings.SetActive(true);
                settingsButtonImage.color = Color.red;
            }
        }

        //update the number of text objects in the texts list
        void UpdateLogLength()
        {
            if (logLength == textList.Count)
                return;

            ClearLog();

            if (logLength < textList.Count)
            {
                int difference = textList.Count - logLength;

                for (int i = 0; i < difference; i++)
                {
                    Destroy(textList[0].gameObject);
                    textList.RemoveAt(0);
                }
            }
            else if (logLength > textList.Count)
            {
                int difference = logLength - textList.Count;

                for (int i = 0; i < difference; i++)
                {
                    tempObject = Instantiate(logPrefab, logParent);
                    textList.Add(tempObject);
                    tempObject.SetActive(false);
                }
            }
        }

        //change the font size for all text objects in the log
        void UpdateFontSize()
        {
            for (int i = 0; i < textList.Count; i++)
            {
                textList[i].GetComponent<Text>().fontSize = logFontSize;
            }
        }

        /// <summary>
        /// Main method for sending the messages to the screen log.
        /// </summary>
        public void SendEvent(TextType logType, string message)
        {
            if (!logging || textList.Count == 0)
                return;

            GameObject tmp;
            currentLogIndex++;
            if (currentLogIndex > textList.Count)
            {
                tmp = textList[0];
                textList.RemoveAt(0);
                textList.Add(tmp);
                logParent.GetChild(0).SetAsLastSibling();
            }
            else
            {
                tmp = textList[currentLogIndex - 1];
            }

            tmp.SetActive(true);
            tmp.GetComponent<Text>().font = logFont;
            tmp.GetComponent<Text>().fontSize = logFontSize;

            if (logType == TextType.Warning)
            {
                tmp.GetComponent<Text>().color = warningTextColor;
                tmp.GetComponent<Text>().text = currentLogIndex + ". Warning: " + message;
            }
            else if (logType == TextType.Error)
            {
                tmp.GetComponent<Text>().color = errorTextColor;
                tmp.GetComponent<Text>().text = currentLogIndex + ". Error: " + message;
            }
            else
            {
                tmp.GetComponent<Text>().color = debugTextColor;
                tmp.GetComponent<Text>().text = currentLogIndex + ". Debug: " + message;
            }

            StartCoroutine(ScrollToBottom());
        }

        //if you set the boolean checkRotation the IEnumerator check that every .3 seconds
        IEnumerator CheckRotation(float delay)
        {
            while (true)
            {
                CheckOrientation();
                yield return new WaitForSecondsRealtime(delay);
            }
        }

        //scroll log to the bottom
        IEnumerator ScrollToBottom()
        {
            yield return new WaitForEndOfFrame();
            scrollRect.verticalNormalizedPosition = 0;
        }

        //show or hide the buttons
        IEnumerator ChangeButtonState()
        {
            if (!buttonsActive && swipe == Swipe.UP && orientation == ScreenOrientation.PORTRAIT)
            {
                buttons.GetComponent<Animator>().Play("ButtonsShowPortrait");
                buttonsActive = true;
            }

            if (buttonsActive && swipe == Swipe.DOWN && orientation == ScreenOrientation.PORTRAIT)
            {
                buttons.GetComponent<Animator>().Play("ButtonsHidePortrait");
                buttonsActive = false;
            }

            if (!buttonsActive && swipe == Swipe.RIGHT && orientation == ScreenOrientation.LANDSCAPE)
            {
                buttons.GetComponent<Animator>().Play("ButtonsShowLandscape");
                buttonsActive = true;
            }

            if (buttonsActive && swipe == Swipe.LEFT && orientation == ScreenOrientation.LANDSCAPE)
            {
                buttons.GetComponent<Animator>().Play("ButtonsHideLandscape");
                buttonsActive = false;
            }

            yield return new WaitForSeconds(.3f);
            buttonsAnimation = false;

            if (buttonsActive && logging)
                buttons.GetComponent<Animator>().Play("ButtonsRecording");
        }

        //enable or disable the logging
        public void EnableDisableLogging()
        {
            if (logging)
            {
                logging = false;
                buttonPlayImage.color = Color.green;
                buttons.GetComponent<Animator>().Play("Idle");
            }
            else
            {
                logging = true;
                buttonPlayImage.color = Color.red;
                buttons.GetComponent<Animator>().Play("ButtonsRecording");
            }
        }

        //if you check the button always on the screen
        public void ButtonAlwaysOnScreen()
        {
            if (toggle.GetComponent<Toggle>().isOn)
            {
                buttonAlwaysOnScreen = true;
                PlayerPrefs.SetInt("LogButtons", 1);

                if (!buttonsActive)
                {
                    if (orientation == ScreenOrientation.PORTRAIT)
                        swipe = Swipe.UP;
                    else if (orientation == ScreenOrientation.LANDSCAPE)
                        swipe = Swipe.RIGHT;

                    buttonsAnimation = true;
                    StartCoroutine(ChangeButtonState());
                }
            }
            else
            {
                buttonAlwaysOnScreen = false;
                PlayerPrefs.SetInt("LogButtons", 0);
            }
        }

        //clear the log (also resets the counter)
        public void ClearLog()
        {
            for (int i = 0; i < textList.Count; i++)
            {
                textList[i].SetActive(false);
                textList[i].GetComponent<Text>().text = "";
            }

            currentLogIndex = 0;
        }

        //copy log to the system copy buffer
        public void CopyLog()
        {
            string tempLog = "";

            for (int i = 0; i < textList.Count; i++)
            {
                if (textList[i].activeSelf)
                {
                    tempLog += textList[i].GetComponent<Text>().text + "\n";
                }
            }

            GUIUtility.systemCopyBuffer = tempLog;
        }

        #region SWIPE CHECK
        //check for the swipes
        void CheckSwipe()
        {
            if (Input.GetMouseButtonDown(0))
            {
                fingerUp = Input.mousePosition;
                fingerDown = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                fingerDown = Input.mousePosition;
                CheckDirection();
            }
        }

        //check the swipe direction
        void CheckDirection()
        {
            if (VerticalMove() > SWIPE_THRESHOLD && VerticalMove() > HorizontalValMove())
            {
                if (fingerDown.y - fingerUp.y > 0)
                {
                    swipe = Swipe.UP;
                }
                else if (fingerDown.y - fingerUp.y < 0)
                {
                    swipe = Swipe.DOWN;
                }
                fingerUp = fingerDown;
            }
            else if (HorizontalValMove() > SWIPE_THRESHOLD && HorizontalValMove() > VerticalMove())
            {
                if (fingerDown.x - fingerUp.x > 0)
                {
                    swipe = Swipe.RIGHT;
                }
                else if (fingerDown.x - fingerUp.x < 0)
                {
                    swipe = Swipe.LEFT;
                }
                fingerUp = fingerDown;
            }
            else
            {
                swipe = Swipe.NONE;
            }
        }

        float VerticalMove()
        {
            return Mathf.Abs(fingerDown.y - fingerUp.y);
        }

        float HorizontalValMove()
        {
            return Mathf.Abs(fingerDown.x - fingerUp.x);
        }
        #endregion
    }
}