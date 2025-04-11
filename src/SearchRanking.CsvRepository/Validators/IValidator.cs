namespace SearchRanking.CsvRepository.Validators;

public interface IValidator
{
    bool IsValid(string path, ILogger logger);
}