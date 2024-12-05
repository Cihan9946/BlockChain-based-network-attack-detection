import os
from pymongo import MongoClient
from datetime import datetime
import json
import time
from scapy.all import sniff, IP, TCP, UDP, Ether, ARP, ICMP
import pandas as pd

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

# Ağ paketlerini yakalama işlemi başlatma
print("Ağ paketleri yakalanıyor...")
sniff(prn=paket_yakala, count=10000)  # 1000 adet paket yakala
