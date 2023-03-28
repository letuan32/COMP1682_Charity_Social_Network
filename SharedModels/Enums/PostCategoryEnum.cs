using System.ComponentModel;

namespace SharedModels.Enums;

public enum PostCategoryEnum
{
    [Description("Request financial assistance")]
    FinancialRequest = 1,
    
    [Description("Sharing")]
    Sharing = 2,
    
    [Description("Fundraising")]
    Fundraising = 3
}