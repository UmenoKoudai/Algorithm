using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : InstanceSystem<GameManager>
{
    Text _stageText;
    PlayableDirector _director;
    int _stageNumber = 1;
    bool _isGame;

    public int StageNumber { get => _stageNumber; set => _stageNumber = value; }
    public bool IsGame { get => _isGame; }

    private void Awake()
    {
        base.Awake();
    }
    private void Update()
    {
        if (_stageText == null)
        {
            _stageText = FindObjectOfType<Text>();
            _stageText.text = $"{_stageNumber}ŠK‘w";
        }
        if (_director == null)
        {
            _director = FindObjectOfType<PlayableDirector>();
            _director.stopped += DirectorStop;
        }
    }

    private void OnDisable()
    {
        _director.stopped -= DirectorStop;
    }

    void DirectorStop(PlayableDirector director)
    {
        if (_director == director)
        {
            _isGame = true;
            _director.gameObject.SetActive(false);
        }
    }
}
