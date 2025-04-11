namespace SearchRanking.CsvRepository.Validators;

public sealed class ReviewFileValidator : IValidator
{
    public bool IsValid(string reviewFile, ILogger logger)
    {
        if (string.IsNullOrEmpty(reviewFile))
        {
            logger.LogError("Reviews file is missing");
            return false;
        }
        
        if (File.Exists(reviewFile) == false)
        {
            logger.LogError("Reviews file doesn't exist: {Path}", reviewFile);
            return false;
        }
        
        return true;
    }
}