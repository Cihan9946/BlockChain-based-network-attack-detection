using MongoDB.Bson;

namespace WebApplication1.Pages.Models
{
    // MongoDB'deki kategori modelini temsil eden sınıf
    public class Category
    {
        public string mesaj { get; set; } // Kategoriye ait URL
        public string tarih { get; set; } // Kategoriye ait URL
        public object _id { get; set; } // MongoDB'deki _id alanı (veritabanındaki benzersiz kimlik)

        public string slug { get; set; } // Kategori için benzersiz kısa ad
        public string name { get; set; } // Kategori adı
        public string url { get; set; } // Kategoriye ait URL
        public List<ProductNew> products { get; set; } // Bu kategoriye ait ürünlerin listesi
    }

    // Ürünleri temsil eden sınıf
    public class ProductNew
    {
        public object id { get; set; } // Ürünün benzersiz kimliği
        public string title { get; set; } // Ürün başlığı
        public string description { get; set; } // Ürün açıklaması
        public string category { get; set; } // Ürünün ait olduğu kategori
        public double price { get; set; } // Ürün fiyatı
        public double discountPercentage { get; set; } // Ürüne uygulanan indirim yüzdesi
        public double rating { get; set; } // Ürünün kullanıcı değerlendirme puanı
        public int stock { get; set; } // Ürün stok adedi
        public List<string> tags { get; set; } // Ürünle ilgili etiketler
        public string brand { get; set; } // Ürünün markası
        public string sku { get; set; } // Ürünün stok tutma birimi (SKU)
        public double weight { get; set; } // Ürünün ağırlığı
        public Dimensions dimensions { get; set; } // Ürünün boyutları (genişlik, yükseklik, derinlik)
        public string warrantyInformation { get; set; } // Ürün garanti bilgisi
        public string shippingInformation { get; set; } // Ürün kargo bilgisi
        public string availabilityStatus { get; set; } // Ürünün stok durumu
        public List<Review> reviews { get; set; } // Ürüne ait kullanıcı yorumları
        public string returnPolicy { get; set; } // Ürünün iade politikası
        public int minimumOrderQuantity { get; set; } // Ürün için minimum sipariş miktarı
        public Meta meta { get; set; } // Ürüne ait meta veriler (oluşturulma, güncellenme tarihi vb.)
        public List<string> images { get; set; } // Ürüne ait resimlerin listesi
        public string thumbnail { get; set; } // Ürüne ait küçük resim (önizleme)
    }

    // Ürünün boyutlarını temsil eden sınıf
    public class Dimensions
    {
        public double width { get; set; } // Ürünün genişliği
        public double height { get; set; } // Ürünün yüksekliği
        public double depth { get; set; } // Ürünün derinliği
    }

    // Ürün değerlendirmelerini (yorumlarını) temsil eden sınıf
    public class Review
    {
        public int rating { get; set; } // Kullanıcı tarafından verilen puan
        public string comment { get; set; } // Kullanıcı yorumu
        public object date { get; set; } // Yorumun yapıldığı tarih
        public string reviewerName { get; set; } // Yorumu yapan kişinin adı
        public string reviewerEmail { get; set; } // Yorumu yapan kişinin e-posta adresi
    }

    // Ürüne ait meta bilgileri temsil eden sınıf
    public class Meta
    {
        public object createdAt { get; set; } // Ürünün oluşturulma tarihi
        public object updatedAt { get; set; } // Ürünün son güncellenme tarihi
        public string barcode { get; set; } // Ürünün barkod numarası
        public string qrCode { get; set; } // Ürünün QR kodu
    }
}
