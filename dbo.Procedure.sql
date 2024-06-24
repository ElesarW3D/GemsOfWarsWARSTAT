CREATE PROCEDURE [dbo].[CalcWR]
    @myTableParam WarDays READONLY
AS
BEGIN
    
    SELECT *
    FROM @myTableParam;
    -- Делайте что-то другое с переданными данными
END
