using BudgetBuddy.Models;

namespace BudgetBuddy.Controllers;

public class CategoryTransferController
{
    public static CategoryTransferType TypeFromEnum(CategoryTransferTypeEnum type)
    {
        return new CategoryTransferType()
        {
            Id = type,
            Name = type.ToString()
        };
    }
}