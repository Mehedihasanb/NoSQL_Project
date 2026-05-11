using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

public class TestController : Controller
{
    private readonly IMongoDatabase _db;
    public TestController(IMongoDatabase db) => _db = db;

    public IActionResult Index()
    {
        var names = _db.ListCollectionNames().ToList();
        return Content($"Mongo OK. DB={_db.DatabaseNamespace.DatabaseName}. Collections: {string.Join(", ", names)}");
    }
}
