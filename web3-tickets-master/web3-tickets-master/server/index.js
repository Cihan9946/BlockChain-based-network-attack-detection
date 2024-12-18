const Web3 = require("web3");
const MyContract = require("./build/contracts/MyContract.json");

// Ganache RPC endpoint (örnek: http://127.0.0.1:7545)
const web3 = new Web3("http://127.0.0.1:7545");

// Ganache'deki bir hesap
const account = "0x5E4e79558C3b69a0d8Ce45307eA1061dC5FBd4Ad"; // Dağıtımda kullanılan hesap
const privateKey = "0x03f2aea448662d7093c6844d654e83baa5d419c5198349af319ccdb922a23d0c"; // Ganache'den alınabilir

(async () => {
    const networkId = await web3.eth.net.getId();
    const deployedNetwork = MyContract.networks[networkId];
    const contract = new web3.eth.Contract(
        MyContract.abi,
        "0x34454E620Cb23101F895865EE3612989DaB922d7" // MyContract adresi
    );

    // Veriyi güncelle
    const tx = contract.methods.setData("abc");
    const gas = await tx.estimateGas({ from: account });
    const gasPrice = await web3.eth.getGasPrice();
    const data = tx.encodeABI();

    const signedTx = await web3.eth.accounts.signTransaction(
        {
            to: "0x34454E620Cb23101F895865EE3612989DaB922d7", // MyContract adresi
            data,
            gas,
            gasPrice,
        },
        privateKey
    );

    const receipt = await web3.eth.sendSignedTransaction(
        signedTx.rawTransaction
    );
    console.log("Transaction Receipt: ", receipt);

    // Veriyi oku
    const storedData = await contract.methods.getData().call();
    console.log("Stored Data: ", storedData);
})();
