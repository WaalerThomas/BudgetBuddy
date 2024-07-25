using BudgetBuddyCore.Interfaces;
using BudgetBuddyCore.Models;
using Dapper;

namespace BudgetBuddySqlite;

public class CategoryRepository : ICategoryRepository
{
    private readonly DatabaseContext _context;

    public CategoryRepository(DatabaseContext context)
    {
        _context = context;
    }

    public bool Delete(int id)
    {
        int rowsDeleted = _context.GetConnection().Execute("DELETE FROM category WHERE id = @id", new { id });
        return rowsDeleted == 1;
    }

    public IEnumerable<Category> GetCategories()
    {
        return _context.GetConnection().Query<Category>("SELECT * FROM category");
    }

    public IEnumerable<Category> GetCategories(Group group)
    {
        return _context.GetConnection().Query<Category>("SELECT * FROM category WHERE group_id = @Id", group);
    }

    public Category? GetCategory(int id)
    {
        return _context.GetConnection().QuerySingleOrDefault<Category>("SELECT * FROM category WHERE id = @id", new { id });
    }

    public Category? GetCategoryByName(string name)
    {
        return _context.GetConnection().QuerySingleOrDefault<Category>("SELECT * FROM category WHERE name = @name", new { name });
    }

    public bool Insert(Category category)
    {
        int rowsInserted = _context.GetConnection().Execute(
            @"
                INSERT INTO category (name, monthly_amount, goal_amount, group_id)
                VALUES (@Name, @MonthlyAmount, @GoalAmount, @GroupId)
            ",
            new { category.Name, category.MonthlyAmount, category.GoalAmount, GroupId = category.Group.Id }
        );
        return rowsInserted == 1;
    }

    public bool Update(Category category)
    {
        int rowsUpdated = _context.GetConnection().Execute(
            @"
                UPDATE categories
                SET name = @Name, monthly_amount = @MonthlyAmount, goal_amount = @GoalAmount, group_id = @GroupId
                WHERE id = @Id
            ",
            new {
                category.Id,
                category.Name, category.MonthlyAmount, category.GoalAmount,
                GroupId = category.Group.Id
            }
        );
        return rowsUpdated == 1;
    }
}