namespace SearchRanking.CsvRepository.Validators;

public sealed class ScoreFileValidator : IValidator
{
    public bool IsValid(string scoreFile,  ILogger logger)
    {
        if (string.IsNullOrEmpty(scoreFile))
        {
            logger.LogError("Scores file is missing");
            return false;
        }
        
        if (File.Exists(scoreFile))
        {
            logger.LogError("Scores file is already exists: {Path}", scoreFile);
            return false;
        }
        
        return true;
    }   
}