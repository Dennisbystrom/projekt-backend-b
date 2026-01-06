using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using Services.Interfaces;
using Models.Interfaces;

public class ListOfFriendsModel : PageModel
{
    private readonly IFriendsService _friendsService;

    public ListOfFriendsModel(IFriendsService friendsService)
    {
        _friendsService = friendsService;
    }

    public List<IFriend> Friends { get; set; }

    public async Task<IActionResult> OnGet()
    {
        var response = await _friendsService.ReadFriendsAsync(true, false, "", 0, 1000);
        Friends = response.PageItems;
        return Page();
    }
}