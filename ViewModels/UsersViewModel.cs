using HelpDesk.Models;
using HelpDesk.Enums;
namespace HelpDesk.ViewModels
{
    public class UsersViewModel
    {
        public List<ApplicationUser> Users { get; set; } = [];

        public string SortOrder { get; set; } = UserSortOrders.DateCreatedDesc;  
      
    }
}