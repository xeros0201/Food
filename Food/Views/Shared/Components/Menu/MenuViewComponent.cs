using Food.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class MenuViewComponent : ViewComponent
{
    private readonly ApplicationDbContext db;

    public MenuViewComponent(ApplicationDbContext thisDb)
    {
        db = thisDb;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        // Logic to retrieve data for the menu

        var model = await db.DanhMucChas.Include(dm => dm.DanhMucs).ToListAsync();

        return View(model);
    }
}
