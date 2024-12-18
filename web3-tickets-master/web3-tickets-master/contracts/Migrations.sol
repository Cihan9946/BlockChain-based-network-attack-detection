// SPDX-License-Identifier: MIT
pragma solidity >=0.4.22 <0.9.0;

contract Migrations {
  address public owner;
  constructor() {
    owner = msg.sender; // Kontratı dağıtan adres, sahibi olur.
}
  uint public last_completed_migration;

  modifier restricted() {
  require(msg.sender == owner, "Yetkiniz yok!");

    _;
  }

  function setCompleted(uint completed) public restricted {
    last_completed_migration = completed;
  }
}
