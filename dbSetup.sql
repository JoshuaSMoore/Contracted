CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS companies(
   id int NOT NULL PRIMARY KEY AUTO_INCREMENT comment 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
   name varchar(255) NOT NULL COMMENT 'company Name',
   creatorId varchar(255) NOT NULL comment 'creatorId'
)default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS contractors(
   id int NOT NULL PRIMARY KEY AUTO_INCREMENT comment 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
   name varchar(255)  NOT NULL COMMENT  'contractor name',
   creatorId varchar(255) NOT NULL comment 'creatorId'
)default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS jobs(
   id int NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
   createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  contractorId int NOT NULL,
  companyId int NOT NULL,
  FOREIGN KEY(contractorId) REFERENCES contractors(id) ON DELETE CASCADE,
  FOREIGN KEY(companyId) REFERENCES companies(id) ON DELETE CASCADE
)default charset utf8 COMMENT '';

INSERT INTO jobs(contractorId, companyId, creatorId)
VALUES (1, 1, 3);