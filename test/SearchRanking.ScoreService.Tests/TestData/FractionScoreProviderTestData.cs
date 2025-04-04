using SearchRanking.Models;

namespace SearchRanking.ScoreService.Tests.TestData;

public class FractionScoreProviderTestData
{
    public static IEnumerable<object[]> Basic()
    {
        // single distinct letter
        yield return
        [
            CommonTestData.GeneralReview with { Sitter = "A" },
            1.0 / 26
        ];

        // multiple distinct letters
        yield return
        [
            CommonTestData.GeneralReview with { Sitter = "abcd" },
            4.0 / 26
        ];

        // duplicate letters
        yield return
        [
            CommonTestData.GeneralReview with { Sitter = "AaBbCc" },
            3.0 / 26
        ];
    }

    public static IEnumerable<object[]> Edge()
    {
        // empty string
        yield return
        [
            CommonTestData.GeneralReview with { Sitter = "" },
            0.0
        ];

        // null string
        yield return
        [
            CommonTestData.GeneralReview with { Sitter = null! },
            0.0
        ];

        // special characters and numbers
        yield return
        [
            CommonTestData.GeneralReview with { Sitter = "1234!@#$%^&*" },
            0.0
        ];

        // non-English alphabetic characters
        yield return
        [
            CommonTestData.GeneralReview with { Sitter = "äöüß" },
            4.0 / 26
        ];

        // string with spaces
        yield return
        [
            CommonTestData.GeneralReview with { Sitter = "abc def" },
            6.0 / 26
        ];
    }

    public static IEnumerable<object[]> MixedCases()
    {
        // string with letters, numbers, and special characters
        yield return
        [
            CommonTestData.GeneralReview with { Sitter = "a1b2c3d4!" },
            4.0 / 26
        ];

        // string with mixed upper and lowercase letters
        yield return
        [
            CommonTestData.GeneralReview with { Sitter = "AaBbCcDdEe" },
            5.0 / 26
        ];
    }

    public static IEnumerable<object[]> Performance()
    {
        // very long string
        yield return
        [
            CommonTestData.GeneralReview with { Sitter = new string('a', 1000) + new string('b', 1000) + "CDEF" },
            6.0 / 26
        ];
    }
}