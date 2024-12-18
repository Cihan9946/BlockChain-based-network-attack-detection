// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

contract MyContract {
    string public data;

    // Veriyi güncelleyen bir fonksiyon
    function setData(string memory _data) public {
        data = _data;
    }

    // Veriyi getiren bir fonksiyon (otomatik olarak public ile gelir)
    function getData() public view returns (string memory) {
        return data;
    }
}
