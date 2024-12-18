const Web3 = require('web3');

// Web3 bağlantısı ve kontrat detayları
const web3 = new Web3('http://127.0.0.1:7545'); // Ganache bağlantısı
const contractABI = [
  {
    "inputs": [
      { "internalType": "uint256", "name": "id", "type": "uint256" },
      { "internalType": "string", "name": "data", "type": "string" }
    ],
    "name": "storeEncryptedData",
    "outputs": [],
    "stateMutability": "nonpayable",
    "type": "function"
  },
  {
    "inputs": [
      { "internalType": "uint256", "name": "id", "type": "uint256" }
    ],
    "name": "retrieveEncryptedData",
    "outputs": [
      { "internalType": "string", "name": "", "type": "string" }
    ],
    "stateMutability": "view",
    "type": "function"
  }
]; // Kontrat ABI
const contractAddress = '0x2D85e709511E300521788C7011Bc2e15A09CA36B'; // Kontrat adresi
const contract = new web3.eth.Contract(contractABI, contractAddress);

// Blockchain'e test verisi ekleme
async function addTestData() {
  try {
    const accounts = await web3.eth.getAccounts();
    await contract.methods.storeEncryptedData(
      1,
      JSON.stringify({
        slug: 'beauty',
        name: 'Beauty',
        url: 'https://dummyjson.com/products/category/beauty',
      })
    ).send({
      from: accounts[0],
      gas: 3000000,
    });
    console.log('Test verisi eklendi.');
  } catch (err) {
    console.error('Test verisi eklenirken hata:', err);
  }
}

// Blockchain'den veri çekme
async function getDataFromBlockchain(id) {
  try {
    const data = await contract.methods.retrieveEncryptedData(id).call();
    console.log(`Blockchain'den çekilen veri: ${data}`);
    return data;
  } catch (err) {
    console.error('Blockchain okuma hatası:', err);
    throw err;
  }
}

// Test fonksiyonu
async function testFunctions() {
  await addTestData(); // Test verisi ekle
  const data = await getDataFromBlockchain(1); // ID 1 için veri çek
  console.log('Çekilen veri:', data);
}

// Test işlemlerini çağır
testFunctions();

module.exports = { getDataFromBlockchain };
