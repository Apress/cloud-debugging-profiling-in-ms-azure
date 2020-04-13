declare @id int 
select @id = 1
while @id >= 1 and @id <= 5000
begin
    insert INTO dbo.CoffeeMakersStatus(CoffeeMakerId, Date, Status, DataFileName)  
    values(NEWID(), GETDATE(), 1, 'SampleData')
    select @id = @id + 1
end