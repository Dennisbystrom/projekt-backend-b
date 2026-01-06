using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using Services.Interfaces;
using Models.Interfaces;

public class FriendsByCountryModel : PageModel
{
    private readonly IFriendsService _friendsService;

    public FriendsByCountryModel(IFriendsService friendsService)
    {
        _friendsService = friendsService;
    }

    public List<CountryFriendCount> Friends { get; set; }

    public async Task<IActionResult> OnGet()
    {
        var response = await _friendsService.ReadFriendsAsync(true, false, string.Empty, 0, 100);

        Friends = response.PageItems
        .Where(f => f.Address != null)
        .GroupBy(f => f.Address.Country)
            .Select(g => new CountryFriendCount
            {
                Country = g.Key,
                Count = g.Count()
            })
            .ToList();
        return Page();
    }
}

public class CountryFriendCount
{
    public string Country { get; set; }
    public int Count { get; set; }
}