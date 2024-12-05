using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks; // Asenkron iþlemler için gerekli kütüphane
using System.Collections.Generic; // List<> kullanýmý için gerekli kütüphane
using WebApplication1.Pages.Models; // Modelleri içeren klasör
using Microsoft.AspNetCore.Mvc; // Redirect iþlemleri için gerekli kütüphane

namespace WebApplication1.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly MongoDBService _mongoDBService; // MongoDB iþlemleri için servis

        // MongoDBService'i dependency injection ile alýyoruz.
        public ProductDetailsModel(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService; // Dependency injection ile MongoDB servisini sýnýfa aktarýyoruz
        }

        public Category SelectedCategory { get; set; } // Seçilen kategori bilgisi

        public List<Category> veri { get; set; } // Kategorileri tutan liste

        // Belirli bir slug'a göre MongoDB'den kategori verisini çekmek için asenkron metot
        public async Task OnGetAsync(string slug)
        {
            var allCategories = await _mongoDBService.GetCategoriesAsync(); // Tüm kategorileri alýyoruz
            SelectedCategory = allCategories.FirstOrDefault(c => c.slug == slug); // Belirli bir slug'a göre seçilen kategoriyi buluyoruz

            // Seçilen kategori bilgilerini konsola yazdýrýyoruz
            if (SelectedCategory != null)
            {
                Console.WriteLine($"Seçilen Kategori - Slug: {SelectedCategory.slug}, URL: {SelectedCategory.url}");
            }
        }

        // Belirli bir slug deðerine göre yönlendirme metodu
        public IActionResult OnGetRedirectToCollection(string slug)
        {
            // Kategorinin slug deðerine göre ilgili ürün detay sayfasýna yönlendiriyoruz
            return RedirectToPage("/ProductDetails", new { slug });
        }
    }
}

// Ürün detaylarý için model sýnýfý
public class Products
{
    public string slug { get; set; } // Ürün slug'ý
    public string url { get; set; } // Ürün URL'si
    public string category { get; set; } // Ürün kategorisi

    public List<ProductDetails> products { get; set; } // Ürün detaylarýný tutan liste
    // Diðer alanlarý buraya ekleyin
}
