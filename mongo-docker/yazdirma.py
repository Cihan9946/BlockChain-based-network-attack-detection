import os
from pymongo import MongoClient
from datetime import datetime
import json
import time
from scapy.all import sniff, IP, TCP, UDP, Ether, ARP, ICMP
import pandas as pd
import random
from threading import Thread

# MongoDB bağlantısı
mongo_uri = os.getenv('MONGO_URI', 'mongodb://mongo:27017/')  # Docker konteyneri için
print(mongo_uri)
client = MongoClient(mongo_uri)
db = client['test']
collection = db['b']

# MongoDB'ye ping atarak bağlantı kontrolü
def check_mongo_connection():
    try:
        client.admin.command('ping')
        print("MongoDB'ye bağlantı başarılı.")
        return True
    except Exception as e:
        print(f"MongoDB bağlantı hatası: {e}")
        return False

# Tehdit seviyesini belirleme fonksiyonu
def tehdit_seviyesi_belirle(packet):
    if packet.haslayer(UDP) and '_microsoft' in str(packet):
        return 'High'
    elif packet.haslayer(TCP) and (packet[TCP].flags == 'S'):
        return 'Medium'
    else:
        return 'Low'

# Paket yakalama ve MongoDB'ye kaydetme fonksiyonu
def paket_yakala(packet):
    if check_mongo_connection():
        # Paket bilgilerini toplama
        paket_bilgileri = {
            'timestamp': pd.Timestamp.now().isoformat(),
            'Kaynak MAC': packet[Ether].src if packet.haslayer(Ether) else 'Bilinmiyor',
            'Hedef MAC': packet[Ether].dst if packet.haslayer(Ether) else 'Bilinmiyor',
            'Kaynak IP': packet[IP].src if packet.haslayer(IP) else 'Bilinmiyor',
            'Hedef IP': packet[IP].dst if packet.haslayer(IP) else 'Bilinmiyor',
            'Protokol': packet.lastlayer().name,
            'Kaynak Port': packet.sport if packet.haslayer(TCP) or packet.haslayer(UDP) else 'Bilinmiyor',
            'Hedef Port': packet.dport if packet.haslayer(TCP) or packet.haslayer(UDP) else 'Bilinmiyor',
            'Tehdit Seviyesi': tehdit_seviyesi_belirle(packet)
        }

        # MongoDB'ye ekle
        result = collection.insert_one(paket_bilgileri)
        paket_bilgileri["_id"] = str(result.inserted_id)
        print(f"MongoDB'ye veri eklendi: {paket_bilgileri}")

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

# Rastgele veri ekleme fonksiyonu
def rastgele_veri_ekle():
    while True:
        # Rastgele bir tehdit türü seç
        rastgele_saldiri = random.choice(saldiri_turleri)
        tehlike_durumu = "Bu paket tehlikeli: " + rastgele_saldiri if rastgele_saldiri != "Tehlike Yok" else "Bu paket tehlikeli değil"

        # Tarih bilgisi ekle
        veri = {
            "mesaj": tehlike_durumu,
            "tarih": datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        }

        # MongoDB'deki 'test' veritabanında 'yeni' koleksiyonuna ekle
        yeni_collection = db['yeni']
        result = yeni_collection.insert_one(veri)

        print(f"Yeni koleksiyona veri eklendi: {veri}")

        # 10 saniye bekle
        time.sleep(10)

# Rastgele veri ekleme işlemini ayrı bir iş parçacığı olarak başlat
rastgele_veri_thread = Thread(target=rastgele_veri_ekle, daemon=True)
rastgele_veri_thread.start()

# Ağ paketlerini yakalama işlemi başlatma
print("Ağ paketleri yakalanıyor...")
sniff(prn=paket_yakala, store=0)  # Sonsuza kadar paketleri yakala
