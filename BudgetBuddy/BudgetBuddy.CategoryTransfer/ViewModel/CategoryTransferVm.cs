namespace CategoryTransfer.ViewModel;

public class CategoryTransferVm
{
    public int Id { get; set; }
    public Guid ClientId { get; set; }
    public DateOnly Date { get; set; }
    public decimal Amount { get; set; }
    public int? FromCategoryId { get; set; }
    public int ToCategoryId { get; set; }
    public bool FromAvailableToBudget { get; set; }
}