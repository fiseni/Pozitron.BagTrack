namespace PozitronDev.SharedKernel.Data;

public class PaginationSettings
{
    public int DefaultPage { get; } = 1;
    public int DefaultPageSize { get; } = 20;
    public int DefaultPageSizeLimit { get; } = 50;

    public PaginationSettings()
    {
    }

    public PaginationSettings(int defaultPageSize, int defaultPageSizeLimit)
    {
        DefaultPageSize = defaultPageSize;
        DefaultPageSizeLimit = defaultPageSizeLimit;
    }
}
