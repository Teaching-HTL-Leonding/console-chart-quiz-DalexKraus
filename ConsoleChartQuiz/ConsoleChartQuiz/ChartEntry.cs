namespace ConsoleChartQuiz
{
    public class ChartEntry
    {
        public ChartEntry(string entryTitle, int entryCount)
        {
            EntryTitle = entryTitle;
            EntryCount = entryCount;
        }

        public string EntryTitle { get; }
        public int EntryCount { get; }
    }
}