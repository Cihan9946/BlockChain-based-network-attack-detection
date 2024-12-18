// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

contract Tickets {
    // Mapping ile veri saklama
    mapping(uint256 => string) public encryptedRecords;

    // Veriyi saklama fonksiyonu
    function storeEncryptedData(uint256 id, string memory data) public {
        encryptedRecords[id] = data;
    }

    // Saklanan veriyi okuma fonksiyonu
    function retrieveEncryptedData(uint256 id) public view returns (string memory) {
        return encryptedRecords[id];
    }
}
