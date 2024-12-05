using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Pages.Models; // Kategori modelini içeren klasörün referansý
using System.Threading.Tasks; // Asenkron iþlemler için gerekli kütüphane
using System.Collections.Generic; // List<> kullanýmý için gerekli kütüphane
using Microsoft.AspNetCore.Mvc; // Redirect iþlemleri için gerekli kütüphane

namespace WebApplication1.Pages
{
    public class CategoriesModel : PageModel
    {
        private readonly MongoDBService _mongoDBService; // MongoDB iþlemlerini yürütecek servis

        // MongoDBService'i dependency injection ile alýyoruz.
        public CategoriesModel(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService; // Dependency injection ile MongoDB servisini sýnýfa aktarýyoruz
        }

        public List<Category> veri { get; set; } // MongoDB'den alýnacak kategorileri tutacak liste

        // Sayfa yüklendiðinde MongoDB'den kategorileri çekmek için asenkron metod
        public async Task OnGetAsync()
        {
            veri = await _mongoDBService.GetCategoriesAsync(); // Kategorileri MongoDB servisinden alýyoruz
        }

        // Belirli bir slug deðerine göre ürün detay sayfasýna yönlendirme metodu
        public IActionResult OnGetRedirectToCollection(string slug)
        {
            // Kategorinin slug deðerine göre ilgili ürün detay sayfasýna yönlendiriyoruz
            return RedirectToPage("/ProductDetails", new { slug });
        }
    }
}
