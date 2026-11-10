using Dima.Core.Enums;

namespace Dima.Core.Requests.Transactions;

public class UpdateTransactionRequest : Request
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public ETransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public long CategoryId { get; set; }
    public DateTime PaidOrReceivedAt { get; set; } 
}