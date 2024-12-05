using MongoDB.Bson;

namespace WebApplication1.Pages.Models
{
    // Ürün detaylarını temsil eden sınıf
    public class ProductDetails
    {
        public ObjectId id { get; set; } // MongoDB'deki benzersiz kimlik (ObjectId)
        public string title { get; set; } // Ürünün başlığı (ismi)
        public string paket_bilgisi { get; set; } // Ürünün başlığı (ismi)
        public string rastgele_veri { get; set; } // Ürünün başlığı (ismi)
    }
}
