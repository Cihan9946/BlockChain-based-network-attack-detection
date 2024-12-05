using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using WebApplication1.Pages.Models;

public class MongoDBService
{
    // MongoDB'deki "Category" koleksiyonunu temsil eden alan
    private readonly IMongoCollection<Category> _categoriesCollection;

    // MongoDB'deki "ProductDetails" koleksiyonunu temsil eden alan
    private readonly IMongoCollection<ProductDetails> _productsCollection;

    // Kategorilerin tutulduğu bir liste
    public readonly List<Category> categories;

    // MongoDB servisini yapılandırmak için kullanılan kurucu metot
    public MongoDBService(IConfiguration configuration)
    {
        // MongoDB istemcisini bağlantı dizesi kullanarak başlatır
        var client = new MongoClient(configuration["MongoDBSettings:ConnectionString"]);

        // Belirtilen veritabanını alır
        var database = client.GetDatabase(configuration["MongoDBSettings:DatabaseName"]);

        // "latesdata" koleksiyonundan verileri almak için bir test koleksiyonu oluşturur
        var test = database.GetCollection<object>("yeni");

        // "latesdata" koleksiyonundaki tüm verileri liste olarak getirir
        var categoryList = test.Find(_ => true).ToList();

        // Verileri JSON formatına dönüştürür
        string json = JsonConvert.SerializeObject(categoryList, Formatting.Indented);

        // JSON verilerini "Category" türündeki bir listeye dönüştürür
        categories = JsonConvert.DeserializeObject<List<Category>>(json);
        
        // "products" adlı koleksiyonu alır (Bu koleksiyonun veritabanında mevcut olduğundan emin olun)
        _productsCollection = database.GetCollection<ProductDetails>("b");
    }

    // Kategorileri asenkron olarak almak için kullanılan metot
    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await Task.FromResult(categories); // Kategori listesini döndürür
    }
}
