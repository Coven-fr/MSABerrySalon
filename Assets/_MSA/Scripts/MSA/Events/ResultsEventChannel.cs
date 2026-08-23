using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ResultsEvent", menuName = "Coven/Events/ResultsEvent")]
public class ResultsEventChannel : ScriptableObject
{
    public UnityAction<ResultsData> onResults;
    
    public void ShowResults(ResultsData results) => onResults?.Invoke(results);
}
