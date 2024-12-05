# Python 3.9 imajını kullan
FROM python:3.9

# Çalışma dizinini ayarla
WORKDIR /app

# Gerekli dosyaları kopyala
COPY yazdirma.py .
COPY veriler.json .


# Gerekli Python kütüphanelerini yükle
RUN pip install pymongo
RUN pip install pandas
RUN pip install scapy
RUN pip install geoip2
