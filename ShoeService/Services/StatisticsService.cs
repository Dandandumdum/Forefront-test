namespace ShoeService.Services;

/// <summary>
/// Fake implementation of statistics service
/// </summary>
public class StatisticsService
{
    private readonly Dictionary<string, int> _statistics = new();

    public async Task RegisterStatistic(string key, int value)
    {
        // No thread safety for now
        if (_statistics.ContainsKey(key))
        {
            _statistics[key] += value;
        }   
        else
        {
            _statistics[key] = value;
        }

        await Task.CompletedTask;
    }

    public async Task<int?> GetStatistic(string key)
    {
        return await Task.FromResult(_statistics.GetValueOrDefault(key, 0));
    }
}