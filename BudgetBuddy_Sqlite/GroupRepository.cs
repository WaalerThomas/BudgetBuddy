using Dapper;
using BudgetBuddyCore.Interfaces;
using BudgetBuddyCore.Models;

namespace BudgetBuddySqlite;

public class GroupRepository : IGroupRepository
{
    private readonly DatabaseContext _context;

    public GroupRepository(DatabaseContext context)
    {
        _context = context;
    }

    public bool Delete(int id)
    {
        int rowsDeleted = _context.GetConnection().Execute("DELETE FROM budget_group WHERE id = @id", new { id });
        return rowsDeleted == 1;
    }

    public Group? GetGroup(int id)
    {
        return _context.GetConnection().QuerySingleOrDefault<Group>("SELECT * FROM budget_group WHERE id = @id", new { id });
    }

    public Group? GetGroupWithCategories(int id)
    {
        var group = _context.GetConnection().QuerySingleOrDefault<Group>("SELECT * FROM budget_group WHERE id = @id", new { id });
        if (group != null)
        {
            var categories = _context.GetConnection().Query<Category>("SELECT * FROM category WHERE group_id = @Id", new { group.Id });
            group.Categories = (ICollection<Category>?)categories;
        }
        return group;
    }

    public IEnumerable<Group> GetGroups()
    {
        return _context.GetConnection().Query<Group>(
            @"SELECT * FROM budget_group"
        );
    }

    public IEnumerable<Group> GetGroupsWithCategories()
    {
        var groups = _context.GetConnection().Query<Group>("SELECT * FROM budget_group");
        foreach (var group in groups)
        {
            var categories = _context.GetConnection().Query<Category>("SELECT * FROM category WHERE group_id = @Id", new { group.Id });
            group.Categories = (ICollection<Category>?)categories;
        }
        return groups;
    }

    public bool Insert(Group group)
    {
        int rowsInserted = _context.GetConnection().Execute(
            @"
                INSERT INTO budget_group (name)
                VALUES (@Name)
            ",
            group
        );
        return rowsInserted == 1;
    }

    public bool Update(Group group)
    {
        int rowsUpdated = _context.GetConnection().Execute(
            @"
                UPDATE budget_group
                SET name = @Name
                WHERE id = @id
            ",
            group
        );
        return rowsUpdated == 1;
    }
}