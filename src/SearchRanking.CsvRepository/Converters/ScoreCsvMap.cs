using CsvHelper.Configuration;
using SearchRanking.CsvModels;

namespace SearchRanking.CsvRepository.Converters;

public sealed class ScoreCsvMap : ClassMap<ScoreCsv>
{
    public ScoreCsvMap()
    {
        Map(s => s.Email);
        Map(s => s.Name);
        Map(s => s.ProfileScore).TypeConverterOption.Format("0.00");
        Map(s => s.RatingsScore).TypeConverterOption.Format("0.00");
        Map(s => s.SearchScore).TypeConverterOption.Format("0.00");
    }
}