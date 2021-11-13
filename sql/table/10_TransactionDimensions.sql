USE Prime;

create table dw.customer (
    Id int identity(1,1),
    Name varchar(60)
)
create table dw.customerGroup (
                             Id int identity(1,1),
                             Name varchar(60)
)


create table dw.customerCustomerGroups
(
    Id               int identity (1,1),
    customerid       int,
    customergroupdid int,
    pctOfSales       decimal(5, 2)
)
MERGE  prime.dw.customer AS TARGET
    USING
(
    
SELECT distinct saajamaksaja
    from prime.dbo.transactionstmp
) AS SOURCE
    ON source.saajamaksaja = target.Name
 WHEN NOT MATCHED 
    THEN insert (Name) VALUES (
        source.saajamaksaja
    );


insert into dw.customerGroup (Name) VALUES 
('Ruokakauppa')
,('Pizza')
,('Urheilu')
,('Asumiskulut')
,('Lounasravintolat')
,(N'Ryyppäys')
,('Terveydenhoito')
,('Viihde')
,('Vaatekaupat')
,('Kulkeminen')

-- SELECT *
-- from dw.customerGroup
