# Search Ranking

Search Ranking is a simple command-line program that reads the Google index for all of the reviews customers have left for their stays with sitters and writes out CSV data. The output is sorted by Search Score in descending order, with sitter names sorted alphabetically as a tie-breaker.

The program imports the data into memory and uses it to recreate the search algorithm. The source code includes tests that demonstrate the code is working as expected.

## Requirements

To run this project, you need to have the following installed:

- [.NET Runtime 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

Ensure that the `dotnet` command is available in your terminal.

## Build

To build the project, follow these steps:

1. Download the ZIP file.
2. Extract the contents into a temporary folder.

   ```sh
   unzip Rover-Takehome-Web.zip
   ```

3. Navigate to the extracted folder.

   ```sh
   cd Rover-Takehome-Web
   ```

4. Run the following command:

   ```sh
   dotnet build
   ```

## Run

To run the project, use the following command:

```sh
dotnet run \
  --project src/SearchRanking.CommandLine \
  --reviews="../../data/reviews.csv" \
  --scores="../../data/scores.csv"
```

### Parameters:

- `--reviews` (required): The location of the source CSV file containing Google Reviews.
- `--scores` (required): The location where the generated reports will be saved.

### Important Notes:

- The program will fail if the reviews file location is incorrect or access is denied.
- If the scores file already exists, the program will not overwrite it.
- Ensure that the program has the necessary write permissions for the scores file location.

## Output Copy

A copy of the output scores CSV is also generated at `../../data/sitters.csv`.

## Discussion Question

### How would you adjust the calculation and storage of search scores in a production application?

In a production application, the calculation of search scores should be structured to minimize redundant processing and ensure scalability. Instead of computing scores directly from raw review data on demand, the system should preprocess and cache these values in a fast-access store such as Redis or a dedicated database table. The `Compute` method should be modified to support incremental updates, where only new or modified reviews trigger a recalculation rather than recomputing scores for all sitters. Additionally, the profile and ratings scores should be stored separately and combined during retrieval to allow for efficient updates without recalculating all components.

For storage optimization, the dictionary-based in-memory approach currently used in `SearchScoreProvider` should be replaced with a persistent store to prevent recalculations on every service restart. The `Compute` method should fetch precomputed ratings scores from storage rather than computing them each time. Instead of aggregating all reviews in memory using `GroupBy`, the system should query only the necessary data using indexed searches, reducing memory overhead and improving response time. The profile score computation, which currently relies on `First()`, should ensure that the review selection is deterministic, such as always picking the most recent review, to prevent inconsistencies when new data arrives.