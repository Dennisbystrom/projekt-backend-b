using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using Services.Interfaces;
using Models.Interfaces;
using Models.DTO;
using Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

public class ViewFriendModel : PageModel
{
    private readonly IFriendsService _friendsService;

    public ViewFriendModel(IFriendsService friendsService)
    {
        _friendsService = friendsService;
    }

    public IFriend Friend { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        var f = (await _friendsService.ReadFriendAsync(id, flat: false)).Item;
        Friend = f;
        return Page();
    }
}