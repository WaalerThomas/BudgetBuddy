SELECT
    Id,
    EntryDate,
    Amount,
    (SELECT Name FROM Categories AS fromCat WHERE fromCat.Id = c.FromCategoryId) AS `From Category`,
    (SELECt Name FROM Categories AS toCat WHERE toCat.Id = c.ToCategoryId) `To Category`,
    (SELECT Name FROM CategoryTransferTypes AS t WHERE c.TransferTypeId = t.Id) AS `Transfer Type`
FROM CategoryTransfers AS c