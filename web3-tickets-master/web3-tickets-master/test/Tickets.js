const Tickets = artifacts.require("Tickets");

contract("Tickets", (accounts) => {
    it("Şifrelenmiş veri kaydedilmeli ve geri alınmalı", async () => {
        const ticketsInstance = await Tickets.deployed();

        // Veri kaydetme
        await ticketsInstance.storeEncryptedData(1, "encrypted-data", { from: accounts[0] });

        // Veriyi getirme
        const data = await ticketsInstance.retrieveEncryptedData(1, { from: accounts[0] });
        assert.equal(data, "encrypted-data", "Veri doğru kaydedildi ve alındı.");
    });
});

