using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dt_team2.Pages;

public class SellAnItemModel : PageModel
{
    private readonly ILogger<SellAnItemModel> _logger;
 
    public SellAnItemModel(ILogger<SellAnItemModel> logger)
    {
        _logger = logger;
    }

    public string itemName{get; set;} = default!;
    public float price{get; set;} = default!;
    public DateTime date{get; set;} = default!;

    public ActionResult Item_DE(Transactions item){
        itemName = item.itemName;
        price = item.price;
        date = item.date;

        return null;
    }

    public void OnPost(Transactions item) {
        itemName = item.itemName;
        price = item.price;
        date = item.date;
    }
}
