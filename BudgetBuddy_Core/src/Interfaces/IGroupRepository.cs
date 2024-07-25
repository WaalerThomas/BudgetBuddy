using BudgetBuddyCore.Models;

namespace BudgetBuddyCore.Interfaces;

public interface IGroupRepository
{
    IEnumerable<Group> GetGroups();
    IEnumerable<Group> GetGroupsWithCategories();
    Group? GetGroup(int id);
    Group? GetGroupWithCategories(int id);
    bool Insert(Group group);
    bool Delete(int id);
    bool Update(Group group);
}