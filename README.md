# BlockChain-based-network-attack-detection

# Ağ Trafiği İzleme, Veri Analizi ve Web Görselleştirme Projesi

Bu proje, ağ trafiğini izleme, makine öğrenmesi modeli kullanarak analiz etme, MongoDB ile veri tabanı yönetimi, Docker container kullanımı ve .net framework'ü ile web tabanlı görselleştirme işlemlerini içermektedir.

---

## Proje Akışı

### 1. Veri Kaynakları
- **Kullanılan veri kaynakları:**
  - [Kaggle](https://www.kaggle.com/) veri setleri.
  - [Canadian Institute for Cybersecurity](https://www.unb.ca/cic/) tarafından sağlanan güvenlik verileri.


### 2. Verilerin Elde Edilmesi ve Makine Öğrenimi Modeli
- **Veri İşleme ve Eğitim:**
  - Python kullanılarak veriler üzerinde ön işleme yapılmıştır.
  - TensorFlow/Keras ile makine öğrenimi modeli eğitilmiştir.
  - Eğitim sonucunda `model.h5` dosyası oluşturulmuştur.
- **Model Kullanımı:**
  - Eğitimli model, gelen veriler üzerinde test edilerek çıktı üretmiştir.
  - Test sonuçları JSON formatında kaydedilmiştir.


### 3. Ağ Trafiğinin İzlenmesi ve Kaydedilmesi
- [Scapy](https://scapy.net/) kütüphanesi ile gerçek zamanlı ağ trafiği izlenmiştir.
- Trafik verileri her 60 saniyede bir kaydedilmiş ve JSON formatında düzenlenmiştir.


### 4. Veritabanı Oluşturma ve Docker Bağlantısı
- **MongoDB:** Docker üzerinde bir container olarak çalıştırılmıştır.
- **Veri Aktarımı:** JSON formatındaki veriler MongoDB'ye aktarılmış ve gerekli tablo yapıları oluşturulmuştur.
- **Docker Kullanımı:**
  - MongoDB'nin taşınabilir ve izole bir ortamda çalıştırılması sağlanmıştır.


### 5. Model Testi ve Verilerin BlockChain'e Gönderilmesi
- Model, yeni gelen veriler üzerinde test edilmiştir.
- Test sonuçları Python kullanılarak MongoDB'ye aktarılmıştır.
- BlockChain entegrasyonu başlatılmış .
  

### 6. Yapının Docker Container'ına Eklenmesi
- Model ve kod yapısı Docker container içine alınarak çalıştırılmıştır.
- Docker container'ı oluşturmak için aşağıdaki komutlar kullanılmıştır:

      docker build -t model-container .
      docker run -d -p 5000:5000 model-container 


### 7. Elde edilen test sonuçlarının .net web app üzerinde görselleştirilmesi
- Gerekli Front kodları oluşturulup , tasarım oluşturulmuştur.
- Bacend tarafında mongoDB bağlantısı yapılıp kodlar tamamlanmış web çıktısı oluşturulmuştur.

## Kullanılan Teknolojiler

- **Programlama Dili:** Python
- **Makine Öğrenimi:** TensorFlow/Keras
- **Ağ İzleme:** Scapy
- **Veritabanı:** MongoDB
- **Konteynerizasyon:** Docker
- **Veri Formatı:** JSON

---
