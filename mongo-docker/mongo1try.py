import os
from pymongo import MongoClient
from datetime import datetime
import time
from scapy.all import sniff, IP, TCP, UDP, Ether
import pandas as pd
import random
from threading import Thread

# MongoDB bağlantısı
mongo_uri = os.getenv('MONGO_URI', 'mongodb://mongo:27017/')
print(mongo_uri)
client = MongoClient(mongo_uri)
db = client['test']
collection = db['veriler']

# Rastgele tehdit türleri listesi
saldiri_turleri = [
    "DDoS Saldırısı",
    "Malware Yazılım",
    "XSS Saldırısı",
    "Kimlik Avı (Phishing)",
    "SQL Injection",
    "Tehlike Yok",
    "Ransomware Saldırısı",
    "APT (Advanced Persistent Threat)",
    "Botnet Etkinliği"
]

# Rastgele veri oluşturma fonksiyonu
def rastgele_tehdit_olustur():
    rastgele_saldiri = random.choice(saldiri_turleri)
    mesaj = f"Bu paket tehlikeli: {rastgele_saldiri}" if rastgele_saldiri != "Tehlike Yok" else "Bu paket tehlikeli değil"
    return {
        "mesaj": mesaj,
        "tarih": datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    }

# Paket bilgilerini toplama ve MongoDB'ye ekleme
def paket_yakala(packet):
    # Paket bilgilerini toplama
    paket_bilgisi = {
        'timestamp': pd.Timestamp.now().isoformat(),
        'Kaynak MAC': packet[Ether].src if packet.haslayer(Ether) else 'Bilinmiyor',
        'Hedef MAC': packet[Ether].dst if packet.haslayer(Ether) else 'Bilinmiyor',
        'Kaynak IP': packet[IP].src if packet.haslayer(IP) else 'Bilinmiyor',
        'Hedef IP': packet[IP].dst if packet.haslayer(IP) else 'Bilinmiyor',
        'Protokol': packet.lastlayer().name,
        'Kaynak Port': packet.sport if packet.haslayer(TCP) or packet.haslayer(UDP) else 'Bilinmiyor',
        'Hedef Port': packet.dport if packet.haslayer(TCP) or packet.haslayer(UDP) else 'Bilinmiyor',
        'Tehdit Seviyesi': "Low"  # Statik değer, model entegrasyonu eklenebilir
    }

    # Rastgele tehdit bilgisi oluştur
    rastgele_veri = rastgele_tehdit_olustur()

    # MongoDB'ye eklenen veri
    veri = {
        "paket_bilgisi": paket_bilgisi,
        "rastgele_veri": rastgele_veri
    }

    # Veriyi MongoDB'ye ekle
    result = collection.insert_one(veri)
    veri["_id"] = str(result.inserted_id)

    # Konsola yazdır
    print(f"MongoDB'ye veri eklendi: {veri}")

# 10 saniyede bir paket yakalama
def paket_yakalama_surekli():
    while True:
        sniff(prn=paket_yakala, count=1)  # Her 10 saniyede bir 1 paket yakala
        time.sleep(10)

# İş parçacığında paket yakalama işlemini başlat
packet_thread = Thread(target=paket_yakalama_surekli, daemon=True)
packet_thread.start()

# Sonsuz döngü
while True:
    time.sleep(1)
