SELECT
    Id,
    EntryDate,
    (SELECT `Name` FROM TransactionTypes WHERE Id == t.TransactionTypeId) AS `Type`,
    Amount,
    (SELECT `Name` FROM Accounts WHERE Id == t.AccountId) AS `Account`,
    (SELECT `Name` FROM TransactionStatuses WHERE Id == t.TransactionStatusId) AS `Status`
FROM Transactions AS t