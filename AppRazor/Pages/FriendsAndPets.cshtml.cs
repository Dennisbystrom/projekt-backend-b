using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using Services.Interfaces;
using Models.Interfaces;

public class FriendsAndPetsModel : PageModel
{

    private readonly IAddressesService _addressesService;

    public FriendsAndPetsModel(IAddressesService addressesService)
    {
        _addressesService = addressesService;
    }

    public List<OverviewCountry> Overview { get; set; }

    public async Task<IActionResult> OnGet()
    {
        var response = await _addressesService.ReadAddressesAsync(true, false, string.Empty, 0, 100);
        Overview = response.PageItems
            .GroupBy(a => a.Country)
            .Select(countryGroup => new OverviewCountry
            {
                Country = countryGroup.Key,
                Cities = countryGroup
                    .GroupBy(a => a.City)
                    .Select(cityGroup => new OverviewCity
                    {
                        City = cityGroup.Key,
                        FriendsCount = cityGroup
                            .SelectMany(a => a.Friends)
                            .Count(),

                        PetsCount = cityGroup
                            .SelectMany(a => a.Friends)
                            .SelectMany(f => f.Pets)
                            .Count()
                    })
                    .OrderBy(c => c.City)
                    .ToList()
            })
            .OrderBy(c => c.Country)
            .ToList();
        return Page();
    }
}

public class OverviewCity
{
    public string City { get; set; }
    public int FriendsCount { get; set; }
    public int PetsCount { get; set; }
}

public class OverviewCountry
{
    public string Country { get; set; }
    public List<OverviewCity> Cities { get; set; }
}