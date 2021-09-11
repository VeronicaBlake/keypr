CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaults(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId VARCHAR(255) NOT NULL COMMENT 'FK: user account',
  name VARCHAR(255) NOT NULL COMMENT 'keep name',
  img VARCHAR(255) COMMENT 'image URL',
  description VARCHAR(255) COMMENT 'keep description',
  isPrivate TINYINT COMMENT 'is the vault private?',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE 
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS keeps(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId VARCHAR(255) NOT NULL COMMENT 'FK: user account',
  name VARCHAR(255) NOT NULL COMMENT 'keep name',
  description VARCHAR(255) COMMENT 'keep description',
  img VARCHAR(255) COMMENT 'image URL',
  views INT  NOT NULL COMMENT 'number of views',
  shares INT NOT NULL COMMENT 'number of shares',
  keeps INT NOT NULL COMMENT 'how many times the keep has been kept in a vault',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE 
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaultKeeps(
  id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  vaultId INT NOT NULL COMMENT 'FK: vault id',
  keepId INT NOT NULL COMMENT 'FK: keep id',
  FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';