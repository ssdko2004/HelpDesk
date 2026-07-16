
using System.ComponentModel.DataAnnotations;
using HelpDesk.Models;
namespace HelpDesk.ViewModels
{
    public class MyTicketsViewModel
    {
        public List<Ticket> Tickets { get; set; } = [];

        public string SortOrder { get; set; } = MyTicketSortOrders.DateDesc;  
      
    }
}